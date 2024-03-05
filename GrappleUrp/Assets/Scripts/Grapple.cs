using UnityEngine;
using UnityEngine.InputSystem;
[RequireComponent(typeof(LineRenderer))]
public class Grapple : MonoBehaviour
{
   
    private LineRenderer _lineRenderer;
    private Vector3 _grapplePoint;
    public LayerMask WhatIsGrappleable;
    public Transform Camera , Gunpoint;
    private SpringJoint Joint;
    static public bool isGrapple;
    [Header("Grapple Settings")]
    [Space(10)]
    [Header("Range of Gun")]
    [SerializeField]
    private float RangeOfGun;
    [Header("Spring Force")]
    [SerializeField]
    private float SpringForce;
    [Header("Damper")]
    [SerializeField]
    private float Damper;
    [Header("Mass Scale")]
    [SerializeField]
    private float MassScale;
    [Header("Max and Min spring Length Between Player and Grapple Point")]
    [Space(10)]
    [SerializeField]
    private float MaxDistance;
    [SerializeField]
    private float MinDistance;
    
    // Start is called before the first frame update
    void Start()
    {
        _lineRenderer = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
  

    private void LateUpdate()
    {
        drawLine();
    }

    void StartGrapple()
    {
        RaycastHit hit;
        if (Physics.Raycast(Gunpoint.position, Gunpoint.forward, out hit, RangeOfGun, WhatIsGrappleable))
        {
            _grapplePoint = hit.point;
            if (Joint == null)
            {
                Joint = gameObject.AddComponent<SpringJoint>();
            }
            Joint.autoConfigureConnectedAnchor = false;
            Joint.connectedAnchor = _grapplePoint;
            
            float distanceFromPoint = Vector3.Distance(transform.position, _grapplePoint);
            
            // The distance grapple will try to keep from grapple point
            Joint.maxDistance = distanceFromPoint * MaxDistance;
            Joint.minDistance = distanceFromPoint * MinDistance;
            
            // Adjust these values to fit your game.
            Joint.spring = SpringForce;
            Joint.damper = Damper;
            Joint.massScale = MassScale;
            _lineRenderer.positionCount = 2;
            //used in GunRotate script
            isGrapple = true;
        }
    }

    void StopGrapple()
    {
        _lineRenderer.positionCount = 0;
        Destroy(Joint);
        isGrapple = false;
    }

    void drawLine()
    {
        if(!Joint) return;
        _lineRenderer.SetPosition(0, Gunpoint.position);
        _lineRenderer.SetPosition(1, _grapplePoint);
    }
    public void OnGrapple(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            StartGrapple();
        }
        else if (context.canceled)
        {
            StopGrapple();
        }
    }
    
    public Vector3 GetGrapplePoint()
    {
        return _grapplePoint;
    }
}


using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
[RequireComponent(typeof(LineRenderer))]
public class Weapon : MonoBehaviour
{
    public Transform firePoint;
    public LayerMask TargetLaymask;
    private LineRenderer _lineRenderer;
    private RaycastHit _hit;
    private int _killCount;
    private Text _killCountText;
    private Vector3 _oldTargetLocation;
    void Start()
    {
        
        _lineRenderer = GetComponent<LineRenderer>();
        _killCountText = GameObject.Find("KillCountText").GetComponent<Text>();
    }

    /*private void Update()
    {
       
    }*/

    private void LateUpdate()
    {
        _killCountText.text = "Points: " + _killCount;
    }


    public void OnShoot(InputAction.CallbackContext context)
    {
       
        if (context.performed)
        {
            if (Physics.Raycast(firePoint.position, firePoint.forward, out _hit, 100, TargetLaymask))
            {
                _oldTargetLocation = _hit.collider.transform.position;
                StartCoroutine(LineDelay(0.1f));
                _killCount++;
                Destroy(_hit.collider.gameObject);
            }
        }
    }

    private IEnumerator LineDelay(float Timer)
    {
        ShowKillLine();
        yield return new WaitForSeconds(Timer);
        _lineRenderer.positionCount = 0;
    }
    void ShowKillLine()
    {
        _lineRenderer.positionCount = 2; 
        _lineRenderer.SetPosition(0, firePoint.position);
        _lineRenderer.SetPosition(1, _oldTargetLocation);
    }
}

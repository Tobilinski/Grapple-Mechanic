using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class Weapon : MonoBehaviour
{
    public Transform firePoint;
    public GameObject bulletPrefab;
    public LayerMask TargetLaymask;
    private LineRenderer _lineRenderer;
    private Grapple _grapple;
    
    void Start()
    {
        _grapple = GetComponent<Grapple>();
        _lineRenderer = GetComponent<LineRenderer>();
    }

    
   

    public void OnShoot(InputAction.CallbackContext context)
    {
       
        if (context.performed)
        {
            RaycastHit hit;
            if (Physics.Raycast(firePoint.position, firePoint.forward, out hit, 100, TargetLaymask))
            {
                //Destroy(hit.collider.gameObject);
                print("Hit");
                _lineRenderer.positionCount = 2;
                
                _lineRenderer.SetPosition(0, firePoint.position);
                _lineRenderer.SetPosition(1, hit.collider.transform.position);
            }
        }
        else if(context.canceled)
        {
            _lineRenderer.positionCount = 0;
        }
    }
}

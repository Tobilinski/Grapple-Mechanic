using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class Weapon : MonoBehaviour
{
    public Transform firePoint;
    public LayerMask TargetLaymask;
    private LineRenderer _lineRenderer;
    private RaycastHit _hit;
    
    void Start()
    {
        _lineRenderer = GetComponent<LineRenderer>();
    }

    private void LateUpdate()
    {
        if(_hit.collider != null)
        {
            _lineRenderer.SetPosition(0, firePoint.position);
            _lineRenderer.SetPosition(1, _hit.collider.transform.position);
        }
    }


    public void OnShoot(InputAction.CallbackContext context)
    {
       
        if (context.performed)
        {
           
            if (Physics.Raycast(firePoint.position, firePoint.forward, out _hit, 100, TargetLaymask))
            {
                //Destroy(hit.collider.gameObject);
                print("Hit");
                _lineRenderer.positionCount = 2;
            }
        }
        else
        {
            _lineRenderer.positionCount = 0;
        }
    }
}

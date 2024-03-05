using System;
using UnityEngine;

public class GunRotate : MonoBehaviour
{
    private Grapple script;
    private Quaternion DRotation;
    private void Start()
    {
        script = GetComponentInParent<Grapple>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(!Grapple.isGrapple)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, transform.parent.rotation, 5f * Time.fixedDeltaTime);
        }
        else
        {
            transform.LookAt(script.GetGrapplePoint());
        }
    }
}

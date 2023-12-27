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
    void Update()
    {
        if(!Grapple.isGrapple)
        {
            //transform.rotation = Quaternion.Lerp(transform.rotation, Camera.main.transform.rotation, Time.deltaTime * 5f);
            transform.rotation = transform.parent.rotation;
        }
        else
        {
            transform.LookAt(script.GetGrapplePoint());
        }



    }
}

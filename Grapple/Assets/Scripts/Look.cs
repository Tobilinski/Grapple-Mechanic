using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Look : MonoBehaviour
{
    public Transform Player;
    [SerializeField]
    private float lookSensit = 20f;
    private float _RotationVelocityOnY;
    private float _RotationVelocityOnX;
    float _UDLRotation;
    float _LRRoatation;

    private float y;
    private float x;

    private float xRotation = 0f;
    
    private void Start()
    {
        
    }

    private void Update()
    {
       
    }

    private void FixedUpdate()
    {
        _LRRoatation -= x * lookSensit * Time.deltaTime;
        _UDLRotation -= y * lookSensit * Time.deltaTime;
        _UDLRotation = Mathf.Clamp(_UDLRotation,-90f, 90f);
        transform.localRotation = Quaternion.Euler(Mathf.SmoothDampAngle(transform.localEulerAngles.y, _UDLRotation, ref _RotationVelocityOnY, 0f), 0f, 0f);
        Player.transform.localRotation = Quaternion.Euler(0f, Mathf.SmoothDampAngle(transform.localEulerAngles.y, -_LRRoatation, ref _RotationVelocityOnX, 0f), 0f);
       
    }
    // Update is called once per frame
    void LateUpdate()
    {
        try
        {
            string[] joystickNames = Input.GetJoystickNames();
            if (joystickNames[0] == "Wireless Controller")
            {
                lookSensit = 225f;
            }
            else
            {
                lookSensit = 20f;
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }
    
    

    public void OnLook(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            y = context.ReadValue<Vector2>().y;
            x = context.ReadValue<Vector2>().x;
        }
        else if (context.canceled)
        {
            y = 0f;
            x = 0f;
        }
    }
}

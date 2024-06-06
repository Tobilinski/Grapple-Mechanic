using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Look : MonoBehaviour
{
    public Transform Player;
    [SerializeField]
    private float lookSensit;
    private float _RotationVelocityOnY;
    private float _RotationVelocityOnX;
    float _UDLRotation;
    float _LRRoatation;

    private float y;
    private float x;

    

    private Vector2 accumulatedDelta = Vector2.zero;

    private void FixedUpdate()
    {
       // Apply accumulated delta to rotation
    _LRRoatation -= accumulatedDelta.x * Time.deltaTime;
    _UDLRotation += accumulatedDelta.y * Time.deltaTime;

    // Clamp vertical rotation
    _UDLRotation = Mathf.Clamp(_UDLRotation, -90f, 90f);

    // Reset accumulated delta
    accumulatedDelta = Vector2.zero;
    }
    // Update is called once per frame
    void LateUpdate()
    {
        
        transform.localRotation = Quaternion.Euler(Mathf.SmoothDampAngle(transform.localEulerAngles.y, -_UDLRotation, ref _RotationVelocityOnY, 0f), 0f, 0f);
        Player.transform.localRotation = Quaternion.Euler(0f, Mathf.SmoothDampAngle(transform.localEulerAngles.y, -_LRRoatation, ref _RotationVelocityOnX, 0f), 0f);
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
        Vector2 delta = context.ReadValue<Vector2>() * lookSensit; // Applies sensitivity
        accumulatedDelta += delta;
    }
    else if (context.canceled)
    {
        accumulatedDelta = Vector2.zero;
    }
}
}

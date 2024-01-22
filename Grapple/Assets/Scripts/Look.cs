using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Look : MonoBehaviour
{
    public Transform Player;
    private float lookSensit = 20f;

    float UDLRotation;

    private float y;
    private float x;

    private void Start()
    {
        y = 90f;
    }

    // Update is called once per frame
    void Update()
    {
        Player.Rotate(Vector3.up * x * lookSensit * Time.deltaTime);

       
        UDLRotation -= y * lookSensit * Time.deltaTime;
        
        UDLRotation = Mathf.Clamp(UDLRotation,-90f, 90f);
        
        transform.localRotation = Quaternion.Euler(UDLRotation, 0f, 0f);
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

using UnityEngine;
using UnityEngine.InputSystem;

public class Look : MonoBehaviour
{
    public Transform Player;
    private float lookSensit = 20f;

    float UDLRotation;

    private float y;
    private float x;


    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        Player.Rotate(Vector3.up * x * lookSensit * Time.deltaTime);

       
        UDLRotation -= y * lookSensit * Time.deltaTime;
        
        UDLRotation = Mathf.Clamp(UDLRotation,-70f, 60f);
        
        transform.localRotation = Quaternion.Euler(UDLRotation, 0f, 0f);
        if (Input.GetJoystickNames()[0] == "Wireless Controller")
        {
           lookSensit = 225f;
        }
        else
        {
            lookSensit = 25f;
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

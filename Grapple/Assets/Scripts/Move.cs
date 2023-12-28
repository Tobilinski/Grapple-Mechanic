using UnityEngine;
using UnityEngine.InputSystem;
using Cursor = UnityEngine.Cursor;

public class Move : MonoBehaviour
{
    private Vector2 _Direction;
    private float speed = 10f;
    private Rigidbody rb;
    private float jumpForce = 10f;
    public Transform groundchecker;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = false;
    }

    private void Update()
    {
        toggleReturnKey();
    }

    void FixedUpdate()
    {
        // Get the world space forward direction
        Vector3 forwardDirection = transform.forward;

        // Project the forward direction onto the horizontal plane
        forwardDirection.y = 0f;
        forwardDirection.Normalize();

        // Calculate the movement vector in world space
        Vector3 movement = new Vector3(_Direction.x, 0f, _Direction.y);
        movement = Quaternion.FromToRotation(Vector3.forward, forwardDirection) * movement;
        
        if (_Direction != Vector2.zero)
        {
            rb.velocity = new Vector3(movement.x * speed, rb.velocity.y, movement.z * speed);
        }
        
        if (!checkGround())
        {
            rb.drag = 0f;
        }
        else
        {
            rb.drag = 2f;
        }
        
    }

    public void MoveForward(InputAction.CallbackContext context)
    {
        if (context.performed && checkGround())
        {
            _Direction = context.ReadValue<Vector2>();
        }
        else if (context.canceled)
        {
            _Direction = Vector2.zero;
        }
    }
    public void Jump(InputAction.CallbackContext context)
    {
        if (context.performed && checkGround())
        {
            rb.velocity = new Vector3(rb.velocity.x, jumpForce, rb.velocity.z);
        }
    }
    bool checkGround()
    {
        RaycastHit hit;
        if (Physics.SphereCast(groundchecker.position, 1f, Vector3.down, out hit, 1f))
        {
            if (hit.collider.CompareTag("Ground"))
            {
                return true;
            }
        }
        return false;
    }
    void toggleReturnKey()
    {
        if (Input.GetKeyDown(KeyCode.BackQuote))
        {
            Cursor.visible = !Cursor.visible;
        }
    }
}
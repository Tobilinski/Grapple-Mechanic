
using UnityEngine;
using UnityEngine.InputSystem;

public class Move : MonoBehaviour
{
    private Vector2 _Direction;
    private float speed = 10f;
    private Rigidbody rb;
    private float jumpForce = 10f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
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
            rb.drag = 1f;
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
        if (Physics.Raycast(transform.position, Vector3.down, out hit, 1.1f))
        {
            if (hit.collider.CompareTag("Ground"))
            {
                print("Grounded");
                return true;
            }
        }
        
        return false;
    }
}
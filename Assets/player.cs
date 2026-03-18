using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;
public class player : MonoBehaviour
{
    public LayerMask groundLayer;
    [SerializeField] private float speed;
    [SerializeField] private float jumpStrength;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private int doubleJumps;
    [SerializeField] private float coyoteTime;
    [SerializeField] private Vector2 boxSize;
    [SerializeField] private float castDistance;
    private float coyoteTimer;
    private int doubleJumpCount = 0;
    private bool grounded;
    private float xVelocity;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // used to check bounds of grounded check hitbox
    /*
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position-transform.up*castDistance, boxSize);
    }
    */
    // Update is called once per frame
    void Update()
    {
        grounded = Physics2D.BoxCast(transform.position, boxSize, 0, -transform.up, castDistance, groundLayer);

        if (grounded)
        {
            coyoteTimer = 0;
        }
        else
        {
            coyoteTimer++;
        }
        rb.linearVelocity = new Vector2 (xVelocity * speed, rb.linearVelocityY);
        //FIX THIS DAVID MAKE IT A BOXCAST SO THAT IT CAN CHECK FOR IF THE VELOCITY VECTOR WOULD SHOVE IT INTO A WALL SO THAT YOU CAN NO LONGER WALL CLING
        if (Physics2D.Raycast(transform.position, new Vector2(rb.linearVelocityX,0), rb.linearVelocityX, groundLayer))
        {
            rb.linearVelocity = new Vector2(0, rb.linearVelocityY);
        }
  


    }

    public void Move(InputAction.CallbackContext context)
    {
        xVelocity = context.ReadValue<Vector2>().x;
    }

    
    public void jump(InputAction.CallbackContext context)
    {
        if (grounded || coyoteTimer < coyoteTime)
        {
            rb.linearVelocityY = jumpStrength;
        }
        else if (doubleJumpCount < doubleJumps && context.performed && context.interaction is PressInteraction)
        {
            rb.linearVelocityY = jumpStrength * .8f;
            doubleJumpCount++;
        }
      
    }
}

using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;
using UnityEngine.Video;
public class player : MonoBehaviour
{
    public LayerMask groundLayer;
    public LayerMask hazardLayer;
    [SerializeField] private VideoPlayer vd;
    [SerializeField] private AudioSource ad;
    [SerializeField] private float speed;
    [SerializeField] private float jumpStrength;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private int doubleJumps;
    [SerializeField] private float coyoteTime;
    [SerializeField] private Vector2 groundedCheckWidth;
    [SerializeField] private Vector2 wallCheckWidth;
    [SerializeField] private float castDistance;
    private float coyoteTimer;
    private int doubleJumpCount = 0;
    private bool grounded;
    private float xVelocity;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    public void DIE()
    {
        ad.Play();
        vd.Play();
    }

    // used to check bounds of grounded check hitbox
    /*
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position-transform.up*castDistance, groundedCheckWidth);
    }
    */
    // Update is called once per frame
    void Update()
    {
        grounded = Physics2D.BoxCast(transform.position, groundedCheckWidth, 0, -transform.up, castDistance, groundLayer);

        if (Physics2D.BoxCast(transform.position, new Vector2(.7f,.7f), 0, transform.up, 0, hazardLayer))
        {
            DIE();
        }
        if (grounded)
        {
            doubleJumpCount = 0;
            coyoteTimer = 0;
        }
        else
        {
            coyoteTimer++;
        }
        rb.linearVelocity = new Vector2 (xVelocity * speed, rb.linearVelocityY);
        
        if (rb.linearVelocityX > 0 && Physics2D.BoxCast(transform.position + new Vector3(0,.05f), wallCheckWidth, 0, transform.right, .18f, groundLayer))
        {
            rb.linearVelocity = new Vector2(0, rb.linearVelocityY);
        }
        else if (rb.linearVelocityX < 0 && Physics2D.BoxCast(transform.position + new Vector3(0, .05f), wallCheckWidth, 0, -transform.right, .22f, groundLayer))
        {
            rb.linearVelocity = new Vector2(0, rb.linearVelocityY);
        }
    }
    /*
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position + new Vector3(.18f,0f), wallCheckWidth);
        Gizmos.DrawWireCube(transform.position - new Vector3(.22f, 0f), wallCheckWidth);
    }
    */
    
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

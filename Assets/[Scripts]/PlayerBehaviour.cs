using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    [Header("Touch Input")]
    public Joystick joystick;
    [Range(0.01f, 1.0f)]
    public float sensitivity;

    [Header("Movement")] 
    public float horizontalForce;
    public float verticalForce;
    public bool isGrounded;
    public Transform groundOrigin;
    public float groundRadius;
    public LayerMask groundLayerMask;
    public float airControlFactor;

    [Header("Animation")]
    public PlayerAnimState state;

    private Animator animatorController;
    private Rigidbody2D rigidbody;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        animatorController = GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Move();
        CheckIfGrounded();
    }

    private void Move()
    {
        float x = (Input.GetAxisRaw("Horizontal") + joystick.Horizontal) * sensitivity;

        if (isGrounded)
        {
            //float deltaTime = Time.deltaTime;

            // Keyboard Input
            float y = (Input.GetAxisRaw("Vertical") + joystick.Vertical) * sensitivity;
            float jump = Input.GetAxisRaw("Jump") + ((UIController.jumpButtonDown) ? 1.0f : 0.0f);

            // Check for Flip
            if (x != 0)
            {
                x = FlipAnimation(x);

                // Player Run
                animatorController.SetInteger("AnimationState", (int)PlayerAnimState.RUN);
                state = PlayerAnimState.RUN;
            } 
            else
            {
                // Player Idle
                animatorController.SetInteger("AnimationState", (int)PlayerAnimState.IDLE);
                state = PlayerAnimState.IDLE;
            }
            
            float horizontalMoveForce = x * horizontalForce;// * deltaTime;
            float jumpMoveForce = jump * verticalForce; // * deltaTime;

            float mass = rigidbody.mass * rigidbody.gravityScale;

            rigidbody.AddForce(new Vector2(horizontalMoveForce, jumpMoveForce) * mass);
            rigidbody.velocity *= 0.99f; // scaling / stopping hack
        }
        else   // In the air
        {
            // Player Jump / Fall
            animatorController.SetInteger("AnimationState", (int)PlayerAnimState.JUMP);
            state = PlayerAnimState.JUMP;

            if (x != 0)
            {
                x = FlipAnimation(x);
                float horizontalMoveForce = x * horizontalForce * airControlFactor;    // Air Control
                float mass = rigidbody.mass * rigidbody.gravityScale;

                rigidbody.AddForce(new Vector2(horizontalMoveForce, 0.0f) * mass);
            }
        }

    }

    private void CheckIfGrounded()
    {
        RaycastHit2D hit = Physics2D.CircleCast(groundOrigin.position, groundRadius, Vector2.down, groundRadius, groundLayerMask);

        isGrounded = (hit) ? true : false;
    }

    private float FlipAnimation(float x)
    {
        // depending on direction scale across the x-axis either 1 or -1
        x = (x > 0) ? 1 : -1;

        transform.localScale = new Vector3(x, 1.0f);
        return x;
    }


    // UTILITIES

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(groundOrigin.position, groundRadius);
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Platform"))
        {
            transform.SetParent(collision.transform);   // Set parent
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Platform"))
        {
            transform.SetParent(null);  // Unset parent
        }
    }
}

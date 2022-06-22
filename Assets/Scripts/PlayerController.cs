using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float jumpHeight;
    [SerializeField] private float raycastDistance;
    [SerializeField] private LayerMask floor;
    [SerializeField] private float coyoteTimeSet;
    [SerializeField] private float inputBufferTimeSet;
    private Queue<KeyCode> inputBuffer;
    private Rigidbody2D rb;
    private RaycastHit2D floorRaycast;
    private float coyoteTime;
    private bool alreadyJumped;
    // private IWeapon weapon;
    // private animationController animator;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        
    }
    // Start is called before the first frame update
    void Start()
    {
        inputBuffer = new Queue<KeyCode>();
    }

    // Update is called once per frame
    void Update()
    {
        FloorRaycast();
        Movement(Input.GetAxisRaw("Horizontal"));
        Timer();
        if (Input.GetKeyDown(KeyCode.Space))
        {
            inputBuffer.Enqueue(KeyCode.Space);
            Invoke("RemoveInput", inputBufferTimeSet);
        }
        Jump();

    }

    void FloorRaycast()
    {
        floorRaycast = Physics2D.Raycast(transform.position, Vector2.down, raycastDistance, floor);
    }

    void Movement(float direction)
    {
        rb.velocity = new Vector3(direction * speed, rb.velocity.y, 0f);
    }

    void Jump()
    {
        if (floorRaycast == true)
        {
            if(inputBuffer.Count > 0)
            {
                if(inputBuffer.Peek() == KeyCode.Space)
                {
                    rb.velocity = new Vector3(rb.velocity.x, jumpHeight, 0f);
                    inputBuffer.Dequeue();
                    alreadyJumped = true;
                }
                
            }
            
        }
        else if(inputBuffer.Count > 0)
        {
            if(inputBuffer.Peek() == KeyCode.Space)

                if (coyoteTime < coyoteTimeSet && alreadyJumped == false)
                {
                    alreadyJumped = true;
                    rb.velocity = new Vector3(rb.velocity.x, jumpHeight, 0f);
                    inputBuffer.Dequeue();
                }
        }

    }

    void Timer()
    {
        if(floorRaycast == true)
        {
            coyoteTime = 0;
        }
        else
        {
            coyoteTime += Time.deltaTime;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.layer == 6)
        {

            alreadyJumped = false;

        }
    }

    private void RemoveInput()
    {
        if(inputBuffer.Count > 0)
        {
            inputBuffer.Dequeue();
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, Vector2.down * raycastDistance);
    }
}

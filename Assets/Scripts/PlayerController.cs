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

    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = new Vector3(Input.GetAxisRaw("Horizontal") * speed, rb.velocity.y, 0f);

        floorRaycast = Physics2D.Raycast(transform.position, Vector2.down, raycastDistance, floor);



        Timer();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
            alreadyJumped = true;
        }

    }

    void Jump()
    {
        if (floorRaycast == true)
        {
            rb.velocity = new Vector3(rb.velocity.x, jumpHeight, 0f);
            
        }
        else
        {
            if(coyoteTime < coyoteTimeSet && alreadyJumped == false)
            {
                alreadyJumped = true;
                rb.velocity = new Vector3(rb.velocity.x, jumpHeight, 0f);
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



    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, Vector2.down * raycastDistance);
    }
}

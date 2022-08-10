using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float jumpHeight;
    [SerializeField] private float raycastDistance;
    [SerializeField] private LayerMask floor;
    [SerializeField] private float coyoteTimeSet;
    [SerializeField] private float inputBufferTimeSet;
    [SerializeField] private Transform hand;
    [SerializeField] private Transform arm;
    private Vector2 movementInput = Vector2.zero;
    private bool jumpInput = false; 
    private Queue<string> inputBuffer;
    private Rigidbody2D rb;
    private RaycastHit2D floorRaycast;
    private float coyoteTime;
    private bool alreadyJumped;
    private IWeapon weapon;
    private GameObject weaponObject;

    public Transform Transform => throw new System.NotImplementedException();

    // private animationController animator;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        
    }
    // Start is called before the first frame update
    void Start()
    {
        inputBuffer = new Queue<string>();
    }

    // Update is called once per frame
    void Update()
    {
        FloorRaycast();
        Movement();
        Jump();
        //Attack();
        //AimUp();
        Timer();
    }

    private void FloorRaycast()
    {
        floorRaycast = Physics2D.Raycast(transform.position, Vector2.down, raycastDistance, floor);
    }

    private void Movement()
    {
        Vector3 aux;
        rb.velocity = new Vector3(movementInput.x * speed, rb.velocity.y, 0f);
        aux = transform.localScale;
        /*if(direction != 0)
        {
            aux.x = Mathf.Abs(aux.x) * direction;
        }
        transform.localScale = aux;*/
        if (movementInput.x < 0)
        {
            var ang = transform.rotation.eulerAngles;
            ang.y = 180;
            transform.rotation = Quaternion.Euler(ang);
        }
        if (movementInput.x > 0)
        {
            var ang = transform.rotation.eulerAngles;
            ang.y = 0;
            transform.rotation = Quaternion.Euler(ang);
        }
    }

    private void Jump()
    {
        if (jumpInput)
        {
            inputBuffer.Enqueue("jump");
            Invoke("RemoveInput", inputBufferTimeSet);
            Debug.Log(jumpInput);
        }
        if (floorRaycast == true)
        {
            if(inputBuffer.Count > 0)
            {
                if(inputBuffer.Peek() == "jump")
                {
                    rb.velocity = new Vector3(rb.velocity.x, jumpHeight, 0f);
                    inputBuffer.Dequeue();
                    alreadyJumped = true;
                }
                
            }
            
        }
        else if(inputBuffer.Count > 0)
        {
            if(inputBuffer.Peek() == "jump")

                if (coyoteTime < coyoteTimeSet && alreadyJumped == false)
                {
                    alreadyJumped = true;
                    rb.velocity = new Vector3(rb.velocity.x, jumpHeight, 0f);
                    inputBuffer.Dequeue();
                    coyoteTime = 0;
                }
        }

    }

    private void AimUp(KeyCode up)
    {
        if (Input.GetKeyDown(up))
        {
            arm.Rotate(0f, 0f, 90f, Space.Self);
        }
        else if (Input.GetKeyUp(up))
        {
            arm.Rotate(0f, 0f, -90f, Space.Self);
        }
    }

    private void Timer()
    {
        if(floorRaycast == true)
        {
            return;
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
        else
        {
            return;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        weapon = collision.GetComponent<IWeapon>();
        weapon.Transform.position = hand.position;
        weapon.Transform.rotation = hand.rotation;
        weapon.Transform.SetParent(hand);
        weapon.Collider2D.enabled = false;

        
    }

    //Editor Debug
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, Vector2.down * raycastDistance);
    }


    public void Attack(KeyCode trigger)
    {
        if (Input.GetKeyDown(trigger))
        {
            weapon.Attack();
        }
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        movementInput = context.ReadValue<Vector2>();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        jumpInput = context.action.triggered;
    }
}

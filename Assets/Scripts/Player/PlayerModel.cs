using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerModel : MonoBehaviour
{
    public PlayerStats Stats { get => this.stats; }

    [SerializeField] private PlayerStats stats;

    [SerializeField] private LayerMask floor;

    [SerializeField] private float coyoteTimeSet;

    [SerializeField] private float inputBufferTimeSet;

    [SerializeField] private Transform hand;

    [SerializeField] private Transform arm;

    private Queue<string> inputBuffer;

    private Rigidbody2D rb;

    private RaycastHit2D floorRaycast;
    private RaycastHit2D sideUpRaycast;
    private RaycastHit2D sideBackRaycast;
    private bool canFalling;

    [SerializeField] private float raycastHitDistance;
    [SerializeField] private float raycastFloorDistance;

    private float coyoteTime;

    private bool alreadyJumped;

    private IWeapon weapon;

    [SerializeField] private Transform floorOffset;
    [SerializeField] private Transform hitOffsetUp;
    [SerializeField] private Transform hitOffsetBack;
    private Vector3 directionRaycast;



    private void Awake()
    {
        stats = ScriptableObject.CreateInstance<PlayerStats>();
        
        rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        
        inputBuffer = new Queue<string>();
    }


    public void Raycasts()
    {
        floorRaycast = Physics2D.Raycast(floorOffset.position, Vector2.down, raycastFloorDistance, floor);
        sideUpRaycast = Physics2D.Raycast(hitOffsetUp.position, Vector2.left, raycastHitDistance, floor);
        sideBackRaycast = Physics2D.Raycast(hitOffsetBack.position, Vector2.left, raycastHitDistance, floor);
    }

    public void Movement(float x)
    {
        if (!sideUpRaycast && !sideBackRaycast)
            rb.velocity = new Vector3(x * stats.speed, rb.velocity.y, 0f);

        if (x < 0)
        {
            raycastHitDistance = 0.6f;
            var ang = transform.rotation.eulerAngles;
            ang.y = 180;
            transform.rotation = Quaternion.Euler(ang);
        }
        if (x > 0)
        {
            raycastHitDistance = -0.6f;
            directionRaycast = Vector2.left;
            var ang = transform.rotation.eulerAngles;
            ang.y = 0;
            transform.rotation = Quaternion.Euler(ang);
        }

    }

    public void Jump()
    {
        
        if (floorRaycast == true && canFalling)
        {
            if (inputBuffer.Count > 0)
            {
                if (inputBuffer.Peek() == "jump")
                {
                    rb.velocity = new Vector3(rb.velocity.x, stats.jumpHeight, 0f);
                    inputBuffer.Dequeue();
                    alreadyJumped = true;
                }
            }
        }
        else if (inputBuffer.Count > 0)
        {
            if (inputBuffer.Peek() == "jump")
            {
                if (coyoteTime < coyoteTimeSet && alreadyJumped == false)
                {
                    alreadyJumped = true;
                    rb.velocity = new Vector3(rb.velocity.x, stats.jumpHeight, 0f);
                    inputBuffer.Dequeue();
                }
            }
        }
    }

    public void JumpQueue()
    {
        inputBuffer.Enqueue("jump");
        Invoke("RemoveInput", inputBufferTimeSet);
    }



    public void AimUp()
    {
        arm.Rotate(0f, 0f, 90f, Space.Self);
    }

    public void AimUpRelease()
    {
        arm.Rotate(0f, 0f, -90f, Space.Self);
    }

    public void Attack()
    {
        weapon.Attack();
    }

    public void Timer()
    {
        if (floorRaycast == true)
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
            coyoteTime = 0;
        }
        else
        {
            canFalling = true;
        }

    }

    private void RemoveInput()
    {
        if (inputBuffer.Count > 0)
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
        Debug.Log("Taken");
        weapon = collision.GetComponent<IWeapon>();
        weapon.Transform.position = hand.position;
        weapon.Transform.rotation = hand.rotation;
        weapon.Transform.SetParent(hand);
        weapon.Collider2D.enabled = false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawRay(floorOffset.position, Vector2.down * raycastFloorDistance);
        Gizmos.DrawRay(hitOffsetUp.position, Vector2.left * raycastHitDistance);
        Gizmos.DrawRay(hitOffsetBack.position, Vector2.left * raycastHitDistance);
    }



}

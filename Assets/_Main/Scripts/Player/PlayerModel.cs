using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerModel : MonoBehaviour
{
    [SerializeField] private LayerMask floor;

    [SerializeField] private float coyoteTimeSet;

    [SerializeField] private float inputBufferTimeSet;

    [SerializeField] private Transform hand;

    [SerializeField] private Transform arm;


    [SerializeField] private Transform dropPosition;

    private Queue<string> inputBuffer = new Queue<string>();

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

    private StatsController statsController;

    [SerializeField] private Transform floorOffset;
    [SerializeField] private Transform hitOffsetUp;
    [SerializeField] private Transform hitOffsetBack;
    private Vector3 directionRaycast;

    private void Awake()
    {

        statsController = GetComponent<StatsController>();

        rb = GetComponent<Rigidbody2D>();
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
            rb.velocity = new Vector3(x * statsController.Speed, rb.velocity.y, 0f);

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
        if (floorRaycast == true)
        {
            if (inputBuffer.Count > 0)
            {
                if (inputBuffer.Peek() == "jump")
                {
                    rb.velocity = new Vector3(rb.velocity.x, statsController.JumpHeight, 0f);
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
                    rb.velocity = new Vector3(rb.velocity.x, statsController.JumpHeight, 0f);
                    inputBuffer.Dequeue();
                }
            }
        }
    }
    private void Update()
    {
        
    }
    public void CancelledJump()
    {
        if (rb.velocity.y > 0)
            rb.velocity = new Vector3(rb.velocity.x, 0f, 0f);
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
        arm.Rotate(0, 0, -90f, Space.Self);
    }

    public void Attack(float input)
    {
        if (weapon != null && input > 0)
        {
            weapon.Attack();
            if (weapon.Ammo <= 0 && weapon.CanLifeTime)
            {
                gameObject.layer = 7;
                weapon.DestroyWeapon();
                weapon = null;
            }
        }
        

    }
    public void DropWeapon()
    {
        if (weapon != null)
        {
            gameObject.layer = 7; //TODO: layer 8 es "player" 
            weapon.Transform.SetParent(null);
            weapon.Transform.position = dropPosition.transform.position;
            weapon.Collider2D.enabled = true;
            weapon.Rigidbody2D.isKinematic = false;
            weapon.Rigidbody2D.simulated = true;
            weapon.SpriteRenderer.sortingOrder = 0;
            weapon = null;

        }
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (weapon == null && collision.gameObject.layer == 8)
        {         
            weapon = collision.GetComponent<IWeapon>();
            GrabWeapon();
        }      

        if(weapon != null && collision.gameObject.layer == 8)
        {
            gameObject.layer = default; //TODO: reemplazar esto por una logica
                                        //con los mesh collision
        }

       
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(floorOffset.position, Vector2.down * raycastFloorDistance);
        Gizmos.DrawRay(hitOffsetUp.position, Vector2.left * raycastHitDistance);
        Gizmos.DrawRay(hitOffsetBack.position, Vector2.left * raycastHitDistance);
    }

    private void GrabWeapon()
    {
      
        weapon.Transform.position = hand.position;
        weapon.Transform.rotation = hand.rotation;
        weapon.Transform.SetParent(hand);
        weapon.Collider2D.enabled = false;
        weapon.SpriteRenderer.sortingOrder = 2;
        weapon.Rigidbody2D.isKinematic = true;
        weapon.Rigidbody2D.simulated = false;
        weapon.Rigidbody2D.velocity = Vector3.zero;

    }



}

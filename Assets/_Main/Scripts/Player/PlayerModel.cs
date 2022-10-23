using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerModel : MonoBehaviour
{

    [SerializeField] private int speedY; //TODO: pasarlo a stats
    [SerializeField] private int speedYFalling=21; //TODO: pasarlo a stats

    [SerializeField] private LayerMask floor;

    [SerializeField] private float coyoteTimeSet;

    [SerializeField] private float inputBufferTimeSet;

    [SerializeField] private Transform hand;

    [SerializeField] private Transform arm;


    [SerializeField] private Transform dropPosition;

    [SerializeField] private float raycastHorizontalDistance;
    [SerializeField] private float raycastFloorDistance;

    [SerializeField] private Transform floorOffset;
    [SerializeField] private Transform hitOffsetLeft;
    [SerializeField] private Transform hitOffsetRight;


    private Queue<string> inputBuffer = new Queue<string>();

    private RaycastHit2D floorRaycast;
    private RaycastHit2D sideLeftRaycast;
    private RaycastHit2D sideRightRaycast;
    private float coyoteTime;
    private bool alreadyJumped;

    private IWeapon weapon;

    private StatsController statsController;
    private Rigidbody2D rb;
    private SpriteRenderer spriteRender;


    private void Awake()
    {

        statsController = GetComponent<StatsController>();
        spriteRender= GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
    }

    public void Raycasts()
    {
        floorRaycast = Physics2D.Raycast(floorOffset.position, Vector2.down, raycastFloorDistance, floor);
        sideRightRaycast = Physics2D.Raycast(hitOffsetRight.position, Vector2.down, raycastHorizontalDistance , floor);
        sideLeftRaycast = Physics2D.Raycast(hitOffsetLeft.position, Vector2.down, raycastHorizontalDistance , floor);
    }

    public void Movement(float x)
    {
        if (!sideRightRaycast && !sideLeftRaycast)
        {
            rb.velocity = new Vector3(x * statsController.Speed, rb.velocity.y, 0f);
        }
       
        if(!floorRaycast && !alreadyJumped && !sideLeftRaycast && !sideRightRaycast)
        {
            print(speedYFalling);
            //rb.velocity = new Vector3(rb.velocity.x, -speedYFalling, 0f);
            
        }
        if (sideLeftRaycast && !alreadyJumped || sideRightRaycast && !alreadyJumped)
        { 
            rb.velocity = new Vector3(x * statsController.Speed, speedY, 0f);
        }

        if (x < 0)
        {
            spriteRender.flipX = true;
            arm.transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        if (x > 0)
        {
            arm.transform.rotation = Quaternion.Euler(0, 0, 0);
            spriteRender.flipX = false;           
        }
    }

    public void Jump(float x)
    {
        if (floorRaycast == true || sideRightRaycast && !floorRaycast || sideLeftRaycast && !floorRaycast)
        {
            if (inputBuffer.Count > 0)
            {
                if (inputBuffer.Peek() == "jump")
                {
                    inputBuffer.Dequeue();
                    alreadyJumped = true;
                    if (!sideLeftRaycast && !sideRightRaycast)
                    {
                        rb.velocity = new Vector3(rb.velocity.x, statsController.JumpHeight, 0f);
                    }
                    else
                    {
                        rb.velocity = new Vector3(x * statsController.Speed, statsController.JumpHeight, 0f);
                    }

                }

            }
        }
        else if (inputBuffer.Count > 0)
        {
            if (inputBuffer.Peek() == "jump")
            {
                if (coyoteTime < coyoteTimeSet && alreadyJumped == false)
                {
                    inputBuffer.Dequeue();
                    alreadyJumped = true;
                    if (!sideLeftRaycast && !sideRightRaycast)
                    {
                        rb.velocity = new Vector3(rb.velocity.x, statsController.JumpHeight, 0f);
                    }
                    else
                    {
                        rb.velocity = new Vector3(x * statsController.Speed, statsController.JumpHeight, 0f);
                    }
                }
            }
        }
    }
    private void Update()
    {
        print(speedYFalling);
    }
    public void CancelledJump()
    {
        if (rb.velocity.y > 0 /*&& !sideLeftRaycast && !sideRightRaycast*/)
            rb.velocity = new Vector3(rb.velocity.x, speedY, 0f);
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

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(floorOffset.position, Vector2.down * raycastFloorDistance);
        Gizmos.DrawRay(hitOffsetRight.position, Vector2.down * raycastHorizontalDistance);
        Gizmos.DrawRay(hitOffsetLeft.position, Vector2.down * raycastHorizontalDistance);
    }



}

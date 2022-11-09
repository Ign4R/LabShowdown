using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Katana : MonoBehaviour, IWeapon
{
    [SerializeField] private float hitTimerSet;

    [SerializeField] private bool canDestroy;

    [SerializeField] private int ammo;

    [SerializeField] private bool isFullAuto;

    [SerializeField] private Animator animator;

    [SerializeField] private GameObject hitBox;

    public Rigidbody2D Rigidbody2D { get; set; }

    public Transform _Transform { get; set; }    

    public float CurrentTime { get; private set; }

    public Collider2D _Collider2D { get; set; }

    public SpriteRenderer _SpriteRenderer { get; set; }

    public bool TouchGround { get; private set; }

    public int Ammo { get => ammo; private set => ammo = value; }

    public bool IsFullAuto { get => isFullAuto; private set => isFullAuto = value; }

    public bool CanDestroy { get => canDestroy; private set => canDestroy = value; }

    public GameObject GO { get; private set; }

    float hitTimer; // Este timer es temporal, hay que quitarlo

    private void Awake()
    {
        _Collider2D = GetComponent<Collider2D>();
        Rigidbody2D = GetComponent<Rigidbody2D>();
        _SpriteRenderer = transform.GetChild(1).GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        TouchGround = false;
        CurrentTime = 0f;
        canDestroy = true;
        _Transform = transform;
    }
    private void Update()
    {
        if (hitTimer > 0)
        {
            hitTimer -= Time.deltaTime;
        }
        else
        {
            hitBox.GetComponent<Collider2D>().enabled = false;
        }
    }

    public void Attack()
    {
        if (hitTimer <= 0)
        {
            animator.SetTrigger("Attack");
            hitBox.GetComponent<Collider2D>().enabled = true;
            //GetComponent<Collider2D>().enabled = true;
            hitTimer = hitTimerSet;
            ammo--;
        }
    }

    public void DestroyWeapon()
    {
        Destroy(gameObject);
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        CurrentTime = 1;
        if (collision.gameObject.layer == 6 && collision.gameObject.layer != 7)
        {
            TouchGround = true;
            CurrentTime = 1;
        }
        if (collision.gameObject.layer == 7)
        {
            CurrentTime = 0f;
        }
        if (collision.gameObject.layer == 8 && !TouchGround) //El que colisiona con weapon y no toca el piso, destruye el objeto a colisionar
        {
            Destroy(collision.gameObject);
        }
    }
}

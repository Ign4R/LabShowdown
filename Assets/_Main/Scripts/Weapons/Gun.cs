using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour, IWeapon
{
    [SerializeField] private GameObject bullet;
    private GameObject go;

    [SerializeField] private Transform positionBullet;
    [SerializeField] private int ammo;
    private float currentTime;
    [SerializeField] private bool canDestroy;

    [SerializeField] private float cadence;

    private float cadenceTimer;

    public Transform _Transform { get; set; }

    public Collider2D _Collider2D { get; set; }

    public Rigidbody2D Rigidbody2D { get; set; }

    public SpriteRenderer _SpriteRenderer { get; set; }
    public int Ammo { get => ammo; private set => ammo = value; }

    public bool CanDestroy { get => canDestroy; private set => canDestroy = value; }

    public float CurrentTime { get => currentTime; private set => currentTime = value; }

    public bool TouchGround { get; private set; }
    public GameObject GO { get => go; private set => go = value; }

    private void Awake()
    {
        go = gameObject;
        _Collider2D = GetComponent<Collider2D>();
        Rigidbody2D = GetComponent<Rigidbody2D>();
        _SpriteRenderer = GetComponent<SpriteRenderer>();

    }
    private void Start()
    {
        
        TouchGround = false;
        currentTime = 0f;
        canDestroy = true;
        _Transform = transform;
    }
    private void Update()
    {
        //print(currentTime);
        
        if (currentTime >= 1) currentTime += Time.deltaTime;

        if (currentTime > DeathMatch.TimeLife)
        {
            currentTime = 0f;
            DestroyWeapon();
        }
        if (cadenceTimer > 0)
        {
            cadenceTimer -= Time.deltaTime;
        }
    }

   

    public void Attack()
    {
        if (cadenceTimer <= 0)
        {
            var temp = Instantiate(bullet, positionBullet.position, transform.rotation);
            ammo--;
            cadenceTimer = 1 / cadence;

        }
    }

    public void DestroyWeapon()
    {       
        Destroy(gameObject);      
    }


    public void OnTriggerEnter2D(Collider2D collision)
    {
        currentTime = 1;
        if (collision.gameObject.layer == 6 && collision.gameObject.layer != 7)
        {
            TouchGround = true;
            currentTime = 1;
        }
        if (collision.gameObject.layer == 7) 
        {         
            currentTime = 0f;
            
        }
        if (collision.gameObject.layer == 8 && !TouchGround) //El que colisiona con weapon y no toca el piso, destruye el objeto a colisionar
        {
            Destroy(collision.gameObject);
        }

   
    }


}

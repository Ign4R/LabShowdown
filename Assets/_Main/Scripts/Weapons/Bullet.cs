using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed = 0;

    [SerializeField] private int bulletType;

    private Rigidbody2D rb;

    [SerializeField] private int damage;
    [SerializeField] private int lifeTime;
    private string audioName;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        switch (bulletType)
        {
            case 0:
                audioName = "rifle";
                break;
            case 1:
                audioName = "firepistol";
                break;
            case 2:
                audioName = "icepistol";
                break;
        }

    }

    private void Start()
    {
        AudioManager.Instance.Play(audioName);
    }

    private void Update()
    {
        Move();
        Destroy(gameObject, lifeTime);
    }

    private void Move()
    {
        rb.velocity = transform.right * speed;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {

        collision.GetComponent<StatsController>()?.TakeDamage(damage);

        if (collision != null && collision.gameObject.layer == 6)  
        {
            Destroy(gameObject);
        }
        
        

        switch (bulletType)
        {
            case 1:
                collision.GetComponent<BuffsController>()?.Ignite();
                break;
            case 2:
                collision.GetComponent<BuffsController>()?.Frozeen();
                break;
        }
    }
}

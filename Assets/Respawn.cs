using UnityEngine;

public class Respawn : MonoBehaviour
{
    [SerializeField] private float damage;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision != null)
        {
            collision.GetComponent<StatsController>()?.TakeDamage(damage);
            collision.gameObject.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        }
    }
}

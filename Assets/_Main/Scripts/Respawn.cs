using UnityEngine;

public class Respawn : MonoBehaviour
{
   [SerializeField] private Transform respawnPoint;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision != null)
        {
            collision.gameObject.transform.position = respawnPoint.position;
        }
    }
}

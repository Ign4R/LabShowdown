using UnityEngine;

public class Respawn : MonoBehaviour
{
    [SerializeField] private float damage;
    private void OnTriggerEnter2D(Collider2D collision)
    {
     
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.GetComponent<StatsController>()?.TakeDamage(damage);
            collision.gameObject.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        
            IWeapon weaponInterface = collision.gameObject.GetComponent<PlayerModel>()?.Weapon;           
            if (weaponInterface != null)
            {
                weaponInterface.DestroyWeapon();
                collision.gameObject.GetComponent<PlayerModel>()?.WeaponIsNull();
            }
        }
        else if (collision.gameObject.layer == 8)
        {
            collision.gameObject.GetComponent<IWeapon>()?.DestroyWeapon();
        }



    }
}

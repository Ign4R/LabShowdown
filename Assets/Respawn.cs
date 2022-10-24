using UnityEngine;

public class Respawn : MonoBehaviour
{
    [SerializeField] private float damage;
    private void OnTriggerEnter2D(Collider2D collision)
    {
      
        if (collision != null)
        {
            print(collision.gameObject.name);
            print(collision.gameObject.GetComponent<IWeapon>() + "WEAPON PREFAB");

            collision.gameObject.GetComponent<IWeapon>()?.DestroyWeapon();
            collision.GetComponent<StatsController>()?.TakeDamage(damage);
            collision.gameObject.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
           

            IWeapon weaponInterface = collision.gameObject.GetComponent<PlayerModel>()?.Weapon;
          
            if (weaponInterface != null)
            {
                weaponInterface.DestroyWeapon();
                collision.gameObject.GetComponent<PlayerModel>()?.WeaponIsNull();
                collision.gameObject.layer = 7;
            }

        }
        if (collision.gameObject.tag == "Weapon")
        {
            print("hola");
            Destroy(collision.gameObject);
        }


    }
}

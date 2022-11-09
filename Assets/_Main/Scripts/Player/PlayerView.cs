using UnityEngine;

public class PlayerView : MonoBehaviour
{
    private Animator anim;
    private void Awake()
    {
        anim = GetComponent<Animator>();   
    }
    public void TakeDamageAnim()
    {
        anim.SetTrigger("Hit");   
    }
    public void GetIgniteAnim(bool state)
    {
        anim.SetBool("ignite", state);
    }

    public void GetFrozenAnim(bool state)
    {
        anim.SetBool("frozen", state);
    }
}

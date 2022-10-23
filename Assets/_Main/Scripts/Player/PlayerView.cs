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
}

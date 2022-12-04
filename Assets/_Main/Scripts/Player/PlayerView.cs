using UnityEngine;

public class PlayerView : MonoBehaviour
{
    private Animator anim;

    public Animator Anim { get => anim; set => anim = value; }

    private void Awake()
    {
        Anim = GetComponent<Animator>();   
    }

}

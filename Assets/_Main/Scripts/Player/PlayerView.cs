using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerView : MonoBehaviour
{
    private Animator anim;
    private void Awake()
    {
        anim = GetComponent<Animator>();   
    }
    // Start is called before the first frame update
    void Start()
    {
        StatsController.OnDamage += TakeDamageAnim;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void TakeDamageAnim()
    {
        anim.SetTrigger("Hit");   
    }
}

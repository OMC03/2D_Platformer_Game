using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tramp_Animation : MonoBehaviour
{
    private Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.transform.CompareTag("Player"))
        {
            anim.SetBool("Bounce", true);
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            anim.SetBool("Bounce", false);
        }
    }
}

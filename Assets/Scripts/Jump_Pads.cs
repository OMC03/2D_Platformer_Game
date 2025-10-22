using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump_Pads : MonoBehaviour
{
    [Header("Trampoline Specs")]
    [SerializeField] private float bounce;
    [SerializeField] private AudioSource Bounce;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up * bounce, ForceMode2D.Impulse);
            Bounce.Play();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemCollector : MonoBehaviour
{
    private int oranges = 0;

    [SerializeField] private AudioSource itemCollectionSound;

    [SerializeField] TMP_Text orangesText;

    [SerializeField] private string text = string.Empty;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Orange"))
        {
            itemCollectionSound.Play();
            Destroy(collision.gameObject);
            oranges++;
            orangesText.text = "Oranges: " + oranges;
        }
    }
}

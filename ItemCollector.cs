using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemCollector : MonoBehaviour
{
    private int collectedKeys = 0;

    [SerializeField] private Text keysText;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Key"))
        {
            Destroy(collision.gameObject);
            collectedKeys++;
            keysText.text = "Keys Collected: " + collectedKeys;
        }
    }
}

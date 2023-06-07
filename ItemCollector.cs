using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ItemCollector : MonoBehaviour
{
    public static int collectedKeys = 0;
    public Text keysText;

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

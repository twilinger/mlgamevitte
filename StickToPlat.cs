using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickToPlat : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Character")
        {
            collision.gameObject.transform.SetParent(transform);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Character")
        {
            collision.gameObject.transform.SetParent(null);
        }
    }

}

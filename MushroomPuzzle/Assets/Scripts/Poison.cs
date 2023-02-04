using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Poison : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Roots" || collision.transform.tag == "Fungus")
        {
            Destroy(collision.gameObject);
        }

        Destroy(gameObject);
    }
}

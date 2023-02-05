using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Poison : MonoBehaviour
{
    //public bool placed;
    public Mushroom parentMushroom;

    private void Start()
    {
        parentMushroom = transform.parent.GetComponent<Mushroom>();
    }

    private void Update()
    {
        //placed = parentMushroom.placed;

        if (parentMushroom.placed)
        {
            Destroy(gameObject, 0.2f);
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Roots" && parentMushroom.placed)
        {
            if (collision.gameObject.GetComponent<Roots>().alpha == 1)
            {
                Roots collidedRoots = collision.gameObject.GetComponent<Roots>();
                if (collidedRoots.evil)
                {
                    Destroy(collidedRoots.gameObject);
                    Destroy(gameObject);
                }
                else
                {
                    collidedRoots.evil = true;
                    Destroy(gameObject);
                }
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Roots : MonoBehaviour
{
    public float alpha = 0.75f;
    public CanvasGroup cg;
    public bool evil;
    public bool connectedToStart;
    public bool connectedToEnd;

    private void Start()
    {
        StartCoroutine(DestroyEvilRootsAfterDelay());
    }

    void Update()
    {
        cg.alpha = alpha;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.transform.tag == "Background" && alpha == 1)
        {
            Destroy(gameObject);
        }

        if (evil && (collision.transform.tag == "Roots" || collision.transform.tag == "Fungus") && alpha == 1)
        {
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }

    public IEnumerator DestroyEvilRootsAfterDelay()
    {
        yield return new WaitForSeconds(0.1f);
        if (alpha == 1 && evil)
        {
            Destroy(gameObject);
        }
        else if (evil)
        {
            StartCoroutine(DestroyEvilRootsAfterDelay());
        }
    }
}

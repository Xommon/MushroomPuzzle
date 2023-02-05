using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Roots : MonoBehaviour
{
    public float alpha = 0.75f;
    public CanvasGroup cg;
    public bool evil;
    public bool connectedToStart;
    public bool connectedToEnd;
    public GameManager gameManager;
    public bool brandNew;
    public RectTransform rt;
    public RectTransform paRt;
    public GameObject skull;
    public Image image; 

    private void Start()
    {
        //StartCoroutine(DestroyEvilRootsAfterDelay());
        gameManager = FindObjectOfType<GameManager>();
        rt = GetComponent<RectTransform>();
        paRt = transform.parent.GetComponent<RectTransform>();
        image = GetComponent<Image>();
    }

    void Update()
    {
        cg.alpha = alpha;
        
        if (alpha == 1 && brandNew)
        {
            gameManager.scoreToAdd += 10;
            gameManager.AddScore();
            brandNew = false;
        }

        if (evil)
        {
            image.color = Color.black;
            skull.SetActive(true);
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.transform.tag == "Bounds" && alpha == 1)
        {
            Destroy(gameObject);
        }

        if (collision.transform.tag == "Grid" && alpha == 1)
        {
            collision.gameObject.GetComponent<GridSquare>().occupied = true;
        }
    }

    public void SpreadPoison()
    {
        // Find nearby roots
        List<Roots> neighbourRoots = new List<Roots>();
        Roots[] allRoots = FindObjectsOfType<Roots>();
        foreach (Roots root in allRoots)
        {
            if (Vector2.Distance(rt.anchoredPosition, root.GetComponent<RectTransform>().anchoredPosition) < 20)
            {
                neighbourRoots.Add(root);
            }
        }

        // Spread poison
        int i = Random.Range(0, neighbourRoots.Count);
        Debug.Log("Poisoning " + neighbourRoots[i].name);
        neighbourRoots[i].evil = true;

        // Destroy self
        Destroy(gameObject);
        gameManager.scoreToAdd -= 20;
        FindObjectOfType<AudioManager>().Play("RootDeath");
        gameManager.AddScore();
    }

    /*public IEnumerator DestroyEvilRootsAfterDelay()
    {
        yield return new WaitForSeconds(0.1f);
        if (alpha == 1 && evil)
        {
            Destroy(gameObject);
            gameManager.scoreToAdd -= 10;
            gameManager.AddScore();
        }
        else if (evil)
        {
            StartCoroutine(DestroyEvilRootsAfterDelay());
        }
    }*/
}

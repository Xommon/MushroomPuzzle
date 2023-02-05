using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class GridSquare : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Image image;
    private GameManager gameManager;
    private Color startColour;
    public bool occupied;

    private void Start()
    {
        image = GetComponent<Image>();
        gameManager = FindObjectOfType<GameManager>();
        startColour = image.color;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (gameManager.heldItem != null && !occupied)
        {
            image.color = new Color(0, 1, 0, startColour.a);
        }
        else if (gameManager.heldItem != null && occupied)
        {
            image.color = new Color(1, 0, 0, startColour.a);
        }
        else
        {
            image.color = new Color(1f, 1f, 0, startColour.a);
        }
        gameManager.gridSquareHoveredOver = this;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        image.color = startColour;
        gameManager.gridSquareHoveredOver = null;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Roots" && collision.gameObject.GetComponent<Roots>().alpha == 1)
        {
            occupied = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Roots" && collision.gameObject.GetComponent<Roots>().alpha == 1)
        {
            occupied = false;
        }
    }
}

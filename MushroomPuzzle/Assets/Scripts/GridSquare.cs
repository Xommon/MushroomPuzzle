using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class GridSquare : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Image image;
    private GameManager gameManager;

    private void Start()
    {
        image = GetComponent<Image>();
        gameManager = FindObjectOfType<GameManager>();
        image.color = new Color(1, 1, 1, 0f);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        image.color = new Color(1, 1, 1, 0.1f);
        gameManager.gridSquareHoveredOver = gameObject;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        image.color = new Color(1, 1, 1, 0f);
        gameManager.gridSquareHoveredOver = null;
    }
}

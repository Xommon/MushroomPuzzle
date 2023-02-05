using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public int gridSize = 12;
    public int turn = 0;
    public GameObject gridSquarePrefab;
    public GameObject grid;
    public GameObject[] gridSquares;
    private GridLayoutGroup gridLayout;
    public GameObject heldItem;
    public GameObject gridSquareHoveredOver;
    public GameObject[] allMushrooms;
    public Transform mushroomsHolder;
    public bool clearToPlace;

    void Start()
    {
        gridLayout = grid.GetComponent<GridLayoutGroup>();
        gridLayout.constraintCount = gridSize;
    }

    void Update()
    {
        // Move mushroom if being held
        if (heldItem != null && gridSquareHoveredOver != null)
        {
            heldItem.transform.position = gridSquareHoveredOver.transform.position;
        }

        // Drop mushrooms
        if (Input.GetMouseButtonDown(0) && heldItem != null && clearToPlace && gridSquareHoveredOver != null)
        {
            heldItem.GetComponent<CanvasGroup>().alpha = 1;
            foreach (Roots root in heldItem.GetComponentsInChildren<Roots>())
            {
                root.alpha = 1;
            }
            heldItem = null;

            // Go to next turn
            SlimeMold[] allSlimeMolds = FindObjectsOfType<SlimeMold>();
            foreach (SlimeMold slimeMold in allSlimeMolds)
            {
                slimeMold.GrowMold();
            }
            turn++;
        }

        // Rotate mushrooms
        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(1)) && heldItem != null)
        {
            heldItem.transform.Rotate(new Vector3(0, 0, 90));
        }
    }

    public void CreateMushroomButton()
    {
        heldItem = Instantiate(allMushrooms[Random.Range(0, allMushrooms.Length)], mushroomsHolder);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public int gridSize = 12;
    public int turn = 0;
    public GameObject grid;
    public GameObject[] gridSquares;
    private GridLayoutGroup gridLayout;
    public GameObject heldItem;
    public GridSquare gridSquareHoveredOver;
    public GameObject[] allMushrooms;
    public Transform mushroomsHolder;
    public bool clearToPlace;
    public List<GameObject> mushroomOrder = new List<GameObject>();
    public Image[] mushroomBank;
    public int score;
    public int scoreToAdd;
    public Text scoreText;
    public GameObject poisonMushroomPrefab;

    void Start()
    {
        FindObjectOfType<AudioManager>().Play("Music");
        gridLayout = grid.GetComponent<GridLayoutGroup>();
        gridLayout.constraintCount = gridSize;
        GenerateMushroomOrder();
        StartGame();
    }

    void Update()
    {
        // Close game
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Quit();
        }

        // Update score
        scoreText.text = score.ToString();

        // Move mushroom if being held
        if (heldItem != null && gridSquareHoveredOver != null)
        {
            heldItem.transform.position = gridSquareHoveredOver.transform.position;
        }

        // Drop mushrooms
        if (Input.GetMouseButtonDown(0) && heldItem != null && gridSquareHoveredOver != null && !gridSquareHoveredOver.occupied)
        {
            // Increase score
            scoreToAdd += 50;
            StartCoroutine(AddScore());

            FindObjectOfType<AudioManager>().Play("PlantMushroom");
            heldItem.GetComponent<CanvasGroup>().alpha = 1;
            foreach (Roots root in heldItem.GetComponentsInChildren<Roots>())
            {
                root.alpha = 1;
            }
            heldItem = null;

            // Detonate poisonous roots
            foreach (Roots root in FindObjectsOfType<Roots>())
            {
                if (root.evil)
                {
                    root.SpreadPoison();
                }
            }

            // Go to next turn
            SlimeMold[] allSlimeMolds = FindObjectsOfType<SlimeMold>();
            foreach (SlimeMold slimeMold in allSlimeMolds)
            {
                slimeMold.GrowMold();
            }
            gridSquareHoveredOver.occupied = true;
            turn++;

            // Remove mushrooms and move to the next
            mushroomOrder.RemoveAt(0);
            if (mushroomOrder.Count > 0)
            {
                heldItem = Instantiate(mushroomOrder[0], mushroomsHolder);
            }

            // Show upcoming mushrooms
            for (int i = 0; i < 3; i++)
            {
                if (mushroomOrder.Count == i)
                {
                    mushroomBank[i].gameObject.SetActive(false);
                }
                else
                {
                    mushroomBank[i].sprite = mushroomOrder[i].GetComponent<Image>().sprite;
                }
            }
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

    public void StartGame()
    {
        heldItem = Instantiate(mushroomOrder[0], mushroomsHolder);

        // Show upcoming mushrooms
        for (int i = 0; i < 3; i++)
        {
            mushroomBank[i].sprite = mushroomOrder[i].GetComponent<Image>().sprite;
        }
    }

    public void GenerateMushroomOrder()
    {
        mushroomOrder.Clear();
        for (int i = 0; i < 15; i++)
        {
            mushroomOrder.Add(allMushrooms[Random.Range(0, allMushrooms.Length)]);
            if (i % 3 == 0)
            {
                mushroomOrder.Add(poisonMushroomPrefab);
            }
        }
    }

    public IEnumerator AddScore()
    {
        yield return new WaitForSeconds(0.01f);
        if (scoreToAdd > 0)
        {
            score++;
            scoreToAdd--;
        }
        else if (scoreToAdd < 0)
        {
            score--;
            scoreToAdd++;
        }

        if (scoreToAdd != 0)
        {
            StartCoroutine(AddScore());
        }
    }

    public void Restart()
    {
        SceneManager.LoadScene(0);
    }

    public void Quit()
    {
        Application.Quit();
    }
}

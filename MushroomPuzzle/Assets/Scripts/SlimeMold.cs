using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeMold : MonoBehaviour
{
    public GameObject rootsPrefab;
    public int[] rootAmounts;

    public void GrowMold()
    {
        rootAmounts[0]++;
        Instantiate(rootsPrefab, new Vector2(transform.position.x + (rootAmounts[0] * 50), transform.position.y), Quaternion.identity, transform);
        Instantiate(rootsPrefab, new Vector2(-(rootAmounts[0] * 50), transform.position.y), Quaternion.identity, transform);
    }
}

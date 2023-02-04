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
        var newRoot = Instantiate(rootsPrefab, transform.position, Quaternion.identity, transform);
        newRoot.GetComponent<RectTransform>().anchoredPosition = new Vector3(rootAmounts[0] * 25, 0, 0);
        newRoot.GetComponent<Roots>().alpha = 1;
    }
}

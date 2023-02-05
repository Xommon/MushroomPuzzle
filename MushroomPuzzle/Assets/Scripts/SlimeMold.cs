using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeMold : MonoBehaviour
{
    public GameObject rootsPrefab;
    public int rootAmounts = 1;

    public void GrowMold()
    {
        rootAmounts++;
        var newRoot = Instantiate(rootsPrefab, transform.position, transform.rotation, transform);
        newRoot.GetComponent<RectTransform>().anchoredPosition = new Vector3(rootAmounts * 40, 0, 0);
        newRoot.GetComponent<Roots>().alpha = 1;
    }
}

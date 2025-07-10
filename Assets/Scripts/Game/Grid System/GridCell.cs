using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using UnityEngine;

public class GridCell : MonoBehaviour
{
    internal Vector2Int coordinates;
    internal List<Element> currentElements;

    public Vector3 elementOffset;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

    internal void Init(int i, int j, CellData cellData)
    {
        currentElements = new List<Element>();
        coordinates = new Vector2Int(i, j);
        for (int x = 0; x < cellData.elements.Count; x++)
        {
            Element element = Instantiate(GridSystem.Instance.elementPrefab, transform);
            element.Init(cellData.elements[x]);

            currentElements.Add(element);
        }
    }
}

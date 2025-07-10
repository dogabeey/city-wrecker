using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using UnityEngine;

public class GridCell : MonoBehaviour
{
    internal Vector2Int coordinates;
    internal List<Element> currentElements;

    public float elementOffset;

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

            // Change element position based on the plane and offset
            switch (GridSystem.Instance.plane)
            {
                case Plane.XY:
                    element.transform.localPosition = new Vector3(0, 0, x * elementOffset);
                    break;
                case Plane.XZ:
                    element.transform.localPosition = new Vector3(0, x * elementOffset, 0);
                    break;
                case Plane.YZ:
                    element.transform.localPosition = new Vector3(x * elementOffset, 0, 0);
                    break;
            }

            currentElements.Add(element);
        }
    }
}

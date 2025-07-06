using Sirenix.OdinInspector;
using System;
using UnityEngine;

public class GridCell : MonoBehaviour
{
    public Vector2Int coordinates;
    public Element currentElement;

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
        coordinates = new Vector2Int(i, j);
    }
}

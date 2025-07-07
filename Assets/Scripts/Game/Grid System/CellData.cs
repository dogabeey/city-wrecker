using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CellData
{
    public Vector2Int coordinates;
    public GridType gridType;
    public List<ElementData> elements;
}

public enum GridType
{
    empty,
    floor
}

[System.Serializable]
public class ElementData
{
    public string elementName;
    public Color elementColor = Color.white;
}
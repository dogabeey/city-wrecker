using System.Collections.Generic;
using UnityEngine;

public class CellData
{
    public Vector2Int coordinates;
    public GridType gridType;
}

public enum GridType
{
    element, // Represents a cell that can hold an element. Color: White, Hotkey: E
    wall, // Represents a wall cell. Color: Brown, Hotkey: W
    feature1, // Placeholder for a feature. It could be e.g. a breakable ice, lock, etc. Color: Red, Hotkey: A
    feature2, // Placeholder for a feature. It could be e.g. a breakable ice, lock, etc. Color: Yellow, Hotkey: S
    feature3, // Placeholder for a feature. It could be e.g. a breakable ice, lock, etc. Color: Green, Hotkey: D
}

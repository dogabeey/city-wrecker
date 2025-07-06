using UnityEngine;
using Sirenix.OdinInspector;

[CreateAssetMenu(fileName = "LevelEditor", menuName = "Scriptable Objects/LevelEditor")]
public class LevelEditor : SerializedScriptableObject
{
    public int gridWidth = 10; // Width of the grid
    public int gridHeight = 10; // Height of the grid
    [TableMatrix(DrawElementMethod = nameof(DrawCell))]
    public CellData[,] gridCells; // 2D array of GridCell objects

    public CellData DrawCell(Rect rect, CellData value)
    {
        // Initialize

        // Draw

        // Events

        return value;
    }

}

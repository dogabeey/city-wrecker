using UnityEngine;
using UnityEditor;
using Sirenix.OdinInspector;
using System.Collections.Generic;
#if UNITY_EDITOR
using Sirenix.Utilities.Editor;
#endif
using Lionsfall;
using System.Linq;

[CreateAssetMenu(fileName = "LevelEditor", menuName = "Scriptable Objects/LevelEditor")]
public class LevelEditor : SerializedScriptableObject
{
    [ValueDropdown(nameof(GetAllElementNames))]
    public List<string> containerColorList;
    public int gridWidth = 10; // Width of the grid
    public int gridHeight = 10; // Height of the grid
    [TableMatrix(DrawElementMethod = nameof(DrawCell), SquareCells = true, ResizableColumns = false)]
    public CellData[,] gridCells; // 2D array of GridCell objects

    [Button]
    public void Initialize()
    {
        gridCells = new CellData[gridWidth, gridHeight];
        for (int i = 0; i < gridWidth; i++)
        {
            for (int j = 0; j < gridHeight; j++)
            {
                gridCells[i, j] = new CellData();
                gridCells[i, j].coordinates = new Vector2Int(i, j);
                gridCells[i, j].gridType = GridType.element;
            }
        }
    }
    [Button]
    public void Refresh()
    {
        for (int i = 0; i < gridWidth; i++)
        {
            for (int j = 0; j < gridHeight; j++)
            {
                gridCells[i, j].coordinates = new Vector2Int(i, j);
                gridCells[i, j].gridType = GridType.element;
            }
        }
    }

    public CellData DrawCell(Rect rect, CellData value)
    {
#if UNITY_EDITOR
        // INIT

        // DRAWING
        // Draw a color based on the grid type.
        switch (value.gridType)
        {
            case GridType.element:
                EditorGUI.DrawRect(rect, Color.white);
                break;
            case GridType.wall: // Brown
                EditorGUI.DrawRect(rect, new Color(0.545f, 0.271f, 0.075f));
                break;
            case GridType.feature1:
                EditorGUI.DrawRect(rect, Color.red); // Red for feature1
                break;
            case GridType.feature2:
                EditorGUI.DrawRect(rect, Color.yellow); // Yellow for feature2
                break;
            case GridType.feature3:
                EditorGUI.DrawRect(rect, Color.green); // Green for feature3
                break;
            default:
                break;
        }


        // EVENTS
        Event e = Event.current;
        if (e.type == EventType.KeyDown && rect.Contains(e.mousePosition))
        {
            // Add a switch case for each grid type
            switch (e.keyCode)
            {
                case KeyCode.E: // Element
                    value.gridType = GridType.element;
                    break;
                case KeyCode.W: // Wall
                    value.gridType = GridType.wall;
                    break;
                case KeyCode.A: // Feature1
                    value.gridType = GridType.feature1;
                    break;
                case KeyCode.S: // Feature2
                    value.gridType = GridType.feature2;
                    break;
                case KeyCode.D: // Feature3
                    value.gridType = GridType.feature3;
                    break;
            }
            e.Use();
        }
        GUI.changed = true;

#endif
        return value;
    }
    public static IEnumerable<string> GetAllElementNames()
    {
        GameManager worldManager = GameManager.Instance;
        if (worldManager == null)
        {
            Debug.LogWarning("WorldManager or currentWorld is not initialized.");
            return Enumerable.Empty<string>();
        }
        else
        {
            return GameManager.ElementData.Select(data => data.elementName)
                                            .Where(name => !string.IsNullOrEmpty(name))
                                            .Distinct()
                                            .OrderBy(name => name);
        }
    }
}

using UnityEngine;
using UnityEditor;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using Sirenix.Utilities.Editor;
using Dogabeey;

[CreateAssetMenu(fileName = "LevelEditor", menuName = "Scriptable Objects/LevelEditor")]
public class LevelEditor : SerializedScriptableObject
{
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
                gridCells[i, j].elements = new List<ElementData>();
            }
        }
    }

    public CellData DrawCell(Rect rect, CellData value)
    {


        // INIT
        // Initialize 3x3 nine squares to each cell.
        List<Rect> nineSquares = new List<Rect>(9);
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                Rect square = new Rect(rect.x + (i * rect.width / 3), rect.y + (j * rect.height / 3), rect.width / 3, rect.height / 3);
                nineSquares.Add(square);
            }
        }

        // DRAWING
        // Draw the nine squares in the cell
        if (value == null) Debug.LogError("Value is null");
        if (value.elements == null) Debug.LogError("Value elements are null");
        for (int i = 0; i < value.elements.Count; i++)
        {
            EditorGUI.DrawRect(nineSquares[i], value.elements[i].elementColor);
        }

        // EVENTS
        Event e = Event.current;
        // Add value dropdown menu that shows each ElementData color. Clicking them will add the color to the cell.
        if (e.type == EventType.MouseDown && e.button == 1 && rect.Contains(e.mousePosition))
        {
            GenericMenu menu = new GenericMenu();
            foreach (ElementData element in WorldManager.Instance.elementData)
            {
                menu.AddItem(new GUIContent(element.elementName.ToString()), false, () =>
                {
                    // Add the color to the cell
                    value.elements.Add(element);
                });
            }
            menu.ShowAsContext();
            e.Use();
        }

        GUI.changed = true;

        return value;
    }

}

using UnityEngine;
using UnityEditor;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using Sirenix.Utilities.Editor;
using Dogabeey;
using static UnityEngine.Rendering.DebugUI;

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
                gridCells[i, j].coordinates = new Vector2Int(i, j);
                gridCells[i, j].gridType = (gridCells[i, j].coordinates.x + gridCells[i, j].coordinates.y) % 2 == 1 ? GridType.empty : GridType.floor; // Set grid type based on coordinates
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
                gridCells[i, j].gridType = (gridCells[i, j].coordinates.x + gridCells[i, j].coordinates.y) % 2 == 1 ? GridType.empty : GridType.floor; // Set grid type based on coordinates
            }
        }
    }

    public CellData DrawCell(Rect rect, CellData value)
    {


        // INIT
        // Check if odd cell
        bool isOddCell = false;
        if ((value.coordinates.x + value.coordinates.y) % 2 == 1)  isOddCell = true;
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
        // Paint the rect black if It's an odd cell
        if(isOddCell)
            EditorGUI.DrawRect(rect, Color.black);
        // Draw the nine squares in the cell
        for (int i = 0; i < value.elements.Count; i++)
        {
            EditorGUI.DrawRect(nineSquares[i], value.elements[i].elementColor);
        }

        // EVENTS
        Event e = Event.current;
        if (!isOddCell)
        {

            if (e.type == EventType.MouseDown && e.button == 1 && rect.Contains(e.mousePosition))
            {
                // Add value dropdown menu that shows each ElementData color. Clicking them will add the color to the cell.
                GenericMenu menu = new GenericMenu();
                foreach (ElementData element in WorldManager.Instance.elementData)
                {
                    menu.AddItem(new GUIContent("Add " + element.elementName.ToString()), false, () =>
                    {
                        // Add the color to the cell
                        value.elements.Add(element);
                    });
                }
                menu.AddSeparator("");
                // Add values for the existing elements in the cell to remove them.
                for (int i = 0; i < value.elements.Count; i++)
                {
                    int temp = i;
                    ElementData element = value.elements[temp];
                    menu.AddItem(new GUIContent("(" + temp + ") Remove " + element.elementName.ToString()), false, () =>
                    {
                        // Remove the color from the cell
                        value.elements.RemoveAt(temp);
                    });
                }

                menu.ShowAsContext();
                e.Use();
            }
        }
        GUI.changed = true;

        return value;
    }

}

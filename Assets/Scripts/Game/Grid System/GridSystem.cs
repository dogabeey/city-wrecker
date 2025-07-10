using Dogabeey;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class GridSystem : MonoBehaviour
{
    public static GridSystem Instance;

    [Header("Generation Settings")]
    public GridCell cellPrefab;
    public Element elementPrefab;
    public Transform levelParent;
    public Plane plane;
    public Vector3 offset;
    public Vector3 spacing;

    internal GridCell[,] grid;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        GenerateGrid(LevelScene.Instance.LevelEditor.gridCells);
    }

    public abstract void GenerateGrid(CellData[,] cells);

    internal void RelocateGrid(CellData[,] cells)
    {

        //Make all cells so they are positioned in the center of the level, based on the plane.
        switch (plane)
        {
            case Plane.XY:
                levelParent.transform.localPosition = new Vector3(-spacing.x * (cells.GetLength(0) - 1) / 2, spacing.y * (cells.GetLength(1) - 1) / 2, 0);
                break;
            case Plane.XZ:
                levelParent.transform.localPosition = new Vector3(-spacing.x * (cells.GetLength(0) - 1) / 2, 0, spacing.y * (cells.GetLength(1) - 1) / 2);
                break;
            case Plane.YZ:
                levelParent.transform.localPosition = new Vector3(0, -spacing.y * (cells.GetLength(0) - 1) / 2, spacing.x * (cells.GetLength(1) - 1) / 2);
                break;
        }
    }
}

public enum Plane
{
    XY,
    XZ,
    YZ
}

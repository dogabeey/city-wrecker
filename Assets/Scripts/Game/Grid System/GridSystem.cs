using UnityEngine;

public class GridSystem : MonoBehaviour
{
    public static GridSystem Instance;

    [Header("Generation Settings")]
    public GridCell cellPrefab;
    public Transform levelParent;
    public Vector3 offset;
    public Vector3 spacing;

    internal GridCell[,] grid;

    private void Awake()
    {
        Instance = this;
    }
    public void GenerateGrid(CellData[,] cells)
    {
        for (int i = 0; i < cells.GetLength(0); i++)
        {
            for (int j = 0; j < cells.GetLength(1); j++)
            {
                if (cells[i, j] == null)
                {
                    cells[i, j] = new CellData();
                }
            }
        }

        grid = new GridCell[cells.GetLength(0), cells.GetLength(1)];

        for (int i = 0; i < grid.GetLength(0); i++)
        {
            for (int j = 0; j < grid.GetLength(1); j++)
            {
                GridCell cell = Instantiate(cellPrefab, levelParent);
                cell.transform.localPosition = new Vector3(i * spacing.x, -j * spacing.y, 0);
                cell.Init(i, j, cells[i, j]);
                grid[i, j] = cell;
            }
        }

        //Make all cells so they are positioned in the center of the level.
        levelParent.transform.localPosition = new Vector3(-spacing.x * (cells.GetLength(0) - 1) / 2, spacing.y * (cells.GetLength(1) - 1) / 2, 0);
        levelParent.transform.localPosition += offset;
    }
}

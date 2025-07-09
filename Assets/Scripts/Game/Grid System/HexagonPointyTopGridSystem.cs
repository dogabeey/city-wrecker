using UnityEngine;

public class HexagonPointyTopGridSystem : GridSystem
{
    // Generate grid for hexagon pointy top layout. For that, each odd row will be offset by half a cell width.
    public override void GenerateGrid(CellData[,] cells)
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
                float xOffset = (j % 2 == 0) ? 0 : spacing.x / 2;
                cell.transform.localPosition = new Vector3(i * spacing.x + xOffset, -j * spacing.y, 0);
                cell.Init(i, j, cells[i, j]);
                grid[i, j] = cell;
            }
        }
        //Make all cells so they are positioned in the center of the level.
        levelParent.transform.localPosition = new Vector3(-spacing.x * (cells.GetLength(0) - 1) / 2, spacing.y * (cells.GetLength(1) - 1) / 2, 0);
        levelParent.transform.localPosition += offset;
    }

    public GridCell GetNorthNeighborCell(GridCell currentCell)
    {
        // Return null if currentCell.coordinates.y - 2 cannot exist.
        if (currentCell.coordinates.y < 2) return null;

        return grid[currentCell.coordinates.x, currentCell.coordinates.y - 2];
    }

    public GridCell GetSouthNeighborCell(GridCell currentCell)
    {
        // Return null if currentCell.coordinates.y + 2 cannot exist.
        if (currentCell.coordinates.y + 2 >= grid.GetLength(1)) return null;

        return grid[currentCell.coordinates.x, currentCell.coordinates.y + 2];
    }

}

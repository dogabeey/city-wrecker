using UnityEngine;

public class FlatTopHexagonGridSystem : GridSystem
{
    // Generate grid for hexagon flat top layout. For that, each odd column will be offset by half a cell height.
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
                float yOffset = (i % 2 == 0) ? 0 : spacing.y / 2;
                cell.transform.localPosition = new Vector3(i * spacing.x, -j * spacing.y + yOffset, 0);
                cell.Init(i, j, cells[i, j]);
                grid[i, j] = cell;
            }
        }
        //Make all cells so they are positioned in the center of the level.
        levelParent.transform.localPosition = new Vector3(-spacing.x * (cells.GetLength(0) - 1) / 2, spacing.y * (cells.GetLength(1) - 1) / 2, 0);
        levelParent.transform.localPosition += offset;
    }

    public GridCell GetEastNeighborCell(GridCell currentCell)
    {
        // Return null if currentCell.coordinates.x + 1 cannot exist.
        if (currentCell.coordinates.x + 1 >= grid.GetLength(0)) return null;

        return grid[currentCell.coordinates.x + 1, currentCell.coordinates.y];
    }

    public GridCell GetWestNeighborCell(GridCell currentCell)
    {
        // Return null if currentCell.coordinates.x - 1 cannot exist.
        if (currentCell.coordinates.x < 1) return null;

        return grid[currentCell.coordinates.x - 1, currentCell.coordinates.y];
    }

    public GridCell GetNorthWestNeighborCell(GridCell currentCell)
    {
        // Return null if currentCell.coordinates.x - 1 cannot exist.
        if (currentCell.coordinates.x < 1) return null;
        // Return null if currentCell.coordinates.y - 1 cannot exist.
        if (currentCell.coordinates.y < 1) return null;

        return grid[currentCell.coordinates.x - 1, currentCell.coordinates.y - 1];
    }

    public GridCell GetNorthEastNeighborCell(GridCell currentCell)
    {
        // Return null if currentCell.coordinates.x + 1 cannot exist.
        if (currentCell.coordinates.x + 1 >= grid.GetLength(0)) return null;
        // Return null if currentCell.coordinates.y - 1 cannot exist.
        if (currentCell.coordinates.y < 1) return null;

        return grid[currentCell.coordinates.x + 1, currentCell.coordinates.y - 1];
    }

    public GridCell GetSouthWestNeighborCell(GridCell currentCell)
    {
        // Return null if currentCell.coordinates.x - 1 cannot exist.
        if (currentCell.coordinates.x < 1) return null;
        // Return null if currentCell.coordinates.y + 1 cannot exist.
        if (currentCell.coordinates.y + 1 >= grid.GetLength(1)) return null;

        return grid[currentCell.coordinates.x - 1, currentCell.coordinates.y + 1];
    }

    public GridCell GetSouthEastNeighborCell(GridCell currentCell)
    {
        // Return null if currentCell.coordinates.x + 1 cannot exist.
        if (currentCell.coordinates.x + 1 >= grid.GetLength(0)) return null;
        // Return null if currentCell.coordinates.y + 1 cannot exist.
        if (currentCell.coordinates.y + 1 >= grid.GetLength(1)) return null;

        return grid[currentCell.coordinates.x + 1, currentCell.coordinates.y + 1];
    }

}
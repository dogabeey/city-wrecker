using UnityEngine;

public class FlatTopHexagonGridSystem : GridSystem
{
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
                cell.Init(i, j, cells[i, j]);
                grid[i, j] = cell;

                // set the local position of the cell based on its coordinates and plane.
                switch (plane)
                {
                    case Plane.XY:
                        cell.transform.localPosition = new Vector3(i * spacing.x, -j * spacing.y, 0);
                        break;
                    case Plane.XZ:
                        cell.transform.localPosition = new Vector3(i * spacing.x, 0, -j * spacing.y);
                        break;
                    case Plane.YZ:
                        cell.transform.localPosition = new Vector3(0, i * spacing.y, -j * spacing.x);
                        break;
                }
            }
        }
        RelocateGrid(cells);
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
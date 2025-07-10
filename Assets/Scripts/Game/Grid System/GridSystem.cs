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

}

public enum Plane
{
    XY,
    XZ,
    YZ
}

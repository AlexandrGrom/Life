using System.Collections;
using UnityEngine;

public class AntGrid : MonoBehaviour
{
    [SerializeField] private int width;
    [SerializeField] private int height;
    [SerializeField] private float cellSize;
    [SerializeField] private float time = 1;

    [SerializeField] private AntCell cellPrefab;

    private Side side;
    private Vector2Int currentCellIndex;

    private AntCell[,] grid;

    private void Awake()
    {
        side = Side.Down;
        currentCellIndex.x = width / 2;
        currentCellIndex.y = height / 2;
        grid = new AntCell[width, height];
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                AntCell cell = Instantiate(cellPrefab);
                cell.transform.position = EvaluateCellSize(i, j);
                cell.transform.SetParent(transform);
                cell.gameObject.name = $"{i} {j}";
                grid[i, j] = cell;
            }
        }

        StartCoroutine(nameof(UpdareLife));

    }
    private Vector3 EvaluateCellSize(int x, int y) => new Vector3(x, y) * cellSize;

    IEnumerator UpdareLife()
    {
        while (true)
        {
            Step();
            yield return new WaitForSeconds(time);
        }
    }

    private void Step()
    {
        AntCell antCell = grid[currentCellIndex.x, currentCellIndex.y];
        if (antCell.isActive)
        {
            TurnLeft();
        }
        else
        {
            TurnRight();
        }
        antCell.StepOnCell();
    }

    private void TurnRight()
    {
        switch (side)
        {
            case Side.Up:
                currentCellIndex.x++;
                side = Side.Right;
                break;
            case Side.Right:
                currentCellIndex.y--;
                side = Side.Down;
                break;
            case Side.Down:
                currentCellIndex.x--;
                side = Side.Left;
                break;
            case Side.Left:
                currentCellIndex.y++;
                side = Side.Up;
                break;
        }
        
    }

    private void TurnLeft()
    {
        switch (side)
        {
            case Side.Up:
                currentCellIndex.x--;
                side = Side.Left;
                break;
            case Side.Right:
                currentCellIndex.y++;
                side = Side.Up;
                break;
            case Side.Down:
                currentCellIndex.x++;
                side = Side.Right;
                break;
            case Side.Left:
                currentCellIndex.y--;
                side = Side.Down;
                break;
            default:
                break;
        }
    }
}

enum Side
{
    Up,
    Right,
    Down,
    Left,
}

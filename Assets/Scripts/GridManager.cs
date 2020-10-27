using System.Collections;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    [SerializeField] private int width;
    [SerializeField] private int height;
    [SerializeField] private float cellSize;
    [SerializeField] private float time = 1;

    [SerializeField] private Cell cellPrefab;

    private Cell[,] grid;
    public static bool isGame;

    private void Awake()
    {
        grid = new Cell[width, height];
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                Cell cell = Instantiate(cellPrefab);
                cell.transform.position = EvaluateCellSize(i, j);
                cell.transform.SetParent(transform);
                cell.gameObject.name = $"{i} {j}";
                grid[i, j] = cell;
            }
        }

        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                grid[i, j].Initialize(EvaluateNeighbors(i, j));
            }
        }

        Camera.main.transform.position = new Vector3(width/2*cellSize,height/2*cellSize,-10);

        StartCoroutine(nameof(UpdareLife));

    }

    IEnumerator UpdareLife()
    {
        while (true)
        {
            yield return new WaitUntil(() => isGame);
            yield return new WaitForSeconds(time);
            foreach (var cell in grid)
            {
                cell.CalculateNeighbours();
            }
            foreach (var cell in grid)
            {
                cell.UpdaeMyLifeCycle();
            }
        }
    }

    private Vector3 EvaluateCellSize(int x, int y) => new Vector3(x, y) * cellSize;

    private Cell[] EvaluateNeighbors(int i,int j)
    {
        Cell[] cells = new Cell[8];


        for (int k = 0; k < 8; k++)
        {
            int iOff = EvaluateIOffset(k);
            int jOff = EvaluateJOffset(k);

            try
            { 
                cells[k] = grid[i+iOff ,j + jOff]; 
            }
            catch
            {
                cells[k] = null;
            }
        }

        return cells;
    }

    private int EvaluateIOffset(int k)
    {
        switch (k)
        {
            case 0: return -1;
            case 1: return 0;
            case 2: return 1; 
            case 3: return -1; 
            case 4: return 1; 
            case 5: return -1; 
            case 6: return 0; 
            case 7: return 1; 
        }
        return 0;
    }

    private int EvaluateJOffset(int k)
    {
        switch (k)
        {
            case 0: return 1;
            case 1: return 1;
            case 2: return 1;
            case 3: return 0;
            case 4: return 0;
            case 5: return -1;
            case 6: return -1;
            case 7: return -1;
        }
        return 0;
    }
}

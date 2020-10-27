using System.Text;
using UnityEngine;

public class Cell : MonoBehaviour
{
    [SerializeField] private SpriteRenderer sprite;

    private const int neighborsCount = 8;
    private Cell[] neighbors = new Cell[neighborsCount];

    [SerializeField] private int alive = 0;
    [SerializeField] private bool debug;
    public int Alive => alive;

    private void Awake()
    {
        alive = Random.Range(0, 2);
    }

    public void Initialize(Cell[] cells)
    {
        for (int i = 0; i < neighborsCount; i++)
        {
            neighbors[i] = cells[i];
        }
    }


    private int aliveNeighborsCount = 0;
    public void CalculateNeighbours()
    {
        aliveNeighborsCount = 0;
        for (int i = 0; i < neighbors.Length; i++)
        {
            if (neighbors[i] != null)
            {
                aliveNeighborsCount += neighbors[i].alive;
            }
        }
    }

    private void OnMouseDown()
    {
        if (alive == 1)
        {
            alive = 0;
            sprite.color = Color.white;
        }
        else
        {
            alive = 1;
            sprite.color = Color.black;
        }
    }
    public void UpdaeMyLifeCycle()
    {
        bool isAlive = alive != 0;


        if (isAlive)
        {
            if (aliveNeighborsCount == 3 || aliveNeighborsCount == 2)
            {
                sprite.color = Color.black;
                alive = 1;
            }
            else
            {
                sprite.color = Color.white;
                alive = 0;

            }
        }
        else
        {
            if (aliveNeighborsCount == 3)
            {
                sprite.color = Color.black;
                alive = 1;

            }
            else
            {
                sprite.color = Color.white;
                alive = 0;
            }
        }
    }
}

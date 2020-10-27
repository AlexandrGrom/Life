using UnityEngine;

public class AntCell : MonoBehaviour
{
    [SerializeField] private SpriteRenderer sprite;
    public bool isActive;

    public void StepOnCell()
    {
        isActive = !isActive;
        if (isActive)
        {
            sprite.color = Color.black;
        }
        else
        {
            sprite.color = Color.white;
        }
    }
}

using UnityEngine;
using UnityEngine.UI;

public class Pause : MonoBehaviour
{
    Button button;
    private void Awake()
    {
        button = GetComponent<Button>();
        //button.onClick.AddListener(GridManager.Game);
        button.onClick.AddListener(()=>GridManager.isGame = !GridManager.isGame);
    }
}

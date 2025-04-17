using UnityEngine;

public class StartGame : MonoBehaviour
{
    [SerializeField] public GameObject startScreen;
    private void Start()
    {
        Time.timeScale = 0;
    }
    public void startGame()
    {
        startScreen.SetActive(false);
        Time.timeScale = 1;
    }
}

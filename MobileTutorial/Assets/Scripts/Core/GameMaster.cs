using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameMaster : MonoBehaviour
{
    public GameObject restartPanel;

    [SerializeField]private Text timerDisplay;

    private bool hasLost;

    [SerializeField] private float timer;
    private void Update()
    {
        if (SceneManager.GetActiveScene().buildIndex != 0 && SceneManager.GetActiveScene().name != "FinalScene") { 
            if (hasLost == false)
            {
                timerDisplay.text = timer.ToString("F0");
                if (timer <= 0)
                {
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                }
                else
                {
                    timer -= Time.deltaTime;
                }
            }

        }
    }
    public bool getHasLost()
    {
        return hasLost;
    }
    public void GameOver()
    {
        hasLost = true;
        Invoke("Delay", 1.5f);
    }

    void Delay()
    {
        restartPanel.SetActive(true);
    }
    public void GoToGameScene()
    {
        SceneManager.LoadScene("Level1");
    }
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class RestartPanel : MonoBehaviour
{
    [SerializeField] Text gameOverText;
    [SerializeField] Text bestScoreText;

    // Start is called before the first frame update
    
    public void ShowScore(int score)
    {
        int bestScore = PlayerPrefs.GetInt("BestScore", 0);
        if (score > bestScore)
        {
            PlayerPrefs.SetInt("BestScore", score);
            bestScore = score;
        }

        gameOverText.text = "Your score is " + score;
        bestScoreText.text = "Your best score is " + bestScore;
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}

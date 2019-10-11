using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class YourScore : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI highScoreText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        string score = PlayerPrefs.GetInt("yourscore").ToString();
        scoreText.text = score;

        GetHighScore();

    }

    public void GetHighScore()
    {

        if (Game.gameMode == "beginner")
        {
            highScoreText.text = "Beginner: " + PlayerPrefs.GetInt("beginnerHighscore").ToString();

        }
        if (Game.gameMode == "intermediate")
        {
            highScoreText.text = "Intermediate: " + PlayerPrefs.GetInt("intermediateHighscore").ToString();
        }
        if (Game.gameMode == "challenging")
        {
            highScoreText.text = "Challenging: " + PlayerPrefs.GetInt("challengingHighscore").ToString();
        }
        if (Game.gameMode == "custom")
        {
            highScoreText.text = "Custom: " + PlayerPrefs.GetInt("customHighscore").ToString();
        }
    }
}

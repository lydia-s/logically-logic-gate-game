using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HighScore : MonoBehaviour
{
    public TextMeshProUGUI beginnerText;
    public TextMeshProUGUI intermediateText;
    public TextMeshProUGUI challengingText;
    public TextMeshProUGUI customText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        beginnerText.text = PlayerPrefs.GetInt("beginnerHighscore").ToString();
        intermediateText.text = PlayerPrefs.GetInt("intermediateHighscore").ToString();
        challengingText.text =  PlayerPrefs.GetInt("challengingHighscore").ToString();
        customText.text = PlayerPrefs.GetInt("customHighscore").ToString();
    }
    
}

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public TextMeshProUGUI timerText;
    private float startTime;
    public string currentTime = "";
    // Start is called before the first frame update
    void Start()
    {
        timerText = GetComponent<TextMeshProUGUI>();
        startTime = Time.time;

    }

    // Update is called once per frame
    void Update()
    {
        float t = Time.time - startTime;
        //string minutes = ((int)t / 60).ToString();
        //string seconds = (t % 60).ToString("f2");
        currentTime = t.ToString("f0");//minutes + " : " + seconds;
        timerText.text = currentTime;
        

    }
}

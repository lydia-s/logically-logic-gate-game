using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimeTaken : MonoBehaviour
{
    public TextMeshProUGUI timeText;
    // Start is called before the first frame update
    void Start()
    {
        timeText = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        string time = PlayerPrefs.GetString("time");
        
        timeText.text = time;
    }
}

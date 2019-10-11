using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class TutorialPlayer : MonoBehaviour
{
    private VideoPlayer videoPlayer;
    public GameObject uiObject;
    public GameObject menuObject;
    public GameObject tutorialObject;
    // Start is called before the first frame update
    private void Awake()
    {
        videoPlayer = GetComponent<VideoPlayer>();
        
    }
    void Start()
    {

        

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P)) {
            PlayPause();
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            menuObject.SetActive(true);
            tutorialObject.SetActive(false);
            uiObject.SetActive(false);
        }
    }

    public void PlayPause()
    {
        if (videoPlayer.isPlaying)
        {
            videoPlayer.Pause();
            uiObject.SetActive(true);

        }
        else
        {
            videoPlayer.Play();
            uiObject.SetActive(false);
        }


    }
}

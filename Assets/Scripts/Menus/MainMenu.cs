using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public Slider gridWidth;
    public Slider gridHeight;
    public Slider fallSpeed;
    public Text gridWidthText;
    public Text gridHeightText;
    public Text fallSpeedText;
    public void PlayBeginner()
    {
        Game.gridHeight = 6;
        Game.gridWidth = 5;
        Game.fallSpeed = 1.5f;
        Game.grid = new Transform[Game.gridWidth, Game.gridHeight];
        Game.gameMode = "beginner";
        SceneManager.LoadScene("Game");
    }
    public void PlayIntermediate()
    {
        Game.gridHeight = 5;
        Game.gridWidth = 4;
        Game.fallSpeed = 1.2f;
        Game.grid = new Transform[Game.gridWidth, Game.gridHeight];
        Game.gameMode = "intermediate";
        SceneManager.LoadScene("Game");
    }
    public void PlayChallenging()
    {
        Game.gridHeight = 5;
        Game.gridWidth = 3;
        Game.fallSpeed = 1f;
        Game.grid = new Transform[Game.gridWidth, Game.gridHeight];
        Game.gameMode = "challenging";
        SceneManager.LoadScene("Game");
    }
    public void PlayCustom() {
        Game.gridHeight = (int)gridHeight.value;
        Game.gridWidth = (int)gridWidth.value;
        Game.fallSpeed = fallSpeed.value;
        Game.grid = new Transform[Game.gridWidth, Game.gridHeight];
        Game.gameMode = "custom";
        SceneManager.LoadScene("Game");

    }

    public void Update()
    {
        gridWidthText.text = gridWidth.value.ToString();
        gridHeightText.text = gridHeight.value.ToString();
        fallSpeedText.text = fallSpeed.value.ToString("f2");

    }
    public void QuitGame()
    {
        Debug.Log("Quit");
        Application.Quit();
    }
}
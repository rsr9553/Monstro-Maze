﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("Maze");
    }

    public void SettingsMenu()
    {
        SceneManager.LoadScene("Setting");
    }

    public void BackToMenuButton()
    {
        SceneManager.LoadScene("MenuScene");
    }
}



using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    private void Start()
    {
        Time.timeScale = 1f;
        Cursor.visible = true;
    }

    public void OnPlay()
    {
        SceneManager.LoadScene(1);
    }
    
    public void OnExit()
    {
        Application.Quit();
    }

    public void OnCredits()
    {
        SceneManager.LoadScene(2);
    }
    
    public void OnMenu()
    {
        SceneManager.LoadScene(0);
    }
}

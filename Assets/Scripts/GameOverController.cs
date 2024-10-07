using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverController : MonoBehaviour
{
    public void OnRetry()
    {
        SceneManager.LoadScene(1);
    }
    
    public void OnMainMenu()
    {
        SceneManager.LoadScene(0);
    }
}

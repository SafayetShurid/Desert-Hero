using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public Button[] playerButtons;
    public GameObject gameOverPaenl;

    public static MenuManager instance;

    private void Awake()
    {
        instance = this;
    }

    public void ShowGameOverPanel()
    {
        foreach(var v in playerButtons)
        {
            v.gameObject.SetActive(false);
        }

        gameOverPaenl.SetActive(true);
    }

    public void LoadScene(int i)
    {
        SceneManager.LoadScene(i);
    }

    public void Exit()
    {
        Application.Quit();
    }
}

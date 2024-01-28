using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuUI;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PressPauseButton();
        }
    }

    public void PressPauseButton()
    {
        if (Time.timeScale == 1)
        {
            Pause();
        }
        else
        {
            Resume();
        }
    }

    public void Resume(){
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1;
    }

    public void Pause(){
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0;
    }

    public void Quit(){
        Application.Quit();
    }
}

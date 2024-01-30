using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public List<PlayerController> controllerList = new List<PlayerController>();
    private bool isGameHasWinner = false;

    public float timer = 180;
    public TMP_Text timerText;

    private static GameManager _instance;
    public static GameManager Instance { get => _instance; }

    public AudioClip startClip;
    public AudioClip halfTimeClip;
    public AudioClip endClip;
    public AudioSource audioSource;
    private bool halfTimePlayed = false;
    private bool endingPlayed = false;

    private void Awake()
    {
        if(_instance == null)
        {
            _instance = this;
        }
        else if(_instance != this)
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        PlayClip(startClip);
        // find all objects with player controller script
        PlayerController[] controllers = GameObject.FindObjectsOfType<PlayerController>();
        // add them to the list
        for (int i = 0; i < controllers.Length; i++)
        {
            controllerList.Add(controllers[i]);
        }
    }

    private void Update()
    {
        //if(!halfTimePlayed && timer <= 90f)
        //{
        //    PlayClip(halfTimeClip);
        //    halfTimePlayed=true;
        //}

        //if(!endingPlayed && timer <= 10f)
        //{
        //    PlayClip(endClip);
        //    endingPlayed=true;
        //}


        if(CheckGameEnd()){

            foreach (PlayerController controller in controllerList)
            {
                if(controller.GetComponent<BodyPartController>().isLongLegs){
                    isGameHasWinner = true; 

                    if(controller.PlayerID == 1)
                    {
                        SceneManager.LoadScene("P1WinScene");
                    }
                    else if(controller.PlayerID == 2)
                    {
                        SceneManager.LoadScene("P2WinScene");
                    }

                    break;
                }
            }
            if(!isGameHasWinner){
                SceneManager.LoadScene("Tie");
            }
        }
    }


    private bool CheckGameEnd(){

        timer -= Time.deltaTime;

        timerText.text = ((int)timer).ToString("D2");

        if(timer <= 0){
            return true;
        }
        return false;
    }
    void PlayClip(AudioClip clip)
    {
        audioSource.clip = clip;
        audioSource.Play();
    }


    public void StartMusic()
    {
        audioSource.Play();
    }

    public void PauseMusic()
    {
        audioSource.Pause();
    }


    public void Restart()
    {
        SceneManager.LoadScene("Main Scene");
        Time.timeScale = 1f;
    }

    public void Quit()
    {
        Application.Quit();
    }

    private void OnDisable()
    {
        Time.timeScale = 1f;

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LeveLoader : MonoBehaviour
{
    public bool isAuto;

    public float autoTime;

    public string sceneName;

    private void Start()
    {
        if(isAuto)
        {
            StartCoroutine(AutoLoadSceneCoroutine());
        }
    }

    private IEnumerator AutoLoadSceneCoroutine()
    {
        yield return new WaitForSeconds(autoTime);

        LoadScene();
    }

    private void LoadScene()
    {
        SceneManager.LoadScene(sceneName);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinnerUI : MonoBehaviour
{
    private List<GameObject> winnerIconList = new();

    private void Awake()
    {
        foreach(Transform t in transform)
        {
            winnerIconList.Add(t.gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(WinnerUICoroutine());
    }


    private void OnDisable()
    {
        StopAllCoroutines();
    }


    IEnumerator WinnerUICoroutine()
    {
        while(!GameManager.Instance.IsGameEnd)
        {
            foreach(GameObject g in winnerIconList)
            {
                g.SetActive(false);
            }

            winnerIconList[GameManager.Instance.CheckWinnerID()].SetActive(true);

            yield return new WaitForSeconds(0.3f);
        }
    }
}

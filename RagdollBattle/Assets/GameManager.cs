using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR;

public class GameManager : MonoBehaviour
{
    public List<PlayerController> controllerList = new List<PlayerController>();
    private bool isGameHasWinner = false;
    public float timer = 180;

    private static GameManager _instance;
    public static GameManager Instance { get => _instance; }

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
        if(CheckGameEnd()){
            foreach (PlayerController controller in controllerList)
            {
                if(controller.GetComponent<BodyPartController>().isLongLegs){
                    Debug.Log("Winner is£º" + controller.PlayerID);
                    isGameHasWinner = true; 
                    break;
                }
            }
            if(!isGameHasWinner){
                Debug.Log("Draw!");
            }
        }
    }

    private bool CheckGameEnd(){
        timer -= Time.deltaTime;
        if(timer <= 0){
            return true;
        }
        return false;
    }
}

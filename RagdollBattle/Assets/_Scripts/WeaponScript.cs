using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponScript : MonoBehaviour
{
    public GameObject bulletPrefab;
    public bool isPickedUp = false;
    public int playerID;

    private void Update()
    {
        // 1. if weapon is equipped, then attach hand to weapon position
        // 2. if shoot condition, then bullet will shoot though weapon's Vector2 Up. And Weapon will aline to that direction
            // 2.2. Meanwhile, weapon will rotate toward mouse/axis direction

        if(isPickedUp)
        {
            AdjustRotation();
        }
    }


    private void AdjustRotation()
    {
        
        if(playerID == 1)
        {
            Vector3 cursorPos = new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y, 0);

            Vector3 difference = cursorPos - transform.position;

            transform.up = difference.normalized;
        }
        else if(playerID == 2)
        {

        }
        else
        {

        }
    }
}

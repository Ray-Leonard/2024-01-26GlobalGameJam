using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponScript : BasePickup
{
    public GameObject bulletPrefab;

    private void Update()
    {
        // 1. if weapon is equipped, then attach hand to weapon position
        // 2. if shoot condition, then bullet will shoot though weapon's Vector2 Up. And Weapon will aline to that direction
            // 2.2. Meanwhile, weapon will rotate toward mouse/axis direction

        if(IsPickedUp)
        {
            AdjustRotation();
        }
    }


    private void AdjustRotation()
    {
        
        if(PlayerID == 1)
        {
            Vector3 cursorPos = new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y, 0);

            Vector3 difference = cursorPos - transform.position;

            transform.up = difference.normalized;
        }
        else if(PlayerID == 2)
        {
            Vector2 input = GameInput.Instance.GetRightJoystickInput();

            if(input != Vector2.zero)
            {
                transform.up = input.normalized;
            }
        }
    }
}

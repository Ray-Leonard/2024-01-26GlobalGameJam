using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponScript : BasePickup
{

    private void OnDestroy()
    {
        if (PlayerID == 1)
        {
            GameInput.Instance.OnPlayer1Shoot -= Shoot;
        }
        else if (PlayerID == 2)
        {
            GameInput.Instance.OnPlayer2Shoot -= Shoot;
        }
    }


    public override void OnPickupInitialization(int playerID, Transform parent)
    {
        base.OnPickupInitialization(playerID, parent);

        if (PlayerID == 1)
        {
            GameInput.Instance.OnPlayer1Shoot += Shoot;
        }
        else if (PlayerID == 2)
        {
            GameInput.Instance.OnPlayer2Shoot += Shoot;
        }
    }

    public override void OnPickupExhausted()
    {
        base.OnPickupExhausted();

        // find the bodypart controller and restore
        GetComponentInParent<BodyPartController>().SetEnableArmHand(true);
    }


    private void Update()
    {
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


    protected virtual void Shoot(object sender, EventArgs e)
    {

    }
}

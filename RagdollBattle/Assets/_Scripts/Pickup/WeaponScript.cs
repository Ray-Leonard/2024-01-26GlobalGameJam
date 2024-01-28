using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponScript : BasePickup
{

    [SerializeField] protected int maxAmmo;
    protected int currAmmo;

    private void Awake()
    {
        currAmmo = maxAmmo;
    }


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



    protected virtual void Shoot(object sender, EventArgs e)
    {

    }
}

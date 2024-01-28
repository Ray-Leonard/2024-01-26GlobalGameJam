using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasePickup : MonoBehaviour, IPickupable
{
    private bool isPickedUp = false;
    private int playerID;

    public bool IsPickedUp { get => isPickedUp; set => isPickedUp = value; }
    public int PlayerID { get => playerID; set => playerID = value; }



    public virtual void OnPickupInitialization(int playerID, Transform parent)
    {
        isPickedUp = true;

        this.playerID = playerID;

        // delete weapon's collider and rigidbody
        Destroy(GetComponent<CircleCollider2D>());

        // turn off transparent circle
        transform.GetChild(0).gameObject.SetActive(false);

        // set parent
        transform.parent = parent;
        transform.localPosition = Vector3.zero;

        // listen to respawn event
        DeathZone.OnPlayerRespawn += DropPickup;

        // listen to pickup long leg event
        GetLegOn.OnPickupLongLeg += DropPickup;
    }

    private void OnDisable()
    {
        DeathZone.OnPlayerRespawn -= DropPickup;
        GetLegOn.OnPickupLongLeg -= DropPickup;
    }

    private void DropPickup(int playerID)
    {
        if(playerID == PlayerID)
        {
            OnPickupExhausted();
        }
    }

    public virtual void OnPickupExhausted()
    {
        Destroy(gameObject);
    }
}

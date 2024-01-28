using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasePickup : MonoBehaviour, IPickupable
{
    private bool isPickedUp = false;
    private int playerID;

    public bool IsPickedUp { get => isPickedUp; set => isPickedUp = value; }
    public int PlayerID { get => playerID; set => playerID = value; }



    public void OnPickupInitialization(int playerID, Transform parent)
    {
        isPickedUp = true;

        this.playerID = playerID;

        // delete weapon's collider and rigidbody
        Destroy(GetComponent<Collider2D>());
        Destroy(GetComponent<Rigidbody2D>());

        // turn off transparent circle
        transform.GetChild(0).gameObject.SetActive(false);

        // set parent
        transform.parent = parent;
        transform.localPosition = Vector3.zero;
    }

}

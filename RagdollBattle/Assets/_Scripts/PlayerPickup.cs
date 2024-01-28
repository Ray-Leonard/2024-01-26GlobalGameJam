using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPickup : MonoBehaviour
{
    private PlayerController playerController;
    private BodyPartController bodyPartController;

    [SerializeField] Transform weaponSpot;


    private void Awake()
    {
        playerController = GetComponentInParent<PlayerController>();
        bodyPartController = GetComponentInParent<BodyPartController>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // pick up weapon.
        if(collision.gameObject.tag == "Weapon")
        {
            // assign weapon player ID and isPickUp bool
            collision.gameObject.GetComponent<BasePickup>().OnPickupInitialization(playerController.PlayerID, weaponSpot);

            // disable grab, put arm to weapon spot.
            bodyPartController.SetEnableArmHand(false);
        }
    }
}

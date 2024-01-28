using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPickup : MonoBehaviour
{
    private PlayerController playerController;

    [SerializeField] Transform weaponSpot;


    private void Awake()
    {
        playerController = GetComponentInParent<PlayerController>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // pick up weapon.
        if(collision.gameObject.tag == "Weapon")
        {
            // assign weapon player ID and isPickUp bool
            collision.gameObject.GetComponent<BasePickup>().OnPickupInitialization(playerController.PlayerID, weaponSpot);
        }
    }
}

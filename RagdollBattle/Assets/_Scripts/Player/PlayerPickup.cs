using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPickup : MonoBehaviour
{
    private PlayerController playerController;
    private BodyPartController bodyPartController;
    public AudioSource audioSource;

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
            WeaponScript pickup = collision.gameObject.GetComponent<WeaponScript>();

            // cant pick up if has something
            if(weaponSpot.childCount > 0 || bodyPartController.isLongLegs || pickup.IsPickedUp)
            {
                return;
            }else{
                audioSource.Play();
            }

            // assign weapon player ID and isPickUp bool
            pickup.OnPickupInitialization(playerController.PlayerID, weaponSpot);

            // disable grab, put arm to weapon spot.
            bodyPartController.SetEnableArmHand(false);
        }
    }
}

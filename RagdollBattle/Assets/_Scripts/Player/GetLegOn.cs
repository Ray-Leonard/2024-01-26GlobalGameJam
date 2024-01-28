using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetLegOn : MonoBehaviour
{
    private PlayerController playerController;
    private BodyPartController bodyController;
    [SerializeField] private AudioSource audioSource;

    public static event Action<int> OnPickupLongLeg;

    private void Awake()
    {
        playerController = GetComponentInParent<PlayerController>();
        bodyController = GetComponentInParent<BodyPartController>();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
       if (collision != null)
       {
            if (collision.gameObject.tag == "LegPickUp" && !bodyController.isLongLegs)
            {
                audioSource.Play();
                playerController.GetComponent<BodyPartController>().ChangeLegs();

                // drop weapon (if there's any)
                OnPickupLongLeg?.Invoke(playerController.PlayerID);

                Destroy(collision.transform.parent.gameObject);
            }
        }
    }
}

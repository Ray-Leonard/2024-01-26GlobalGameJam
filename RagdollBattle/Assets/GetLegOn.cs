using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetLegOn : MonoBehaviour
{
    [SerializeField] private GameObject PlayerWithLegs;
    private PlayerController controller;

    private void Awake()
    {
        controller = GetComponentInParent<PlayerController>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
       if (collision != null)
       {
            if (collision.gameObject.tag == "LegPickUp")
            {
                GameObject playerWithLegs = Instantiate(PlayerWithLegs, transform.position, Quaternion.identity);
                playerWithLegs.GetComponent<PlayerController>().PlayerID = controller.PlayerID;
                Destroy(controller.gameObject);
                Destroy(collision.gameObject);
            }
        }
    }
}

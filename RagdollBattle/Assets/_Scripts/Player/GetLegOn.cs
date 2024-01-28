using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetLegOn : MonoBehaviour
{
    [SerializeField] private GameObject PlayerWithLegs;
    private PlayerController controller;
    private bool isCollided = false;

    private void Awake()
    {
        controller = GetComponentInParent<PlayerController>();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
       if (collision != null)
       {
            if (collision.gameObject.tag == "LegPickUp" && !isCollided)
            {
                isCollided = true;
                if (transform.position.y < collision.transform.position.y)
                {
                    controller.GetComponent<BodyPartController>().ChangeLegs();
                }
                else
                {
                    controller.GetComponent<BodyPartController>().ChangeLegs();
                }
                Destroy(collision.transform.parent.gameObject);
            }
        }
    }
}

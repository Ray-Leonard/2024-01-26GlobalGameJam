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
                GameObject playerWithLegs;
                if (transform.position.y < collision.transform.position.y)
                {
                    playerWithLegs = Instantiate(PlayerWithLegs, collision.transform.position, Quaternion.identity);
                }
                else
                {
                    playerWithLegs = Instantiate(PlayerWithLegs, transform.position, Quaternion.identity);
                }

                PlayerController longLegPlayerController = playerWithLegs.GetComponent<PlayerController>();
                longLegPlayerController.PlayerID = controller.PlayerID;
                
                // reassign camera target group
                CinemachineTargetGroup targetGroup = FindFirstObjectByType<CinemachineTargetGroup>();
                targetGroup.RemoveMember(controller.playerPos);
                targetGroup.AddMember(longLegPlayerController.playerPos, 0.5f, 0);

                Destroy(controller.gameObject);
                Destroy(collision.transform.parent.gameObject);
            }
        }
    }
}

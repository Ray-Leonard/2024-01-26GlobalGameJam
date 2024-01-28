using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyPartController : MonoBehaviour
{
    [SerializeField] private GameObject leftLegShort;
    [SerializeField] private GameObject rightLegShort;
    [SerializeField] private GameObject leftLegLong;
    [SerializeField] private GameObject rightLegLong;

    [SerializeField] private GameObject playerPosShort;
    [SerializeField] private GameObject playerPosLong;

    [SerializeField] private Grab leftHandGrab;
    [SerializeField] private Grab rightHandGrab;
    [SerializeField] private Arms leftUpperArm;
    [SerializeField] private Arms rightUpperArm;
    [SerializeField] private Arms leftLowerArm;
    [SerializeField] private Arms rightLowerArm;

    public bool isLongLegs = false;
    public PlayerController playerController;

    private void Start()
    {
        playerController = GetComponent<PlayerController>();
    }


    [ContextMenu("ChangeLegs")]
    public void ChangeLegs(){
        if(isLongLegs){
            isLongLegs = false;
            leftLegLong.GetComponent<SpriteRenderer>().enabled = false;
            rightLegLong.GetComponent<SpriteRenderer>().enabled = false;
            leftLegShort.GetComponent<SpriteRenderer>().enabled = true;
            rightLegShort.GetComponent<SpriteRenderer>().enabled = true;
            leftLegLong.GetComponent<BoxCollider2D>().enabled = false;
            rightLegLong.GetComponent<BoxCollider2D>().enabled = false;
            leftLegShort.GetComponent<BoxCollider2D>().enabled = true;
            rightLegShort.GetComponent<BoxCollider2D>().enabled = true;
            playerController.ChangeMovementSpeed(isLongLegs);
            playerController.playerPos.transform.position = playerPosShort.transform.position;
        }
        else{
            isLongLegs = true;
            leftLegLong.GetComponent<SpriteRenderer>().enabled = true;
            rightLegLong.GetComponent<SpriteRenderer>().enabled = true;
            leftLegShort.GetComponent<SpriteRenderer>().enabled = false;
            rightLegShort.GetComponent<SpriteRenderer>().enabled = false;
            leftLegLong.GetComponent<BoxCollider2D>().enabled = true;
            rightLegLong.GetComponent<BoxCollider2D>().enabled = true;
            leftLegShort.GetComponent<BoxCollider2D>().enabled = false;
            rightLegShort.GetComponent<BoxCollider2D>().enabled = false;
            playerController.ChangeMovementSpeed(isLongLegs);
            playerController.playerPos.transform.position = playerPosLong.transform.position;
        }
    }


    public void SetEnableArmHand(bool isEnabled)
    {
        // if hand is grabbing anything, release it. 
        leftHandGrab.Unhold();
        leftHandGrab.enabled = isEnabled;
        rightHandGrab.Unhold();
        rightHandGrab.enabled = isEnabled;

        leftUpperArm.enabled = isEnabled;
        rightUpperArm.enabled = isEnabled;
        leftLowerArm.enabled = isEnabled;
        rightLowerArm.enabled = isEnabled;
    }
}

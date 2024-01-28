using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyPartController : MonoBehaviour
{
    [SerializeField] private GameObject leftLeg;
    [SerializeField] private GameObject rightLeg;
    [SerializeField] private float legLength = 2.571169f;
    private bool isLongLegs = false;
    private Vector3 legScale;


    private void Start()
    {
        legScale = leftLeg.transform.localScale;
    }
    [ContextMenu("ChangeLegs")]
    private void ChangeLegs(){
        if(isLongLegs){
            isLongLegs = false;
            leftLeg.transform.localScale = new Vector3(legScale.x, legScale.y, legScale.z);
            rightLeg.transform.localScale = new Vector3(legScale.x, legScale.y, legScale.z);
        }
        else{
            isLongLegs = true;
            leftLeg.transform.localScale = new Vector3(legScale.x, legLength, legScale.z);
            rightLeg.transform.localScale = new Vector3(legScale.x, legLength, legScale.z);
        }
    }
}

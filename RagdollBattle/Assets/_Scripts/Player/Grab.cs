using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grab : MonoBehaviour
{
    private PlayerController player;
    [SerializeField] private LeftRight handDir;

    [SerializeField] private LayerMask ignoreLayer;
    [SerializeField] private AudioClip grabSound;
    [SerializeField] private AudioClip hitSound;
    [SerializeField] private AudioSource audioSource;
    private bool isCanPlayGrabSound = true;
    private void Awake()
    {
        player = GetComponentInParent<PlayerController>();
    }

    private bool hold;

    void Update()
    {
        if(GameInput.Instance.GetHandButtonHold(handDir, player.PlayerID))
        {
            hold = true;
        }
        else
        {
            Unhold();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(hold)
        {
            if ((1 << collision.gameObject.layer & ignoreLayer) != 0)
            {
                audioSource.clip = hitSound;
                audioSource.Play();
                Debug.Log("Cannot grab player");
                return;
            }else{
                if(isCanPlayGrabSound){
                    audioSource.clip = grabSound;
                    audioSource.Play();
                    isCanPlayGrabSound = false;
                }
            }


            Rigidbody2D rb = collision.transform.GetComponent<Rigidbody2D>();
            if(rb != null)
            {
                FixedJoint2D fj = transform.gameObject.AddComponent(typeof(FixedJoint2D)) as FixedJoint2D;
                fj.connectedBody = rb;
            }
            else
            {
                FixedJoint2D fj = transform.gameObject.AddComponent(typeof(FixedJoint2D)) as FixedJoint2D;
            }
        }
    }

    public void Unhold()
    {
        hold = false;
        isCanPlayGrabSound = true;
        Destroy(GetComponent<FixedJoint2D>());
    }
}

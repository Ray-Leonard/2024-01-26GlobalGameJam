using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grab : MonoBehaviour
{
    private PlayerController player;
    [SerializeField] private LeftRight handDir;

    [SerializeField] private LayerMask ignoreLayer;

    private void Awake()
    {
        player = GetComponentInParent<PlayerController>();
    }

    private bool hold;

    void Update()
    {
        if(GameInput.Instance.GetHandPressed(handDir, player.PlayerID))
        {
            hold = true;
        }
        else
        {
            hold = false;
            Destroy(GetComponent<FixedJoint2D>());
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(hold)
        {
            if ((1 << collision.gameObject.layer & ignoreLayer) != 0)
            {
                Debug.Log("Cannot grab player");
                return;
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
}

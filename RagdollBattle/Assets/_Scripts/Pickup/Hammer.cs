using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Hammer : WeaponScript
{
    [SerializeField] float angleOffset = -360;
    [SerializeField] float duration = 0.3f;
    [SerializeField] float hitForce;
    [SerializeField] AudioSource audioSourceFire;

    [SerializeField] Transform longManVisualPrefab;
    [SerializeField] Transform longLegPrefab;

    private bool isSwing = false;

    private BoxCollider2D boxCollider;

    private void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        boxCollider.enabled = false;
    }


    protected override void Shoot(object sender, EventArgs e)
    {
        if(!isSwing)
        {
            audioSourceFire.Play();
            isSwing = true;
            transform.DORotate(new Vector3(0, 0, angleOffset), duration, RotateMode.WorldAxisAdd).SetEase(Ease.InOutCubic)
                .OnComplete(() => 
                {
                    isSwing = false;

                }
            );
        }

    }

    public override void OnPickupInitialization(int playerID, Transform parent)
    {
        base.OnPickupInitialization(playerID, parent);

        // enable the collider. 
        boxCollider.enabled = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player" && isSwing)
        {
            // add force
            Rigidbody2D otherPlayerRb = collision.gameObject.GetComponent<Rigidbody2D>();


            bool isMeOnLeft = playerController.playerPos.position.x < collision.transform.position.x;

            Vector2 bodyDir = isMeOnLeft ? new Vector2(-1, 1).normalized : new Vector2(1, 1).normalized;

            otherPlayerRb.AddForce(hitForce * bodyDir);


            // leg check
            BodyPartController otherPlayerBodyPartController = collision.transform.GetComponentInParent<BodyPartController>();
            if (otherPlayerBodyPartController.isLongLegs)
            {

                Debug.Log("hit and change legs");
                otherPlayerBodyPartController.ChangeLegs();

                // spawn a new leg.
                Transform longLeg = Instantiate(longLegPrefab, collision.transform.position - 5 * new Vector3(bodyDir.x, bodyDir.y, 0), Quaternion.identity);

                // apply force to long leg
                longLeg.GetChild(0).GetComponent<Rigidbody2D>().AddForce(hitForce * -bodyDir);
            }


            // add shake scale to the other player
            ShakeScale(collision.transform);





            // ammo
            currAmmo--;
            if (currAmmo <= 0)
            {
                // drop the machine gun. 
                OnPickupExhausted();
            }


            // destroy the collider so it does not hit again. 
            Destroy(GetComponent<BoxCollider2D>());
        }
    }


    private void ShakeScale(Transform otherPlayer)
    {
        Transform longManVisual = Instantiate(longManVisualPrefab, otherPlayer.position, otherPlayer.rotation, otherPlayer.parent);
        longManVisual.DOShakeScale(1, 5, 15).OnComplete(() => { Destroy(longManVisual.gameObject); });
    }
}

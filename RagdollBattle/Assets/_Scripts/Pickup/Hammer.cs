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

    private Rigidbody2D rb;

    private bool isSwing = false;

    private void Start()
    {
        rb  = GetComponent<Rigidbody2D>();
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

                    currAmmo--;

                    if (currAmmo <= 0)
                    {
                        // drop the machine gun. 
                        OnPickupExhausted();
                    }
                }
            );
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player" && isSwing)
        {
            // add force
            Rigidbody2D otherPlayerRb = collision.gameObject.GetComponent<Rigidbody2D>();

            List<ContactPoint2D> contactPoints = new List<ContactPoint2D>();
            collision.GetContacts(contactPoints);

            Vector2 avgNormal = Vector2.zero;
            foreach(ContactPoint2D contactPoint in contactPoints)
            {
                avgNormal += contactPoint.normal;
            }

            otherPlayerRb.AddForce(hitForce * -avgNormal / contactPoints.Count);
            

            // leg check

        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineGun : WeaponScript
{
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] float force;
    [SerializeField] Transform firePosition;
    [SerializeField] private int maxAmmo;
    private int currAmmo;


    private void Awake()
    {
        currAmmo = maxAmmo;
    }


    protected override void Shoot(object sender, EventArgs e)
    {
        GameObject projectile = Instantiate(projectilePrefab, firePosition.position, firePosition.rotation);

        Destroy(projectile, 5f);

        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();

        rb.AddForce(transform.up * force);


        currAmmo--;

        if(currAmmo <= 0) 
        {
            // drop the machine gun. 
            OnPickupExhausted();
        }
    }
}

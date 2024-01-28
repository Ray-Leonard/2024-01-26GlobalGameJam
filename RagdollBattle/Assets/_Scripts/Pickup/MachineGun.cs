using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineGun : WeaponScript
{
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] float force;
    [SerializeField] Transform firePosition;

    private void Update()
    {
        if (IsPickedUp)
        {
            AdjustRotation();
        }
    }

    private void AdjustRotation()
    {

        if (PlayerID == 1)
        {
            Vector3 cursorPos = new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y, 0);

            Vector3 difference = cursorPos - transform.position;

            transform.up = difference.normalized;
        }
        else if (PlayerID == 2)
        {
            Vector2 input = GameInput.Instance.GetRightJoystickInput();

            if (input != Vector2.zero)
            {
                transform.up = input.normalized;
            }
        }
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

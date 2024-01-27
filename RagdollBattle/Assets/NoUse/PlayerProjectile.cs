using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProjectile : MonoBehaviour
{
    [SerializeField] private Transform shootingPoint;
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private float projectileSpeed = 10f;


    private void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        GameObject projectile = Instantiate(projectilePrefab, shootingPoint.position, shootingPoint.rotation);
        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();

        if(transform.localScale.x > 0)
        {
            rb.velocity = transform.right * projectileSpeed;
        }
        else
        {
            rb.velocity = -transform.right * projectileSpeed;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponScript : MonoBehaviour
{
    public GameObject bulletPrefab;
    private void Update()
    {
        // 1. if weapon is equipped, then attach hand to weapon position
        // 2. if shoot condition, then bullet will shoot though weapon's Vector2 Up. And Weapon will aline to that direction
            // 2.2. Meanwhile, weapon will rotate toward mouse/axis direction
    }
}

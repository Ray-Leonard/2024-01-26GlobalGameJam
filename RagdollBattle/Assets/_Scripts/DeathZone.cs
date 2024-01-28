using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathZone : MonoBehaviour
{
    [SerializeField] private Transform respawnPoint;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Collision");
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("Respawn");

            Transform playerParent = collision.transform.parent;

            Vector3 displacement = respawnPoint.position - collision.transform.position;

            foreach(Transform t in playerParent) 
            {
                t.position += displacement;
            }
        }
    }

}

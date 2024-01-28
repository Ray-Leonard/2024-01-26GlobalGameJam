using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathZone : MonoBehaviour
{
    public static event Action<int> OnPlayerRespawn;

    [SerializeField] private Transform respawnPointPlayer;
    [SerializeField] private Transform respawnPointLeg;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("RespawnPlayer");

            Teleport(collision, respawnPointPlayer);

            OnPlayerRespawn?.Invoke(collision.gameObject.GetComponentInParent<PlayerController>().PlayerID);
        }

        else if(collision.gameObject.tag == "LegPickUp")
        {
            Debug.Log("RespawnLeg");
            Teleport(collision, respawnPointLeg);
        }
    }


    private void Teleport(Collider2D collision, Transform point)
    {
        // reset speed
        collision.gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;

        Transform playerParent = collision.transform.parent;

        Vector3 displacement = point.position - collision.transform.position;

        foreach (Transform t in playerParent)
        {
            t.position += displacement;
        }
    }

}

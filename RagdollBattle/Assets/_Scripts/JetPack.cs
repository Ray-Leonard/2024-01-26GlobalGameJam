using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class JetPack : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float thrustSpeed;
    [SerializeField] private float activationTime = 1f;
    [SerializeField] private ParticleSystem particle;

    private PlayerController player;

    private bool isActivated = true;

    private float timer = 0f;

    private bool isParticleSystemPlaying;

    private void Awake()
    {
        player = GetComponent<PlayerController>();
    }

    private void Update()
    {

        if (Input.GetButton("Jump") && isActivated && !player.IsOnGround)
        {
            timer += Time.deltaTime;

            if(timer > activationTime)
            {
                rb.velocity = new Vector2(rb.velocity.x, thrustSpeed * Time.deltaTime);
                if (!isParticleSystemPlaying)
                {
                    particle.Play();
                    isParticleSystemPlaying = true;
                }
            }

        }
        else
        {
            if (isParticleSystemPlaying)
            {
                particle.Stop();
                isParticleSystemPlaying = false;
            }
        }


        if (player.IsOnGround)
        {
            timer = 0;

        }
    }
}

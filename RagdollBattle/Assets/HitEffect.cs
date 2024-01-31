using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitEffect : MonoBehaviour
{
    [SerializeField] private List<AudioClip> audioClipsHit = new List<AudioClip>();
    [SerializeField] private AudioClip audioClipFire;
    [SerializeField] private AudioClip audioClipHit;
    [SerializeField] private AudioSource audioSourceHitSound;
    [SerializeField] private AudioSource audioSourceFireSound;
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private bool isBullet = false;
    private bool isCanPlay = true;

    private void Start()
    {
        audioSourceHitSound = GetComponent<AudioSource>();
        if(isBullet){
            audioSourceFireSound = GetComponent<AudioSource>();
            audioSourceFireSound.PlayOneShot(audioClipFire);
        }
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(isCanPlay){
            audioSourceHitSound.clip = audioClipHit;
            audioSourceHitSound.Play();
            if ((layerMask.value & (1 << collision.gameObject.layer))!= 0)
            {
                PlayRandomMusic();
            }
            if (isBullet)
            {
                isCanPlay = false;
            }
        }
    }


    void PlayRandomMusic(){
        if (audioClipsHit.Count > 0){
            int index = UnityEngine.Random.Range(0, audioClipsHit.Count);
            audioSourceHitSound.clip = audioClipsHit[index];
            audioSourceHitSound.PlayOneShot(audioSourceHitSound.clip);
        }
    }
}

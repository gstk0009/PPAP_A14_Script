using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class VehicleEffect : MonoBehaviour
{

    private AudioSource audioSource;
    public AudioClip hornSound;
    public AudioClip explosionSound;
    public AudioClip skidMarkSound;
    public AudioClip collisionSound;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }



    public void HornSound()
    {
        audioSource.loop = true;
        audioSource.clip = hornSound;
        audioSource.Play();
    }

    public void SkidMarkSound()
    {
        audioSource.PlayOneShot(skidMarkSound);
    }
    public void ExplosionSound()
    {
        audioSource.PlayOneShot(explosionSound);
    }

    public void CollisionSound()
    {
        audioSource.PlayOneShot(collisionSound);
    }

    public void StopSound()
    {
        audioSource.Stop();
    }
}
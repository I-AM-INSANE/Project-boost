﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EscortPlayerCollisions : MonoBehaviour
{
    [SerializeField] private float LevelLoadDelay;
    [SerializeField] private ParticleSystem crashParticles;
    [SerializeField] private ParticleSystem finishParticles;
    private AudioSource audioSource;
    private bool isTransitioning = false;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!isTransitioning)
        {
            if (collision.gameObject.layer == LayerMask.NameToLayer("Obstacle"))
            {
                StartCoroutine(CrashRoutine());
            }
            else if (collision.gameObject.CompareTag("Finish"))
            {
                StartCoroutine(FinishRoutine());
            }
        }
    }

    private IEnumerator CrashRoutine()
    {
        isTransitioning = true;
        DisablePlayerMovement();
        crashParticles.Play();
        PlaySoundEffect(AudioStorage.Instance.SFX_SpaceShip_Death);
        yield return new WaitForSeconds(LevelLoadDelay);
        LevelManager.ReloadLevel();
    }

    private IEnumerator FinishRoutine()
    {
        isTransitioning = true;
        DisablePlayerMovement();
        finishParticles.Play();
        PlaySoundEffect(AudioStorage.Instance.SFX_Finish);
        yield return new WaitForSeconds(LevelLoadDelay);
        LevelManager.LoadNextLevel();
    }

    private void PlaySoundEffect(AudioClip clip)
    {
        audioSource.Stop();
        audioSource.PlayOneShot(clip);
    }

    private void DisablePlayerMovement()
    {
        GetComponent<Player_Movement>().enabled = false;
    }
}

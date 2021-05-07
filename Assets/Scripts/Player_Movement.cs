using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Movement : MonoBehaviour
{
    #region Fields

    [SerializeField] private ParticleSystem mainEngineParticles;
    [SerializeField] private ParticleSystem leftEngineParticles;
    [SerializeField] private ParticleSystem rightEngineParticles;
    [SerializeField] private float boost;
    [SerializeField] private float rotateSpeed;
    private Rigidbody rb;
    private float horizontalAxisValue;
    private float jumpAxisValue;
    private RigidbodyConstraints originalConstraints;
    private AudioSource audioSource;

    #endregion

    #region Methods

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        originalConstraints = rb.constraints;
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        horizontalAxisValue = Input.GetAxis("Horizontal");
        jumpAxisValue = Input.GetAxis("Jump");
        ProcessRotation();
    }

    private void FixedUpdate()
    {
        ProcessBoost();
    }

    private void ProcessRotation()
    {
        rb.constraints = RigidbodyConstraints.FreezeRotationZ;
        transform.Rotate(Vector3.forward * -horizontalAxisValue * rotateSpeed * Time.deltaTime);
        rb.constraints = originalConstraints;
        if (horizontalAxisValue > 0)
        {
            if (!leftEngineParticles.isPlaying)
                leftEngineParticles.Play();
        }
        else if (horizontalAxisValue < 0)
        {
            if (!rightEngineParticles.isPlaying)
                rightEngineParticles.Play();
        }
        else
        {
            leftEngineParticles.Stop();
            rightEngineParticles.Stop();
        }
        
    }

    private void ProcessBoost()
    {
        BoostAudioEffect();
        if (jumpAxisValue != 0)
        {
            rb.AddRelativeForce(Vector3.up * jumpAxisValue * boost * Time.fixedDeltaTime);
            if (!mainEngineParticles.isPlaying)
                mainEngineParticles.Play();
        }
        else
            mainEngineParticles.Stop();

    }
    private void BoostAudioEffect()
    {
        if (jumpAxisValue != 0)
        {
            if (!audioSource.isPlaying)
                audioSource.PlayOneShot(AudioStorage.Instance.SFX_SpaceShip_Engine);
        }
        else
            audioSource.Stop();
    }

    #endregion
}

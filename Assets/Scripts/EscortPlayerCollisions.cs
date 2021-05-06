using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EscortPlayerCollisions : MonoBehaviour
{
    [SerializeField] private float LevelLoadDelay;
    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter(Collision collision)
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

    private IEnumerator CrashRoutine()
    {
        DisablePlayerMovement();
        audioSource.PlayOneShot(AudioStorage.Instance.SFX_SpaceShip_Death);
        yield return new WaitForSeconds(LevelLoadDelay);
        LevelManager.ReloadLevel();
    }

    private IEnumerator FinishRoutine()
    {
        DisablePlayerMovement();
        audioSource.PlayOneShot(AudioStorage.Instance.SFX_Finish);
        yield return new WaitForSeconds(LevelLoadDelay);
        LevelManager.ReloadLevel();
    }

    private void DisablePlayerMovement()
    {
        GetComponent<Player_Movement>().enabled = false;
    }
}

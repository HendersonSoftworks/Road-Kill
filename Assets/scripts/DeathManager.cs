using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathManager : MonoBehaviour
{
    public bool isDead = false;

    [SerializeField] Quaternion playerRot;

    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip screamClip;
    [SerializeField] AudioClip imSorryClip;

    private void OnTriggerEnter(Collider other)
    {
        transform.SetPositionAndRotation(transform.position, playerRot);
        
        Debug.Log("Collided with: " + other.name);
        isDead = true;
        transform.position = new Vector3(transform.position.x, 2.5f, transform.position.z);

        audioSource.Stop();
        audioSource.clip = screamClip;
        audioSource.PlayOneShot(screamClip);
        audioSource.volume = 0.2f;
        audioSource.PlayOneShot(imSorryClip);
    }
}

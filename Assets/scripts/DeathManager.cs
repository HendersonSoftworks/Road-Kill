using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathManager : MonoBehaviour
{
    public bool isDead = false;

    [SerializeField] Quaternion playerRot;

    private void Start()
    {
        //playerRot = transform.rotation;
    }

    private void OnTriggerEnter(Collider other)
    {
        transform.SetPositionAndRotation(transform.position, playerRot);
        
        Debug.Log("Collided with: " + other.name);
        isDead = true;
        transform.position = new Vector3(transform.position.x, 1.8f, transform.position.z);


    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveGround : MonoBehaviour
{
    [SerializeField] DeathManager deathManager;

    private void Start()
    {
        deathManager = FindObjectOfType<DeathManager>();
    }

    private void Update()
    {
        // Return if character is dead
        if (deathManager.isDead)
        {
            return;
        }

        transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, transform.position.y, -7), 4 * Time.deltaTime);
        if (transform.position.z <= -7)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, 13);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayerX : MonoBehaviour
{
    [SerializeField] GameObject player;
    private float playerXPos;

    [SerializeField] DeathManager deathManager;
    [SerializeField] Vector3 deathRot;

    private void Start()
    {
        deathManager = FindObjectOfType<DeathManager>();
    }

    private void Update()
    {
        if (deathManager.isDead)
        {
            transform.rotation = Quaternion.AngleAxis(90, deathRot);
        }

        playerXPos = player.transform.position.x;
        transform.position = new Vector3(playerXPos, transform.position.y, transform.position.z);
    }
}

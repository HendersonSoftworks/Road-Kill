using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayerX : MonoBehaviour
{
    [SerializeField] GameObject player;
    private float playerXPos;

    private void Update()
    {
        playerXPos = player.transform.position.x;
        transform.position = new Vector3(playerXPos, transform.position.y, transform.position.z);
    }
}

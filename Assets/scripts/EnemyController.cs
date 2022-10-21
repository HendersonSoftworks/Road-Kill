using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private Vector3 playerPos;
    [SerializeField] float speed;

    //values for internal use
    private Quaternion lookRotation;
    private Vector3 playerDirection;

    void Start()
    {
        player = GameObject.FindObjectOfType<PlayerController>().gameObject;
        // Define the playerPos as slightly behind player so car zooms by
        playerPos = new Vector3(player.transform.position.x, 3, player.transform.position.z - 10)  ;        
    }

    void Update()
    {
        // Move towards the position defined on spawn in
        transform.position = Vector3.MoveTowards(transform.position, playerPos, speed * Time.deltaTime);
        // despawn car when behind player
        if (transform.position == playerPos)
        {
            Destroy(gameObject);
        }

        float distance = Vector3.Distance(player.transform.position, transform.position);
        if (distance >= 50)
        {
            // Keep facing player
            //find the vector pointing from our position to the target
            playerDirection = (player.transform.position - transform.position).normalized;

            //create the rotation we need to be in to look at the target
            lookRotation = Quaternion.LookRotation(playerDirection);

            //rotate us over time according to speed until we are in the required rotation
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * speed);
        }
    }
}

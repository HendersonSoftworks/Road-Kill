using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private Vector3 playerPos;
    [SerializeField] float speed;
    [SerializeField] float despawnDistance;

    //values for internal use
    private Quaternion lookRotation;
    private Vector3 playerDirection;


    // rotation values
    [SerializeField] private Vector3 enemyDirection;
    [SerializeField] private float enemyAngle;

    // Audio
    [SerializeField] private float beepDistance;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip[] honks;
    [SerializeField] private bool canBeep = true;
    [SerializeField] private float currentDist;

    // Stats
    [SerializeField] private float jumpOverDist;
    [SerializeField] private bool canLogJump = true;


    void Start()
    {
        player = GameObject.FindObjectOfType<PlayerController>().gameObject;
        // Define the playerPos as slightly behind player so car zooms by
        //playerPos = new Vector3(player.transform.position.x, 2f, player.transform.position.z)  ;
        playerPos = new Vector3(player.transform.position.x, 2f, player.transform.position.z - despawnDistance);

        transform.rotation.Set(enemyDirection.x, enemyDirection.y, enemyDirection.z, 0);

        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        // Get dist from Player
        currentDist = Vector3.Distance(transform.position, player.transform.position);
        // Beep if near player
        if (currentDist <= beepDistance && canBeep)
        {
            int rand = Random.Range(0, honks.Length);
            canBeep = false;
            audioSource.PlayOneShot(honks[rand]);

            // If close enough to beep, then start the process of spawning another enemy
            EnemySpawner enemySpawner = FindObjectOfType<EnemySpawner>();
            enemySpawner.enemyActive = false;
        }

        // check if being jumped over
        if (currentDist <= jumpOverDist && player.GetComponent<PlayerController>().isGrounded == false && canLogJump)
        {
            canLogJump = false;

            int totalJumps = PlayerPrefs.GetInt("jumps");
            PlayerPrefs.SetInt("jumps", totalJumps + 1);

            Debug.Log("Total jumps: " + PlayerPrefs.GetInt("jumps"));
        }

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
            lookRotation = Quaternion.LookRotation(-playerDirection);

            //rotate us over time according to speed until we are in the required rotation
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * speed);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(transform.position, jumpOverDist);
    }
}

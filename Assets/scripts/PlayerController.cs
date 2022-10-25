using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Vector3 playerVelocity;
    [SerializeField] private float playerSpeed;
    [SerializeField] private float fallSpeed;

    [SerializeField] private float jumpHeight = 2.5f;
    private float gravityValue = -9.81f;
    [SerializeField] private int boundsX;
    private enum direction { left, neutral, right}
    [SerializeField]direction dir;
    [SerializeField] bool isGrounded = true;
    [SerializeField] float resetJumpPos;
    [SerializeField] private Animator animator;

    [SerializeField] private DeathManager deathManager;

    [SerializeField] private AudioSource audioSource;

    private void Start()
    {
        dir = direction.neutral;
        resetJumpPos = transform.position.y;

        deathManager = FindObjectOfType<DeathManager>();
    }

    void Update()
    {
        if (deathManager.isDead)
        {
            animator.SetBool("dead", true);
            return;
        }

        ManageAnimation();

        // Disable input when in air
        if (isGrounded == true)
        {
            ManageInput();
            // Manage running audio
            if (audioSource.isPlaying == false)
            {
                audioSource.Play();
            }
        }
        // Falling
        if (isGrounded == false)
        {
            if (transform.position.y >= resetJumpPos)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y - fallSpeed * Time.deltaTime);
            }
            else if (transform.position.y <= resetJumpPos)
            {
                isGrounded = true;
                animator.SetBool("jump", false);
            }

            // Stop running audio when jumping
            if (audioSource.isPlaying)
            {
                audioSource.Stop();
            }
        }
    }

    private void ManageAnimation()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded != false)
        {
            animator.SetBool("jump", true);
        }
    }

    private void ManageInput()
    {
        // move left
        if (Input.GetKey(KeyCode.A))
        {
            if (transform.position.x >= -boundsX) // check bounds
            {
                gameObject.transform.position = new Vector3(transform.position.x - playerSpeed * Time.deltaTime, transform.position.y);
                dir = direction.left;
            }
            gameObject.transform.rotation = new Quaternion(transform.rotation.x, -0.5f, transform.rotation.z, transform.rotation.w);
        }
        // move right
        if (Input.GetKey(KeyCode.D))
        {
            if (transform.position.x <= boundsX) // check bounds
            {
                gameObject.transform.position = new Vector3(transform.position.x + playerSpeed * Time.deltaTime, transform.position.y);
                dir = direction.right;

            }
            gameObject.transform.rotation = new Quaternion(transform.rotation.x, 0.5f, transform.rotation.z, transform.rotation.w);
        }
        // jumping
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded != false)
        {
            // player jumps straight up
            gameObject.transform.position = new Vector3(transform.position.x, transform.position.y * jumpHeight, transform.position.z);
            isGrounded = false;
        }
        // Set to neutral position when keys are released
        if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D))
        {
            dir = direction.neutral;
            gameObject.transform.rotation = new Quaternion(transform.rotation.x, 0.0f, transform.rotation.z, transform.rotation.w);
        }
    }
}
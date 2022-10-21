using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Vector3 playerVelocity;
    [SerializeField] private float playerSpeed;
    [SerializeField] private float fallSpeed;

    private float jumpHeight = 1.0f;
    private float gravityValue = -9.81f;
    [SerializeField] private int boundsX;
    private enum direction { left, neutral, right}
    [SerializeField]direction dir;
    [SerializeField] bool isGrounded = true;
    [SerializeField] float resetJumpPos = 2.3f;

    private void Start()
    {
        dir = direction.neutral;
    }

    void Update()
    {
        // Disable input when in air
        if (isGrounded != false)
        {
            ManageInput();
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
            }
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
            gameObject.transform.position = new Vector3(transform.position.x, transform.position.y * 2.5f, transform.position.z);
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
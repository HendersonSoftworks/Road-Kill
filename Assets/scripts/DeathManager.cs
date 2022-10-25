using UnityEngine.SceneManagement;
using UnityEngine;

public class DeathManager : MonoBehaviour
{
    public bool isDead = false;

    [SerializeField] Quaternion playerRot;

    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip screamClip;
    [SerializeField] AudioClip imSorryClip;

    private Scene currentScene;

    // Endless mode
    GameManager gameManager;

    private void Start()
    {
        currentScene = SceneManager.GetActiveScene();

        gameManager = FindObjectOfType<GameManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        transform.SetPositionAndRotation(transform.position, playerRot);
        
        isDead = true;
        transform.position = new Vector3(transform.position.x, 2.5f, transform.position.z);

        audioSource.Stop();
        audioSource.clip = screamClip;
        audioSource.PlayOneShot(screamClip);
        audioSource.volume = 0.2f;
        audioSource.PlayOneShot(imSorryClip);

        if (currentScene.name == "endless")
        {
            PlayerPrefs.SetFloat("score", gameManager.distance);
        }
    }
}

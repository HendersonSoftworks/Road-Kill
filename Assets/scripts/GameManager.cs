using UnityEngine;
using UnityEngine.UI;
using Unity.Jobs;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject[] thoughts;
    [SerializeField] private float thoughtTimerDuration;
    [SerializeField] private float thoughtTimer;

    [SerializeField] private float distance;
    [SerializeField] private Text distanceText;
    
    [SerializeField] private DeathManager deathManager;

    [SerializeField] private GameObject scream;

    [SerializeField] private GameObject player;

    [SerializeField] private GameObject pausePanel;

    private void Awake()
    {
        Time.timeScale = 1;
    }

    void Start()
    {
        deathManager = FindObjectOfType<DeathManager>();

        thoughtTimer = thoughtTimerDuration;

        distanceText.text = distance.ToString();
    }

    void Update()
    {
        if (deathManager.isDead)
        {
            DisableThoughts();
            scream.SetActive(true);

            return;
        }

        if (Input.GetKeyDown(KeyCode.Escape) && Time.timeScale == 1) 
        {
            Time.timeScale = 0;

            PauseAllAudio();

            player.SetActive(false);

            // Show menu
            pausePanel.SetActive(true);
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && Time.timeScale == 0)
        {
            Time.timeScale = 1;

            ResumeAllAudio();

            player.SetActive(true);

            // Hide menu
            pausePanel.SetActive(false);
        }

        // Thoughts
        thoughtTimer -= Time.deltaTime;
        if (thoughtTimer <= 0)
        {
            thoughtTimer = thoughtTimerDuration;
            int rand = Random.Range(0, thoughts.Length);

            // Disable other thoughts
            DisableThoughts();

            // Enable random thought
            thoughts[rand].SetActive(true);

            // TO DO: Play thought audio
        }

        // Distance
        distance -= Time.deltaTime * 2;
        if (distance <= 0)
        {
            distance = 0;

            // Win condition
            // Load cutscene of player finding out that Julie never existed

        }

        distanceText.text = distance.ToString("0.0");
    }

    private void DisableThoughts()
    {
        for (int i = 0; i < thoughts.Length; i++)
        {
            thoughts[i].SetActive(false);
        }
    }

    private void PauseAllAudio()
    {
        var audioSources = GameObject.FindObjectsOfType<AudioSource>();
        foreach (AudioSource source in audioSources)
        {
            source.Pause();
        }
    }

    private void ResumeAllAudio()
    {
        var audioSources = GameObject.FindObjectsOfType<AudioSource>();
        foreach (AudioSource source in audioSources)
        {
            source.UnPause();
        }
    }
}

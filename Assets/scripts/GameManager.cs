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
            // TO DO: Play scream sound

            return;
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
}

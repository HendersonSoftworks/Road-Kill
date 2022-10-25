using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    private Scene currentScene;
    private DeathManager deathManager;

    [SerializeField] GameObject restartPanel;
    [SerializeField] float restartTimerDuration;
    [SerializeField] float restartTimer;

    private void Start()
    {
        currentScene = SceneManager.GetActiveScene();

        deathManager = FindObjectOfType<DeathManager>();

        restartTimer = restartTimerDuration;
    }

    private void Update()
    {
        if (currentScene.name == "main")
        {
            if (deathManager.isDead)
            {
                restartTimer -= Time.deltaTime;
                if (restartTimer <= 0)
                {
                    restartTimer = 0;
                    restartPanel.SetActive(true);
                }
            }
        }
    }

    public void LoadMainScene()
    {
        SceneManager.LoadScene("main");        
    }

    public void LoadmenuScene()
    {
        SceneManager.LoadScene("menu");
    }
}

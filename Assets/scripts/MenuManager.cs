using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    private Scene currentScene;
    private DeathManager deathManager;

    [SerializeField] GameObject restartPanel;
    [SerializeField] float restartTimerDuration;
    [SerializeField] float restartTimer;

    [SerializeField] GameObject helpPanel;
    [SerializeField] GameObject mainPanel;

    [SerializeField] Text highscoreText;
    [SerializeField] Text carsJumpedText;
    [SerializeField] Text storyText;
    [SerializeField] Text endlessText;

    private void Awake()
    {
        PlayerPrefs.Save();

        Screen.SetResolution(640, 480, true);

        Time.timeScale = 1;

        //Screen.fullScreen = false;
    }

    private void Start()
    {
        currentScene = SceneManager.GetActiveScene();

        deathManager = FindObjectOfType<DeathManager>();

        restartTimer = restartTimerDuration;

        var highscorePref = PlayerPrefs.GetFloat("score");
        var jumpsPref = PlayerPrefs.GetInt("jumps");

        if (currentScene.name == "menu")
        {
            highscoreText.text = "Highscore: " + highscorePref.ToString("0.0");
            carsJumpedText.text = "cars jumped: " + jumpsPref.ToString();

            if (PlayerPrefs.GetInt("story_completed") == 1)
            {
                storyText.text = "Story Mode Completed [X]";
            }
            else
            {
                storyText.text = "Story Mode Compleyed [ ]";
            }

            if (PlayerPrefs.GetInt("story_completed") == 0)
            {
                endlessText.text = "[Locked]";
                endlessText.color = Color.grey;
            }
            else
            {
                endlessText.text = "Endless Mode";
            }
        }
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

        if (currentScene.name == "endless")
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

        if (currentScene.name == "menu")
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                mainPanel.SetActive(true);
                helpPanel.SetActive(false);
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

    public void LoadEndlessScene()
    {
        if (PlayerPrefs.GetInt("story_completed") == 1)
        {
            SceneManager.LoadScene("endless");
        }
    }

    public void ShowHelp()
    {
        mainPanel.SetActive(false);
        helpPanel.SetActive(true);
    }

    public void ClearPrefs()
    {
        PlayerPrefs.DeleteAll();
        SceneManager.LoadScene("menu");
    }
}

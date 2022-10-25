using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndingCutscene : MonoBehaviour
{
    [SerializeField ]private Text endingText;

    [SerializeField] private Text SPACEText;


    private int cutsceneNum = 0;

    [SerializeField] private GameObject music;

    [SerializeField] private Animator animator;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && cutsceneNum == 0)
        {
            endingText.text = "I forgot...";
            cutsceneNum++;

            animator.SetBool("idle", true);

            return;
        }

        if (Input.GetKeyDown(KeyCode.Space) && cutsceneNum == 1)
        {
            endingText.text = "Julie doesn't exist.";
            SPACEText.text = "Press SPACE to return to the main menu";

            endingText.color = Color.red;

            transform.position = new Vector3(0, 1.78f, -0.94f);

            //animator.SetBool("scared", true);
            animator.SetBool("tpose", true);

            music.SetActive(true);

            cutsceneNum++;
            return;
        }

        if (Input.GetKeyDown(KeyCode.Space) && cutsceneNum == 2)
        {
            SceneManager.LoadScene("menu");
        }
    }
}

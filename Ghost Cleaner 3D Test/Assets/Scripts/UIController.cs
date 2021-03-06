using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    public Animator transition;
    public GameObject helpPanel;
    public GameObject creditsPanel;
    public GameObject pausePanel;
    public float transitionTime = 1f;

    // Start is called before the first frame update
    void Start()
    {

    }

    public void OnClickQuitButton() //Quit
    {
        Application.Quit();
        print("This button works!");
    }

    public void OnClickHelpButton() //Open help menu
    {
        helpPanel.gameObject.SetActive(true);
    }

    public void OnClickBackHelpButton() //Close help menu
    {
        helpPanel.gameObject.SetActive(false);
    }

    public void OnClickCreditsButton() //Open credits menu
    {
        creditsPanel.gameObject.SetActive(true);
    }

    public void OnClickBackCredButton() //Close credits menu
    {
        creditsPanel.gameObject.SetActive(false);
    }

    public void LoadMenuLevel()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1;
        Debug.Log("menu loaded");
    }

    public void LoadLevelOne()
    {
        SceneManager.LoadScene(1);
    }

    public void LoadCurrentLevel()
    {
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex));
    }

    public void LoadNextLevel()
    {
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
    }

    IEnumerator LoadLevel(int levelIndex)
    {
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene(levelIndex);
    }

    public void NextLevelNoTransition(string level)
    {
        SceneManager.LoadScene(level);
    }

    public void OnReturnButtonClick()
    {
        pausePanel.SetActive(false);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [Header("UI Elements")]
    public Animator transition;
    public GameObject helpPanel;
    public GameObject creditsPanel;
    public GameObject pausePanel;
    public float transitionTime = 1f;
    [SerializeField] Text winTokenTxt;
    [SerializeField] Text totalTimeTxt;
    [SerializeField] Text bestTimeTxt;
    [SerializeField] GameObject bonusPanel;


    void Start()
    {
        if(SceneManager.GetActiveScene().name == "win")
        {
            winTokenTxt.text = "x" + " " + GhostCon.totalCoins.ToString();

            float timeToDisplay = ObjectPlacementCon.totalTime;
            float minutes = Mathf.FloorToInt(timeToDisplay / 60);
            float seconds = Mathf.FloorToInt(timeToDisplay % 60);
            totalTimeTxt.text = string.Format("{0:00}:{1:00}", minutes, seconds);

            float timeToDisplay2 = PlayerPrefs.GetFloat("bestTime");
            float minutes2 = Mathf.FloorToInt(timeToDisplay2 / 60);
            float seconds2 = Mathf.FloorToInt(timeToDisplay2 % 60);
            bestTimeTxt.text = "Best Time: \n" + string.Format("{0:00}:{1:00}", minutes2, seconds2);

            if (GhostCon.totalCoins == 6)
            {
                bonusPanel.SetActive(true);
            }
            else
            {
                bonusPanel.SetActive(false);
            }
        }
    }

    //Button Events
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
        GhostCon.totalCoins = 0;
        Time.timeScale = 1;
        StartCoroutine(LoadLevel(0));
    }

    public void LoadLevelOne()
    {
        GhostCon.totalCoins -= GhostCon.totalCoins;
        StartCoroutine(LoadLevel(1));
    }

    public void LoadCurrentLevel()
    {
        GhostCon.totalCoins -= GhostCon.currentLevelTotal;
        Time.timeScale = 1;
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
        Time.timeScale = 1; GhostCon.canGo = true;
        pausePanel.SetActive(false);
    }

    public void OnEnterBonusClick()
    {
        SceneManager.LoadScene(5);
    }

    public void OnResetBestTimeClick()
    {
        PlayerPrefs.SetFloat("bestTime", 180f);
        print("Best Time reset");
    }
}

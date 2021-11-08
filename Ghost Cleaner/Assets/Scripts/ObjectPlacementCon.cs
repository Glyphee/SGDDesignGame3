using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectPlacementCon : MonoBehaviour
{
    [Header("Level Objects")]
    [SerializeField] GameObject[] objectsToPlace = new GameObject[3];
    [SerializeField] GameObject[] ghostObjects = new GameObject[3];

    [Header("UI Values")]
    [SerializeField] Text timerTxt;
    [SerializeField] Text timerTxtShdw;
    [SerializeField] Image winPanel;
    [SerializeField] Text winStatsTxt;
    [SerializeField] Text winStatsTxtShdw;
    float gameTime;
    float startTime;
    float stoppedTime;
    bool levelDone;


    // Start is called before the first frame update
    void Start()
    {
        startTime = Time.time;
        StartCoroutine(DoPlacementCheck());
        levelDone = false;

        winPanel.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //the timer for each level; should add rating tracking here later
        Timer();
    }

    //the following should check if objects are placed in the correct location, and if so, freeze the timer and provide a next level/menu option
    void PlacementCheck()
    {
        int placed = 0;
        foreach (GameObject o in objectsToPlace)
        {
            foreach(GameObject g in ghostObjects)
            {
                if(Vector3.Distance(o.transform.position, g.transform.position) < 1f && o.transform.parent == null)
                {
                    if(System.Array.IndexOf(objectsToPlace, o) == System.Array.IndexOf(ghostObjects, g))
                    {
                        o.transform.position = g.transform.position;
                        o.transform.rotation = g.transform.rotation;
                        placed++;
                        o.gameObject.GetComponent<Collider>().isTrigger = false;
                        g.gameObject.SetActive(false);

                        //print("Objects in place: " + placed.ToString());
                    }
                    else
                    {
                        //print("Not the right place for this object");
                    }                    
                }
            }
        }

        //check if they're all in the right place, and if so, stop the timer
        if (placed == ghostObjects.Length)
        {
            levelDone = true;
            GhostCon.canGo = false;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            stoppedTime = gameTime;
            DisplayTime(stoppedTime);            
            winPanel.gameObject.SetActive(true);            
            //print("You did it! It took " + stoppedTime.ToString());
        }
    }

    IEnumerator DoPlacementCheck()
    {
        while (true)
        {
            PlacementCheck();
            yield return new WaitForSeconds(.2f);
        }        
    }

    #region Timer
    void Timer()
    {
        if (levelDone)
        {
            return;
        }
        gameTime = Time.time - startTime;
        DisplayTime(gameTime);
    }

    void DisplayTime(float timeToDisplay)
    {
        timeToDisplay += 1;
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);
        timerTxt.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        timerTxtShdw.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        winStatsTxt.text = "It took: " + string.Format("{0:00}:{1:00}", minutes, seconds) + " to clean the room!";
        winStatsTxtShdw.text = "It took: " + string.Format("{0:00}:{1:00}", minutes, seconds) + " to clean the room!";
    }
    #endregion
}

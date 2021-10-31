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
    float gameTime;
    float stoppedTime;
    bool levelDone;


    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DoPlacementCheck());
        levelDone = false;
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
                    o.transform.position = g.transform.position;
                    o.transform.rotation = g.transform.rotation;
                    placed++;
                    print("Objects in place: " + placed.ToString());
                }
            }
        }

        if(placed == ghostObjects.Length)
        {
            //check if they're all in the right place, and if so, stop the timer
            levelDone = true;
            stoppedTime = gameTime;
            print("You did it! It took " + stoppedTime.ToString());
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

        gameTime = Time.time;
        DisplayTime(gameTime);
    }
    void DisplayTime(float timeToDisplay)
    {
        timeToDisplay += 1;
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        timerTxt.text = "Time Passed: " + string.Format("{0:00}:{1:00}", minutes, seconds);
    }
    #endregion
}

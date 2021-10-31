using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPlacementCon : MonoBehaviour
{
    [Header("Level Objects")]
    [SerializeField] GameObject[] objectsToPlace = new GameObject[3];
    [SerializeField] GameObject[] ghostObjects = new GameObject[3];

    float stoppedTime;


    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DoPlacementCheck());
    }

    // Update is called once per frame
    void Update()
    {
        
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
            print("You did it!");
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
}

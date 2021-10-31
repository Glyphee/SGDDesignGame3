using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPlacementCon : MonoBehaviour
{
    [Header("Level Objects")]
    [SerializeField] GameObject[] objectsToPlace;
    [SerializeField] GameObject[] ghostObjects;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        PlacementCheck();
    }

    //the following should check if objects are placed in the correct location, and if so, freeze the timer and provide a next level/menu option
    void PlacementCheck()
    {
               
    }
}

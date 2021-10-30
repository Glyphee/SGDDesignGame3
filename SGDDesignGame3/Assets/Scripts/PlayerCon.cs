using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCon : MonoBehaviour
{
    [Header("Movement values")]
    Vector2 move;
    [SerializeField] float speed = 0;
    [SerializeField] private GameObject[] objects = new GameObject[0];
    [SerializeField] private int possessedObject;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        PlayerMovement();
    }

    void PlayerMovement()
    {
        move.x = Input.GetAxis("Horizontal"); //Modified this part a bit to make the ghost accelerate and decelerate instead of moving instantaneously
        move.y = Input.GetAxis("Vertical");

        transform.Translate(move * speed * Time.deltaTime);
    }

    GameObject DetectClosest()
    {
        for (int i = 0; i < objects.Length; i++)
        {
            if (Vector2.Distance(this.gameObject.transform.position, objects[i].transform.position) < 1)
            {
                int closestObject = 0;
                for (int k = 0; k < objects.Length; k++)
                {
                    if (Vector2.Distance(this.gameObject.transform.position, objects[k].transform.position) < Vector2.Distance(this.gameObject.transform.position, objects[closestObject].transform.position))
                    {
                        closestObject = k;
                    }
                }
                return objects[closestObject];
            }
        }
        return null;
    }
}

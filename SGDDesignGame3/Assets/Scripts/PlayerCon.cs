using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCon : MonoBehaviour
{
    [Header("Movement values")]
    Vector2 move;
    [SerializeField] float speed = 0;
    [SerializeField] float accelerationTime = 2;


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

    //possession mechanic to follow

}

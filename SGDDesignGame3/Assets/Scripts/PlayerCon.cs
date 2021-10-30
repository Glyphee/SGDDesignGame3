using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCon : MonoBehaviour
{
    [Header("Movement values")]
    Vector2 move;
    [SerializeField] float speed;


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
        move.x = Input.GetAxisRaw("Horizontal");
        move.y = Input.GetAxisRaw("Vertical");
        move = move.normalized;

        transform.Translate(move * speed * Time.deltaTime);
    }

    //possession mechanic to follow

}

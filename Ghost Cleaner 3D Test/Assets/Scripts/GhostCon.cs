using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostCon : MonoBehaviour
{
    [Header("Move and Look Values")]
    public float moveSpeed;
    public float lookSpeed;
    public static bool canGo, canLook, paused;
    float lookY;
    float lookX;

    bool on;

    private Transform cam;
    [SerializeField] GameObject pausePanel;
    [SerializeField] GameObject ghostBody;


    void Start()
    {
        //cam and movement values
        cam = GetComponentInChildren<Camera>().transform;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        canGo = true; canLook = true;
        paused = false;

        //the ghost main body
        //ghostBody = 
        on = true;
    }

    void Update()
    {
        if (canGo)
        {
            //player movement
            float moveX = Input.GetAxis("Horizontal");
            float moveY = Input.GetAxis("Vertical");
            float vertMove = 1f;

            transform.Translate(new Vector3(moveX, 0f, moveY) * moveSpeed * Time.deltaTime);

            if (Input.GetKey(KeyCode.Q))
            {
                transform.Translate(new Vector3(0f, vertMove, 0f) * moveSpeed/2 * Time.deltaTime);
            }
            if (Input.GetKey(KeyCode.E))
            {
                transform.Translate(new Vector3(0f, -vertMove, 0f) * moveSpeed/2 * Time.deltaTime);
            }
        }
        if (canLook)
        {
            //player look
            lookX += Input.GetAxis("Mouse X") * lookSpeed;
            lookY -= Input.GetAxis("Mouse Y") * lookSpeed;

            lookY = Mathf.Clamp(lookY, -.25f, 35f);

            cam.rotation = Quaternion.Euler(new Vector3(lookY, lookX, 0f));
            transform.rotation = Quaternion.Euler(new Vector3(0f, lookX, 0f));
        }

        //extended functionality (pausing, possession mechanic, etc)
        OnPauseAndResume();
        HideAndRevealGhost();
    }

    void OnPauseAndResume()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!paused)
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                pausePanel.SetActive(true);
                Time.timeScale = 0;
                canGo = false; canLook = false; paused = true;
            }
            else if (paused)
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                pausePanel.SetActive(false);
                Time.timeScale = 1;
                canGo = true; canLook = true; paused = false;
            }
        }
    }

    void HideAndRevealGhost()
    {
        string ghostState;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (on)
            {
                ghostBody.SetActive(false); on = false;
                ghostState = "vanished";
            }
            else
            {
                ghostBody.SetActive(true); on = true;
                ghostState = "visible";
            }
            Debug.Log("Ghost is: " + ghostState);
        }        
    }
}

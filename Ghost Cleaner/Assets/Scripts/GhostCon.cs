using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GhostCon : MonoBehaviour
{
    [Header("Move and Look Values")]
    public float moveSpeed; public float lookSpeed;
    float lookY, lookX;
    public static bool canGo, canLook, paused;

    [Header("Gameplay values")]
    bool on;
    bool possessing;
    Transform cam;
    [SerializeField] Transform origin;
    [SerializeField] GameObject pausePanel;
    [SerializeField] GameObject ghostBody;
    [SerializeField] Transform camPosition;
    GameObject player;
    GameObject holding;
    CharacterController chrCon;
    int coinCount;


    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        chrCon = player.GetComponent<CharacterController>();
        on = true; possessing = false;
        holding = null;
        coinCount = 0;

        //cam and movement values
        cam = GetComponentInChildren<Camera>().transform;
        cam.position = camPosition.position; cam.rotation = Quaternion.Euler(35, 0, 0);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        canGo = true; //canLook = true;
        paused = false;
    }

    void Update()
    {
        if (canGo)
        {
            //player movement
            float moveX = Input.GetAxis("Horizontal");
            float moveY = Input.GetAxis("Vertical");
            Vector3 moveDir = new Vector3(moveX, 0f, moveY);

            transform.Translate(moveDir * moveSpeed * Time.deltaTime);
            ghostBody.transform.LookAt(moveDir + ghostBody.transform.position);

            /*float vertMove = 1f;
            if (Input.GetKey(KeyCode.Q))
            {
                transform.Translate(new Vector3(0f, vertMove, 0f) * moveSpeed / 2 * Time.deltaTime);
            }
            if (Input.GetKey(KeyCode.E))
            {
                transform.Translate(new Vector3(0f, -vertMove, 0f) * moveSpeed / 2 * Time.deltaTime);
            }*/
        }
        /*if (canLook)
        {
            //player look
            lookX += Input.GetAxis("Mouse X") * lookSpeed;
            lookY -= Input.GetAxis("Mouse Y") * lookSpeed;

            lookY = Mathf.Clamp(lookY, -.25f, 35f);

            cam.rotation = Quaternion.Euler(new Vector3(lookY, lookX, 0f));
            transform.rotation = Quaternion.Euler(new Vector3(0f, lookX, 0f));
        }*/

        //extended functionality
        OnPauseAndResume();
        Possession();        
    }

    //for pausing the game
    void OnPauseAndResume()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (pausePanel.activeSelf)
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                pausePanel.SetActive(false);
                Time.timeScale = 1;
                canGo = true; canLook = true; paused = false;
            }
            else if (!pausePanel.activeSelf)
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                pausePanel.SetActive(true);
                Time.timeScale = 0;
                canGo = false; canLook = false; paused = true;
            }
        }
    }    

    #region Possession Mechanic
    void Possession()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !possessing)
        {
            if(holding != null)
            {
                if (Vector3.Distance(chrCon.transform.position, holding.transform.position) <= 2f)
                {
                    AudioCon.sfx.PlayPossess();
                    ghostBody.SetActive(false); on = false;
                    holding.transform.SetParent(player.transform);
                    possessing = true;
                    Debug.Log("parented object");
                }
                else
                {
                    AudioCon.sfx.PlayTooFar();
                    print("not close enough");
                }
            }
            else
            {
                AudioCon.sfx.PlayTooFar();
                print("have not started to clean");
            }
        }
        else if (Input.GetKeyDown(KeyCode.Space) && possessing)
        {
            AudioCon.sfx.PlayUnpossess();
            holding.transform.parent = null;
            holding = null;
            possessing = false;
            ghostBody.SetActive(true); on = true;
            Debug.Log("unparented object now");
        }
    }
    #endregion

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.CompareTag("prop"))
        {            
            holding = col.gameObject;
            print("found an object");
        }

        if (col.gameObject.CompareTag("teleport"))
        {
            player.transform.position = origin.transform.position;
        }

        if (col.gameObject.CompareTag("coin"))
        {
            AudioCon.sfx.PlayCoinGet();
            coinCount++;
            Destroy(col.gameObject);
            print("got coin!");
        }
    }    
}

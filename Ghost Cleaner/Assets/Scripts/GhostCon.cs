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
    static int coinCount = 0;
    [SerializeField] Text coinTxt;


    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        chrCon = player.GetComponent<CharacterController>();
        on = true; possessing = false;
        holding = null;

        //cam and movement values
        cam = GetComponentInChildren<Camera>().transform;
        cam.position = camPosition.position; cam.rotation = Quaternion.Euler(35, 0, 0);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        canGo = true; //canLook = true;
        paused = false;

        print("coins held" + coinCount.ToString());
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
        }
        OnPauseAndResume();
        Possession();
        CamRotate();

        coinTxt.text = "Coins Collected: " + coinCount.ToString();
    }

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

    void CamRotate()
    {
        if (Input.GetKeyDown("e"))
        {
            //transform.localRotation = new Quaternion(0, transform.localRotation.y + -0.25f, 0, 0);
            transform.localRotation *= Quaternion.Euler(0, -90, 0);
            print("turn camera right\n" + transform.localRotation);
        }
        else if (Input.GetKeyDown("q"))
        {
            //transform.localRotation = new Quaternion(0, transform.localRotation.y + 0.25f, 0, 0);
            transform.localRotation *= Quaternion.Euler(0, 90, 0);
            print("turn camera left\n" + transform.localRotation);
        }
    }
}

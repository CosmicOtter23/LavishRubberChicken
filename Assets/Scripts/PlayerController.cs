using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class PlayerController : MonoBehaviour
{
    public KeyCode up;
    public KeyCode left;
    public KeyCode down;
    public KeyCode right;
    public KeyCode space;

    public float initialSpeed;
    private float moveSpeed;

    public Rigidbody2D rb;

    public List<Transform> startNodes = new List<Transform>();

    public bool freezePlayer = false;

    private GameObject torch;
    
    public float rollCooldown, rollDuration;
    private float rollTimer, rollDurationTimer;
    private bool rolling;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        if (rb == null)
        {
            Debug.Log("RB not found");
        }

        torch = GameObject.Find("PlayerLight");

        if (torch == null)
        {
            Debug.Log("Torch not found");
        }

        transform.position = new Vector2(startNodes[PlayerPrefs.GetInt("StartNode")].position.x, startNodes[PlayerPrefs.GetInt("StartNode")].position.y);

        freezePlayer = false;

        torch.GetComponent<Light2D>().pointLightOuterRadius = PlayerPrefs.GetFloat("TorchMultiplier");
    }
    
    void Update()
    {
        //DEV COMMANDS
        //if (Input.GetKey("h"))
        //{
        //    PlayerPrefs.SetInt("Health", 3);
        //    Debug.Log("Full health");
        //}
        //if (Input.GetKey("g"))
        //{
        //    PlayerPrefs.SetInt("GateOpen", 1);
        //    Debug.Log("Opened Gate");
        //}
        //if (Input.GetKey("b"))
        //{
        //    PlayerPrefs.SetInt("GateOpen", 0);
        //    Debug.Log("Closed Gate");
        //}
        //if (Input.GetKey("j"))
        //{
        //    Debug.Log("PlayerPrefs Output");
        //    Debug.Log("Money: " + PlayerPrefs.GetInt("TotalMoney"));
        //    Debug.Log("Gate Open: " + PlayerPrefs.GetInt("GateOpen"));
        //    Debug.Log("Gate 2 Open: " + PlayerPrefs.GetInt("Gate2Open"));
        //    Debug.Log("Health: " + PlayerPrefs.GetInt("Health"));
        //    Debug.Log("Keys: " + PlayerPrefs.GetInt("2"));
        //}
        //if (Input.GetKey("k"))
        //{
        //    Debug.Log("Set Keys to 2");
        //    PlayerPrefs.SetInt("Keys", 2);
        //}
        //if (Input.GetKey("m"))
        //{
        //    Debug.Log("Set money to 0");
        //    PlayerPrefs.SetInt("TotalMoney", 0);
        //}
        //if (Input.GetKey("n"))
        //{
        //    Debug.Log("Set money to 9999");
        //    PlayerPrefs.SetInt("TotalMoney", 9999);
        //}
        //if (Input.GetKey("o"))
        //{
        //    Debug.Log("Reset all PlayerPrefs");
        //    PlayerPrefs.SetInt("Keys", 0);
        //    PlayerPrefs.SetInt("Health", 3);
        //    PlayerPrefs.SetInt("TotalHealth", 3);
        //    PlayerPrefs.SetInt("TotalMoney", 0);
        //    PlayerPrefs.SetInt("StartNode", 0);
        //    PlayerPrefs.SetInt("GateOpen", 0);
        //    PlayerPrefs.SetInt("Gate2Open", 0);
        //    PlayerPrefs.SetInt("TorchFlickered", 0);
        //    PlayerPrefs.SetFloat("TorchMultiplier", 2);
        //    PlayerPrefs.SetFloat("SpeedMultiplier", 1);
        //    PlayerPrefs.SetInt("DodgeRoll", 0);
        //    PlayerPrefs.SetString("TreasureGot", "");
        //    PlayerPrefs.SetString("KeyGot", "");
        //    PlayerPrefs.SetString("ShopItemsGot", "");
        //}

        moveSpeed = initialSpeed * PlayerPrefs.GetFloat("SpeedMultiplier");

        //Allows diagonal movement
        //if (Input.GetKey(up))
        //{
        //    rb.velocity = new Vector2(rb.velocity.x, moveSpeed);
        //    transform.rotation = Quaternion.Euler(0, 0, 0);
        //}
        //else if (Input.GetKey(left))
        //{
        //    rb.velocity = new Vector2(-moveSpeed, rb.velocity.y);
        //    transform.rotation = Quaternion.Euler(0, 0, 90);
        //}
        //else if (Input.GetKey(down))
        //{
        //    rb.velocity = new Vector2(rb.velocity.x, -moveSpeed);
        //    transform.rotation = Quaternion.Euler(0, 0, 180);
        //}
        //else if (Input.GetKey(right))
        //{
        //    rb.velocity = new Vector2(moveSpeed, rb.velocity.y);
        //    transform.rotation = Quaternion.Euler(0, 0, 270);
        //}
        //else
        //{
        //    rb.velocity = new Vector2(0, 0);
        //}
        //if (rb.velocity.magnitude > moveSpeed || rb.velocity.magnitude < -moveSpeed)
        //{
        //    moveSpeed = initialSpeed / 2;
        //}
        //else
        //{
        //    moveSpeed = initialSpeed;
        //}

        //Does not allow diagonal movement
        if (!freezePlayer && !rolling)
        {
            if (Input.GetKey(up))
            {
                rb.velocity = new Vector2(0, moveSpeed);
                transform.rotation = Quaternion.Euler(0, 0, 180);
            }
            else if (Input.GetKey(left))
            {
                rb.velocity = new Vector2(-moveSpeed, 0);
                transform.rotation = Quaternion.Euler(0, 0, 270);
            }
            else if (Input.GetKey(down))
            {
                rb.velocity = new Vector2(0, -moveSpeed);
                transform.rotation = Quaternion.Euler(0, 0, 0);
            }
            else if (Input.GetKey(right))
            {
                rb.velocity = new Vector2(moveSpeed, 0);
                transform.rotation = Quaternion.Euler(0, 0, 90);
            }
            else
            {
                rb.velocity = new Vector2(0, 0);
            }

            if (Input.GetKeyDown(space) && rollTimer >= rollCooldown && PlayerPrefs.GetInt("DodgeRoll") == 1)
            {
                Debug.Log("Roll");
                rb.velocity *= 5;
                rollTimer = 0;
                rolling = true;
            }
        }

        if (freezePlayer)
        {
            rb.velocity = new Vector2(0, 0);
        }

        if (rolling)
        {
            rollDurationTimer += Time.deltaTime;
            transform.Rotate(0, 0, 1800 * Time.deltaTime);
        }

        rollTimer += Time.deltaTime;

        if (rollDurationTimer >= rollDuration)
        {
            Debug.Log("End roll");
            rb.velocity = new Vector2(0, 0);
            rolling = false;
            rollDurationTimer = 0;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.Experimental.Rendering.LWRP;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine.SceneManagement;

public class GuardController : MonoBehaviour
{
    public float moveSpeed;

    private Rigidbody2D rb;

    public List<Transform> nodes = new List<Transform>();
    private Transform nextNode;
    private int nextNodeNo;
    private Vector2 vectorNode;

    public int startingNode;

    public bool circularPath;
    private int increment = 1;
    
    public GameObject guardLight;
    //public Light2D guardLight2;

    private Transform player;

    public float timeBtwFootsteps, fadeSpeed;
    private float countdown;
    public Sprite leftStep, rightStep;
    public GameObject stepPrefab;
    public GameObject theStep;
    private bool step;

    private Vector3 lastPos;

    private bool playerFound = false;

    public GameObject excMark;

    private float deathCountdown;

    void Awake()
    {
        nextNodeNo = startingNode;

        player = GameObject.Find("Player").transform;
        if (player == null)
        {
            Debug.Log("Player not found");
        }

        rb = GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            Debug.Log("Rigidbody not found");
        }
        else
        {
            //Debug.Log("Rigidbody found");
        }

        guardLight = GameObject.Find("GuardLight");
        if (guardLight == null)
        {
            Debug.Log("Guard Light not found");
        }

        deathCountdown = 3;

        countdown = timeBtwFootsteps;

        lastPos = transform.position;

        //guardLight = GameObject.Find("GuardLight");
        //if (guardLight == null)
        //{
        //    Debug.Log("Guard light not found");
        //}

        //guardLight2 = guardLight.GetComponent<Light2D>();
    }
    
    void Update()
    {
        vectorNode = new Vector2(nodes[nextNodeNo].position.x, nodes[nextNodeNo].position.y);

        if(!playerFound)
        {
            transform.position = Vector2.MoveTowards(transform.position, vectorNode, moveSpeed * Time.deltaTime);
        }
        else
            guardLight.GetComponent<GuardLightController>().GetComponent<Light2D>().pointLightOuterRadius = 3;


        vectorNode = new Vector2(nodes[nextNodeNo].position.x, nodes[nextNodeNo].position.y);

        if (new Vector2(transform.position.x, transform.position.y) == vectorNode)
        {
            nextNodeNo += increment;
        }

        if(nextNodeNo >= nodes.Count)
        {
            if(circularPath)
            {
                nextNodeNo = 0;
            }
            else
            {
                nextNodeNo -= 1;
                increment *= -1;
                //Debug.Log(nextNodeNo);
            }
        }
        if (nextNodeNo < 0)
        {
            nextNodeNo = 0;
            increment *= -1;
        }
        
        var velocity = transform.position - lastPos;
        lastPos = transform.position;
        var rotation = new Quaternion();

        if (velocity.y > 0.001)
        {
            //Debug.Log("up");
            transform.rotation = Quaternion.Euler(0, 0, 180);
            rotation.eulerAngles = new Vector3(0, 0, 0);

            if (countdown <= 0)
            {
                if (step)
                {
                    theStep = Instantiate(stepPrefab, new Vector2(transform.localPosition.x - 0.2f, transform.position.y), rotation);
                    theStep.GetComponent<SpriteRenderer>().sprite = leftStep;
                    //theStep.transform.rotation = transform.rotation;
                    step = false;
                }
                else if (!step)
                {
                    theStep = Instantiate(stepPrefab, new Vector2(transform.localPosition.x + 0.2f, transform.position.y), rotation);
                    theStep.GetComponent<SpriteRenderer>().sprite = rightStep;
                    step = true;
                }
                countdown = timeBtwFootsteps;
            }
        }
        else if (velocity.y < -0.001)
        {
            //Debug.Log("down");
            transform.rotation = Quaternion.Euler(0, 0, 0);
            rotation.eulerAngles = new Vector3(0, 0, 180);

            if (countdown <= 0)
            {
                if (step)
                {
                    theStep = Instantiate(stepPrefab, new Vector2(transform.localPosition.x + 0.2f, transform.position.y), rotation);
                    theStep.GetComponent<SpriteRenderer>().sprite = leftStep;
                    //theStep.transform.rotation = transform.rotation;
                    step = false;
                }
                else if (!step)
                {
                    theStep = Instantiate(stepPrefab, new Vector2(transform.localPosition.x - 0.2f, transform.position.y), rotation);
                    theStep.GetComponent<SpriteRenderer>().sprite = rightStep;
                    step = true;
                }
                countdown = timeBtwFootsteps;
            }
        }
        else if (velocity.x > 0.001)
        {
            //Debug.Log("right");
            transform.rotation = Quaternion.Euler(0, 0, 90);
            rotation.eulerAngles = new Vector3(0, 0, 270);

            if (countdown <= 0)
            {
                if (step)
                {
                    theStep = Instantiate(stepPrefab, new Vector2(transform.localPosition.x, transform.position.y + 0.2f), rotation);
                    theStep.GetComponent<SpriteRenderer>().sprite = leftStep;
                    //theStep.transform.rotation = transform.rotation;
                    step = false;
                }
                else if (!step)
                {
                    theStep = Instantiate(stepPrefab, new Vector2(transform.localPosition.x, transform.position.y - 0.2f), rotation);
                    theStep.GetComponent<SpriteRenderer>().sprite = rightStep;
                    step = true;
                }
                countdown = timeBtwFootsteps;
            }
        }
        else if (velocity.x < -0.001)
        {
            //Debug.Log("left");
            transform.rotation = Quaternion.Euler(0, 0, 270);
            rotation.eulerAngles = new Vector3(0, 0, 90);

            if (countdown <= 0)
            {
                if (step)
                {
                    theStep = Instantiate(stepPrefab, new Vector2(transform.localPosition.x, transform.position.y - 0.2f), rotation);
                    theStep.GetComponent<SpriteRenderer>().sprite = leftStep;
                    //theStep.transform.rotation = transform.rotation;
                    step = false;
                }
                else if (!step)
                {
                    theStep = Instantiate(stepPrefab, new Vector2(transform.localPosition.x, transform.position.y + 0.2f), rotation);
                    theStep.GetComponent<SpriteRenderer>().sprite = rightStep;
                    step = true;
                }
                countdown = timeBtwFootsteps;
            }
        }

        countdown -= Time.deltaTime;
        if(playerFound)
        {
            deathCountdown -= Time.deltaTime;
        }

        if (deathCountdown <= 0)
        {
            Debug.Log("Return to main room");
            PlayerPrefs.SetInt("Health", PlayerPrefs.GetInt("Health") - 1);
            PlayerPrefs.SetInt("StartNode", 0);
            SceneManager.LoadScene("Main Room");
        }

        //if (countdown <= 0)
        //{
        //    if (step)
        //    {
        //        theStep = Instantiate(stepPrefab, new Vector2(transform.localPosition.x - 0.2f, transform.position.y), rotation);
        //        theStep.GetComponent<SpriteRenderer>().sprite = leftStep;
        //        //theStep.transform.rotation = transform.rotation;
        //        step = false;
        //    }
        //    else if (!step)
        //    {
        //        theStep = Instantiate(stepPrefab, new Vector2(transform.localPosition.x + 0.2f, transform.position.y), rotation);
        //        theStep.GetComponent<SpriteRenderer>().sprite = rightStep;
        //        step = true;
        //    }
        //    countdown = timeBtwFootsteps;
        //}

        //guardLight2.pointLightOuterRadius = Vector2.Distance(new Vector2(transform.position.x, transform.position.y), new Vector2(player.position.x, player.position.y));
        //Debug.Log(guardLight2.pointLightOuterRadius);
    }

    //public void OnTriggerEnter2D(Collider2D collision)
    //{
    //    bool lineOfSight = false;
        
    //    RaycastHit2D hit;
    //    hit = Physics2D.Raycast(transform.position, player.transform.position - transform.position);
    //    if (hit.collider != null)
    //    {
    //        //Debug.Log("We found Target!");
    //        lineOfSight = false;
    //    }
    //    else
    //    {
    //        lineOfSight = false;
    //    }

    //    //RaycastHit2D hit = Physics2D.Linecast(transform.position, player.transform.position - transform.position, 100);
    //    ////RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.right);

    //    //if (hit.collider.gameObject.tag == "Player")
    //    //{
    //    //    Debug.Log("We found Target!");
    //    //    lineOfSight = true;
    //    //}
    //    //else
    //    //{
    //    //    //Debug.Log("I found something else with name = " + hit.collider.name);
    //    //    lineOfSight = false;
    //    //}


    //    Debug.Log("Collision");
    //    if (collision.tag == "Player" && lineOfSight)
    //    {
    //        Debug.Log("Player found");
    //        playerFound = true;
    //        Instantiate(excMark, new Vector2(transform.position.x, transform.position.y + 0.5f), Quaternion.identity);
    //    }
    //}

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Debug.Log("Player found");
            playerFound = true;
            Instantiate(excMark, new Vector2(transform.position.x, transform.position.y + 0.5f), Quaternion.identity);
            player.GetComponent<PlayerController>().freezePlayer = true;
        }
    }

    //public void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.blue;
    //    Gizmos.DrawSphere(guardLight.transform.position, 1);
    //}
}

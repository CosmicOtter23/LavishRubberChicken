using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class GuardLightController : MonoBehaviour
{
    public Transform player;

    float distance;

    public float lightMultiplier;

    public bool lineOfSight;

    void Start()
    {
        player = GameObject.Find("Player").transform;
        if(player == null)
        {
            Debug.Log("Player not found");
        }
    }
    
    void Update()
    {
        //var hit: RaycastHit;
        ////RaycastHit hit;
        //var rayDirection = player.position - transform.position;
        //if (Physics.Raycast(transform.position, rayDirection, hit))
        //{
        //    if (hit.transform == player)
        //    {
        //        lineOfSight = true;
        //    }
        //    else
        //    {
        //        lineOfSight = false;
        //    }
        //}

        RaycastHit2D hit;
        hit = Physics2D.Raycast(transform.position, player.transform.position - transform.position);
        if (hit.collider.gameObject.tag == "Player")
        {
            //Debug.Log("We found Target!");
            lineOfSight = true;
        }
        else
        {
            //Debug.Log("I found something else with name = " + hit.collider.name);
            lineOfSight = false;
        }

        distance = Vector2.Distance(new Vector2(transform.position.x, transform.position.y), new Vector2(player.position.x, player.position.y));

        if (lineOfSight)
        {
            GetComponent<Light2D>().pointLightOuterRadius = (lightMultiplier / distance) - 5;

            if (GetComponent<Light2D>().pointLightOuterRadius <= 0)
            {
                GetComponent<Light2D>().pointLightOuterRadius = 0;
            }
            else if (GetComponent<Light2D>().pointLightOuterRadius >= 5)
            {
                GetComponent<Light2D>().pointLightOuterRadius = 5;
            }
        }
        else
        {
            GetComponent<Light2D>().pointLightOuterRadius = 0;
        }
    }
}

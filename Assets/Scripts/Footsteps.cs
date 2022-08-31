using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Footsteps : MonoBehaviour
{
    public float fadeSpeed;
    public GameObject guard;

    void Start()
    {
        //guard = GameObject.Find("Guard");
        //if (guard == null)
        //{
        //    Debug.Log("Guard not found");
        //}

        //fadeSpeed = guard.GetComponent<GuardController>().fadeSpeed;
    }
    
    void Update()
    {
        //Debug.Log("z Rotation: " + guard.transform.rotation.z);
        //transform.rotation = Quaternion.Euler(0, 0, guard.transform.rotation.z * -360);

        Color stepColor = GetComponent<Renderer>().material.color;
        float fadeAmount = stepColor.a - (fadeSpeed * Time.deltaTime);
        stepColor = new Color(stepColor.r, stepColor.g, stepColor.b, fadeAmount);
        GetComponent<Renderer>().material.color = stepColor;

        if (stepColor.a <= 0)
        {
            Destroy(gameObject);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour
{
    public string nextScene;

    public int nextStartNode;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            Debug.Log("Loading next scene...");
            PlayerPrefs.SetInt("StartNode", nextStartNode);
            SceneManager.LoadScene(nextScene);
        }
    }
}

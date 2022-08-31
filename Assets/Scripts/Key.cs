using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Key : MonoBehaviour
{
    private void Awake()
    {
        if (PlayerPrefs.GetString("KeyGot").Contains(SceneManager.GetActiveScene().name))
        {
            Debug.Log(PlayerPrefs.GetString("KeyGot"));
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            PlayerPrefs.SetInt("Keys", PlayerPrefs.GetInt("Keys") + 1);
            Debug.Log("Got Key!");
            PlayerPrefs.SetString("KeyGot", PlayerPrefs.GetString("KeyGot") + SceneManager.GetActiveScene().name);
            Destroy(gameObject);
        }
    }
}

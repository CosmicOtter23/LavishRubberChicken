using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Experimental.Rendering.Universal;

public class GameManager : MonoBehaviour
{
    public Text moneyText, keyText;

    public GameObject playerTorch;

    private GameObject player;

    private float flickerTime = 1f;

    public GameObject movingWall;

    private bool moveDoor = false, moveDoorBack = false;
    
    void Start()
    {
        playerTorch = GameObject.Find("PlayerLight");
        if (playerTorch == null)
        {
            Debug.Log("Torch not found");
        }

        player = GameObject.Find("Player");
        if (player == null)
        {
            Debug.Log("Player not found");
        }

        //
        //PlayerPrefs.SetInt("TorchFlickered", 0);
        //
        //PlayerPrefs.SetInt("NoTorch", 0);

        //if (PlayerPrefs.GetInt("NoTorch") == 1)
        //    playerTorch.SetActive(false);
        //else
        //    playerTorch.SetActive(true);

        //Debug.Log(PlayerPrefs.GetInt("TorchFlickered"));

        if (SceneManager.GetActiveScene().name == "205" /*&& PlayerPrefs.GetInt("TorchFlickered") == 0*/)
        {
            Debug.Log("205 Event");
            PlayerPrefs.SetInt("TorchFlickered", 1);
            StartCoroutine(TorchFlicker());
        }
    }
    
    void Update()
    {
        if (moneyText != null && keyText != null)
        {
            keyText.text = PlayerPrefs.GetInt("Keys").ToString();
            moneyText.text = "£" + PlayerPrefs.GetInt("TotalMoney").ToString();
        }

        if (moveDoor)
        {
            Debug.Log("move door");
            movingWall.transform.position = Vector2.MoveTowards(movingWall.transform.position, new Vector2(-2, -3), 1 * Time.deltaTime);
        }
        if (moveDoorBack)
        {
            Debug.Log("move door back");
            movingWall.transform.position = Vector2.MoveTowards(movingWall.transform.position, new Vector2(0, -3), 3 * Time.deltaTime);
        }
    }

    public IEnumerator TorchFlicker()
    {
        while (flickerTime >= 0.05)
        {
            player.GetComponent<PlayerController>().freezePlayer = true;
            playerTorch.GetComponent<Light2D>().intensity = 1.8f;
            yield return new WaitForSeconds(flickerTime);
            //playerTorch.SetActive(false);
            playerTorch.GetComponent<Light2D>().intensity = 0;
            yield return new WaitForSeconds(flickerTime/2);
            PlayerPrefs.SetInt("NoTorch", 1);
            flickerTime /= 1.5f;
        }
        yield return new WaitForSeconds(0.5f);
        Debug.Log("bool set");
        moveDoor = true;
        player.GetComponent<PlayerController>().freezePlayer = false;
        yield return new WaitForSeconds(3f);
        moveDoor = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            moveDoorBack = true;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Chest : MonoBehaviour
{
    public GameObject rubberChicken;
    private GameObject chickenPrefab;
    private bool chickenMove = false, occurred = false;

    private float countdown;

    public Text moneyText;

    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        if (player == null)
        {
            Debug.Log("Player not found");
        }

        countdown = 3;
    }

    // Update is called once per frame
    void Update()
    {
        if (chickenMove)
        {
            chickenPrefab.transform.position = Vector2.MoveTowards(chickenPrefab.transform.position, new Vector2(0, 0), 2 * Time.deltaTime);
        }
        if (occurred)
        {
            countdown -= Time.deltaTime;
        }
        if (countdown <= 0 && chickenMove)
        {
            VictoryScreen();
        }
        if (countdown <= -5 && !chickenMove)
        {
            SceneManager.LoadScene("Main Menu");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && !occurred)
        {
            Debug.Log("Collision");
            occurred = true;
            chickenPrefab = Instantiate(rubberChicken, new Vector2(transform.position.x, transform.position.y), Quaternion.identity);
            chickenMove = true;
            countdown = 3;
            player.GetComponent<PlayerController>().freezePlayer = true;
        }
    }

    public void VictoryScreen()
    {
        Debug.Log("Victory Screen");
        countdown = 3;
        chickenMove = false;
        chickenPrefab.transform.localScale = new Vector2(8, 8);
        moneyText.enabled = true;
        moneyText.transform.position = new Vector2(3, 0);
        moneyText.transform.localScale = new Vector2(3, 3);
        moneyText.text = "£2000";
        PlayerPrefs.SetInt("TotalMoney", PlayerPrefs.GetInt("TotalMoney") + 2000);
    }
}

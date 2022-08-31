using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TreasurePickup : MonoBehaviour
{
    private int price = 0;

    public Sprite[] sprites = new Sprite[5];
    private Sprite sprite;

    public Text priceText;

    public GameObject player;

    public float priceDisplayTime;
    private float countdown;

    void Awake()
    {
        player = GameObject.Find("Player");
        if(player == null)
        {
            Debug.Log("Player not found");
        }

        if (PlayerPrefs.GetString("TreasureGot").Contains(SceneManager.GetActiveScene().name))
        {
            Debug.Log(PlayerPrefs.GetString("TreasureGot"));
            Debug.Log("Destroyed");
            Destroy(gameObject);
        }

        int rnd = Random.Range(0, sprites.Length);
        GetComponent<SpriteRenderer>().sprite = sprites[rnd];

        countdown = priceDisplayTime;

        priceText.enabled = false;
    }
    
    void Update()
    {
        countdown -= Time.deltaTime;

        //Debug.Log(countdown);

        if(countdown <= 0 && GetComponent<SpriteRenderer>().enabled == false)
        {
            priceText.enabled = false;
            Destroy(this);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Collision");
        if(other.tag == "Player")
        {
            CollectTreasure();
        }
    }

    public void CollectTreasure()
    {
        Debug.Log("Subroutine");
        countdown = priceDisplayTime;
        Debug.Log("Treasure Collected!");
        priceText.enabled = true;
        GetComponent<SpriteRenderer>().enabled = false;
        price = Random.Range(100, 500);
        PlayerPrefs.SetInt("TotalMoney", PlayerPrefs.GetInt("TotalMoney") + price);
        priceText.transform.position = new Vector2(transform.position.x, transform.position.y);
        priceText.text = "£" + price;
        PlayerPrefs.SetString("TreasureGot", PlayerPrefs.GetString("TreasureGot") + SceneManager.GetActiveScene().name);
    }
}

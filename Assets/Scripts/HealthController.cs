using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HealthController : MonoBehaviour
{
    private int currentHealth, totalHealth;

    public Sprite fullHeartSprite;

    public List<GameObject> hearts = new List<GameObject>();

    public Text gameOverText;

    private bool gameOver, currentCountdown;

    private float countdown;

    // Start is called before the first frame update
    void Start()
    {
        //currentHealth = 3;
        if (gameOverText != null)
        {
            gameOverText.enabled = false;
        }

        countdown = 2;

        for (int i = 0; i < hearts.Count; i++)
        {
            hearts[i].GetComponent<SpriteRenderer>().enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (gameOverText != null && gameOverText.enabled)
        {
            countdown -= Time.deltaTime;
        }

        totalHealth = PlayerPrefs.GetInt("TotalHealth");
        for (int i = 0; i < totalHealth; i++)
        {
            hearts[i].GetComponent<SpriteRenderer>().enabled = true;
        }

        currentHealth = PlayerPrefs.GetInt("Health");
        for (int i = 0; i < currentHealth; i++)
        {
            hearts[i].GetComponent<SpriteRenderer>().sprite = fullHeartSprite;
        }

        if (currentHealth <= 0 && SceneManager.GetActiveScene().name == "Main Room" && !gameOver)
        {
            currentHealth = 0;
            gameOverText.enabled = true;
            Debug.Log("Game over");
            gameOver = true;
            countdown = 2;
        }

        if (countdown <= 0)
        {
            gameOver = false;
            SceneManager.LoadScene("Main Menu");
            PlayerPrefs.SetInt("Health", 3);
        }

        //Debug.Log(countdown);
    }
}

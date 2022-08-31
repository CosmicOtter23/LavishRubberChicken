using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chains2 : MonoBehaviour
{
    public Sprite open, closed;
    private BoxCollider2D box;
    private CapsuleCollider2D caps;

    // Start is called before the first frame update
    void Start()
    {
        box = GetComponent<BoxCollider2D>();
        if (box == null)
        {
            Debug.Log("Box not found");
        }

        caps = GetComponent<CapsuleCollider2D>();
        if (box == null)
        {
            Debug.Log("Capsule not found");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerPrefs.GetInt("Gate2Open") == 0)
        {
            //Gate is closed
            GetComponent<SpriteRenderer>().sprite = closed;
            box.enabled = true;
            caps.enabled = true;
        }
        else if (PlayerPrefs.GetInt("Gate2Open") == 1)
        {
            //Gate is closed
            GetComponent<SpriteRenderer>().sprite = open;
            box.enabled = false;
            caps.enabled = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && PlayerPrefs.GetInt("Keys") == 2)
        {
            PlayerPrefs.SetInt("Gate2Open", 1);
            PlayerPrefs.SetInt("Keys", 0);
        }
    }
}

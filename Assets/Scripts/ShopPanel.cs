using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopPanel : MonoBehaviour
{
    public int price;
    public string name1;
    private int money;

    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.GetString("ShopItemsGot").Contains(name1))
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Collision");

        if (PlayerPrefs.GetInt("TotalMoney") >= price)
        {
            PlayerPrefs.SetInt("TotalMoney", PlayerPrefs.GetInt("TotalMoney") - price);

            if (name1 == "Torch")
            {
                Debug.Log("Torch upgrade");
                PlayerPrefs.SetFloat("TorchMultiplier", 4);
            }
            else if (name1 == "Speed")
            {
                Debug.Log("Speed upgrade");
                PlayerPrefs.SetFloat("SpeedMultiplier", 1.5f);
            }
            else if (name1 == "Roll")
            {
                Debug.Log("Dodge roll");
                PlayerPrefs.SetInt("DodgeRoll", 1);
            }
            else if (name1 == "Health")
            {
                Debug.Log("Health upgrade");
                PlayerPrefs.SetInt("TotalHealth", 4);
                PlayerPrefs.SetInt("Health", PlayerPrefs.GetInt("Health") + 1);
            }

            PlayerPrefs.SetString("ShopItemsGot", PlayerPrefs.GetString("ShopItemsGot") + name1);

            Destroy(gameObject);
        }
        else
        {
            Debug.Log("Too broke");
        }
    }
}

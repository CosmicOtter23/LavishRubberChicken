using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void Play()
    {
        Debug.Log("Play");
        PlayerPrefs.SetInt("Keys", 0);
        PlayerPrefs.SetInt("TotalHealth", 3);
        PlayerPrefs.SetInt("Health", 3);
        //PlayerPrefs.SetInt("TotalMoney", 0);
        PlayerPrefs.SetInt("StartNode", 0);
        PlayerPrefs.SetInt("GateOpen", 0);
        PlayerPrefs.SetInt("Gate2Open", 0);
        PlayerPrefs.SetInt("TorchFlickered", 0);
        PlayerPrefs.SetFloat("SpeedMultiplier", 1);
        PlayerPrefs.SetFloat("TorchMultiplier", 2);
        PlayerPrefs.SetString("TreasureGot", "");
        PlayerPrefs.SetString("KeyGot", "");
        SceneManager.LoadScene("Main Room");
    }

    public void Exit()
    {
        Debug.Log("Quit game");
        Application.Quit();
    }
}

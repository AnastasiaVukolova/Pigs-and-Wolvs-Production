using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameManager : MonoBehaviour {

    public static bool isGameEnd;
    // Update is called once per frame
    public string levelNextName = "Level02";

    public int leveToUnlock = 2;

    public GameObject gameoverUUI;

    public SceneFader fader;

    void Start()
    {
        isGameEnd = false;
    }
	void Update ()
    {
        if (isGameEnd) return;
		if(PlayerStats.Lives <= 0)
        {
            PlayerStats.Lives = 0;
            EndGame();
        }

        if (Input.GetKeyDown("e"))
        {
            EndGame();
        }
	}

    void EndGame()
    {
        isGameEnd = true;
        gameoverUUI.SetActive(true);
    }

    public void WinLevel()
    {

        Debug.Log("Level WONNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNN!");
        PlayerPrefs.SetInt("levelReached", leveToUnlock);
        try
        {
            if (leveToUnlock > 3)
            {
                fader.FadeTo("MainMenu");
                return;
            }
            fader.FadeTo(levelNextName);
        }catch(UnityException ex)
        {
            fader.FadeTo("MainMenu");
            PlayerPrefs.SetInt("levelReached", leveToUnlock - 1);
        }
    }
}

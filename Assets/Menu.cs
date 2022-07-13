using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public GameObject LevelsMenuWindow;
    public GameObject LevelsModeWindow;
    public GameObject MenuWindow;

    public GameObject WinWindow;
    public List<GameObject> Confetti;

    public LevelManager manager;

    public int LevelNumber;
    public int OpenedLevelNumber;
    public string LevelType;

    private void Start()
    {
        manager = FindObjectOfType<LevelManager>();

        LevelType = PlayerPrefs.GetString("LevelType");
        LevelNumber = PlayerPrefs.GetInt("LevelToLoad") + 1;

    }

    public void OpenLevelsMenu(string mode)
    {
        manager.SetupLevels(mode);
        LevelsModeWindow.SetActive(false);
        LevelsMenuWindow.SetActive(true);
    }

    public void CloseLevelsMenu()
    {
        LevelsModeWindow.SetActive(true);
        LevelsMenuWindow.SetActive(false);
    }

    public void OpenLevelsMode()
    {
        LevelsModeWindow.SetActive(true);
        LevelsMenuWindow.SetActive(false);
        MenuWindow.SetActive(false);
    }

    public void CloseLevelsMode()
    {

        LevelsModeWindow.SetActive(false);
        MenuWindow.SetActive(true);
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(1);
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void NextLevel()
    {
        switch (LevelType)
        {
            case "Easy":

                break;
            case "Medium":

                break;
            case "Hard":

                break;
        }

        print("Next level " + LevelNumber);

        if (LevelNumber > 34)
        {
            PlayerPrefs.SetString("LevelType", "Random");
            PlayerPrefs.SetInt("LevelToLoad", LevelNumber - 1);
        }
        else
        {
            PlayerPrefs.SetString("LevelType", LevelType);
            PlayerPrefs.SetInt("LevelToLoad", LevelNumber);
        }

        SceneManager.LoadScene(1);
    }

    public void LevelIsComplete()
    {
        WinWindow.SetActive(true);
        foreach (GameObject go in Confetti)
        {
            go.SetActive(true);
        }

        switch (LevelType)
        {
            case "Easy":
                OpenedLevelNumber = PlayerPrefs.GetInt("LevelOpenedEasy");
                print(OpenedLevelNumber + "  " + LevelNumber);
                if (LevelNumber == OpenedLevelNumber)
                {
                    PlayerPrefs.SetInt("LevelOpenedEasy", OpenedLevelNumber + 1);
                }
                break;
            case "Medium":
                OpenedLevelNumber = PlayerPrefs.GetInt("LevelOpenedMedium");
                if (LevelNumber == OpenedLevelNumber)
                {
                    OpenedLevelNumber = PlayerPrefs.GetInt("LevelOpenedMedium");
                }
                PlayerPrefs.SetInt("LevelOpenedMedium", OpenedLevelNumber + 1);
                break;
            case "Hard":
                OpenedLevelNumber = PlayerPrefs.GetInt("LevelOpenedHard");
                print(OpenedLevelNumber + "  " + LevelNumber);
                if (LevelNumber == OpenedLevelNumber)
                {
                    OpenedLevelNumber = PlayerPrefs.GetInt("LevelOpenedHard");
                }
                PlayerPrefs.SetInt("LevelOpenedHard", OpenedLevelNumber + 1);
                break;
        }
    }
    public void LoadRandomLevel()
    {
        PlayerPrefs.SetString("LevelType", "Random");

        SceneManager.LoadScene(1);
    }

    public void OpenURL(string url)
    {
        Application.OpenURL(url);
        /*if (id == 0)
        {
            Application.OpenURL("https://vk.com/fee1sgood");
        }
        if (id == 1)
        {
            Application.OpenURL("https://www.twitch.tv/fee1goodoff");
        }
        if (id == 2)
        {
            Application.OpenURL("https://discord.gg/Zx6RdJ2");
        }*/
    }
}

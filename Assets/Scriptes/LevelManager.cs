using System;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public Transform content;
    public int levelsEasyCount;
    public int levelsMediumCount;
    public int levelsHardCount;

    public GameObject levelIcon;

    private int levelsOpened;

    private void Start()
    {
    }

    public void SetupLevels(string levelsType)
    {
        for (int i = 0; i < content.childCount; i++)
        {
            Destroy(content.GetChild(i).gameObject);
        }

        if (PlayerPrefs.HasKey("LevelOpenedEasy") == false)
        {
            PlayerPrefs.SetInt("LevelOpenedEasy", 1);
            PlayerPrefs.SetInt("LevelOpenedMedium", 1);
            PlayerPrefs.SetInt("LevelOpenedHard", 1);
        }

        switch (levelsType)
        {
            case "Easy":
                levelsOpened = PlayerPrefs.GetInt("LevelOpenedEasy");

                for (int i = 0; i < levelsEasyCount; i++)
                {
                    GameObject go = Instantiate(levelIcon, content);
                    LevelIcon icon = go.GetComponent<LevelIcon>();
                    icon.LevelNumber = i + 1;

                    if(i + 1 > levelsOpened)
                    {
                        icon.SetupIcon(false, levelsType);
                    }
                    else
                    {
                        icon.SetupIcon(true, levelsType);
                    }
                }
                break;
            case "Medium":
                levelsOpened = PlayerPrefs.GetInt("LevelOpenedMedium");

                for (int i = 0; i < levelsMediumCount; i++)
                {
                    GameObject go = Instantiate(levelIcon, content);
                    LevelIcon icon = go.GetComponent<LevelIcon>();
                    icon.LevelNumber = i + 1;

                    if (i + 1 > levelsOpened)
                    {
                        icon.SetupIcon(false, levelsType);
                    }
                    else
                    {
                        icon.SetupIcon(true, levelsType);
                    }
                }
                break;
            case "Hard":
                levelsOpened = PlayerPrefs.GetInt("LevelOpenedHard");

                for (int i = 0; i < levelsHardCount; i++)
                {
                    GameObject go = Instantiate(levelIcon, content);
                    LevelIcon icon = go.GetComponent<LevelIcon>();
                    icon.LevelNumber = i + 1;

                    if (i + 1 > levelsOpened)
                    {
                        icon.SetupIcon(false, levelsType);
                    }
                    else
                    {
                        icon.SetupIcon(true, levelsType);
                    }
                }
                break;
        }
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class LevelIcon : MonoBehaviour
{
    public int LevelNumber;
    public string LevelType;

    public Sprite activeSprite;
    public Sprite inactiveSprite;

    public Image sprite;
    public Button button;
    public TextMeshProUGUI levelNumberText;

    public AudioClip clickSound;
    private AudioSource _audio;

    public GameObject lockIcon;

    public void SetupIcon(bool isOpened, string type)
    {
        sprite = GetComponent<Image>();
        button = GetComponent<Button>();
        _audio = FindObjectOfType<AudioSource>();
        levelNumberText.text = LevelNumber.ToString();
        LevelType = type;

        if (isOpened == false)
        {
            lockIcon.SetActive(true);
            sprite.sprite = inactiveSprite;
            button.interactable = false;
        }
        else
        {
            button.onClick.AddListener(LoadLevel);
        }
    }

    public void LoadLevel()
    {
        _audio.PlayOneShot(clickSound);
        PlayerPrefs.SetString("LevelType", LevelType);
        PlayerPrefs.SetInt("LevelToLoad", LevelNumber - 1);

        SceneManager.LoadScene(1);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlaskManager : MonoBehaviour
{
    [SerializeField]private Flask _chosenFlask;

    public List<Flask> Flasks;

    private Menu _menu;
    private AudioManager _audio;

    private YandexSDK _yandex;

    private void Start()
    {
        _menu = FindObjectOfType<Menu>();
        _audio = FindObjectOfType<AudioManager>();
        _yandex = YandexSDK.instance;
        _yandex.onRewardedAdReward += Reward;
    }

    public void ChoseFlask(Flask flask)
    {
        if (_chosenFlask)
        {
            if (_chosenFlask == flask)
            {
                _chosenFlask = null;
                flask.IsChosen = false;
            }
            else
            {
                //Проверить можно ли перелить                
                if(flask.HasEmptyPart())
                {
                    _audio.PlayFlightFlaskSound();
                    _chosenFlask.OverflowTo(flask, flask.EmptyParts());

                    _chosenFlask.IsChosen = false;
                    _chosenFlask = null;
                }
                else
                {
                    _chosenFlask.IsChosen = false;
                    _chosenFlask = null;

                    _chosenFlask = flask;
                    flask.IsChosen = true;
                }
            }
        }
        else
        {
            if (!flask.IsEmpty())
            {
                _audio.PlayChooseFlaskSound();
                _chosenFlask = flask;
                flask.IsChosen = true;
            }
        }        
    }

    public void Reward(string placement)
    {
        if(placement == "skip")
        {
            CheckFlasksState(true);
        }
        if (placement == "test")
        {
            Debug.Log("AD TEST FLASKMAN");
        }
    }

    public void CheckFlasksState(bool skip)
    {
        bool isComplete = false;
        for (int i = 0; i < Flasks.Count; i++)
        {
            if (Flasks[i].FilledWithOneColor == false && !Flasks[i].Empty)
            {
                isComplete = false;
                break;
            }
            else
            {
                isComplete = true;
            }
        }

        if (skip)
        {
            isComplete = true;
        }

        if (isComplete)
        {
            foreach(Flask fl in Flasks)
            {
                fl.Win = true;
            }
            _audio.PlayWinSound();
            _menu.LevelIsComplete();
            _yandex.ShowInterstitial();
        }
    }
}

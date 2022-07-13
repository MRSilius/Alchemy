using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Level", menuName = "FLASKS/New Level")]
public class LevelPrefab : ScriptableObject
{
    public List<FlaskPrefab> flasks;

    public void SetupFlasks()
    {
        flasks = new List<FlaskPrefab>();
    }

    public LevelPrefab(List<FlaskPrefab> flasks)
    {
        this.flasks = flasks;
    }

}

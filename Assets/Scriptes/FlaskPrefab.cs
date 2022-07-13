using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class FlaskPrefab
{
    public List<LiquidPrefab> liquids;

    public void SetupLiquid()
    {
        liquids = new List<LiquidPrefab>();
    }
    public FlaskPrefab(List<LiquidPrefab> liquids)
    {
        this.liquids = liquids;
    }
}

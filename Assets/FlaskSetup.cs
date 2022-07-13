using System.Collections.Generic;
using UnityEngine;

public class FlaskSetup : MonoBehaviour
{
    public LevelPrefab level;

    public LevelPrefab randomLevel;

    public int LevelNumber;
    public string LevelType;

    public int FlaskCount;
    public float flaskWidth = 2;
    [SerializeField] private GameObject _flaskPrefab;

    public List<Flask> _flasks;

    private float x = 0;

    public List<LevelPrefab> levelsEasy;
    public List<LevelPrefab> levelsMedium;
    public List<LevelPrefab> levelsHard;
    private FlaskManager _manager;

    private void Start()
    {
        LevelType = PlayerPrefs.GetString("LevelType");
        LevelNumber = PlayerPrefs.GetInt("LevelToLoad");
        print(LevelNumber);

        switch (LevelType)
        {
            case "Easy":
                level = levelsEasy[LevelNumber];
                break;
            case "Medium":
                level = levelsMedium[LevelNumber];
                break;
            case "Hard":
                level = levelsHard[LevelNumber];
                break;
            case "Random":                
                level = RandomLevel();
                break;
        }

        _manager = FindObjectOfType<FlaskManager>();
        FlaskCount = level.flasks.Count;

        SetupFlasks();
        _manager.Flasks = _flasks;

        for (int i = 0; i < level.flasks.Count; i++)
        {
            for (int j = 0; j < level.flasks[i].liquids.Count; j++)
            {
                _flasks[i].SetLiquid(j, level.flasks[i].liquids[j].Color);
            }
        }
    }

    private LevelPrefab RandomLevel()
    {
        int flaskMin = 4;
        int flaskMax = 9;
        int flaskCount = Mathf.RoundToInt(Random.Range(flaskMin, flaskMax));
        int colorsCount = flaskCount - 1;


        //Создать колбы
        List<FlaskPrefab> flasks = new List<FlaskPrefab>();
        for (int i = 0; i < flaskCount; i++)
        {
            flasks.Add(new FlaskPrefab(new List<LiquidPrefab>()));
        }

        //Добавить колбы в список уровня
        LevelPrefab level = new LevelPrefab(flasks);

        LiquidPrefab liqq = new LiquidPrefab();
        //randomLevel.flasks[0].liquids.Add(liqq);

        //Заполнить колбы
        int[] colors = new int[colorsCount];

        for (int i = 0; i < colorsCount * 4; i++)
        {
            int flaskNumber = Mathf.RoundToInt(Random.Range(0, flaskCount));
            int x = 200;
            //print(flasks[flaskNumber].liquids);
            while(level.flasks[flaskNumber].liquids.Count >= 4 && x > 0)
            {
                flaskNumber = Mathf.RoundToInt(Random.Range(0, flaskCount));
                x--;
            }
            int y = 200;
            int colorNumber = Mathf.RoundToInt(Random.Range(0, colorsCount));
            while (colors[colorNumber] > 3 && x > 0)
            {
                colorNumber = Mathf.RoundToInt(Random.Range(0, colorsCount));
                y--;
            }
            LiquidPrefab liq = new LiquidPrefab();
            liq.type = (LiquidPrefab.ColorType)colorNumber;
            //Добавление цвета в колбу
            level.flasks[flaskNumber].liquids.Add(liq);
            colors[colorNumber] += 1;
        }

        return level;
    }

    private void SetupFlasks()
    {
        float XPos = (flaskWidth / 2) - (FlaskCount * flaskWidth) / 2;

        for (int i = 0; i < FlaskCount; i++)
        {
            GameObject go = Instantiate(_flaskPrefab, new Vector3(XPos, 0, 0), Quaternion.identity);
            _flasks.Add(go.GetComponent<Flask>());
            XPos += flaskWidth;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance { get { Init(); return _instance; } }
    public Queue<WeatherDatas> _weatherQueue = new Queue<WeatherDatas>();

    private Define.Weather[] weathers = new Define.Weather[5]
    {
        Define.Weather.Clear,
        Define.Weather.Cloudy,
        Define.Weather.Rainy,
        Define.Weather.Stormy,
        Define.Weather.Hail,
    };
    private float[] percents = new float[5] {50f, 30f, 10f, 5f, 5f };

    public WeatherDatas CurrentWeather { get; private set; }
    public int Hour { get; private set; }
    public float inGameTime;
    private const float realDay = 120f;
    private const float inGameDay = 24f;

    private float growPoint;
    private float hamPoint;

    private void Awake()
    {
        Init();
        NextWeek();
        StartCoroutine(UpdateWater());
    }
    private void Update()
    {
        float gameRealTime = inGameDay / realDay;
        inGameTime += Time.deltaTime * gameRealTime;

        if(inGameTime >= 24f)
        {
            inGameTime -= 24f;
            NextWeek();
        }
        Hour = GetHour();
    }
    public int GetHour() => Mathf.FloorToInt(inGameTime);
    private void NextWeek()
    {
        switch (CurrentWeather.curWeather)
        {
            case Define.Weather.Clear:
                growPoint = 2f;
                hamPoint = -1;
                break;
            case Define.Weather.Rainy:
                growPoint = 2f;
                hamPoint = 1;
                break;
            case Define.Weather.Hail:
                growPoint = -1f;
                hamPoint = -10;
                break;
            case Define.Weather.Stormy:
                growPoint = 1f;
                hamPoint = -15;
                break;
            case Define.Weather.Cloudy:
                growPoint = 1f;
                hamPoint = -1;
                break;
        }

        if (_weatherQueue.Count < 2)
            SetWeather();

        CurrentWeather = _weatherQueue.Dequeue();

      

        foreach (var tile in Manager.Tile._tileEx)
        {
            if (!tile.Lock)
                continue;
            tile.SetGrowPoint(growPoint);
        }
    }
    private IEnumerator UpdateWater()
    {
        while (true)
        {
            foreach (var tile in Manager.Tile._tileEx)
            {
                if (!tile.Lock)
                    continue;

                tile.UpdateWater(hamPoint);
            }
            yield return new WaitForSeconds(1f);
        }
    }
    private void SetWeather()
    {
        while(_weatherQueue.Count <= 2)
        {
            WeatherDatas datas = new WeatherDatas();
            int cur = Manager.Random.GetRandomValue(percents);
            Define.Weather weather = weathers[cur];

            datas.curWeather = weather;
            datas.SetButketDatas();
            datas.Wea = Define.GetWeather(weather);
            datas.WeaExplation = Define.GetWeatherString(weather, datas.Wea, growPoint, hamPoint);

            _weatherQueue.Enqueue(datas);
        }
    }
    private static void Init()
    {
        if (_instance != null)
            return;

        GameObject go = GameObject.Find("@GameManager");
        if (go == null)
        {
            go = new GameObject("@GameManager");
            go.AddComponent<GameManager>();
        }
        _instance = go.GetComponent<GameManager>();
        DontDestroyOnLoad(go);


    }
}
public struct WeatherDatas
{
    public Define.Weather curWeather;
    Dictionary<Define.Crops, ButketDatas> weathers;
    public string Wea;
    public string WeaExplation;
    public void SetButketDatas()
    {
        if (weathers == null)
            weathers = new Dictionary<Define.Crops, ButketDatas>();

        foreach (Define.Crops crops in System.Enum.GetValues(typeof(Define.Crops)))
        {
            ButketDatas data = new ButketDatas();
            float value = Random.Range(0.3f, 2f);
            string str = Define.GetButketString(crops, value);

            data.Value = value;
            data.Explation = str;

            weathers.Add(crops, data);
        }
    }

    public ButketDatas GetButket(Define.Crops type)
    {
        return weathers[type];
    }
}
public struct ButketDatas
{
    public float Value;
    public string Explation;
}

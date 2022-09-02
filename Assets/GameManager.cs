using System;
using System.Collections;
using System.Collections.Generic;
using MyBox;
using UnityEngine;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update

    public static GameManager instance;
    public bool isPlay;
    public bool finish;
    public List<ConfigHouseEnemy> houseEnemys;
    float result;


	[Foldout(name:"Configs")]
       public int numberMaxCivis;
   


    public Text textTimeTreinoSlide;
    public Slider slideTime;

    //Variables Stats
    [SerializeField]
    private int civisDead;
    [SerializeField]
    private float enemiesDead;
    [SerializeField]
    private float totalShoots;
    [SerializeField]
    private float seconds=60f;
    [SerializeField]
    private float minutes=2;
    [SerializeField]
    private Text textTimeMinutes;
    [SerializeField]
    private Text textTimeSeconds;
    [SerializeField]
    private Text textCivil;
    [SerializeField]
    private Text textEnemies;
    [SerializeField]
    private Text textTotalShoots;
    [SerializeField]
    private Text textResult;

    [SerializeField]
    private GameObject painelFinish;

    [SerializeField]
    public int totalEnemies=3;

    public int enemiesInGame;

    private float minutesSave;
    private float secondsSave;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            isPlay = false;
        }
    }

    private void Start()
    {
        minutesSave = minutes;
        secondsSave = seconds;
    }


    public void AddCivilDead()
    {
        Debug.Log("CIVIL DEAD");
        civisDead++;
    }

    public void AddEnemyDead()
    {
        enemiesDead++;
    }
    public void AddShoot()
    {
        totalShoots++;
    }

    private void Update()
    {
        if (finish)
        {

            if (totalShoots > 0)
            {
               result  = (enemiesDead / totalShoots);
               result = result * 100;
            }

            textCivil.text = "Civis shoots: " + civisDead.ToString();
            textEnemies.text = "Enemies shoots: " + enemiesDead.ToString();
            textTotalShoots.text = "Total shoots: " + totalShoots.ToString();
            textResult.text = "Result: " + result.ToString("F2")+ "%";
            painelFinish.SetActive(true);
        }

		
        
    }


	
    public void SetTime(float _minutes)
	{
        minutes = _minutes;
        textTimeTreinoSlide.text = _minutes.ToString();

    }

    [ButtonMethod()]
    public void simular()
    {
        result  = enemiesDead / totalShoots;
        result = result * 100;
        Debug.Log("Result: " + result.ToString("F2") + "%");
    }

    private void FixedUpdate()
    {
        if (!isPlay)
            return;
        TimeGame();
    }

    public void TimeGame()
    {

        
        if (minutes < 1 && seconds <= 0)
        {
            seconds = 0;
            finish = true;
            isPlay = false;
            painelFinish.SetActive(true);
        }
        seconds -= Time.deltaTime;
        if (seconds <= 0 && minutes>0)
        {
            minutes--;
            seconds = 60;
        }

        textTimeMinutes.text = minutes.ToString("F0");
        textTimeSeconds.text = seconds.ToString("F2");

    }

    public void SearchHouses()
    {
        houseEnemys = new List<ConfigHouseEnemy>(GameObject.FindObjectsOfType<ConfigHouseEnemy>());
    }

    public ConfigHouseEnemy RandonHouse()
	{
        int random = UnityEngine.Random.Range(0, houseEnemys.Count);
        return houseEnemys[random];
	}

    // Update is called once per frame
    public void StartSimulation()
    {
        if (isPlay)
            return;

        isPlay = true;
        SearchHouses();
        for (int i = 0; i < houseEnemys.Count; i++)
        {
            houseEnemys[i].StartRespaws();
        }

       
    }

    public void RandomStart()
	{
         minutes = minutesSave;
         seconds = secondsSave;
    }

    public void StopSimulation()
    {
        isPlay = false;

    }
}

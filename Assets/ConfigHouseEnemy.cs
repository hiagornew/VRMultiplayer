using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ConfigHouseEnemy : MonoBehaviour
{
    [SerializeField]
    private GameObject[] enemysPoints;
    private List<Transform> positionEnemysPoints;
    [SerializeField]
    private GameObject[] enemytypes;
    [SerializeField]
    private GameObject[] civis;
    [SerializeField]
    private int numberCivis = 5;

    [SerializeField]
    private Transform target;

    public Transform[] civilWayPoint;

    [SerializeField]
    private float timeRandSpawEnemy;

  

    private void Start()
    {
        numberCivis = GameManager.instance.numberMaxCivis;
        positionEnemysPoints = new List<Transform>();
        target = GameObject.FindGameObjectWithTag("Player").transform;
        this.transform.position = new Vector3(this.transform.position.x, 0f, this.transform.position.z);
        StartCoroutine(DelaySetPosition());
    }

    IEnumerator DelaySetPosition()
	{
        yield return new WaitForSeconds(0.8f);
        this.transform.position = new Vector3(this.transform.position.x, 0f, this.transform.position.z);

    }

    public void StartRespaws()
    {
        /*for (int i = 0; i < enemysPoints.Length; i++)
        {
            positionEnemysPoints.Add(enemysPoints[i].transform);
        }
        RandomPointPosition();*/
        
        StartCoroutine(DelayRespaws());
        this.transform.position = new Vector3(this.transform.position.x, 0f, this.transform.position.z);
        Invoke("RandomEnemyTypesAndSpan", 2f);
    }

    private void OnEnable()
    {
        //RandomPointPosition();
        this.transform.position = new Vector3(this.transform.position.x, 0f, this.transform.position.z);
    }

    IEnumerator DelayRespaws()
    {
        RespawCivis();
        yield return new WaitForSeconds(3);
        for (int i = 0; i < enemysPoints.Length; i++)
        {
            positionEnemysPoints.Add(enemysPoints[i].transform);
        }
        RandomPointPosition();
    }

    public void RespawCivis()
    {

        for (int i = 0; i < numberCivis; i++)
        {
            var rand = Random.Range(0, civis.Length);
            int value = Random.Range(0, enemysPoints.Length);
            Instantiate(civis[rand], enemysPoints[value].transform.position,Quaternion.identity);
        }

    }

    public void RespawOneCivil()
    {
        StartCoroutine(DelayRespawCivil());
    }

    IEnumerator DelayRespawCivil()
    {
        yield return new WaitForSeconds(5f);
        var rand = Random.Range(0, civis.Length);
        int value = Random.Range(0, enemysPoints.Length);
        Instantiate(civis[rand], enemysPoints[value].transform.position,Quaternion.identity);

    }

    public void RandomEnemyTypesAndSpan()
    {
        StartCoroutine(DelayRespaw());
    }

    IEnumerator DelayRespaw()
    {
        timeRandSpawEnemy = Random.RandomRange(1, 8);
        yield return new WaitForSeconds(timeRandSpawEnemy);
        var rand = Random.Range(0, enemytypes.Length);
        int value = Random.Range(0, enemysPoints.Length);
        Instantiate(enemytypes[rand], enemysPoints[value].transform.position,Quaternion.identity);
    }

    public void RandomPointPosition()
    {
        

        for (int i = 0; i < enemysPoints.Length; i++)
        {
            enemysPoints[i].SetActive(false);
        }
        int value = Random.Range(0, enemysPoints.Length);

       enemysPoints[value].SetActive(true);

        GameManager.instance.enemiesInGame++;
       
    }

    public void ResetPosition(GameObject enemy)
    {
        enemy.transform.position =  Vector3.zero;
    }
}

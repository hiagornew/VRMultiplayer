using System;
using System.Collections;
using System.Collections.Generic;
using EnemyAI;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class CivilChanges : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private List<Transform> wayPoints;

    [SerializeField]
    private ConfigHouseEnemy[] Houses;

    private StateController _stateController;

    private void Start()
    {
        Houses = GameObject.FindObjectsOfType<ConfigHouseEnemy>();
        _stateController = GetComponent<StateController>();
        wayPoints = new List<Transform>();
        Invoke("SetWayPoints",0.2f);
    }

    public void SetWayPoints()
    {
        int index1;
        int index2;
        int lenght;
        for (int i = 0; i < Houses.Length; i++)
        {
            for (int j = 0; j < Houses[i].civilWayPoint.Length; j++)
            {
                wayPoints.Add(Houses[i].civilWayPoint[j]);
            }

        }

        for (int i = 0; i < wayPoints.Count+1; i++)
        {
            index1 = (Random.Range(0, wayPoints.Count) );
            index2 = (Random.Range(0, wayPoints.Count) );
            while (index1 == index2)
            {
                index2 = (Random.Range(0, wayPoints.Count) );
            }
            Debug.Log("Index 1: " + index1);
            Debug.Log("Index 2: " + index2);
            var backup = wayPoints[index1];
            wayPoints[index1] = wayPoints[index2];
            wayPoints[index2] = backup;
        }

        _stateController.patrolWayPoints = wayPoints;

    }
}

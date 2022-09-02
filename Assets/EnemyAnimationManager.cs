using System;
using System.Collections;
using System.Collections.Generic;
//using System.Runtime.Remoting.Messaging;
using UnityEngine;

public class EnemyAnimationManager : MonoBehaviour
{

    [SerializeField]
    private GameObject animWalk;
    [SerializeField]
    private GameObject animLower;
    [SerializeField]
    private GameObject animIdle;
    [SerializeField]
    private GameObject animRun;
    public enum EstateHuman{ Walk, lower,Idle,Run}
    public EstateHuman myType;

    private void Start()
    {
        DelayFunction(1,"InitializeEnemy");
    }

    public void DelayFunction(float time, string method)
    {
        Invoke(method,time);

    }

    public GameObject GetActive()
    {
        List<GameObject> objs = new List<GameObject>();
        for (int i = 0; i < transform.childCount; i++)
        {
            objs.Add(this.transform.GetChild(i).gameObject);
        }

        for (int i = 0; i < objs.Count; i++)
        {
            if (objs[i].activeInHierarchy)
            {
                return objs[i];
            }
        }

        return null;

    }

    public void InitializeEnemy()
    {

        switch (myType)
        {
            case EstateHuman.Walk:
                SetAnimationWalk();
                break;
            case EstateHuman.lower:
                SetAnimationLower();
                break;
            case EstateHuman.Idle:
                SetAnimationIdle();
                break;
            case EstateHuman.Run:
                SetAnimationRun();
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

   
    public void SetAnimationWalk()
    {
        
        animWalk.SetActive(true);
        animLower.SetActive(false);
        animIdle.SetActive(false);
        animRun.SetActive(false);
    }
    public void SetAnimationLower()
    {
        
        animWalk.SetActive(false);
        animLower.SetActive(true);
        animIdle.SetActive(false);
        animRun.SetActive(false);
    }
    public void SetAnimationIdle()
    {
        animRun.SetActive(false);
        animWalk.SetActive(false);
        animLower.SetActive(false);
        animIdle.SetActive(true);
    }
    public void SetAnimationRun()
    {
        animRun.SetActive(true);
        animWalk.SetActive(false);
        animLower.SetActive(false);
        animIdle.SetActive(false);
    }
    
}


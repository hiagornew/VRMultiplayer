using System;
using System.Collections;
using System.Collections.Generic;
using MyBox;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Playables;
using Random = UnityEngine.Random;


public class CivilManager : MonoBehaviour
{

    [SerializeField]
    private NavMeshAgent agent;

    [Header("Animations")]
    [SerializeField]
    private Animator anim;
    [SerializeField]
    private PlayableDirector director;

    [SerializeField]
    private Transform targetPlayer;
    [SerializeField]
    private ConfigHouseEnemy[] Houses;
    [SerializeField]
    private List<Transform> targets;

    //Privates
    private bool isWalking=false;
    private CivilHealth _civilHealth;
    private int valueOldrand;
    //Walking2

    // Start is called before the first frame update
    void Start()
    {
        _civilHealth = GetComponent<CivilHealth>();
        targets = new List<Transform>();
        targetPlayer = GameObject.FindGameObjectWithTag("muzzle").transform;
        Houses = GameObject.FindObjectsOfType<ConfigHouseEnemy>();
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        anim.Play("Idle");
        valueOldrand = -1;
    }

    private void OnEnable()
    {
       Invoke("SetTargets",2f);
    }

    private void OnDisable()
    {
        targets = new List<Transform>();
    }


    // Update is called once per frame
    void Update()
    {

       /* if (isWalking && _civilHealth.death==false)
        {
            if (agent.pathStatus == NavMeshPathStatus.PathComplete && agent.remainingDistance<=agent.stoppingDistance)
            {

                isWalking = false;
                //anim.Play("Idle");
                Invoke("AnimIdle",1f);
                Invoke("WalkingToTarget",2f);
            }
        }*/
    }

    public void AnimIdle()
    {
        anim.Play("Idle");
    }

    public void WalkingToTarget()
    {
        int rand = Random.Range(0, targets.Count);
        if (valueOldrand == rand)
        {
            if (rand == 0)
            {
                rand++;
            }
            else if (rand == targets.Count-1)
            {
                rand--;
            }
        }
        valueOldrand = rand;
        agent.destination = targets[rand].position;
        isWalking = true;
        anim.Play("Walking2");
    }

    public void SetTargets()
    {

        for (int i = 0; i < Houses.Length; i++)
        {
            targets.Add(Houses[i].transform);
        }
        targets.Add(targetPlayer);
        Invoke("WalkingToTarget",2f);
    }

}

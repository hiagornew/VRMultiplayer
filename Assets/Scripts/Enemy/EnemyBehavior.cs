using System;
using System.Collections;
using System.Collections.Generic;
using MyBox;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Playables;

public class EnemyBehavior : MonoBehaviour
{

    [SerializeField]
    private Transform target;
    [SerializeField]
    private GameObject render;

    public float distanceToTarget;
    public float distanceToWalk=5f;
    public float speed;

    public enum TypeEnemy{ Windowns, DoorLeft,DoorRight}
    public TypeEnemy myType;

    [Header("Config Cine")]
    public EnemyAnimationManager animationManager;

    [Header("Animations")]
    [SerializeField]
    private Animator anim;
    [SerializeField]
    private PlayableDirector director;

    [SerializeField]
    private NavMeshAgent agent;
    [SerializeField]
    private ConfigHouseEnemy configHouse;


    // Start is called before the first frame update
    void Start()
    {
        speed = 2;
        target = GameObject.FindWithTag("Player").transform;
        anim = GetComponent<Animator>();
        GetDirector();
        //director = GetComponent<PlayableDirector>();
        agent = GetComponent<NavMeshAgent>();
    }

    public void GetDirector()
    {
        var obj = animationManager.GetActive();
        director = obj.GetComponent<PlayableDirector>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckDistance();

        /*if (Input.GetKeyDown(KeyCode.A))
        {
            if (anim == null)
            {
                anim = GetComponent<Animator>();
            }
            director.Stop();
            anim.Play("Death");
        }*/
    }

    private void CheckDistance()
    {
        if (target == null)
        {
            target = GameObject.FindWithTag("Player").transform;
        }

        float dist = Vector3.Distance(this.gameObject.transform.position, target.position);
        if (dist < distanceToTarget)
        {
            render.SetActive(true);
        }
        else
        {
            render.SetActive(false);
        }

        if (dist > distanceToWalk )
        {
            if (myType != TypeEnemy.Windowns)
            {
                WalkToPlayer();
            }
        }else if (dist <= distanceToWalk)
        {
            //animationManager.SetAnimationIdle();
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Bullet"))
        {
            GetDirector();
            if (anim == null)
            {
                anim = GetComponent<Animator>();
            }
            director.Stop();
            anim.Play("Death");
        }
    }


    public void WalkToPlayer()
    {
        var targetPlayer = new Vector3(target.transform.position.x, 0, target.transform.position.z);
        transform.LookAt(targetPlayer);
        //this.transform.Translate(Vector3.forward * speed * Time.deltaTime);
        animationManager.SetAnimationWalk();
        agent.SetDestination(target.transform.position);

    }

    [ButtonMethod()]
    public void HIT()
    {
        GetDirector();
        if (anim == null)
        {
            anim = GetComponent<Animator>();
        }
        director.Stop();
        anim.Play("Death");
        StartCoroutine(ResetEnemy());
    }

    IEnumerator ResetEnemy()
    {
        yield return new WaitForSeconds(3);
        this.transform.localPosition = new Vector3(0, 0, 0);
        configHouse.RandomPointPosition();

    }
}

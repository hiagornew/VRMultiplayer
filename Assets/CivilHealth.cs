using System;
using System.Collections;
using System.Collections.Generic;
using MyBox;
using UnityEngine;
using UnityEngine.AI;
using Object = UnityEngine.Object;

public class CivilHealth : HealthManager
{

    [Header("Animations")]
    [SerializeField]
    private Animator anim;
    [Tooltip("The game object particle emitted when hit.")]
    public GameObject bloodSample;

    public bool death;
    [SerializeField]
    private ConfigHouseEnemy configHouse;
    private void Start()
    {
        anim = GetComponent<Animator>();
        anim.Play("Idle");
        configHouse = GameObject.FindObjectOfType<ConfigHouseEnemy>();
    }

    [ButtonMethod()]
    public override void TakeDamage(Vector3 location, Vector3 direction, float damage, Collider bodyPart = null, GameObject origin = null)
    {
        Object.Instantiate<GameObject>(bloodSample, location, Quaternion.LookRotation(-direction), this.transform);
        Death();
    }

    [ButtonMethod()]
    public void Death()
    {
        GameManager.instance.AddCivilDead();
        death = true;
        if (anim == null)
        {
            anim = GetComponent<Animator>();
        }
        anim.Play("Death");
        Destroy(this.GetComponent<NavMeshAgent>());
        Invoke("RemoveAllForces",2f);
        Destroy(this.gameObject,4f);

    }

    private void RemoveAllForces()
    {
        if (configHouse != null)
        {
            configHouse.RespawOneCivil();
        }
        else
        {
            configHouse = GameObject.FindObjectOfType<ConfigHouseEnemy>();
        }
        foreach (Rigidbody member in GetComponentsInChildren<Rigidbody>())
        {
            member.isKinematic = false;
            member.velocity = Vector3.zero;
        }
        anim.enabled = false;
    }




}

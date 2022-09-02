using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHit : MonoBehaviour
{
    [SerializeField]
    private EnemyBehavior enemy;


    public void HIT()
    {
        enemy.HIT();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletControl : MonoBehaviour
{
   /* [SerializeField, Range(1, 30)]
    private float raycastCheckUnits = 5.0f;

    [SerializeField]
    private Vector3 enemyINpulseOnHit = Vector3.forward * 5f;

    [SerializeField]
    private LayerMask whatToHit = -0;

    private void FixedUpdate()
    {
        RaycastHit hit;
        if(Physics.Raycast(transform.position,transform.TransformDirection(Vector3.forward), out hit, raycastCheckUnits,whatToHit))
        {
           
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
        }
    }*/

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("ACertou");
        }
    }
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public enum Botao { X, Y, A, B, AtrazL, AtrazR, LadoL, LadoR };
    public GameObject bulletPrefab;
    public GameObject decal;
    public GameObject pivotWeapon;
    private Vector3 positionBullet;
    public float timeRespaw;
    private bool canShoot;
    public float floatInFrontOfWall = 0.001f;
    public AudioSource source;
    public AudioClip shoot;
    public Animator gunAnimator;
    public GameObject luzarma;

    [SerializeField, Range(1, 60)]
    private float raycastCheckUnits = 5.0f;

    [SerializeField]
    private Vector3 enemyINpulseOnHit = Vector3.forward * 5f;

    [SerializeField]
    private LayerMask whatToHit = -0;

    [SerializeField]
    private Botao botaoAtirar;

    private void Start()
    {
        canShoot = true;
    }
    private void Update()
    {
        positionBullet = pivotWeapon.transform.position;
        if (canShoot)
        {
            if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger) || OVRInput.GetDown(OVRInput.Button.Three) || Input.GetMouseButtonDown(0))
            {
                if (canShoot)
                {
                    /*GameObject bullet = Instantiate(bulletPrefab, positionBullet, pivotWeapon.transform.rotation);
                    Rigidbody bulletBody = bullet.GetComponent<Rigidbody>();
                    bulletBody.AddForceAtPosition(pivotWeapon.transform.forward * 100, pivotWeapon.transform.position, ForceMode.Impulse);
                    Object.Destroy(bullet, 2);*/
                    source.PlayOneShot(shoot);
                    gunAnimator.SetTrigger("Fire");
                    luzarma.SetActive(true);
                    StartCoroutine(DelayLuz());
                    RaycastHit hit;
                    if (Physics.Raycast(pivotWeapon.transform.position, pivotWeapon.transform.TransformDirection(Vector3.forward), out hit, 60))
                    {

                        /*if (hit.transform.gameObject.CompareTag("Enemy") )
                        {
                            Debug.Log("Enemy HIT");
                            hit.transform.GetComponent<EnemyHit>().HIT();
                        }*/


                        if ( hit.transform.gameObject.CompareTag("Box"))
                        {
                            //Rigidbody enemy = hit.transform.GetComponent<Rigidbody>();

                            GameObject particleClone = (GameObject)Instantiate(decal, hit.point, Quaternion.LookRotation(hit.normal));
                            if (hit.transform.GetComponent<Animator>().isActiveAndEnabled)
                            {
                                hit.transform.GetComponent<Animator>().SetTrigger("Hit");
                                particleClone.transform.SetParent(hit.transform.GetChild(0).GetChild(0).GetChild(0).transform);
                            }
                            else
                            {
                                particleClone.transform.SetParent(hit.transform);

                            }
                                
                           
                           
                            // enemy.AddForceAtPosition(enemyINpulseOnHit, hit.transform.position, ForceMode.Impulse);
                           
                        }
                        else if ( hit.transform.gameObject.CompareTag("Box"))
                        {
                            GameObject particleClone = (GameObject)Instantiate(decal, hit.point, Quaternion.LookRotation(hit.normal));
                            particleClone.transform.SetParent(hit.transform);
                            //LookAtPlayer.morte = true;
                        }
                        //Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
                    }

                    canShoot = false;
                    StartCoroutine(DelayShoot());

                }

            }
        }
       
    }

    IEnumerator DelayShoot()
    {
        yield return new WaitForSecondsRealtime(0.1f);
        canShoot = true;
        StopCoroutine(DelayShoot());
    }

    IEnumerator DelayLuz()
    {
        yield return new WaitForSecondsRealtime(0.1f);
        luzarma.SetActive(false);
    }
}

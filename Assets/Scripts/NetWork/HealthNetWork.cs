using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.InputSystem;
using TMPro;
public class HealthNetWork : MonoBehaviourPunCallbacks , IPunObservable
{

    [SerializeField]
    private float health;
    [SerializeField]
    private HealthManager healthManager;
	 
    

	[SerializeField]
	InputManagerXR inputManagerXR;

    private bool triggerLeftDetect;



	private PhotonView photonView;
	public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        //Sync Health
        if (stream.IsWriting)
        {
            stream.SendNext(health);
            
        }
        else
        {
            health = (int)stream.ReceiveNext();
			
		}
    }

    public void TakeDamage(int damageValue)
    {
        health -= damageValue;

	}

    // Start is called before the first frame update
    void Start()
    {

        photonView = GetComponent<PhotonView>();


	}

    // Update is called once per frame
    void Update()
    {
		if (!photonView.IsMine)
			return;
		if (triggerLeftDetect)
        {
            photonView.RPC("TakeDamageAndUpdateUI", RpcTarget.All);
			

		}
        triggerLeftDetect = inputManagerXR.GetTriggerLeft();

	}

    [PunRPC]
       void TakeDamageAndUpdateUI()
    {
		//health -= damageValue;
        GameManagerNetWork.instance.point1++;
		
	}

	
}

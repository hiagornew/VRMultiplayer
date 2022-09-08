using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;

public class GameManagerNetWork : MonoBehaviourPunCallbacks
{

    [SerializeField]
    private TMP_Text textPoint1;
	[SerializeField]
	private TMP_Text textPoint2;

	public static GameManagerNetWork instance;

    public  int point1;
	public  int point2;

	private void Awake()
    {
       if(instance == null)
        {
            instance = this;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        SetScore();

	}

    public void SetScore()
    {
		textPoint1.text = point1.ToString();
		
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ControlePivot : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject pivot;
    public GameObject maodireita;
    public GameObject arma;
    public GameObject mira;

    [Header("Botao para ativar o Lazer")]
    public OVRInput.Button botaoLazer;
    [Header("Botao para ativar a Calibracao")]
    public OVRInput.Button botaoCalibracao;

    // Update is called once per frame

    public bool terminaCalibracao = true;
    private bool mostraMira;

    public Text textoCalibracao;


    private Vector3 posMao;
    private Vector3 posMaoEsquerda;

    public Vector3 rotacaoDireita;
    public GameObject weapon;
    private Vector3 rotacaoAuxArma;
    private bool calibrando;

    public static ControlePivot instance;

    private Vector3 GuardaPosMao;
    private Quaternion GuardaRotArma;
    Vector3 targetPosition;

    [Header("UI")]
    [SerializeField]
    private GameObject canvasCalibração;
    [SerializeField]
    private GameObject canvasNormal;

    private void Awake()
    {
        if(instance == null || instance!=this)
        {
            instance = this;
        }
    }
    private void Start()
    {
       // terminaCalibracao = true;
        mostraMira = false;
        calibrando = false;
        terminaCalibracao = true;
        if (PlayerPrefs.HasKey("pivotPosX"))
        {
            StartCoroutine(DelayPegaCalibracao());
		}
		else
		{
            ResetPivot();

        }

    }

	[ContextMenu("Reset Arma")]
    public void ResetPivot()
	{
        arma.transform.position = maodireita.transform.position;
    }
    void Update()
    {

        if (calibrando)
        {
             targetPosition = new Vector3(weapon.transform.position.x, weapon.transform.position.y, weapon.transform.position.z);


            //posMao = maodireita.transform.position;

            arma.transform.LookAt(targetPosition);
            arma.transform.localRotation = Quaternion.Euler(arma.transform.localEulerAngles.x, arma.transform.localEulerAngles.y, arma.transform.localEulerAngles.z + 7);
            GuardaRotArma = arma.transform.localRotation;
        }

        if (terminaCalibracao == false)
        {
            textoCalibracao.text = "Calibração ON";
        }
        else
        {
            textoCalibracao.text = "Calibração OFF";
        }
        if (mostraMira)
        {
            mira.SetActive(true);
        }
        else
        {
            mira.SetActive(false);
        }
        if (OVRInput.GetDown(botaoCalibracao))
        {
            terminaCalibracao = !terminaCalibracao;
        }
        if (OVRInput.GetDown(botaoLazer))
        {
            mostraMira = !mostraMira;
        }
        if (!terminaCalibracao)
        {
            canvasCalibração.SetActive(true);
            canvasNormal.SetActive(false);

            if (OVRInput.GetDown(OVRInput.Button.SecondaryIndexTrigger))
            {
                //Controle de Ajuste de Pivo

                arma.transform.position = maodireita.transform.position;

                GuardaPosMao = new Vector3(maodireita.transform.position.x, maodireita.transform.position.y, maodireita.transform.position.z);
                //arma.transform.position = new Vector3(pivot.transform.position.x, pivot.transform.position.y, pivot.transform.position.z);

            }
            if (OVRInput.GetDown(OVRInput.Button.SecondaryHandTrigger) || Input.GetKeyDown(KeyCode.V))
            {

                calibrando = true;
               
                Debug.Log("ON");
            }

            if (OVRInput.GetUp(OVRInput.Button.SecondaryHandTrigger) || Input.GetKeyDown(KeyCode.B))
            {
                GuardaCalibracao();
                calibrando = false;
                terminaCalibracao = true;
               
                Debug.Log("OFF");
            }
		}
		else
		{
            canvasCalibração.SetActive(false);
            canvasNormal.SetActive(true);
        }

    }

	[ContextMenu("Salva Calibração")]
    public void GuardaCalibracao()
    {
        //posicao do pivot
        PlayerPrefs.SetFloat("pivotPosX", targetPosition.x);
        PlayerPrefs.SetFloat("pivotPosY", targetPosition.y);
        PlayerPrefs.SetFloat("pivotPosZ", targetPosition.z);

        //rotacao do pivo
        PlayerPrefs.SetFloat("pivotRotX", arma.transform.localEulerAngles.x);
        PlayerPrefs.SetFloat("pivotRotY", arma.transform.localEulerAngles.y);
        PlayerPrefs.SetFloat("pivotRotZ", arma.transform.localEulerAngles.z);

        //posicaoArma
        PlayerPrefs.SetFloat("pivotPosArmaX", arma.transform.localPosition.x);
        PlayerPrefs.SetFloat("pivotPosArmaY", arma.transform.localPosition.y);
        PlayerPrefs.SetFloat("pivotPosArmaZ", arma.transform.localPosition.z);

        //posicaoMaoEsquerda
        PlayerPrefs.SetFloat("pivotPosArmaEsquerdaX", posMaoEsquerda.x);
        PlayerPrefs.SetFloat("pivotPosArmaEsquerdaY", posMaoEsquerda.y);
        PlayerPrefs.SetFloat("pivotPosArmaEsquerdaZ", posMaoEsquerda.z);

        //RotacaoAtma
        PlayerPrefs.SetFloat("pivotRotArmaX", arma.transform.localRotation.x);
        PlayerPrefs.SetFloat("pivotRotArmaY", arma.transform.localRotation.y);
        PlayerPrefs.SetFloat("pivotRotArmaZ", arma.transform.localRotation.z);


       

    }

    IEnumerator DelayPegaCalibracao()
    {
        yield return new WaitForSecondsRealtime(1);
        Debug.Log("Tem Calibração");
        PegaCalibracao();
    }

	
    public void PegaCalibracao()
    {
        arma.transform.position = maodireita.transform.position;
        Vector3 aux;
        Vector3 aux1;
        Vector3 aux2;
        Vector3 aux3;

        float x;
        float y;
        float z;

        float x1;
        float y1;
        float z1;

        float x2;
        float y2;
        float z2;

        float x3;
        float y3;
        float z3;

        x1 = PlayerPrefs.GetFloat("pivotPosX");
        y1 = PlayerPrefs.GetFloat("pivotPosY");
        z1 = PlayerPrefs.GetFloat("pivotPosZ");

        aux1 = new Vector3(x1, y1, z1);
        arma.transform.LookAt(aux1);

        /*x3 = PlayerPrefs.GetFloat("pivotPosArmaX");
        y3 = PlayerPrefs.GetFloat("pivotPosArmaY");
        z3 = PlayerPrefs.GetFloat("pivotPosArmaZ");
        posArma = new Vector3(x3, y3, z3);
        arma.transform.position = posArma;*/

        x2 = PlayerPrefs.GetFloat("pivotRotArmaX");
        y2 = PlayerPrefs.GetFloat("pivotRotArmaY");
        z2 = PlayerPrefs.GetFloat("pivotRotArmaZ");
        aux2 = new Vector3(x2, y2, z2);
        arma.transform.localRotation = Quaternion.Euler(aux2.x, aux2.y, aux2.z);

        x3 = PlayerPrefs.GetFloat("pivotRotX");
        y3 = PlayerPrefs.GetFloat("pivotRotY");
        z3 = PlayerPrefs.GetFloat("pivotRotZ");
        aux3 = new Vector3(x3, y3, z3);
        arma.transform.localRotation = Quaternion.Euler(aux3.x, aux3.y, aux3.z);

        x = PlayerPrefs.GetFloat("pivotPosArmaX");
        y = PlayerPrefs.GetFloat("pivotPosArmaY");
        z = PlayerPrefs.GetFloat("pivotPosArmaZ");
        aux = new Vector3(x, y, z);
        arma.transform.localPosition = aux;

        Debug.Log("Pegou calibração: " + x +"," + y+ ","+ z);

        /* x4 = PlayerPrefs.GetFloat("pivotPosArmaEsquerdaX");
         y4 = PlayerPrefs.GetFloat("pivotPosArmaEsquerdaY");
         z4 = PlayerPrefs.GetFloat("pivotPosArmaEsquerdaZ");
         posMaoEsquerda = new Vector3(x4, y4, z4);

         maoEsquerda.transform.position = posMaoEsquerda;*/

        //arma.transform.rotation = pivot.transform.rotation;
        //pivot.transform.position = arma.transform.position;
    }
}

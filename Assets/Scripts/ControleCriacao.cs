using System.Collections;
using System.Collections.Generic;
using MyBox;
using UnityEngine;
using UnityEngine.UI;


public class ControleCriacao : MonoBehaviour
{
    // Start is called before the first frame update
    [Header("Teleporte")]
    public VRTeleporter teleporter;

    [Header("Salvamento dos Objetos")]
    public GuardaObjetos guardaObjetos;

    [Header("Teste De Escala e rotacao")]
    public GameObject pontoA;
    public GameObject pontoB;
    public GameObject pontoC;
    public GameObject cuboTeste;
    private GameObject targetPosition;

    [Header("Prefabs Box")]
    public GameObject prefabBox;

    [Header("Prefabs Alvo")]
    public GameObject prefabAlvo;
    public GameObject cabeca;

    [Header("Prefabs Casa")]
    public GameObject prefabCasa;
    

    public GameObject maoDireita;
    public GameObject pontoSelecao;
    
    private Vector3 posmaoDireita;
    private int numeroCliques;
    private bool clicado;
    public Vector3 localScale;
    public GameObject pivotWeapon;

    //Privates
    private GameObject auxAlvo;
    public GameObject auxQuadrado;
    //privates bool
    [SerializeField]
    private bool objSelecionado;
    private bool objetoSolto;
    private bool objPodeScalar;
    private bool rotaciona;
    private bool setpos1;
    private bool setpos2;
    private bool selecionado;
    private bool clicou;

    //privates float
    private float numRotDireita;
    private float numRotEsquerda;
    private float valorRotYalvo;
    private float valorXCalculoEscala;
    private float valorYCalculoEscala;
    private float valorZCalculoEscala;
    private float valorXCalculoPosicao;
    private float valorYCalculoPosicao;
    private float valorZCalculoPosicao;
    private float distanceHit;

    //private int
    private int contadorNumeroNomes;
    private int contadorNumeroCasas;
    private int contadorNumeroParedes;


    //privates vector
    private Vector3 posicaoInicial;
    private Vector3 posicalFinal;
    private Vector3 auxMao;
    private Vector3 auxRot;
    private Vector3 ultimaPosicaoMarcador;
    private Transform auxLook;

    

    //private string
    private string nameObjSelecionado;
    private string nameObjetoCriado;
    

    public Text texto;
    public Text textoPosicao1;
    public Text textoPosicao2;

    [Header("Materiais")]
    public Material matCubo;
    public Material matSelecionaCubo;

    [Header("Audios")]
    public AudioSource source;
    public AudioClip clip;

    [Header("Calculo distancia")]
    public Vector3 posicao1;
    public Vector3 posicao2;

    public Vector3 posicaoFinal;
    public Vector3 escalaFinal;

    public GameObject miraExluir;
    
    public GameObject controleDireito;

    [Header("Criaçãode Linhas")]
    public LineRenderer linha;
    public GameObject linhaObj;
    public Material linhaMaterialSelecionado;
    public Material linhaMaterialDeletar;

    private GameObject casaAtual;

    private void Start()
    {
        distanceHit = 60;
         auxAlvo = new GameObject();
        targetPosition = new GameObject();
        numRotDireita = 0.5f;
        numRotEsquerda = 0.5f;
        objSelecionado = false;
        objPodeScalar = false;
        rotaciona = false;
        clicado = false;
        setpos1 = false;
        setpos2 = false;
        selecionado = false;
        clicou = false;
        //CalculaPosicoes();
        //CalculoEscala();
        numeroCliques = 0;
        teleporter.ToggleDisplay(false);
        linha.SetPosition(0, linha.transform.position);
       

    }

    public void CriaChao()
    {
        var vertices = new Vector3[4]
        {
            new Vector3(posicao1.x, posicao1.y, posicao1.z),
            new Vector3(posicao1.x, posicao1.y, posicao2.z),
            new Vector3(posicao2.x, posicao1.y, posicao1.z),
            new Vector3(posicao2.x, posicao1.y, posicao2.z)
        };
      /*  posicao1 = new Vector3(0, 0.5f, 1);
        posicao2 = new Vector3(2, 0.5f, 3);
        var vertices = new Vector3[4]
        {
            

            new Vector3(posicao1.x, posicao1.y, posicao1.z),
            new Vector3(posicao1.x, posicao1.y, posicao2.z),
            new Vector3(posicao2.x, posicao1.y, posicao1.z),
            new Vector3(posicao2.x, posicao1.y, posicao2.z)
        };*/


        auxQuadrado.GetComponent<MeshFilter>().mesh.vertices = vertices;
        auxQuadrado.transform.position = posicao1;
    }

    public void TesteCalculoRotacao()
    {
        cuboTeste.transform.position = pontoA.transform.position;
        //Calculo Escala
        float valorX = Mathf.Sqrt(((pontoB.transform.position.z - pontoA.transform.position.z) * (pontoB.transform.position.z - pontoA.transform.position.z)) +
            ((pontoB.transform.position.x - pontoA.transform.position.x) * (pontoB.transform.position.x - pontoA.transform.position.x)));
        escalaFinal.x = valorX;
        float valorY = (pontoB.transform.position.y - pontoA.transform.position.y);
        escalaFinal.y = valorY;
        float valorZ = 0.5f;
        escalaFinal.z = valorZ;
        cuboTeste.transform.localScale = escalaFinal;
        //Fim calculo Escala
        



        if (Input.GetKeyDown(KeyCode.A))
        {
            Vector3 auxPos = new Vector3(pontoB.transform.position.x, pontoC.transform.position.y, pontoB.transform.position.z);
           
             pontoC.transform.LookAt(auxPos);
            Debug.Log(pontoC.transform.rotation.eulerAngles.y);
            pontoC.transform.rotation = Quaternion.Euler(pontoC.transform.rotation.eulerAngles.x, pontoC.transform.rotation.eulerAngles.y-90, pontoC.transform.rotation.eulerAngles.z);
           
        }
    }

    public void CalculaPosicoes()
    {
        //Calculo valor X
       // float valorX = (posicao2.x - posicao1.x)/2 ;
        posicaoFinal.x = posicao1.x;

        //Calculo valor Y
        //float valorY = (posicao2.y - posicao1.y) /2;
        posicaoFinal.y =  posicao1.y ;

        //Calculo valor Z
       // float valorZ = (posicao2.z - posicao1.z)/2;
        posicaoFinal.z = posicao1.z;

        


    }

    IEnumerator DelayCalculaEscala()
    {
        yield return new WaitForSecondsRealtime(0.1f);
         CalculoEscala();
       // CalculoEscalaChao();
    }

    

    public void CalculoEscala()
    {

        valorXCalculoEscala = Mathf.Sqrt(((posicao2.z - posicao1.z) * (posicao2.z - posicao1.z)) +
           ((posicao2.x - posicao1.x) * (posicao2.x - posicao1.x)));
        escalaFinal.x = valorXCalculoEscala;
         valorYCalculoEscala = (posicao2.y - posicao1.y);
        escalaFinal.y = valorYCalculoEscala;
         valorZCalculoEscala = 0.05f;
        escalaFinal.z = valorZCalculoEscala;

        CriaCubo();
    }

    public void CalculoEscalaChao()
    {

        valorXCalculoEscala = (posicao2.x - posicao1.x);
        escalaFinal.x = valorXCalculoEscala;
        valorYCalculoEscala = 0.05f;
        escalaFinal.y = valorYCalculoEscala;
        valorZCalculoEscala = Mathf.Sqrt(((posicao2.z - posicao1.z) * (posicao2.z - posicao1.z)) +
           ((posicao2.x - posicao1.x) * (posicao2.x - posicao1.x)));
        escalaFinal.z = -valorZCalculoEscala;

         CriaCubo();
        //CriaChao();
    }

    private void Update()
    {
        // CriarLinha();
        //TesteCalculoRotacao();
        if (Input.GetKeyDown(KeyCode.Y))
        {
            CriaChao();
        }

         if(ControleCanvasSelecao.instance.deletarLigado == true || ControleCanvasSelecao.instance.tipoEscolha == ControleCanvasSelecao.TipoCriacao.Selecionar)
         {
            linhaObj.SetActive(true);
            CriarLinha();

        }
         else
         {
            linhaObj.SetActive(false);
         }

        if (OVRInput.GetDown(OVRInput.Button.SecondaryIndexTrigger) && ControleCanvasSelecao.instance.deletarLigado == true)
        {
            Deletar();
        }
       /*     if (OVRInput.GetDown(OVRInput.Button.One) && ControleCanvasSelecao.instance.deletarLigado == true)
        {
            Deletar();
            //ScalaCuboMaisX();
        }*/
        if (OVRInput.GetDown(OVRInput.Button.Two))
        {
           // ScalaCuboMenosX();
        }
        if (OVRInput.GetDown(OVRInput.Button.SecondaryIndexTrigger) && ControleCanvasSelecao.instance.deletarLigado == false && ControleCanvasSelecao.instance.tipoEscolha != ControleCanvasSelecao.TipoCriacao.Selecionar)
        {
            //CriaCubo();
            if (ControlePivot.instance.terminaCalibracao)
            {
                //source.PlayOneShot(clip);
                numeroCliques++;
                clicou = true;
            }
        }
        if (ControleCanvasSelecao.instance.tipoEscolha == ControleCanvasSelecao.TipoCriacao.Salvar && ControleCanvasSelecao.instance.salvo)
        {
           
            guardaObjetos.SalvarCena();
            ControleCanvasSelecao.instance.salvo = false;
            source.PlayOneShot(clip);

        }
        if (OVRInput.GetDown(OVRInput.Button.SecondaryIndexTrigger) && !objSelecionado && ControleCanvasSelecao.instance.tipoEscolha == ControleCanvasSelecao.TipoCriacao.Selecionar)
        {
            objSelecionado = true;
            objetoSolto = false;
            selecionado = true;
        }
        if (OVRInput.GetUp(OVRInput.Button.SecondaryIndexTrigger) && ControleCanvasSelecao.instance.tipoEscolha == ControleCanvasSelecao.TipoCriacao.Selecionar)
        {
            objSelecionado = false;
            objPodeScalar = false;
            objetoSolto = true;
            selecionado = false;
            linhaObj.SetActive(false);
            miraExluir.SetActive(false);
        }
        if (OVRInput.Get(OVRInput.Button.SecondaryThumbstickRight))
        {
            //rotaciona = true;
            RotacionaDireita();
            
        }
        if (OVRInput.Get(OVRInput.Button.SecondaryThumbstickLeft))
        {
            // rotaciona = true;
            RotacionaEsquerda();
           
        }

        #region CriaAlvo Com Angulo
        if (OVRInput.GetDown(OVRInput.Button.SecondaryIndexTrigger) && ControleCanvasSelecao.instance.tipoEscolha == ControleCanvasSelecao.TipoCriacao.Alvo && ControleCanvasSelecao.instance.deletarLigado == false)
        {
            teleporter.ToggleDisplay(true);
            GameObject alvo = Instantiate(prefabAlvo);
            teleporter.bodyTransforn = alvo.transform;
            // alvo.transform.LookAt(targetPosition);
            alvo.name = "alvo" + contadorNumeroNomes + Random.Range(0,50);
            nameObjetoCriado = alvo.name;
            guardaObjetos.AdicionarAlvo(alvo);
            
        }

        if (OVRInput.GetUp(OVRInput.Button.SecondaryIndexTrigger) && ControleCanvasSelecao.instance.tipoEscolha == ControleCanvasSelecao.TipoCriacao.Alvo)
        {
            contadorNumeroNomes++;
            teleporter.Teleport();
            teleporter.ToggleDisplay(false);
            ultimaPosicaoMarcador = new Vector3(cabeca.transform.position.x, teleporter.bodyTransforn.position.y, cabeca.transform.position.z);
            guardaObjetos.LookAtObjeto(ultimaPosicaoMarcador, nameObjetoCriado);
            clicou = false;
            numeroCliques = 0;

        }

        #endregion

        #region Cria Casa Com Angulo
        if (OVRInput.GetDown(OVRInput.Button.SecondaryIndexTrigger) && ControleCanvasSelecao.instance.tipoEscolha == ControleCanvasSelecao.TipoCriacao.Casa && ControleCanvasSelecao.instance.deletarLigado == false)
        {
            teleporter.ToggleDisplay(true);
            GameObject casa = Instantiate(prefabCasa);
            teleporter.bodyTransforn = casa.transform;
            casa.transform.GetChild(0).name = "casa" + contadorNumeroCasas + Random.Range(0, 50);
            casa.transform.position = new Vector3(casa.transform.position.x, 0, casa.transform.position.z);
            casaAtual = casa;
            // auxAlvo.Add(alvo);
            // Vector3 targetPosition = new Vector3(cabeca.transform.position.x, casa.transform.position.y, cabeca.transform.position.z);
            //casa.transform.LookAt(targetPosition);
            guardaObjetos.AdicionarCasa(casa);

        }

        if (OVRInput.GetUp(OVRInput.Button.SecondaryIndexTrigger) && ControleCanvasSelecao.instance.tipoEscolha == ControleCanvasSelecao.TipoCriacao.Casa)
        {
            contadorNumeroCasas++;
            teleporter.Teleport();
            teleporter.ToggleDisplay(false);
            clicou = false;
            numeroCliques = 0;
            casaAtual.transform.position = new Vector3(casaAtual.transform.position.x, 0, casaAtual.transform.position.z);

        }

        #endregion

        if (objSelecionado)
        {
           
            SelecionaCubo();
        }
        else if(objetoSolto && ControleCanvasSelecao.instance.deletarLigado == false && objSelecionado==false)
        {
            SoltaCubo();
        }

        if (numeroCliques == 1)
        {
            if (ControleCanvasSelecao.instance.tipoEscolha == ControleCanvasSelecao.TipoCriacao.Parede && ControleCanvasSelecao.instance.deletarLigado==false)
            {
                Clique1();
            }
           /* else if (ControleCanvasSelecao.instance.tipoEscolha == ControleCanvasSelecao.TipoCriacao.Alvo && ControleCanvasSelecao.instance.deletarLigado == false)
            {
                //  CriaCubo();
                

            }
            else if (ControleCanvasSelecao.instance.tipoEscolha == ControleCanvasSelecao.TipoCriacao.Casa && ControleCanvasSelecao.instance.deletarLigado == false)
            {
               // CriaCubo();

            }*/

        }
        if (numeroCliques >= 2)
        {
            Clique2();
          
            numeroCliques = 0;
            setpos1 = false;
        }
        //texto.text = " Passo : " + numeroCliques.ToString();
    }

    public void Clique1()
    {
       
            if (!setpos1)
            {
                posicao1 = maoDireita.transform.position;
                textoPosicao1.text = "posicao1 = " + posicao1.x + posicao1.y;
                setpos1 = true;
                source.PlayOneShot(clip);
            }
        
    }
    public void Clique2()
    {
        posicao2 = maoDireita.transform.position;
        textoPosicao2.text = "posicao2 = " + posicao2.x + posicao2.y;
        CalculaPosicoes();
        source.PlayOneShot(clip);
        StartCoroutine(DelayFimBeep());
        StartCoroutine(DelayCalculaEscala());
    }

    IEnumerator DelayFimBeep()
    {
        yield return new WaitForSeconds(clip.length-0.6f);
        source.PlayOneShot(clip);
        StopCoroutine(DelayFimBeep());
    }

    public void CalculaLookAt()
    {
        float x = (posicao1.x + posicao2.x)/2;
        float y = (posicao1.y + posicao2.y) / 2;
        float z = (posicao1.z + posicao2.z) ;
        auxLook.position = new Vector3(x, y, z);
    }


    #region Criacao
    public void CriaCubo()
    {
        valorZCalculoEscala = 0;
        valorXCalculoEscala = 0;
        valorYCalculoEscala = 0;
        if (ControleCanvasSelecao.instance.tipoEscolha == ControleCanvasSelecao.TipoCriacao.Parede)
        {
            #region Cria parede
            //posmaoDireita = new Vector3(maoDireita.transform.position.x, maoDireita.transform.position.y, maoDireita.transform.position.z);
            GameObject cubo = Instantiate(prefabBox);
            cubo.transform.position = posicao1;
            cubo.transform.localScale = escalaFinal;
            Vector3 targetPosition = new Vector3(posicao2.x, cubo.transform.position.y, posicao2.z);
            cubo.transform.LookAt(targetPosition);
            cubo.transform.rotation = Quaternion.Euler(cubo.transform.rotation.eulerAngles.x, cubo.transform.rotation.eulerAngles.y - 90, cubo.transform.rotation.eulerAngles.z);
            cubo.name = "parede" + contadorNumeroParedes + Random.Range(0, 50);
            guardaObjetos.AdicionarParede(cubo);
            contadorNumeroParedes++;

            #endregion
        }

       /* if (ControleCanvasSelecao.instance.tipoEscolha == ControleCanvasSelecao.TipoCriacao.Parede)
        {
            #region Cria chão
            //posmaoDireita = new Vector3(maoDireita.transform.position.x, maoDireita.transform.position.y, maoDireita.transform.position.z);
            GameObject cubo = Instantiate(prefabBox);
            cubo.transform.position = posicao1;
            cubo.transform.localScale = escalaFinal;
           // Vector3 targetPosition = new Vector3(posicao2.x, cubo.transform.position.y, posicao2.z);
            //cubo.transform.LookAt(targetPosition);
            //cubo.transform.rotation = Quaternion.Euler(cubo.transform.rotation.eulerAngles.x +180, cubo.transform.rotation.eulerAngles.y , cubo.transform.rotation.eulerAngles.z);
            cubo.name = "parede" + contadorNumeroParedes + Random.Range(0, 50);
            guardaObjetos.AdicionarParede(cubo);
            contadorNumeroParedes++;

            #endregion
        }*/
        /*else if(ControleCanvasSelecao.instance.tipoEscolha == ControleCanvasSelecao.TipoCriacao.Alvo && clicou )
        {
            #region Cria Alvo
            GameObject alvo = Instantiate(prefabAlvo);
            Vector3 auxpos = new Vector3(maoDireita.transform.position.x,-0.77f, maoDireita.transform.position.z);
            alvo.transform.position = auxpos;
            Vector3 targetPosition = new Vector3(cabeca.transform.position.x, alvo.transform.position.y, cabeca.transform.position.z);
            alvo.transform.LookAt(targetPosition);
            clicou = false;
            numeroCliques = 0;
            guardaObjetos.AdicionarAlvo(alvo);
            #endregion
        }*/
        /* else if(ControleCanvasSelecao.instance.tipoEscolha == ControleCanvasSelecao.TipoCriacao.Casa && clicou)
         {
             #region Cria Casa
             GameObject casa = Instantiate(prefabCasa);
             Vector3 auxpos = new Vector3(maoDireita.transform.position.x, -0.80f, maoDireita.transform.position.z);
             casa.transform.position = auxpos;
            // Vector3 targetPosition = new Vector3(cabeca.transform.position.x, alvo.transform.position.y, cabeca.transform.position.z);
            // alvo.transform.LookAt(targetPosition);
             clicou = false;
             numeroCliques = 0;
             guardaObjetos.AdicionarCasa(casa);
             #endregion
         }*/





    }


    public void CriaParedeLoad(Vector3 pos, Quaternion rot, Vector3 escale, string name)
    {
        GameObject cubo = Instantiate(prefabBox);
        cubo.transform.position = pos;
        cubo.transform.localScale = escale;
        //Vector3 targetPosition = new Vector3(posicao2.x, cubo.transform.position.y, posicao2.z);
        //cubo.transform.LookAt(targetPosition);
        cubo.transform.rotation = rot;
        cubo.transform.gameObject.name = name;
        guardaObjetos.AdicionarParede(cubo);
    }

    public void CriaAlvoLoad(Vector3 pos, Quaternion rot,string nameObj)
    {
        GameObject alvo = Instantiate(prefabAlvo);
        alvo.transform.position = pos;
        alvo.transform.rotation = rot;
        alvo.transform.gameObject.name = nameObj;
        guardaObjetos.AdicionarAlvo(alvo);

    }

    public void CriaCasaLoad(Vector3 pos, Quaternion rot, string name)
    {
        GameObject casa = Instantiate(prefabCasa);
        casa.transform.position = pos;
        casa.transform.rotation = rot;
        casa.transform.GetChild(0).name = name;
        guardaObjetos.AdicionarCasa(casa);
    }

    #endregion

    #region Escalar Objeto

    //Escala X
    public void ScalaCuboMaisX()
    {
        RaycastHit hit;
        if (Physics.Raycast(pivotWeapon.transform.position, pivotWeapon.transform.TransformDirection(Vector3.forward), out hit, 30))
        {
            if (hit.transform.gameObject.CompareTag("Box"))
            {

                    hit.transform.localScale = new Vector3(hit.transform.localScale.x+0.02f, hit.transform.localScale.y, hit.transform.localScale.z);
              
            }

        }
    }

    public void ScalaCuboMenosX()
    {
        RaycastHit hit;
        if (Physics.Raycast(pivotWeapon.transform.position, pivotWeapon.transform.TransformDirection(Vector3.forward), out hit, 30))
        {
            if (hit.transform.gameObject.CompareTag("Box"))
            {

                if (hit.transform.localScale.x > 0.5f)
                {
                    hit.transform.localScale = new Vector3(hit.transform.localScale.x - 0.02f, hit.transform.localScale.y, hit.transform.localScale.z);
                }

            }

        }
    }

    //EscalaY
    public void ScalaCuboMaisY()
    {
        RaycastHit hit;
        if (Physics.Raycast(pivotWeapon.transform.position, pivotWeapon.transform.TransformDirection(Vector3.forward), out hit, 30))
        {
            if (hit.transform.gameObject.CompareTag("Box"))
            {

                hit.transform.localScale = new Vector3(hit.transform.localScale.x , hit.transform.localScale.y + 0.02f, hit.transform.localScale.z);

            }

        }
    }

    public void ScalaCuboMenosY()
    {
        RaycastHit hit;
        if (Physics.Raycast(pivotWeapon.transform.position, pivotWeapon.transform.TransformDirection(Vector3.forward), out hit, 30))
        {
            if (hit.transform.gameObject.CompareTag("Box"))
            {

                if (hit.transform.localScale.x > 0.5f)
                {
                    hit.transform.localScale = new Vector3(hit.transform.localScale.x , hit.transform.localScale.y - 0.02f, hit.transform.localScale.z);
                }

            }

        }
    }

    //Escala Z

    public void ScalaCuboMaisZ()
    {
        RaycastHit hit;
        if (Physics.Raycast(pivotWeapon.transform.position, pivotWeapon.transform.TransformDirection(Vector3.forward), out hit, 30))
        {
            if (hit.transform.gameObject.CompareTag("Box"))
            {

                hit.transform.localScale = new Vector3(hit.transform.localScale.x, hit.transform.localScale.y , hit.transform.localScale.z + 0.02f);

            }

        }
    }

    public void ScalaCuboMenosZ()
    {
        RaycastHit hit;
        if (Physics.Raycast(pivotWeapon.transform.position, pivotWeapon.transform.TransformDirection(Vector3.forward), out hit, 30))
        {
            if (hit.transform.gameObject.CompareTag("Box"))
            {

                if (hit.transform.localScale.x > 0.5f)
                {
                    hit.transform.localScale = new Vector3(hit.transform.localScale.x, hit.transform.localScale.y , hit.transform.localScale.z - 0.02f);
                }

            }

        }
    }



    #endregion

    #region Selecionar
    public void SelecionaCubo()
    {
       // linhaObj.SetActive(true);
       // CriarLinha();
       // miraExluir.SetActive(true);

        RaycastHit hit;
        if (Physics.Raycast(pivotWeapon.transform.position, pivotWeapon.transform.TransformDirection(Vector3.forward), out hit, distanceHit))
        {
          

            if (hit.transform.gameObject.CompareTag("Box"))
            {
                if (nameObjSelecionado == "")
                {
                    nameObjSelecionado = hit.transform.name;
                }

                if (nameObjSelecionado == hit.transform.name)
                {
                    distanceHit = hit.distance +2;
                    if (selecionado)
                    {
                        if (hit.transform.position.y < 0)
                        {
                            hit.transform.position = new Vector3(hit.transform.position.x, 0, hit.transform.position.z);
                        }
                        pontoSelecao.transform.position = hit.transform.position;

                        // ultimaPosicaoMarcador = new Vector3(cabeca.transform.position.x, hit.transform.position.y, cabeca.transform.position.z);
                        //guardaObjetos.LookAtObjeto(ultimaPosicaoMarcador, hit.transform.name);
                        hit.transform.GetComponent<MaterialSelect>().MudaMaterial();
                        selecionado = false;
                    }
                   // GameObject aux = hit.transform.GetComponent<GameObject>();
                    // hit.transform.position = pontoSelecao.transform.position;
                    //hit.transform.GetChild(0).GetComponent<MeshRenderer>().material = matSelecionaCubo;
                    //hit.transform.GetComponent<MeshRenderer>().material = matSelecionaCubo;
                    // aux.transform.position = pontoSelecao.transform.position;
                    // hit.transform.rotation = controleDireito.transform.rotation;
                    //aux.transform.position = pontoSelecao.transform.position;
                    
                    hit.transform.position = pontoSelecao.transform.position;
                   if( ControleCanvasSelecao.instance.tipoEscolha != ControleCanvasSelecao.TipoCriacao.Selecionar)
                    hit.transform.rotation = OVRInput.GetLocalControllerRotation(OVRInput.Controller.RTouch);
                    else
                    {
                        hit.transform.rotation = OVRInput.GetLocalControllerRotation(OVRInput.Controller.RTouch);
                        hit.transform.rotation = Quaternion.Euler(-hit.transform.eulerAngles.x, hit.transform.eulerAngles.y + 180, -hit.transform.eulerAngles.z);
                    }
                    // posicaoInicial = hit.transform.position;
                    // posicalFinal = maoDireita.transform.position;
                }
                else
                {
                    nameObjSelecionado = "";
                    selecionado = false;
                    objSelecionado = false;
                }

            }


        }
        else
        {
            nameObjSelecionado = "";
            distanceHit = 60;
        }
    }

    public void SoltaCubo()
    {
        RaycastHit hit;
        if (Physics.Raycast(pivotWeapon.transform.position, pivotWeapon.transform.TransformDirection(Vector3.forward), out hit, 30))
        {
            if (hit.transform.gameObject.CompareTag("Box"))
            {
                //hit.transform.GetChild(0).GetComponent<MeshRenderer>().material = matCubo;
                hit.transform.GetComponent<MaterialSelect>().VoltaMaterial();
                //Calculo Scale

            }
        }
    }
    #endregion
    
    #region Rotacionar

    public void RotacionaDireita()
    {
        RaycastHit hit;
        if (Physics.Raycast(pivotWeapon.transform.position, pivotWeapon.transform.TransformDirection(Vector3.forward), out hit, 30))
        {
            if (hit.transform.gameObject.CompareTag("Box"))
            {

          
                    auxRot = new Vector3(hit.transform.rotation.x, hit.transform.rotation.y + numRotDireita, hit.transform.rotation.z);
               
                hit.transform.rotation = Quaternion.Euler(auxRot);
                numRotDireita += 0.5f;


            }

        }
    }
    public void RotacionaEsquerda()
    {
        RaycastHit hit;
        if (Physics.Raycast(pivotWeapon.transform.position, pivotWeapon.transform.TransformDirection(Vector3.forward), out hit, 30))
        {
            if (hit.transform.gameObject.CompareTag("Box"))
            {

                 auxRot = new Vector3(hit.transform.rotation.x, hit.transform.rotation.y - numRotEsquerda, hit.transform.rotation.z);
                hit.transform.rotation = Quaternion.Euler(auxRot);
                numRotEsquerda += 0.5f;



            }

        }
    }

    #endregion


    #region Deletar
    public void Deletar()
    {
        miraExluir.SetActive(false);
        RaycastHit hit;
        if (Physics.Raycast(pivotWeapon.transform.position, pivotWeapon.transform.TransformDirection(Vector3.forward), out hit, 30))
        {
            if (hit.transform.gameObject.CompareTag("Box"))
            {
                guardaObjetos.BuscaObjetoeDestroi(hit.transform.gameObject.name);
               // Destroy(hit.transform.gameObject);
              // ControleCanvasSelecao.instance.deletarLigado = false;
                hit.transform.gameObject.SetActive(false);
               
                
            }
        }
    }
    #endregion


    #region Criacao Linha
    public void CriarLinha()
    {
        if(ControleCanvasSelecao.instance.deletarLigado == true)
        {
            linha.material = linhaMaterialDeletar;
        }
        else if (ControleCanvasSelecao.instance.tipoEscolha == ControleCanvasSelecao.TipoCriacao.Selecionar)
        {
            linha.material = linhaMaterialSelecionado;
        }

        RaycastHit hit;
        if (Physics.Raycast(pivotWeapon.transform.position, pivotWeapon.transform.TransformDirection(Vector3.forward), out hit, 30))
        {
            if (hit.transform.gameObject.CompareTag("Box"))
            {
                linha.SetPosition(0, pivotWeapon.transform.position);
                linha.SetPosition(1, hit.point);
            }

        }
        else
        {
            linha.SetPosition(0, pivotWeapon.transform.position);
            linha.SetPosition(1, pivotWeapon.transform.forward*100);
        }
    }
    #endregion



}

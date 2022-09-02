using MyBox;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardaObjetos : MonoBehaviour
{

    public List<GameObject> paredes;
    public List<GameObject> alvos;
    public List<GameObject> casas;

    public static int numeroObjetos;

    public static GuardaObjetos instance;

	[SerializeField]
    private bool  timeSave = true;

   

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        if (instance != this)
        {
            Destroy(this);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        paredes = new List<GameObject>();
        alvos = new List<GameObject>();
        casas = new List<GameObject>();
    }
   

    public void AdicionarParede( GameObject obj)
    {
        paredes.Add(obj);
    }

    public void AdicionarAlvo(GameObject obj)
    {
        alvos.Add(obj);
    }

    public void AdicionarCasa(GameObject obj)
    {
        casas.Add(obj);
       
    }

    //Salva os Objetos
    [ButtonMethod()]
    public void SalvarCena()
    {
        if (timeSave)
        {
            for (int i = 0; i < paredes.Count; i++)
            {
                if (paredes.Count > 0)
                    Serializer.instance.Serializar("parede", paredes[i]);
            }
            for (int i = 0; i < alvos.Count; i++)
            {
                if (alvos.Count > 0)
                    Serializer.instance.Serializar("alvo", alvos[i]);
            }
            for (int i = 0; i < casas.Count; i++)
            {
                if (casas.Count > 0)
                    Serializer.instance.Serializar("casa", casas[i]);
            }
            Debug.Log("SAVE");
            timeSave = false;
          
            StopCoroutine(DelayTimeSave());
            StartCoroutine(DelayTimeSave());
            StopCoroutine(DelaySalvaObjetos());
            StartCoroutine(DelaySalvaObjetos());
           
        }
        /*else 
        {
            StopCoroutine(DelayTimeSave());
            StartCoroutine(DelayTimeSave());
        }*/

    }

    IEnumerator DelaySalvaObjetos()
    {
       
            yield return new WaitForSeconds(2);
            Serializer.instance.SalvarTodosObjetos();
           
     
    }

    IEnumerator DelayTimeSave()
	{
        timeSave = false;
        yield return new WaitForSecondsRealtime(8);
        timeSave = true;
        StopCoroutine(DelayTimeSave());

    }

    //Busca o Objeto com esse nome e Remove Da Lista
    public void  BuscaObjetoeDestroi(string name)
    {
        
        for (int i = 0; i < paredes.Count; i++)
        {
            if (paredes[i].name == name)
            {
                paredes.RemoveAt(i);
                paredes.TrimExcess();
                break;
            }
        }
        for (int i = 0; i < alvos.Count; i++)
        {
            if (alvos[i].name == name)
            {
                alvos.RemoveAt(i);
                alvos.TrimExcess();
                break;

            }
        }
        for (int i = 0; i < casas.Count; i++)
        {
            if (casas[i].name == name)
            {
                casas.RemoveAt(i);
                casas.TrimExcess();
                break;

            }
        }

       

    }

    // Destroi All Objects
    public void DestroyAllObjects()
	{
        for (int i = 0; i < paredes.Count; i++)
        {
           
                paredes.RemoveAt(i);
                paredes.TrimExcess();
                break;
            
        }
        for (int i = 0; i < alvos.Count; i++)
        {
           
                alvos.RemoveAt(i);
                alvos.TrimExcess();
                break;

           
        }
        for (int i = 0; i < casas.Count; i++)
        {
           
                casas.RemoveAt(i);
                casas.TrimExcess();
                break;

          
        }
    }


    public void LookAtObjeto( Vector3 look, string name)
    {
       /* for (int i = 0; i < paredes.Count; i++)
        {
            if (paredes[i].name == name)
            {
                paredes[i].transform.LookAt(look);
                break;
            }
        }*/
        for (int i = 0; i < alvos.Count; i++)
        {
            if (alvos[i].name == name)
            {
                alvos[i].transform.LookAt(look);
                break;

            }
        }
       /* for (int i = 0; i < casas.Count; i++)
        {
            if (casas[i].name == name)
            {
                casas[i].transform.LookAt(look);
                break;

            }
        }*/
    }

    

    




}

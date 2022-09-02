using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Serializer : MonoBehaviour
{
    [Header("Referencia para criacao de Objeto")]
    public ControleCriacao controleCriar;

    public Text textoSaveLoad;

     DataSave.DataContainer dataSave;
    DataSave.DataContainer dataLoad;
    public static Serializer instance;
    List<DataSave> datas;
    private int numberObjetos;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private void Start()
    {
        datas = new List<DataSave>();
        dataSave = new DataSave.DataContainer();
        dataLoad = new DataSave.DataContainer();

        if(ControleSalvar.Load() != null)
        {
             textoSaveLoad.text = "Carregado";
           // textoSaveLoad.text = Application.persistentDataPath;
            LoadTodosObjetos();
        }
        else
        {
            textoSaveLoad.text = Application.persistentDataPath;
        }

    }

    public void Serializar(string tipo,GameObject obj)
    {

        DataSave dataAux = new DataSave();
        dataAux.position = obj.transform.position;
        dataAux.rotation = obj.transform.rotation;
        dataAux.escale = obj.transform.localScale;
        dataAux.tipo = tipo;
        dataAux.name = obj.name;
        datas.Add(dataAux);
    }

    public void SalvarTodosObjetos()
    {
        textoSaveLoad.text = "Salvo";
        numberObjetos = datas.Count;
        PlayerPrefs.SetInt("numObjetos", numberObjetos);
        PlayerPrefs.Save();
        dataSave.dataContainer = new DataSave[datas.Count];
        for (int i = 0; i < dataSave.dataContainer.Length; i++)
        {
            dataSave.dataContainer[i] = datas[i];
        }

        string json = JsonHelper.ToJson(dataSave.dataContainer, true);
        ControleSalvar.Save(json);
       

    }

    public void LoadTodosObjetos()
    {
        //Pega o json e passa para um Objeto do Tipo DataSave
        dataLoad.dataContainer = new DataSave[PlayerPrefs.GetInt("numObjetos")];
        string datafile = ControleSalvar.Load();
       
        dataLoad.dataContainer = JsonHelper.FromJson<DataSave>(datafile);

        //Cria os Objetos
        for (int i = 0; i < dataLoad.dataContainer.Length; i++)
        {
            if (dataLoad.dataContainer[i].tipo == "parede")
            {
                controleCriar.CriaParedeLoad(dataLoad.dataContainer[i].position, dataLoad.dataContainer[i].rotation, dataLoad.dataContainer[i].escale, dataLoad.dataContainer[i].name);
                //Debug.Log("Parede Nome" + dataLoad.dataContainer[i])
            }
            else if (dataLoad.dataContainer[i].tipo == "alvo")
            {
                controleCriar.CriaAlvoLoad(dataLoad.dataContainer[i].position, dataLoad.dataContainer[i].rotation, dataLoad.dataContainer[i].name);
            }
            else if(dataLoad.dataContainer[i].tipo == "casa")
            {
                controleCriar.CriaCasaLoad(dataLoad.dataContainer[i].position, dataLoad.dataContainer[i].rotation, dataLoad.dataContainer[i].name);
            }
        }
        
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            LoadTodosObjetos();
        }
    }
}

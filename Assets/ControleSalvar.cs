using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class ControleSalvar : MonoBehaviour
{
    public static  string SAVE_FOLDER = Application.persistentDataPath + "/Saveds/";

    

    private void Awake()
    {

        
        //Testa se a pasta existe
        if (!Directory.Exists(SAVE_FOLDER))
        {
            //Cria a pasta
            Directory.CreateDirectory(SAVE_FOLDER);
           
        }
    }

    public static void Save(string saveString)
    {
       // Debug.Log("Folder" + SAVE_FOLDER);
        if (!Directory.Exists(SAVE_FOLDER))
        {
            //Cria a pasta
            Directory.CreateDirectory(SAVE_FOLDER);

		}
		else
		{
            File.Delete(SAVE_FOLDER + "/save.txt");
        }
        File.WriteAllText(SAVE_FOLDER + "/save.txt", saveString);
       

    }

    IEnumerator DelayCreateDiretory()
	{
        yield return new WaitForSeconds(0.3f);
	}

    public static string Load()
    {
        if(File.Exists(SAVE_FOLDER + "/save.txt"))
        {
            string saveString = File.ReadAllText(SAVE_FOLDER + "/save.txt");
            return saveString;
        }
        else
        {
            return null;
        }
    }


}

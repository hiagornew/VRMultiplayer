using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CilindroSelecao : MonoBehaviour
{

    private bool encontrado;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Box") && encontrado==false)
        {
            other.gameObject.GetComponent<MaterialSelect>().MudaMaterial();
            encontrado = true;
            
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Box"))
        {
           
            other.gameObject.GetComponent<MaterialSelect>().VoltaMaterial();
            encontrado = false;
        }
    }
   
}

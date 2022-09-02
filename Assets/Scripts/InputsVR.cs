using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputsVR : MonoBehaviour
{
    public Text textoLog;

    // Update is called once per frame
    void Update()
    {

        #region Controle L
        if (OVRInput.Get(OVRInput.Button.PrimaryHandTrigger))
        {
            //Gatilho lateral Controle L
            textoLog.text = "Trigger dois L";
        }
        if (OVRInput.Get(OVRInput.Button.PrimaryIndexTrigger))
        {
            //Gatilho de Traz Controle L
            textoLog.text = "Trigger um L";
        }

        if (OVRInput.Get(OVRInput.Button.Three))
        {
            Debug.Log("Trigger um");
            textoLog.text = "Trigger X lado L";
        }
        if (OVRInput.Get(OVRInput.Button.Four))
        {
            Debug.Log("Trigger um");
            textoLog.text = "Trigger Y lado L";
        }

        //Joystic
        if (OVRInput.Get(OVRInput.Button.PrimaryThumbstickDown))
        {
            //Gatilho de Traz Controle L
            textoLog.text = "Touch baixo L";
        }
        if (OVRInput.Get(OVRInput.Button.PrimaryThumbstickLeft))
        {
            //Gatilho de Traz Controle L
            textoLog.text = "Touch esquerda L";
        }
        if (OVRInput.Get(OVRInput.Button.PrimaryThumbstickRight))
        {
            //Gatilho de Traz Controle L
            textoLog.text = "Touch direita L";
        }
        if (OVRInput.Get(OVRInput.Button.PrimaryThumbstickUp))
        {
            //Gatilho de Traz Controle L
            textoLog.text = "Touch Cima L";
        }
        #endregion


        #region Controle R
        if (OVRInput.Get(OVRInput.Button.Two))
        {
            Debug.Log("Trigger um");
            textoLog.text = "Trigger B lado R";
        }
       

        if (OVRInput.Get(OVRInput.Button.One))
        {
            Debug.Log("Trigger um");
            textoLog.text = "Trigger A lado R";
        }

        if (OVRInput.Get(OVRInput.Button.SecondaryIndexTrigger))
        {
            Debug.Log("Trigger um");
            textoLog.text = "Trigger  Atraz Controle R";
        }

        if (OVRInput.Get(OVRInput.Button.SecondaryHandTrigger))
        {
            Debug.Log("Trigger um");
            textoLog.text = "Trigger  Lado Controle R";
        }

        //Joystic
        if (OVRInput.Get(OVRInput.Button.SecondaryThumbstickLeft))
        {
            //Gatilho de Traz Controle L
            textoLog.text = "Touch esquerda R";
        }
        if (OVRInput.Get(OVRInput.Button.SecondaryThumbstickRight))
        {
            //Gatilho de Traz Controle L
            textoLog.text = "Touch direita R";
        }
        if (OVRInput.Get(OVRInput.Button.SecondaryThumbstickUp))
        {
            //Gatilho de Traz Controle L
            textoLog.text = "Touch cima R";
        }
        if (OVRInput.Get(OVRInput.Button.SecondaryThumbstickDown))
        {
            //Gatilho de Traz Controle L
            textoLog.text = "Touch baixo R";
        }

        #endregion




    }


}

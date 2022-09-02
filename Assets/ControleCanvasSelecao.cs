using System.Collections;
using System.Collections.Generic;
using MyBox;
using UnityEngine;
using UnityEngine.UI;

public class ControleCanvasSelecao : MonoBehaviour
{
    // Start is called before the first frame update
    public static ControleCanvasSelecao instance;
    public Button[] botoes;
    private int index;
    public enum TipoCriacao { Parede, Alvo, Casa, Deletar, Selecionar, Salvar}
    public TipoCriacao tipoEscolha;
    public bool deletarLigado;
    public bool salvo;
    public GameObject miraLaserDeletarSelecionar;
    private void Awake()
    {
        if(instance== null || instance != this)
        {
            instance = this;
        }
    }
    void Start()
    {
        salvo = true;
        miraLaserDeletarSelecionar.SetActive(false);
        tipoEscolha = TipoCriacao.Parede;
        index = 0;
        //botoes[0].Select();
    }

    public void  SetIndexBtn(int index2)
    {
        index = index2;
       // SelecionaBotao();
        // Debug.Log("Sim yes");
    }

    // Update is called once per frame
    void Update()
    {
        #region Movimentacao pelo Menu
        if (OVRInput.GetDown(OVRInput.Button.SecondaryThumbstickDown)|| Input.GetKeyDown(KeyCode.DownArrow))
        {
            index++;
            if (index > botoes.Length-1)
            {
                index = 0;
            }
            SelecionaBotao();
        }

        if (OVRInput.GetDown(OVRInput.Button.SecondaryThumbstickUp) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            index--;
            if (index < 0)
            {
                index = botoes.Length-1;
            }
            SelecionaBotao();
        }

        #endregion

        #region Escolha
        if (OVRInput.GetDown(OVRInput.Button.Two))
        {
            SelecaoParede();
            
        }
        #endregion
    }

    [ButtonMethod()]
    public void SelecaoParede()
    {
        switch (index)
        {
            case 0:

                tipoEscolha = TipoCriacao.Parede;
                deletarLigado = false;
                botoes[0].GetComponent<Image>().color = Color.green;
                miraLaserDeletarSelecionar.SetActive(false);
                break;

            case 1:

                tipoEscolha = TipoCriacao.Alvo;
                deletarLigado = false;
                botoes[1].GetComponent<Image>().color = Color.green;
                miraLaserDeletarSelecionar.SetActive(false);
                break;

            case 2:
                tipoEscolha = TipoCriacao.Casa;
                deletarLigado = false;
                botoes[2].Select();
                miraLaserDeletarSelecionar.SetActive(false);
                break;
            case 3:
                tipoEscolha = TipoCriacao.Deletar;
                deletarLigado = true;
                botoes[3].Select();
                miraLaserDeletarSelecionar.SetActive(true);
                break;
            case 4:
               tipoEscolha = TipoCriacao.Selecionar;
                deletarLigado = false;
                botoes[4].Select();
                miraLaserDeletarSelecionar.SetActive(true);
                break;
            case 5:
                tipoEscolha = TipoCriacao.Salvar;
                deletarLigado = false;
                salvo = true;
                botoes[5].Select();
                miraLaserDeletarSelecionar.SetActive(false);
                break;
            case 6:
                //tipoEscolha = TipoCriacao.Salvar;
                deletarLigado = false;
               
                botoes[6].Select();
                miraLaserDeletarSelecionar.SetActive(false);
                break;
            case 7:
                //tipoEscolha = TipoCriacao.Salvar;
                deletarLigado = false;
               
                botoes[7].Select();
                miraLaserDeletarSelecionar.SetActive(false);
                break;
        }

    }

   

    public void SelecionaBotao()
    {
        for (int i = 0; i < botoes.Length; i++)
        {
            botoes[i].GetComponent<Image>().color = Color.white;
        }
        botoes[index].GetComponent<Image>().color = Color.green;
    }

    public void SelecionaBotao(int indexAux)
    {
        for (int i = 0; i < botoes.Length; i++)
        {
            botoes[i].GetComponent<Image>().color = Color.white;
        }
        botoes[indexAux].GetComponent<Image>().color = Color.green;
    }
}

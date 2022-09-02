using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialSelect : MonoBehaviour
{
    // Start is called before the first frame update
    public MeshRenderer render;
    public SkinnedMeshRenderer renderSkin;
    public Material materialnormal;
    public Material materialSelecionado;
    public bool skineMesh;

   
    public void MudaMaterial()
    {
        if(!skineMesh)
        render.material = materialSelecionado;
        else
            renderSkin.material = materialSelecionado;
    }

    public void VoltaMaterial()
    {
        if(!skineMesh)
        render.material = materialnormal;
        else
            renderSkin.material = materialnormal;
    }
}

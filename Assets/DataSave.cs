using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class DataSave
{
    public string tipo;
    public Vector3 position;
    public Quaternion rotation;
    public Vector3 escale;
    public string name;

    
    public class DataContainer
    {
        public DataSave[] dataContainer;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestDestoy : MonoBehaviour
{
       // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            DestroyObjectsNetWork.instance.DestrotObject(this.gameObject, 0.5f);
        }
    }
}

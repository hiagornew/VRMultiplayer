using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TesteInfo : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
       // Debug.Log("Rotacao Normal" + transform.rotation);
       // Debug.Log("Rotacao Local" + transform.localRotation);
       // Debug.Log("Rotacao Euler" + transform.eulerAngles);
       // Debug.Log("Rotacao Local Euler" + transform.localEulerAngles);
        Debug.Log("Rotacao Rotation Euler" + transform.rotation.eulerAngles);
        Debug.Log("Rotacao Rotation normalized" + transform.rotation);
      

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            transform.rotation = Quaternion.Euler(0, 320, 0);
        }
    }


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecalControl : MonoBehaviour
{
    private void Start()
    {
        if (this.gameObject.activeInHierarchy)
        {
            Destroy(this.gameObject, 3);
        }
    }
}

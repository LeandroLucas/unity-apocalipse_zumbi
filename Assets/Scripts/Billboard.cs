using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{

    void Update()
    {
        Vector3 direcao = transform.position + Camera.main.transform.forward;
        transform.LookAt(direcao);
    }
}

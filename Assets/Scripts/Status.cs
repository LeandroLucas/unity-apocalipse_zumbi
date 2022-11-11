using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Status : MonoBehaviour
{
    public float Velocidade = 5;
    public int VidaInicial = 100;
    [HideInInspector]
    public int Vida;

    private void Awake()
    {
        Vida = VidaInicial;
    }
}

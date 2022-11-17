using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeradorChefe : MonoBehaviour
{
    private float tempoProximaGeracao = 0;
    public float TempoEntreGeracoes = 60;

    public GameObject ChefePrefab;

    private void Start()
    {
        tempoProximaGeracao = TempoEntreGeracoes;
    }

    void Update()
    {
        if (Time.timeSinceLevelLoad > tempoProximaGeracao)
        {
            Instantiate(ChefePrefab, transform.position, Quaternion.identity);
            tempoProximaGeracao = Time.timeSinceLevelLoad + TempoEntreGeracoes;
        }
    }
}

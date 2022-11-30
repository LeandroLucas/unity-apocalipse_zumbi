using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeradorChefe : MonoBehaviour
{
    private float tempoProximaGeracao = 0;
    public float TempoEntreGeracoes = 60;
    private ControlaInterface controlaInterface;
    private Transform jogador;

    public GameObject ChefePrefab;
    public Transform[] PosicoesPossiveisDeGeracao;

    private void Start()
    {
        jogador = GameObject.FindWithTag(Tags.Jogador).transform;
        tempoProximaGeracao = TempoEntreGeracoes;
        controlaInterface = GameObject.FindObjectOfType(typeof(ControlaInterface)) as ControlaInterface;
    }

    void Update()
    {
        if (Time.timeSinceLevelLoad > tempoProximaGeracao)
        {
            Vector3 posicaoDeCriacao = CalcularPosicaoPossivelMaisDistanteDoJogador();
            Instantiate(ChefePrefab, posicaoDeCriacao, Quaternion.identity);
            controlaInterface.AparecerTextoDoChefeCriado();
            tempoProximaGeracao = Time.timeSinceLevelLoad + TempoEntreGeracoes;
        }
    }

    Vector3 CalcularPosicaoPossivelMaisDistanteDoJogador()
    {
        Vector3 posicaoDeMaiorDistancia = Vector3.zero;
        float maiorDistancia = 0;
        foreach (Transform posicao in PosicoesPossiveisDeGeracao)
        {
            float distanciaPosicaoJogador = Vector3.Distance(jogador.position, posicao.position);
            if (distanciaPosicaoJogador > maiorDistancia)
            {
                maiorDistancia = distanciaPosicaoJogador;
                posicaoDeMaiorDistancia = posicao.position;
            }
        }
        return posicaoDeMaiorDistancia;
    }


}

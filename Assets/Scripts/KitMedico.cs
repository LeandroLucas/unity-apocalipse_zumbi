using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitMedico : MonoBehaviour
{
    private int quantidadeDeCura = 15;
    private int tempoDestruicao = 5;

    private void Start()
    {
        Destroy(gameObject, tempoDestruicao);
    }

    void OnTriggerEnter(Collider objetoDeColisao)
    {
        if (objetoDeColisao.tag == Tags.Jogador)
        {
            objetoDeColisao.GetComponent<ControlaJogador>().CurarVida(quantidadeDeCura);
            Destroy(gameObject);
        }
    }
}

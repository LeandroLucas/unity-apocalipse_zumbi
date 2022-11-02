using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlaInimigo : MonoBehaviour
{
    public float Velocidade = 5;
    public float DistanciaAtaque = 1.6f;

    private GameObject jogador;
    private Rigidbody rigidbodyInimigo;
    private Animator animatorInimigo;
    private ControlaJogador controlaJogador;

    private void Start()
    {
        jogador = GameObject.FindWithTag("Player");
        int geraTipoZumbi = Random.Range(1, 28);
        transform.GetChild(geraTipoZumbi).gameObject.SetActive(true);

        rigidbodyInimigo = GetComponent<Rigidbody>();
        animatorInimigo = GetComponent<Animator>();
        controlaJogador = jogador.GetComponent<ControlaJogador>();
    }

    void FixedUpdate()
    {
        float distancia = Vector3.Distance(transform.position, jogador.transform.position);
        Vector3 direcao = jogador.transform.position - transform.position;

        Quaternion novaRotacao = Quaternion.LookRotation(direcao);

        rigidbodyInimigo.MoveRotation(novaRotacao);
        if (distancia > DistanciaAtaque)
        {
            rigidbodyInimigo.MovePosition(rigidbodyInimigo.position + (direcao.normalized * Velocidade * Time.deltaTime));
            animatorInimigo.SetBool("Atacando", false);
        }
        else
        {
            animatorInimigo.SetBool("Atacando", true);
        }
    }

    void AtacaJogador()
    {
        int dano = Random.Range(20, 31);
        controlaJogador.ReceberDano(dano);
    }

}

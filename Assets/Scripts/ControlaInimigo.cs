using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlaInimigo : MonoBehaviour, IMatavel
{
    public float DistanciaAtaque = 1.6f;

    public AudioClip SomDeMorte;
    private GameObject jogador;

    private ControlaJogador controlaJogador;
    private MovimentaPersonagem movimentaPersonagem;
    private AnimacaoPersonagem animacaoPersonagem;
    private Status status;

    private void Start()
    {
        jogador = GameObject.FindWithTag(Tags.Jogador);
        AleatorizarZumbi();

        controlaJogador = jogador.GetComponent<ControlaJogador>();
        movimentaPersonagem = GetComponent<MovimentaPersonagem>();
        animacaoPersonagem = GetComponent<AnimacaoPersonagem>();
        status = GetComponent<Status>();
    }

    void FixedUpdate()
    {
        float distancia = Vector3.Distance(transform.position, jogador.transform.position);
        Vector3 direcao = jogador.transform.position - transform.position;

        movimentaPersonagem.Rotacionar(direcao);

        if (distancia > DistanciaAtaque)
        {
            movimentaPersonagem.Movimentar(direcao, status.Velocidade);
            animacaoPersonagem.Atacar(false);
        }
        else
        {
            animacaoPersonagem.Atacar(true);
        }
    }

    void AtacaJogador()
    {
        int dano = Random.Range(20, 31);
        controlaJogador.ReceberDano(dano);
    }

    void AleatorizarZumbi()
    {
        int geraTipoZumbi = Random.Range(1, 28);
        transform.GetChild(geraTipoZumbi).gameObject.SetActive(true);
    }

    public void ReceberDano(int dano)
    {
        status.Vida -= dano;
        if (status.Vida <= 0)
        {
            Morrer();
        }
    }

    public void Morrer()
    {
        ControlaAudio.instancia.PlayOneShot(SomDeMorte);
        Destroy(gameObject);
    }

}

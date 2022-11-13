using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlaInimigo : MonoBehaviour, IMatavel
{
    public float DistanciaAtaque = 1.6f;
    public float DistanciaPerseguir = 15;

    public AudioClip SomDeMorte;
    private GameObject jogador;

    private ControlaJogador controlaJogador;
    private MovimentaPersonagem movimentaPersonagem;
    private AnimacaoPersonagem animacaoPersonagem;
    private Status status;
    private Vector3 posicaoAleatoria;
    private int distanciaPosicaoAleatoria = 10;
    private Vector3 direcao;
    private float contadorVagar;
    private float tempoEntrePosicoesAleatorias = 4;


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

        if (distancia > DistanciaPerseguir)
        {
            Vagar();
        }
        else if (distancia > DistanciaAtaque)
        {
            SeguirJogador();
        }
        else
        {
            direcao = jogador.transform.position - transform.position;
            animacaoPersonagem.Atacar(true);
        }
        movimentaPersonagem.Rotacionar(direcao);
        animacaoPersonagem.Movimentar(direcao.magnitude);
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

    void SeguirJogador()
    {
        direcao = jogador.transform.position - transform.position;

        movimentaPersonagem.Movimentar(direcao, status.Velocidade);
        animacaoPersonagem.Atacar(false);
    }

    void Vagar()
    {
        contadorVagar -= Time.deltaTime;
        if (contadorVagar <= 0)
        {
            posicaoAleatoria = PosicaoAleatoria();
            contadorVagar += tempoEntrePosicoesAleatorias;
        }
        bool pertoOSuficiente = Vector3.Distance(transform.position, posicaoAleatoria) <= 0.05;
        if (!pertoOSuficiente)
        {
            direcao = posicaoAleatoria - transform.position;
            movimentaPersonagem.Movimentar(direcao, status.Velocidade);
        }
    }

    Vector3 PosicaoAleatoria()
    {
        Vector3 posicao = Random.insideUnitSphere * distanciaPosicaoAleatoria;
        posicao += transform.position;
        posicao.y = transform.position.y;
        return posicao;
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

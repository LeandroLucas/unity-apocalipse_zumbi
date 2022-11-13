using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlaJogador : MonoBehaviour, IMatavel, ICuravel
{

    public LayerMask MascaraChao;

    public ControlaInterface ControlaInterface;

    public AudioClip SomDeDano;
    private Vector3 direcao;
    private AnimacaoPersonagem animacaoPersonagem;
    private MovimentaJogador movimentaPersonagem;
    [HideInInspector]
    public Status Status;

    void Start()
    {
        movimentaPersonagem = GetComponent<MovimentaJogador>();
        animacaoPersonagem = GetComponent<AnimacaoPersonagem>();
        Status = GetComponent<Status>();
    }

    void FixedUpdate()
    {
        float eixoX = Input.GetAxis("Horizontal");
        float eixoZ = Input.GetAxis("Vertical");

        direcao = new Vector3(eixoX, 0, eixoZ);

        animacaoPersonagem.Movimentar(direcao.magnitude);

        movimentaPersonagem.Movimentar(direcao, Status.Velocidade);

        movimentaPersonagem.RotacionarJogador(MascaraChao);
    }

    public void ReceberDano(int dano)
    {
        Status.Vida -= dano;

        ControlaInterface.AtualizarSliderVidaJogador();
        ControlaAudio.instancia.PlayOneShot(SomDeDano);
        if (Status.Vida <= 0)
        {
            Morrer();
        }
    }

    public void Morrer()
    {
        ControlaInterface.GameOver();
    }

    public void CurarVida(int quantidadeDeCura)
    {
        Status.Vida += quantidadeDeCura;
        if (Status.Vida > Status.VidaInicial)
        {
            Status.Vida = Status.VidaInicial;
        }
        ControlaInterface.AtualizarSliderVidaJogador();
    }
}

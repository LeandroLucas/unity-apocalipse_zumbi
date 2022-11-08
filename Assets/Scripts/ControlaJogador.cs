using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ControlaJogador : MonoBehaviour, IMatavel
{

    public LayerMask MascaraChao;

    public GameObject TextoGameOver;

    public ControlaInterface ControlaInterface;

    public AudioClip SomDeDano;

    private bool Vivo = true;
    private Vector3 direcao;
    private AnimacaoPersonagem animacaoPersonagem;
    private MovimentaJogador movimentaPersonagem;
    [HideInInspector]
    public Status Status;

    void Start()
    {
        Time.timeScale = 1;
        movimentaPersonagem = GetComponent<MovimentaJogador>();
        animacaoPersonagem = GetComponent<AnimacaoPersonagem>();
        Status = GetComponent<Status>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Vivo == false)
        {
            if (Input.GetButtonDown("Submit"))
            {
                SceneManager.LoadScene("game");
            }
        }
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
        TextoGameOver.SetActive(true);
        Vivo = false;
        Time.timeScale = 0;
    }

    public bool EstaVivo()
    {
        return Vivo;
    }
}

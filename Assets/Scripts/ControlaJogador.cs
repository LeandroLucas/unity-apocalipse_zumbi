using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ControlaJogador : MonoBehaviour
{

    public float Velocidade = 10;

    public LayerMask MascaraChao;

    public GameObject TextoGameOver;

    public int Vida = 100;

    public ControlaInterface ControlaInterface;

    public AudioClip SomDeDano;

    private bool Vivo = true;
    private Vector3 direcao;
    private Rigidbody rigidbodyJogador;
    private Animator animatorJogador;

    void Start()
    {
        Time.timeScale = 1;
        rigidbodyJogador = GetComponent<Rigidbody>();
        animatorJogador = GetComponent<Animator>();
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

        bool movendo = (direcao != Vector3.zero);
        animatorJogador.SetBool("Movendo", movendo);

        rigidbodyJogador.MovePosition(rigidbodyJogador.position + (direcao * Time.deltaTime * Velocidade));

        Ray raio = Camera.main.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(raio.origin, raio.direction * 100, Color.red);

        RaycastHit impacto;

        if (Physics.Raycast(raio, out impacto, 100, MascaraChao))
        {
            Vector3 posicaoMidaJogador = impacto.point - transform.position;
            posicaoMidaJogador.y = transform.position.y;


            Quaternion novaRotacao = Quaternion.LookRotation(posicaoMidaJogador);
            novaRotacao.x = rigidbodyJogador.rotation.x;
            novaRotacao.z = rigidbodyJogador.rotation.z;

            rigidbodyJogador.MoveRotation(novaRotacao);
        }
    }

    public void ReceberDano(int dano)
    {
        Vida -= dano;
        ControlaInterface.AtualizarSliderVidaJogador();
        ControlaAudio.instancia.PlayOneShot(SomDeDano);
        if (Vida <= 0)
        {
            TextoGameOver.SetActive(true);
            Vivo = false;
            Time.timeScale = 0;
        }
    }

    public bool EstaVivo()
    {
        return Vivo;
    }
}

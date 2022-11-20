using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class ControlaChefe : MonoBehaviour, IMatavel
{
    private Transform jogador;
    private NavMeshAgent agente;
    private Status status;
    private AnimacaoPersonagem animacaoPersonagem;
    private MovimentaPersonagem movimentaPersonagem;
    private ControlaInterface controlaInterface;

    public GameObject KitMedico;
    public Slider SliderVida;
    public Image SliderFillImage;
    public Color CorVidaMaxima, CorVidaMinima;

    private void Start()
    {
        jogador = GameObject.FindWithTag(Tags.Jogador).transform;
        agente = GetComponent<NavMeshAgent>();
        animacaoPersonagem = GetComponent<AnimacaoPersonagem>();
        movimentaPersonagem = GetComponent<MovimentaPersonagem>();
        status = GetComponent<Status>();
        agente.speed = status.Velocidade;
        controlaInterface = GameObject.FindObjectOfType(typeof(ControlaInterface)) as ControlaInterface;
        SliderVida.maxValue = status.VidaInicial;
        AtualizarInterface();
    }

    private void Update()
    {
        agente.SetDestination(jogador.position);
        animacaoPersonagem.Movimentar(agente.velocity.magnitude);

        if (agente.hasPath)
        {
            bool pertoJogador = agente.remainingDistance <= agente.stoppingDistance;

            animacaoPersonagem.Atacar(pertoJogador);


            Vector3 direcao = jogador.position - transform.position;
            movimentaPersonagem.Rotacionar(direcao);
        }
    }

    public void AtacaJogador()
    {
        int dano = Random.Range(30, 41);
        jogador.GetComponent<ControlaJogador>().ReceberDano(dano);
    }

    public void ReceberDano(int dano)
    {
        status.Vida -= dano;
        AtualizarInterface();
        if (status.Vida <= 0)
        {
            Morrer();
        }
    }

    public void Morrer()
    {

        SliderVida.enabled = false;
        controlaInterface.AtualizarQuantidadeZumbisMortos();
        movimentaPersonagem.Morrer();
        animacaoPersonagem.Morrer(Random.Range(1, 3));
        Destroy(gameObject, 6);
        this.enabled = false;
        agente.enabled = false;
        Instantiate(KitMedico, transform.position, Quaternion.identity);
    }

    void AtualizarInterface()
    {
        SliderVida.value = status.Vida;
        float porcentagemVida = (float)status.Vida / status.VidaInicial;
        SliderFillImage.color = Color.Lerp(CorVidaMinima, CorVidaMaxima, porcentagemVida);
    }
}

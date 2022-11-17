using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ControlaChefe : MonoBehaviour, IMatavel
{
    private Transform jogador;
    private NavMeshAgent agente;
    private Status status;
    private AnimacaoPersonagem animacaoPersonagem;
    private MovimentaPersonagem movimentaPersonagem;
    private ControlaInterface controlaInterface;

    public GameObject KitMedico;

    private void Start()
    {
        jogador = GameObject.FindWithTag(Tags.Jogador).transform;
        agente = GetComponent<NavMeshAgent>();
        animacaoPersonagem = GetComponent<AnimacaoPersonagem>();
        movimentaPersonagem = GetComponent<MovimentaPersonagem>();
        status = GetComponent<Status>();
        agente.speed = status.Velocidade;
        controlaInterface = GameObject.FindObjectOfType(typeof(ControlaInterface)) as ControlaInterface;
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
        if (status.Vida <= 0)
        {
            Morrer();
        }
    }

    public void Morrer()
    {
        // ControlaAudio.instancia.PlayOneShot(SomDeMorte);

        controlaInterface.AtualizarQuantidadeZumbisMortos();
        movimentaPersonagem.Morrer();
        animacaoPersonagem.Morrer(Random.Range(1, 3));
        Destroy(gameObject, 6);
        this.enabled = false;
        agente.enabled = false;
        Instantiate(KitMedico, transform.position, Quaternion.identity);
    }
}

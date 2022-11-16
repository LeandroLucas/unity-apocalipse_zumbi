using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeradorZumbis : MonoBehaviour
{

    public GameObject Zumbi;
    public int DistanciaDoJogadorParaGeracao = 20;
    public float TempoGerarZumbi = 1;
    public LayerMask layerZumbi;
    private int distanciaGeracao = 3;
    private float contadorTempo = 0;
    private GameObject jogador;
    private int quantidadeMaximaZumbisVivos = 2;
    private int quantidadeZumbisVivos;
    private float tempoProximoAumentoDeDificuldade = 30;
    private float contadorDeAumentarDificuldade = 0;

    private void Start()
    {
        jogador = GameObject.FindGameObjectWithTag(Tags.Jogador);
        gerarZumbisInicio();
        contadorDeAumentarDificuldade = tempoProximoAumentoDeDificuldade;
    }

    // Update is called once per frame
    void Update()
    {
        bool podeGerarZumbisPelaDistancia = Vector3.Distance(transform.position, jogador.transform.position) > DistanciaDoJogadorParaGeracao;
        if (podeGerarZumbisPelaDistancia && quantidadeZumbisVivos < quantidadeMaximaZumbisVivos)
        {
            contadorTempo += Time.deltaTime;
            if (contadorTempo >= TempoGerarZumbi)
            {
                contadorTempo = 0;
                StartCoroutine(gerarZumbi());
            }
        }
        if (Time.timeSinceLevelLoad > contadorDeAumentarDificuldade)
        {
            contadorDeAumentarDificuldade = Time.timeSinceLevelLoad + tempoProximoAumentoDeDificuldade;
            quantidadeMaximaZumbisVivos++;
        }
    }

    private void gerarZumbisInicio()
    {
        for (int i = 0; i < quantidadeMaximaZumbisVivos; i++)
        {
            StartCoroutine(gerarZumbi());
        }
    }

    IEnumerator gerarZumbi()
    {
        Vector3 posicaoCriacao = PosicaoAleatoria();
        Collider[] colliders = Physics.OverlapSphere(posicaoCriacao, 1, layerZumbi);
        while (colliders.Length > 0)
        {
            posicaoCriacao = PosicaoAleatoria();
            colliders = Physics.OverlapSphere(posicaoCriacao, 1, layerZumbi);
            yield return null;
        }
        ControlaInimigo zumbiCriado = Instantiate(Zumbi, posicaoCriacao, transform.rotation).GetComponent<ControlaInimigo>();
        zumbiCriado.meuGerador = this;
        quantidadeZumbisVivos++;
    }

    Vector3 PosicaoAleatoria()
    {
        Vector3 posicao = Random.insideUnitSphere * distanciaGeracao;
        posicao += transform.position;
        posicao.y = transform.position.y;
        return posicao;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, distanciaGeracao);
    }

    public void DiminuirQuantidadeZumbisVivos()
    {
        quantidadeZumbisVivos--;
    }
}

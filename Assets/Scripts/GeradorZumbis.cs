using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeradorZumbis : MonoBehaviour
{

    public GameObject Zumbi;

    public int DistanciaDoJogadorParaGeracao = 20;

    public float TempoGerarZumbi = 1;
    private float contadorTempo = 0;

    public LayerMask layerZumbi;

    private int distanciaGeracao = 3;

    private GameObject jogador;

    private void Start()
    {
        jogador = GameObject.FindGameObjectWithTag(Tags.Jogador);
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, jogador.transform.position) > DistanciaDoJogadorParaGeracao)
        {
            contadorTempo += Time.deltaTime;
            if (contadorTempo >= TempoGerarZumbi)
            {
                contadorTempo = 0;
                StartCoroutine(gerarZumbi());
            }
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
        Instantiate(Zumbi, posicaoCriacao, transform.rotation);
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
}

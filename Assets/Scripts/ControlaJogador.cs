using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlaJogador : MonoBehaviour
{

    public float Velocidade = 10;

    Vector3 direcao;

    public LayerMask MascaraChao;

    // Update is called once per frame
    void Update()
    {
        float eixoX = Input.GetAxis("Horizontal");
        float eixoZ = Input.GetAxis("Vertical");

        direcao = new Vector3(eixoX, 0, eixoZ);

        bool movendo = (direcao != Vector3.zero);
        GetComponent<Animator>().SetBool("Movendo", movendo);
    }

    void FixedUpdate()
    {
        GetComponent<Rigidbody>().MovePosition(GetComponent<Rigidbody>().position + (direcao * Time.deltaTime * Velocidade));

        Ray raio = Camera.main.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(raio.origin, raio.direction * 100, Color.red);

        RaycastHit impacto;

        if (Physics.Raycast(raio, out impacto, 100, MascaraChao))
        {
            Vector3 posicaoMidaJogador = impacto.point - transform.position;
            posicaoMidaJogador.y = transform.position.y;

            Quaternion novaRotacao = Quaternion.LookRotation(posicaoMidaJogador);

            GetComponent<Rigidbody>().MoveRotation(novaRotacao);
        }
    }
}

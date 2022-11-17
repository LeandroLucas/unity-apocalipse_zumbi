using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimentaPersonagem : MonoBehaviour
{
    private Rigidbody rigidbodyPersonagem;

    private void Awake()
    {
        rigidbodyPersonagem = GetComponent<Rigidbody>();

    }

    public void Movimentar(Vector3 direcao, float velocidade)
    {
        rigidbodyPersonagem.MovePosition(rigidbodyPersonagem.position + (direcao.normalized * Time.deltaTime * velocidade));
    }

    public void Rotacionar(Vector3 direcao)
    {
        Quaternion novaRotacao = Quaternion.LookRotation(direcao);
        novaRotacao.x = rigidbodyPersonagem.rotation.x;
        novaRotacao.z = rigidbodyPersonagem.rotation.z;
        rigidbodyPersonagem.MoveRotation(novaRotacao.normalized);
    }

    public void Morrer()
    {
        // rigidbodyPersonagem.constraints = RigidbodyConstraints.None;
        if(!rigidbodyPersonagem.isKinematic) {
            rigidbodyPersonagem.velocity = Vector3.zero;
        }
        GetComponent<Collider>().enabled = false;
    }
}

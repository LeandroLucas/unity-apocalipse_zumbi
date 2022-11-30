using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bala : MonoBehaviour
{

    public float Velocidade = 30;

    private Rigidbody rigidbodyBala;

    private void Start()
    {
        rigidbodyBala = GetComponent<Rigidbody>();
        Destroy(this, 10);
    }

    void FixedUpdate()
    {
        rigidbodyBala.MovePosition(rigidbodyBala.position + (transform.forward * Velocidade * Time.deltaTime));
    }

    void OnTriggerEnter(Collider objetoDeColisao)
    {
        if (objetoDeColisao.tag == "Inimigo")
        {
            Quaternion rotacaoBala = Quaternion.LookRotation(transform.forward);
            objetoDeColisao.GetComponent<IMatavel>().ReceberDano(transform.position, rotacaoBala, 1);
        }
        Destroy(gameObject);
    }
}

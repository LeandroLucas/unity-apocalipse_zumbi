using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bala : MonoBehaviour
{

    public float Velocidade = 30;

    private Rigidbody rigidbodyBala;

    public AudioClip SomDeMorte;

    private void Start()
    {
        rigidbodyBala = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        rigidbodyBala.MovePosition(rigidbodyBala.position + (transform.forward * Velocidade * Time.deltaTime));
    }

    void OnTriggerEnter(Collider objetoDeColisao)
    {
        if (objetoDeColisao.tag == "Inimigo")
        {
            ControlaAudio.instancia.PlayOneShot(SomDeMorte);
            Destroy(objetoDeColisao.gameObject);
        }
        Destroy(gameObject);
    }
}

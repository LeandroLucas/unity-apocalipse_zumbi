using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimacaoPersonagem : MonoBehaviour
{
    private Animator meuAnimator;

    private void Awake()
    {
        meuAnimator = GetComponent<Animator>();
    }

    public void Atacar(bool estado)
    {
        meuAnimator.SetBool("Atacando", estado);
    }

    public void Movimentar(float valorMovimento)
    {
        meuAnimator.SetFloat("Movendo", valorMovimento);
    }

    /**
    * Tipos de morte: 1 ou 2
    */
    public void Morrer(int tipoMorte)
    {
        meuAnimator.SetInteger("Morrer", tipoMorte);
    }
}

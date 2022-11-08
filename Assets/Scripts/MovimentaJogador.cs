using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimentaJogador : MovimentaPersonagem
{
    public void RotacionarJogador(LayerMask mascaraChao)
    {
        Ray raio = Camera.main.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(raio.origin, raio.direction * 100, Color.red);

        RaycastHit impacto;

        if (Physics.Raycast(raio, out impacto, 100, mascaraChao))
        {
            Vector3 posicaoMidaJogador = impacto.point - transform.position;
            posicaoMidaJogador.y = transform.position.y;


            Rotacionar(posicaoMidaJogador);
        }
    }
}

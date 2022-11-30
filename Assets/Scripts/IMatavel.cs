using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMatavel
{
    public void ReceberDano(Vector3 posicao, Quaternion rotacao, int dano);

    void Morrer();
}

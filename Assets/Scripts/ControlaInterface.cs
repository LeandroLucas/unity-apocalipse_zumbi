using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControlaInterface : MonoBehaviour
{
    private ControlaJogador controlaJogador;
    public Slider SliderVidaJogador;
    // Start is called before the first frame update
    void Start()
    {
        controlaJogador = GameObject.FindWithTag(Tags.Jogador).GetComponent<ControlaJogador>();
        SliderVidaJogador.maxValue = controlaJogador.Status.Vida;
        AtualizarSliderVidaJogador();
    }

    public void AtualizarSliderVidaJogador()
    {
        SliderVidaJogador.value = controlaJogador.Status.Vida;

    }
}

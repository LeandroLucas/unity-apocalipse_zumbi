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
        controlaJogador = GameObject.FindWithTag("Player").GetComponent<ControlaJogador>();
        SliderVidaJogador.maxValue = controlaJogador.Vida;
        AtualizarSliderVidaJogador();
    }

    public void AtualizarSliderVidaJogador()
    {
        SliderVidaJogador.value = controlaJogador.Vida;

    }
}

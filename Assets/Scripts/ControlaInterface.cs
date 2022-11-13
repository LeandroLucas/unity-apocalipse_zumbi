using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ControlaInterface : MonoBehaviour
{
    private ControlaJogador controlaJogador;
    public Slider SliderVidaJogador;

    public GameObject PainelGameOver;

    public Text TextoTempoSobrevivencia;

    public Text TextoRecordeTempoSobrevivencia;

    private float tempoPontuacaoSalvo;

    // Start is called before the first frame update
    void Start()
    {
        controlaJogador = GameObject.FindWithTag(Tags.Jogador).GetComponent<ControlaJogador>();
        SliderVidaJogador.maxValue = controlaJogador.Status.Vida;
        AtualizarSliderVidaJogador();
        Time.timeScale = 1;
        tempoPontuacaoSalvo = PlayerPrefs.GetFloat("PontuacaoMaxima");
    }

    public void AtualizarSliderVidaJogador()
    {
        SliderVidaJogador.value = controlaJogador.Status.Vida;
    }

    public void GameOver()
    {
        PainelGameOver.SetActive(true);
        Time.timeScale = 0;
        int minutos = pegarMin(Time.timeSinceLevelLoad);
        int segundos = pegarSeg(Time.timeSinceLevelLoad);
        TextoTempoSobrevivencia.text = "Você sobreviveu por " + minutos + " minuto(s) e " + segundos + " segundo(s)";
        ajustarPontuacaoMaxima(minutos, segundos);
    }

    void ajustarPontuacaoMaxima(int min, int seg)
    {
        if (Time.timeSinceLevelLoad > tempoPontuacaoSalvo)
        {
            tempoPontuacaoSalvo = Time.timeSinceLevelLoad;
            PlayerPrefs.SetFloat("PontuacaoMaxima", tempoPontuacaoSalvo);
        }
        else
        {
            min = pegarMin(Time.timeSinceLevelLoad);
            seg = pegarSeg(Time.timeSinceLevelLoad);
        }
        TextoRecordeTempoSobrevivencia.text = string.Format("Seu melhor tempo é {0} minuto(s) e {1} segundo(s)", min, seg);
    }

    int pegarMin(float timeInSeconds)
    {
        return (int)(timeInSeconds / 60);
    }

    int pegarSeg(float timeInSeconds)
    {
        return (int)(Time.timeSinceLevelLoad % 60);
    }

    public void Reiniciar()
    {
        SceneManager.LoadScene("game");
    }

}

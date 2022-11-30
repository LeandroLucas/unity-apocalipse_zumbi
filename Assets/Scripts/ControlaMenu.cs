using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ControlaMenu : MonoBehaviour
{

    public GameObject BotaoSair;
    void Start()
    {
#if UNITY_STANDALONE || UNITY_EDITOR
        BotaoSair.SetActive(true);
#endif
    }
    public void Jogar()
    {
        StartCoroutine(MudarCena("game"));
    }

    public void Sair()
    {
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }

    IEnumerator MudarCena(string cena)
    {
        yield return new WaitForSeconds(0.3f);
        SceneManager.LoadScene(cena);
    }
}

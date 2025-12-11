using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;   

public class BarraDeVida : MonoBehaviour
{
    public float vidaJogador = 100f;

    public float vidaMaxima = 100f;
    public Image barraDeVida;


    

    void Start()
    {
        AtualizarBarraDeVida();
    }

    void Update()
    {
        AtualizarBarraDeVida();
    }

    void AtualizarBarraDeVida()
    {
        if(barraDeVida == null) return;

        float vidaNormalizada = Mathf.Clamp01(vidaJogador / vidaMaxima);
        barraDeVida.fillAmount = vidaNormalizada;
    }

}
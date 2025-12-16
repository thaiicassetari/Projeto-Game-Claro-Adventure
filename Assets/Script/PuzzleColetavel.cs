using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PuzzleColetavel : MonoBehaviour
{
    public int totalItens = 1; //17
    public static int ItensColetado = 0;
    public Text textoColetavelAbrigo;
    public UnityEngine.UI.Text ItensColetadoText;

    void Start()
    {
        ItensColetado = 0;
        AtualizarTexto();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ItensColetado++;
            AtualizarTexto();
            Destroy(gameObject);
        }
    }

    private void AtualizarTexto()
    {
        if (textoColetavelAbrigo != null)
        {
            textoColetavelAbrigo.text = ItensColetado.ToString() + " / " + totalItens.ToString();
        }

        // Verifica se o puzzle foi concluído aqui
        //if (ItensColetado >= totalItens)
        //{
        //    ConcluirPuzzle();
        //}

    }
    //private void ConcluirPuzzle()
    //{
    //    Debug.Log("Puzzle Coletável Concluído!");
    //    if (GameManager.Instance != null)
    //    {
    //        GameManager.Instance.PlayPuzzleCompletionSFX(); 
    //    }

    //    this.enabled = false;
    //}
}

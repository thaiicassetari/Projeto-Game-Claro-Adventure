using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PuzzleColetavel : MonoBehaviour
{
    public int totalItens = 17;
    public static int ItensColetado = 0;
    public Text textoColetavelAbrigo;
    public UnityEngine.UI.Text ItensColetadoText;

    void Start()
    {
        ItensColetado = 0;
        AtualizarTexto();
    }

    // Update is called once per frame
    void Update()
    {
        
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

    }
}

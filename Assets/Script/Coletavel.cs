using UnityEngine;
using UnityEngine.UI;

public class Coletavel : MonoBehaviour
{
    public static int totalColetaveis = 0;
    public Text textoColetavel;

    void Start()
    {
        totalColetaveis = 0;
        AtualizarTexto();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            totalColetaveis++;
            AtualizarTexto();
            Destroy(gameObject);
        }
    }

    private void AtualizarTexto()
    {
        if (textoColetavel != null)
        {
            textoColetavel.text = totalColetaveis.ToString();

        }

    }
}

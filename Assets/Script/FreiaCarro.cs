using UnityEngine;

public class FreiaCarro : MonoBehaviour
{

    [Header("Componentes")]
    public MonoBehaviour seguirSpline;  
    // Coloque aqui o componente que move o veículo na Spline 

    [Header("Configurações")]
    public string playerTag = "Player";  // Tag do player

    private bool parado = false;

    // Quando o collider detecta o Player
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(playerTag))
        {
            PararVeiculo();
        }
    }

    // Quando o player sai do trigger, o veículo volta a andar
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(playerTag))
        {
            ContinuarVeiculo();
        }
    }

    void PararVeiculo()
    {
        if (seguirSpline == null) return;

        // Desativar movimento do spline follower
        seguirSpline.enabled = false;
        parado = true;
        Debug.Log("Veículo parado: Player na frente.");
    }

    void ContinuarVeiculo()
    {
        if (seguirSpline == null) return;

        // Reativar movimento
        seguirSpline.enabled = true;
        parado = false;
        Debug.Log("Veículo voltou a andar.");
    }
}

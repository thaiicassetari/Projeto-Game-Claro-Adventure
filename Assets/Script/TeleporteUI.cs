using UnityEngine;

public class TeleporteUI : MonoBehaviour
{
    private Teleporte portalAtual;

    public GameObject painelMenu;
    public Transform[] destinos; 

    public void AbrirMenu(Teleporte portal)
    {
        portalAtual = portal;
        painelMenu.SetActive(true);
    }

    public void FecharMenu(Teleporte portal)
    {
        painelMenu.SetActive(false);
    } 
    public void EscolherDestino(int index)
    {
        if (portalAtual == null) return;
        painelMenu.SetActive(false);
        portalAtual.Teleportar(destinos[index]);
    }
}

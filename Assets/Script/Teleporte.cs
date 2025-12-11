using UnityEngine;

public class Teleporte : MonoBehaviour
{
    public Transform _player;        // Transform do player
    public GameObject _playerObj;    // Obj do player (para desativar/ativar)
    public TeleporteUI teleporteUI;  // Referência ao MENU UI

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            teleporteUI.AbrirMenu(this);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        teleporteUI.FecharMenu(this);

    }

    // Este método será chamado pela UI (recebe o Transform do destino)
    public void Teleportar(Transform destinoEscolhido)
    {
        _playerObj.SetActive(false);
        _player.position = destinoEscolhido.position;
        _playerObj.SetActive(true);
    }
}

using UnityEngine;

public class Teleporte : MonoBehaviour
{
    public Transform _player;   
    public GameObject _playerObj;  
    public TeleporteUI teleporteUI;  

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

    public void Teleportar(Transform destinoEscolhido)
    {
        _playerObj.SetActive(false);
        _player.position = destinoEscolhido.position;
        _playerObj.SetActive(true);
    }
}

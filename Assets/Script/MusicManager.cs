using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance;

    private void Awake()
    {
        // 1. Lógica de Singleton (garante que só haja um)
        if (Instance == null)
        {
            Instance = this;
            // 2. Comanda a Unity para manter este objeto vivo entre as cenas
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            // Se já existe uma instância (que veio de outra cena), destrói o novo
            Destroy(gameObject);
        }
    }

    // Você pode adicionar uma função aqui se precisar parar ou trocar a música,
    // mas o AudioSource no objeto já fará o play em loop.
}
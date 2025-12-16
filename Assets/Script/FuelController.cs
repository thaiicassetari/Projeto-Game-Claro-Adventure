using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class _FuelController : MonoBehaviour
{
    public float _fuelMax = 100f;
    public float _fuelMin = 0f;
    public float _fuel;
    public float _fuelDecay;

    public float _dano;
    public Slider _fuelBar;

    public float _abastecer;

    // Suavização
    private float _displayedFuel = 0f;
    public float _suavizarDano = 5f; // maior = mais rápido
    public float _suavizarAbastecer = 5f;

    void Start()
    {
        _fuel = _fuelMax;
        _displayedFuel = _fuelMax;

        _fuelBar.maxValue = _fuelMax;
        _fuelBar.minValue = _fuelMin;
        _fuelBar.value = _displayedFuel;
    }

    void Update()
    {
        // Consome combustível com o tempo
        _fuel -= _fuelDecay;
        _fuel = Mathf.Clamp(_fuel, _fuelMin, _fuelMax);

        // Suaviza a barra de combustível
        _displayedFuel = Mathf.Lerp(_displayedFuel, _fuel, Time.deltaTime * _suavizarDano);
        _fuelBar.value = _displayedFuel;

        // Quando acabar o combustível real, desativa o player
        if (_fuel <= _fuelMin)
        {
            gameObject.SetActive(false);
            SceneManager.LoadScene(2);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //if (other.CompareTag("Inimigo"))
        //{
        //    _TomarDano();
        //   // Debug.Log("Colidiu com inimigo via TRIGGER.");
        //}
        if (other.CompareTag("Vida"))
        {
            Abastece();
        }
    }

    void _TomarDano()
    {
        _fuel -= _dano;
        _fuel = Mathf.Clamp(_fuel, _fuelMin, _fuelMax);
        // _fuelBar.value será atualizado suavemente via Update()

    }

    void Abastece()
    {
        _fuel += _abastecer;
        _fuel = Mathf.Clamp(_fuel, _fuelMin, _fuelMax);
        // _fuelBar.value será atualizado suavemente via Update()
    }
}
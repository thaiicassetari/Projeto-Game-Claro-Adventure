using System.Collections.Generic;
using UnityEngine;

public class PosicionaVida : MonoBehaviour
{
    public GameObject[] _gasolina;
    public List<GameObject> _novaGasolina;
    public Vector2 _quantidadeGasolina;

    private void Start()
    {

        int novoNumeroGasolina = (int)Random.Range(_quantidadeGasolina.x, _quantidadeGasolina.y);

        //instancia a gasolina aleatoriamente
        for (int i = 0; i < novoNumeroGasolina; i++)
        {
            _novaGasolina.Add(Instantiate(_gasolina[Random.Range(0, _gasolina.Length)], transform));
            _novaGasolina[i].SetActive(false);
        }

        PosicionaInimigo();
    }

    void PosicionaInimigo()
    {
        for (int i = 0; i < _novaGasolina.Count; i++)
        {
            //tem que saber qual o tamanho do "terreno" para poder posicionar o inimigos
            float posZMinima = (10f / _novaGasolina.Count) + (80 / _novaGasolina.Count) * i;
            float posZMaxima = (10f / _novaGasolina.Count) + (80 / _novaGasolina.Count) * i + 1;

            //float posXMinima = (-14f / _novaGasolina.Count) + (13f / _novaGasolina.Count) * i;
            //float posXmaxima = (-14f / _novaGasolina.Count) + (13f / _novaGasolina.Count) * i + 1;

            _novaGasolina[i].transform.localPosition = new Vector3(Random.Range(-10f, 11f), 0, Random.Range(posZMinima, posZMaxima));
            _novaGasolina[i].SetActive(true);
        }

    }
}

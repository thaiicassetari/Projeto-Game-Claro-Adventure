using System;
using UnityEngine;

public class GoalChecker : MonoBehaviour
{
    private BoxIdentifier goalID;

    void Start()
    {
        goalID = GetComponent<BoxIdentifier>();
        if (goalID == null)
        {
            Debug.LogError("GoalChecker requer o componente BoxIdentifier no mesmo objeto.");
        }
    }

    //quando um objeto entra no Collider (Is Trigger = true)
    private void OnTriggerEnter(Collider other) // Se for 2D, use OnTriggerEnter2D
    {
        BoxIdentifier box = other.GetComponent<BoxIdentifier>(); //pegar o BoxIdentifier do objeto que esta entrando

        if (box != null && !box.isSolved)
        {
            if (box.boxID == goalID.boxID)
            {
                box.isSolved = true;

                // Exemplo de ação: Mudar a cor da caixa para verde
                // var renderer = other.GetComponent<Renderer>();
                // renderer.material.color = Color.green; 

                // Notifica o Game Manager para contar o ponto
                GameManager.Instance.BoxPlacedCorrectly();
            }
        }
    }

    // Opcional: Se a caixa for movida para fora, reverter o estado
    private void OnTriggerExit(Collider other) // Se for 2D, use OnTriggerExit2D
    {
        BoxIdentifier box = other.GetComponent<BoxIdentifier>();

        // Verifica se a caixa está saindo de um destino correto.
        if (box != null && box.isSolved && box.boxID == goalID.boxID)
        {
            box.isSolved = false;// Notifica para remover o ponto
            GameManager.Instance.BoxRemoved();
        }
    }
}

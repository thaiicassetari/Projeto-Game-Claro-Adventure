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
          //  Debug.LogError("GoalChecker requer o componente BoxIdentifier no mesmo objeto.");
        }
    }
    
    private void OnTriggerEnter(Collider other) 
    {
        BoxIdentifier box = other.GetComponent<BoxIdentifier>();

        if (box != null && !box.isSolved)
        {
            if (box.boxID == goalID.boxID)
            {
                box.isSolved = true;
                GameManager.Instance.BoxPlacedCorrectly();
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        BoxIdentifier box = other.GetComponent<BoxIdentifier>();
        if (box != null && box.isSolved && box.boxID == goalID.boxID)
        {
            box.isSolved = false;
            GameManager.Instance.BoxRemoved();
        }
    }
}

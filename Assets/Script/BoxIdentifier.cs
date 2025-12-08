using UnityEngine;

public class BoxIdentifier : MonoBehaviour
{// ID DE 1 A 4: 1=E-lixo, 2=Reciclável, 2=Metal, 4=Orgânico
    public int boxID;


    // Usado para garantir que a caixa só conte como ponto uma vez
    [HideInInspector] public bool isSolved = false;
}
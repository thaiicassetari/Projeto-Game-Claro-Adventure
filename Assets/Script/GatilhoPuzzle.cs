using UnityEngine;

public class GatilhoPuzzle : MonoBehaviour
{
    public GameObject puzzleUI;
    private const string TagDoPlayer = "Player";

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(TagDoPlayer))
        {
            puzzleUI.SetActive(true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(TagDoPlayer))
        {
            puzzleUI.SetActive(false);
        }
    }
}

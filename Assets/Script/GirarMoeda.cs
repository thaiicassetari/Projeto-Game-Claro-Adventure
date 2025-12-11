using UnityEngine;

public class GirarMoeda : MonoBehaviour
{
    public float speedGiro = 100f;
    public int ladoRotacao;

    // Update is called once per frame
    void Update()
    {
        switch (ladoRotacao)
        {
            case 1:
                transform.Rotate(Vector3.up * speedGiro * Time.deltaTime);
                break;

            case 2:
                transform.Rotate(Vector3.forward * speedGiro * Time.deltaTime);
                break;

            case 3:
                transform.Rotate(Vector3.right * speedGiro * Time.deltaTime);
                break;
        }
        
    }
}

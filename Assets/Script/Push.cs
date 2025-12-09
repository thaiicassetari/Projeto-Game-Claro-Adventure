using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class Push : MonoBehaviour
{
    public float power;
    private Rigidbody rb;

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.collider.CompareTag("ObjectPush"))
        {
            rb = hit.collider.GetComponent<Rigidbody>();
            if (rb != null)
            {
                //Debug.Log("Colidiu com o objeto empurravel");
                Vector3 direction = hit.gameObject.transform.position - this.transform.position;
                direction.y = 0;
                direction.Normalize();
                rb.AddForceAtPosition(direction * power, transform.position, ForceMode.Impulse);
            }

        }
    }
}

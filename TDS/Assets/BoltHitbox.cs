using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoltHitbox : MonoBehaviour
{
    public Vector3 dir;
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other);
        if (other.gameObject.tag == "Enemy")
        {
            dir = gameObject.GetComponentInParent<Bolt>().getShootDirection();
            Rigidbody rb = other.gameObject.GetComponent<Rigidbody>();
            dir.y = 0;
            rb.AddForce(dir.normalized * gameObject.GetComponentInParent<Bolt>().KnockbackStrength, ForceMode.Impulse);
        }
    }
}

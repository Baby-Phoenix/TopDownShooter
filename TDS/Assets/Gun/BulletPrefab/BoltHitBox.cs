using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class BoltHitBox : Bolt
{
    private void OnTriggerEnter(Collider other)
    {
        Rigidbody rb = other.gameObject.GetComponent<Rigidbody>();
        Debug.Log(other.gameObject);
        if (other.gameObject.tag == "Player")
        {

            Vector3 direction = rb.position + transform.position;
            direction.y = 0;

            rb.AddForce(direction.normalized * KnockbackStrength, ForceMode.Impulse);
        }
    }
}
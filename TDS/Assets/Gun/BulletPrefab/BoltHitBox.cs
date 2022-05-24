using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoltHitbox : PrefabBullet
{
    public bool explodeOnTouch = true;
    private void Update()
    {
        isExplosive = gameObject.GetComponentInParent<Bolt>().isExplosive;
        explosion = gameObject.GetComponentInParent<Bolt>().explosion;
        explosionDamage = gameObject.GetComponentInParent<Bolt>().explosionDamage;
        explosionRange = gameObject.GetComponentInParent<Bolt>().explosionRange;
        explosionForce = gameObject.GetComponentInParent<Bolt>().explosionForce;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            if (explodeOnTouch == true && isExplosive == true)
            {
                Explode();
            }
            dir = gameObject.GetComponentInParent<Bolt>().getShootDirection();
            Rigidbody rb = other.gameObject.GetComponent<Rigidbody>();
            rb.AddForce(dir.normalized * gameObject.GetComponentInParent<Bolt>().KnockbackStrength, ForceMode.Impulse);
        }
    }
}

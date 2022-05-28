using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoltKnockHitbox : BoltBullet
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            Rigidbody rb = other.gameObject.GetComponent<Rigidbody>();
            rb.AddForce(transform.parent.GetComponent<BoltBullet>().getKnockbackDirection().normalized * transform.parent.GetComponent<BoltBullet>().getKnockbackStrength(), ForceMode.Impulse);
            GameObject effect = Instantiate(muzzleFlash, transform.position, Quaternion.identity);
            Destroy(effect, 5);
        }
    }
}

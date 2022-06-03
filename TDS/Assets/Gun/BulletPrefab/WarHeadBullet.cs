using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WarheadBullet : BulletPrefab
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Wall" || collision.gameObject.tag == "Enemy")
        {
            EffectOnHit();
            GameObject effect = Instantiate(muzzleFlash, transform.position, Quaternion.identity);
            Rigidbody rb = collision.gameObject.GetComponent<Rigidbody>();
            if (rb != null && collision.gameObject.tag == "Enemy") 
            {
                collision.gameObject.GetComponent<Target>().TakeDamage(damage);
                Debug.Log(knockbackStrength);
                rb.AddForce(knockbackDirection * knockbackStrength, ForceMode.Impulse);
                if (collision.gameObject.GetComponent<NavMeshAgent>().isActiveAndEnabled)
                {
                    collision.gameObject.GetComponent<NavMeshAgent>().enabled = false;               }
            }
            Destroy(effect, 2);
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        EffectWhileBulletFlying();
    }
}

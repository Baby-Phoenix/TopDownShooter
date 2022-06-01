using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoltBullet : BulletPrefab
{
    bool homingStop;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Wall")
        {
            EffectOnHit();
            gameObject.GetComponent<Rigidbody>().velocity = new Vector3(0,0,0);
            gameObject.GetComponent<Rigidbody>().isKinematic = true;
            GameObject effect = Instantiate(muzzleFlash, transform.position, Quaternion.identity);
            Destroy(effect, 5);
            Destroy(gameObject, 0.5f);
        }
        else
        {
            Destroy(gameObject, 2f);
        }
    }
    private void Update()
    {
        if (gameObject.GetComponent<Rigidbody>()!=null&&gameObject.GetComponent<Rigidbody>().isKinematic != true)
        {
            EffectWhileBulletFlying();
        }
    }
    public Vector3 getKnockbackDirection()
    {
        return knockbackDirection;
    }

    public float getKnockbackStrength()
    {
        return knockbackStrength;
    }

    public float getDamage()
    {
        return damage;
    }

}

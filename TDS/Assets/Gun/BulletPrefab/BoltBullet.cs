using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoltBullet : BulletPrefab
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Wall")
        {
            gameObject.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
            GameObject effect = Instantiate(muzzleFlash, transform.position, Quaternion.identity);
            Destroy(effect, 5);
            Destroy(gameObject, 0.5f);
        }
        else
        {
            Destroy(gameObject, 2f);
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarheadBullet : BulletPrefab
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Wall" || collision.gameObject.tag == "Enemy")
        {
            GameObject effect = Instantiate(muzzleFlash, transform.position, Quaternion.identity);
            Rigidbody rb = collision.gameObject.GetComponent<Rigidbody>();
            if (rb != null && collision.gameObject.tag == "Enemy") 
            {
                mod.ModEffect1(collision.transform.position);
                rb.AddForce(knockbackDirection.normalized * knockbackStrength, ForceMode.Impulse);
            }
            Destroy(effect, 5);
            Destroy(gameObject);
        }
    }
}

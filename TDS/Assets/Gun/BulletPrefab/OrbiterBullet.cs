using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbiterBullet : BulletPrefab
{
    GameObject orbiter;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Wall" || collision.gameObject.tag == "Enemy")
        {

            if (explosion != null) Instantiate(explosion, transform.position, Quaternion.identity);
            Collider[] enemies = Physics.OverlapSphere(transform.position, 5, whatIsEnemy);
            for (int i = 0; i < enemies.Length; i++)
            {
                if (enemies[i].GetComponent<Rigidbody>() && enemies[i].tag == "Enemy")
                {
                    enemies[i].GetComponent<Rigidbody>().AddExplosionForce(50, transform.position, 5);
                }
            }

            GameObject effect = Instantiate(muzzleFlash, transform.position, Quaternion.identity);
            Rigidbody rb = collision.gameObject.GetComponent<Rigidbody>();
            if (rb != null && collision.gameObject.tag == "Enemy") 
            {
                collision.gameObject.GetComponent<Target>().TakeDamage(damage);
                rb.AddForce(knockbackDirection.normalized * knockbackStrength, ForceMode.Impulse);
            }
            Destroy(effect, 2);
            Destroy(gameObject);
        }
    }


    private void FixedUpdate()
    {
        Vector3 heading;
        if (orbiter != null&& orbiter.GetComponent<Orbiter>().getTarget() != null && gameObject.GetComponent<Rigidbody>() != null )
        {
            target=orbiter.GetComponent<Orbiter>().getTarget();
            heading = target.transform.position - transform.position;
            heading.Normalize();
            Vector3 rotationAmount = Vector3.Cross(transform.forward, heading);
            gameObject.GetComponent<Rigidbody>().angularVelocity = rotationAmount * 5;
            gameObject.GetComponent<Rigidbody>().velocity = transform.forward * bulletSpeed;
        }
    }
    protected override void Awake()
    {
        Destroy(gameObject, 5);
    }

    public void setOrbiter(GameObject orb)
    {
        orbiter = orb;
    }
}

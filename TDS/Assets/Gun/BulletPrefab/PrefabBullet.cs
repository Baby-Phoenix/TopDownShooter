using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabBullet : MonoBehaviour
{
    [HideInInspector] public float bulletspeed;
    public Rigidbody rb;
    public Vector3 dir;
    public LayerMask whatIsEnemies;

    public bool isExplosive=false;
    public GameObject explosion;
    public float explosionDamage;
    public float explosionRange;
    public float explosionForce;

    public void setBulletSpeed(float BulletSpeed)
    {
        bulletspeed = BulletSpeed;
    }
    public void setShootDirection(Vector3 Shootdir)
    {
        dir = Shootdir;
    }
    public Vector3 getShootDirection()
    {
        return dir;
    }

    public void Explode()
    {
        if (explosion != null)
        {
            Instantiate(explosion, transform.position, Quaternion.identity);

            Collider[] enemies = Physics.OverlapSphere(transform.position, explosionRange, whatIsEnemies);
            for (int i = 0; i < enemies.Length; i++)
            {
                enemies[i].GetComponent<Target>().TakeDamage(10);
                if (enemies[i].GetComponent<Rigidbody>())
                {
                    enemies[i].GetComponent<Rigidbody>().AddExplosionForce(explosionForce, transform.position, explosionRange);
                }
            }
        }

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPrefab : MonoBehaviour
{
    public ModFunction.Effect f1;
    public ModFunction.Effect f2;
    public ModFunction.Effect f3;

    protected float knockbackStrength;
    protected float damage;
    protected float bulletSpeed;

    protected Vector3 knockbackDirection;

    public GameObject muzzleFlash;
    public GameObject explosion;
    public LayerMask whatIsEnemy;

    protected void Awake()
    {
        Destroy(gameObject,5);
    }
    public void setKnockbackStrength(float k)
    {
        knockbackStrength = k;
    }
    public void setDamage(float d)
    {
        damage = d;
    }
    public void setKnockbackDirection(Vector3 dir)
    {
        knockbackDirection = dir;
    }
    public void setBulletSpeed(float speed)
    {
        bulletSpeed = speed;
    }
    public void setModFunction1(ModFunction.Effect function1)
    {
        f1 = function1;
    }
    public void setModFunction2(ModFunction.Effect function2)
    {
        f3 = function2;
    }
    public void setModFunction3(ModFunction.Effect function3)
    {
        f3 = function3;
    }

    //Mod
    protected virtual void EffectOnHit()
    {
        switch (f1)
        {
            case ModFunction.Effect.Explosive:
                if (explosion != null) Instantiate(explosion, transform.position, Quaternion.identity);
                Collider[] enemies = Physics.OverlapSphere(transform.position, 5, whatIsEnemy);
                for (int i = 0; i < enemies.Length; i++)
                {
                    if (enemies[i].GetComponent<Rigidbody>())
                    {
                        enemies[i].GetComponent<Rigidbody>().AddExplosionForce(50, transform.position, 5);
                    }
                }
                break;
        }
    }
}

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

    protected Vector3 firePosition;
    protected Vector3 knockbackDirection;

    public GameObject muzzleFlash;
    public GameObject explosion;
    public LayerMask whatIsEnemy;



    ////Mod
    //Get mouse position
    public Vector3 screenPosition;
    public Vector3 worldPosition;

    protected Collider target;
    protected void Awake()
    {
        LockOn();
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

    public void setFirePosition(Vector3 pos)
    {
        firePosition = pos;
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
        f2 = function2;
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
        switch (f2)
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
        switch (f3)
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

        protected virtual void EffectWhileBulletFlying()
    {
        switch (f1)
        {
            case ModFunction.Effect.Homing:
                Homing();
                break;
        }
        switch (f2)
        {
            case ModFunction.Effect.Homing:
                Homing();
                break;
        }
        switch (f3)
        {
            case ModFunction.Effect.Homing:
                Homing();
                break;
        }
    }

    protected virtual void LockOn()
    {
        screenPosition = Input.mousePosition;
        Ray ray = Camera.main.ScreenPointToRay(screenPosition);
        if (Physics.Raycast(ray, out RaycastHit hitData))
        {
            worldPosition = hitData.point;
        }
        float minDist = Mathf.Infinity;
        Collider[] enemies = Physics.OverlapSphere(worldPosition, 5, whatIsEnemy);
        for (int i = 0; i < enemies.Length; i++)
        {
            if (enemies[i].tag == "Enemy")
            {
                float dist = Vector3.Distance(enemies[i].transform.position, worldPosition);
                if (dist < minDist)
                {
                    target = enemies[i];
                    minDist = dist;
                }
            }
        }
    }

    private void Homing()
    {
        Vector3 heading;
        Debug.Log(Vector3.Distance(firePosition, transform.position));
        if (target != null && Vector3.Distance(firePosition, transform.position) > 3)
        {
            heading = target.transform.position - transform.position;
            heading.Normalize();
            Vector3 rotationAmount = Vector3.Cross(transform.forward, heading);
            gameObject.GetComponent<Rigidbody>().angularVelocity = rotationAmount * 2;
            gameObject.GetComponent<Rigidbody>().velocity = transform.forward * bulletSpeed;
        }
    }
}

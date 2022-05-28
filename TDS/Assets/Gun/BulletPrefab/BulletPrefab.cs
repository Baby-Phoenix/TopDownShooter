using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPrefab : MonoBehaviour
{
    protected float knockbackStrength;
    protected float damage;

    protected Vector3 knockbackDirection;

    public GameObject muzzleFlash;

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
}

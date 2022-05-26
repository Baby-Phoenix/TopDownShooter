using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chamber : MonoBehaviour
{
    public bool allowButtonHold;
    public bool IsHandLoaded;

    public float damage_M;
    public float timeBetweenShooting_M;


    public virtual float damageModifier(float baseDamage)
    {
        return baseDamage * damage_M;
    }

    public virtual float FiringSpeedModifier(float baseFiringSpeed)
    {
        return baseFiringSpeed * timeBetweenShooting_M;
    }
}

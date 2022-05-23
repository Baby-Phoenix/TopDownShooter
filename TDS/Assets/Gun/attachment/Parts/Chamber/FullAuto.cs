using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FullAuto : Chamber
{
    public FullAuto()
    {
        allowButtonHold = true;
        IsHandLoaded = false;
        damage_M = 0;
        timeBetweenShooting_M = 0.1f;
    }

    public override float damageModifier(float baseDamage) { 
        return baseDamage* damage_M; 
    }

    public override float FiringSpeedModifier(float baseFiringSpeed)
    {
        return baseFiringSpeed * timeBetweenShooting_M;
    }
}

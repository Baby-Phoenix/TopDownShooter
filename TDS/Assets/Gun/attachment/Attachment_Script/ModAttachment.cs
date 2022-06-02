using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Mod", menuName = "Gun/Attachment/Mod")]

public class ModAttachment : ScriptableObject
{
    public ModFunction f1;
    public ModFunction f2;
    public ModFunction f3;

    private ModFunction.EffectType effectType1;
    private ModFunction.Effect effect1;

    private ModFunction.EffectType effectType2;
    private ModFunction.Effect effect2;

    private ModFunction.EffectType effectType3;
    private ModFunction.Effect effect3;

    private float damageModifier;
    private float fireRateModifier;
    private float spreadModifier;
    private float rangeModifier;
    private float reloadTimeModifier;
    private float timeBetweenShootsModifier;
    private int bulletPerTapModifier;
    private float bulletSpeedModifier;
    private int magazineSizeModifier;
    private float knockbackStrengthModifier;
    //For mode Function
    private bool DualModeOn = false;
    private bool OrbiterOn = false;
    public void setModAttachmentModifier()
    {
        damageModifier = (1+f1.damageModifier) * (1 + f2.damageModifier) * (1 + f3.damageModifier);
        fireRateModifier = (1 - f1.fireRateModifier) * (1 - f2.fireRateModifier) * (1 - f3.fireRateModifier);
        spreadModifier = (1- f1.spreadModifier) * (1 - f2.spreadModifier) * (1 - f3.spreadModifier);
        rangeModifier = (1+f1.rangeModifier) *  (1 + f2.rangeModifier) * (1 + f3.rangeModifier);
        reloadTimeModifier = (1 - f1.reloadTimeModifier) * (1 - f2.reloadTimeModifier) * (1 - f3.reloadTimeModifier);
        timeBetweenShootsModifier = (1-f1.timeBetweenShootsModifier) * (1-f2.timeBetweenShootsModifier) * (1-f3.timeBetweenShootsModifier);
        bulletPerTapModifier = (1+f1.bulletPerTapModifier) * (1+f2.bulletPerTapModifier) * (1+f3.bulletPerTapModifier);
        bulletSpeedModifier = (1+f1.bulletSpeedModifier) * (1+f2.bulletSpeedModifier) * (1+f3.bulletSpeedModifier);
        magazineSizeModifier = (1+f1.magazineSizeModifier) * (1+f2.magazineSizeModifier) * (1+f3.magazineSizeModifier);
        knockbackStrengthModifier = (1 + f1.knockbackStrengthModifier) * (1 + f2.knockbackStrengthModifier) * (1 + f3.knockbackStrengthModifier);

        effectType1 = f1.effectType;
        effectType2 = f2.effectType;
        effectType3 = f3.effectType;

        effect1 = f1.effect;
        effect2 = f2.effect;
        effect3 = f3.effect;

        //Dual Gun mode
        if (f1.effect== ModFunction.Effect.DualMode|| f2.effect == ModFunction.Effect.DualMode|| f3.effect == ModFunction.Effect.DualMode)
        {
            DualModeOn = true;
        }
        else
        {
            DualModeOn = false;
        }
        //Orbiter
        if (f1.effect == ModFunction.Effect.Orbiter || f2.effect == ModFunction.Effect.Orbiter || f3.effect == ModFunction.Effect.Orbiter)
        {
            OrbiterOn = true;
        }
        else
        {
            OrbiterOn = false;
        }
    }

    public virtual float DamageModifier()
    {
        return damageModifier;
    }
    public virtual float FireRateModifier()
    {
        return fireRateModifier;
    }
    public virtual float SpreadModifier()
    {
        return spreadModifier;
    }
    public virtual float RangeModifier()
    {
        return rangeModifier;
    }
    public virtual float ReloadTimeModifier()
    {
        return reloadTimeModifier;
    }
    public virtual int BulletPerTapModifier()
    {
        return bulletPerTapModifier;
    }
    public virtual float BulletSpeedModifier()
    {
        return bulletSpeedModifier;
    }
    public virtual int MagazineSizeModifier()
    {
        return magazineSizeModifier;
    }
    public virtual float KnockbackStrengthModifier()
    {
        return knockbackStrengthModifier;
    }

    public ModFunction.EffectType GetEffectType1()
    {
        return effectType1;
    }
    public ModFunction.EffectType GetEffectType2()
    {
        return effectType2;
    }
    public ModFunction.EffectType GetEffectType3()
    {
        return effectType3;
    }

    public ModFunction.Effect GetEffect1()
    {
        return effect1;
    }
    public ModFunction.Effect GetEffect2()
    {
        return effect2;
    }
    public ModFunction.Effect GetEffect3()
    {
        return effect3;
    }

    public bool GetDualMode()
    {
        return DualModeOn;
    }
    public bool GetOrbiterOn()
    {
        return OrbiterOn;
    }
}

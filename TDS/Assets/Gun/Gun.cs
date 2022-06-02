using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
[CreateAssetMenu(fileName = "New Gun", menuName = "Gun/Gun")]
public class Gun : ScriptableObject
{
    public new string name;
    public BarrelAttachment barrel;
    public ChamberAttachment chamber;
    public ScopeAttachment scope;
    public MagazineAttachment magazine;
    public StockAttachment stock;
    public ModAttachment mod;

    private GameObject bulletPrefab;

    private float damage;
    private float fireRate, spread, range, reloadTime, timeBetweenShoots, knockbackStrength, bulletSpeed;
    private int magazineSize, bulletsPerTap;
    private bool allowButtonHold;
    private int bulletsLeft, bulletsShot;

    private bool isRaycast,isShotgun, isHandloaded, canPenetrate;


    public void GunUpdate()
    {
        float baseDamage;
        float baseFireRate;
        float baseSpread;
        float baseRange;
        float baseReloadTime;
        float baseTimeBetweenShots;
        float baseKnockbackStrength;
        float baseBulletSpeed;
        int baseBulletsPerTap;
        int baseMagazineSize;
        //barial
        baseDamage = barrel.baseDamage;
        baseFireRate = barrel.baseFireRate;
        baseSpread = barrel.baseSpread;
        baseRange = barrel.baseRange;
        baseReloadTime = barrel.baseReloadTime;
        baseTimeBetweenShots = barrel.baseTimeBetweenShoots;
        baseBulletsPerTap = barrel.baseBulletsPerTap;
        baseKnockbackStrength = barrel.baseKnockbackStrength;

        mod.setModAttachmentModifier();

        isShotgun = barrel.isShotgun;

        //Chamber
        allowButtonHold = chamber.allowButtonHold;
        isHandloaded = chamber.isHandLoaded;

        //Magazine

        isRaycast = magazine.isRaycast;
        canPenetrate = magazine.canPenetrate;

        baseMagazineSize = magazine.baseMagazineSize;

        //Add all the Modifier
        damage = baseDamage * chamber.DamageModifier(baseDamage) * magazine.DamageModifier(baseDamage)* mod.DamageModifier();
        fireRate = baseFireRate * chamber.FireRateModifier(baseFireRate) * mod.FireRateModifier();
        spread = baseSpread * scope.SpreadModifier(baseSpread) * stock.SpreadModifier(baseSpread)* mod.SpreadModifier();
        range = baseRange * scope.RangeModifier(baseRange)*mod.RangeModifier();
        reloadTime = baseReloadTime * chamber.ReloadTimeModifier(baseReloadTime)*mod.ReloadTimeModifier();
        timeBetweenShoots = baseTimeBetweenShots;
        knockbackStrength = baseKnockbackStrength * magazine.KnockbackStrengthModifier(baseKnockbackStrength)*mod.KnockbackStrengthModifier();
        bulletsPerTap = baseBulletsPerTap * mod.BulletPerTapModifier();
        magazineSize = baseMagazineSize * mod.MagazineSizeModifier();

        //For Prefab Bullet
        if (magazine.bulletPrefab != null)
        {
            bulletPrefab = magazine.bulletPrefab;
            baseBulletSpeed = magazine.baseBulletSpeed;
            bulletSpeed = baseBulletSpeed * barrel.BulletSpeedModifer(baseBulletSpeed)*mod.BulletSpeedModifier();
        }
    }

    public GameObject getBulletPrefab()
    {
        return bulletPrefab;
    }

    public float getDamege()
    {
        return damage;
    }
    public float getFireRate()
    {
        return fireRate;
    }
    public float getRange()
    {
        return range;
    }
    public float getReloadTime()
    {
        return reloadTime;
    }
    public float getTimeBetweenShoots()
    {
        return timeBetweenShoots;
    }
    public float getSpread()
    {
        return spread;
    }

    public float getKnockbackStrength()
    {
        return knockbackStrength;
    }

    public float getBulletSpeed()
    {
        return bulletSpeed;
    }

    public int getMagazineSize()
    {
        return magazineSize;
    }
    public int getBulletsPerTap()
    {
        return bulletsPerTap;
    }

    public bool getIsShotgun()
    {
        return isShotgun;
    }
    public bool getIsRaycast()
    {
        return isRaycast;
    }
    public bool getAllowButtonHold()
    {
        return allowButtonHold;
    }

    public bool getIsHandLoaded()
    {
        return isHandloaded;
    }

    public bool getCanPenetrate()
    {
        return canPenetrate;
    }
}

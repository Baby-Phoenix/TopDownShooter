using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosive : Mod
{
    public GameObject explosion;
    public float explosionDamage;
    public float explosionRange;
    public float explosionForce;
    public override void initMod(GameObject gun)
    {
        level = 0;
        EXP = 0;
        explosionDamage = 10;
        explosionRange = 0.5f;
        explosionForce = 10;
        if (gun.GetComponent<Gun>().IsRayCast == false)
        {
            gun.GetComponent<Gun>().bulletPrefab.GetComponent<PrefabBullet>().isExplosive = true;
            gun.GetComponent<Gun>().bulletPrefab.GetComponent<PrefabBullet>().explosion = explosion;
            gun.GetComponent<Gun>().bulletPrefab.GetComponent<PrefabBullet>().explosionDamage = explosionDamage;
            gun.GetComponent<Gun>().bulletPrefab.GetComponent<PrefabBullet>().explosionRange = explosionRange;
            gun.GetComponent<Gun>().bulletPrefab.GetComponent<PrefabBullet>().explosionForce = explosionForce;
        }
    }
    public override void undoMod(GameObject gun)
    {
        if (gun.GetComponent<Gun>().IsRayCast == false)
        {
            gun.GetComponent<Gun>().bulletPrefab.GetComponent<PrefabBullet>().isExplosive = false;
            gun.GetComponent<Gun>().bulletPrefab.GetComponent<PrefabBullet>().explosion = null;
            gun.GetComponent<Gun>().bulletPrefab.GetComponent<PrefabBullet>().explosionDamage = 0;
            gun.GetComponent<Gun>().bulletPrefab.GetComponent<PrefabBullet>().explosionRange = 0;
            gun.GetComponent<Gun>().bulletPrefab.GetComponent<PrefabBullet>().explosionForce = 0;
        }
    }
}

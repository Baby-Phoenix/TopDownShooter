using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Firearm : MonoBehaviour
{
    //bullets count
    int bulletsLeft, bulletsShot;

    //bools
    bool shooting, readyToShoot, reloading;
    bool allowInvoke = true;
    //For Dual Mode
    bool dualMode = false;
    bool IsShootingFromLeft = true;
    //Reference
    public Gun gun;
    public Transform attackPoint;

    public Transform attackPointLeft;
    public Transform attackPointRight;

    public RaycastHit rayHit;
    public LayerMask whatIsEnemy;

    //Graphics
    public GameObject muzzleFlash, HitEffect;
    public TextMeshProUGUI text;
    public GameObject explosion;

    private void Awake()
    {
        gun.GunUpdate();
        ConstantEffect();
        bulletsLeft = gun.getMagazineSize();
        readyToShoot = true;
    }

    private void Update()
    {
        gun.GunUpdate();
        ConstantEffect();
        PlayerInput();
        text.SetText(bulletsLeft + " / " + gun.getMagazineSize());
    }

    private void PlayerInput()
    {
        if (Input.GetKeyDown(KeyCode.R) && bulletsLeft < gun.getMagazineSize() && !reloading)
        {
            Reload();
        }
        //full-Auto or Semi-Auto
        if (gun.getAllowButtonHold())
        {
            shooting = Input.GetKey(KeyCode.Mouse0);
        }
        else
        {
            shooting = Input.GetKeyDown(KeyCode.Mouse0);
        }

        //Shoot
        if (readyToShoot && shooting && !reloading && bulletsLeft > 0 && !gun.getIsHandLoaded())
        {
            bulletsShot = gun.getBulletsPerTap();
            Shoot();
        }
        else if (readyToShoot && shooting && bulletsLeft > 0 && gun.getIsHandLoaded())
        {
            CancelInvoke("ReloadFinished");
            reloading = false;
            bulletsShot = gun.getBulletsPerTap();
            Shoot();
        }
    }
    private void Shoot()
    {
        readyToShoot = false;
        //Spread
        float x = Random.Range(-gun.getSpread(), gun.getSpread());
        float y = Random.Range(-gun.getSpread(), gun.getSpread());
        float z = Random.Range(-gun.getSpread(), gun.getSpread());
        Vector3 direction;
        //Calculate Direction with the spread
        if (!dualMode)
        {
            direction = attackPoint.forward + new Vector3(x, y, z); ;
        }
        else
        {
            if (IsShootingFromLeft)
            {
                direction = attackPointLeft.forward + new Vector3(x, y, z);
            }
            else
            {
                direction = attackPointRight.forward + new Vector3(x, y, z);
            }
        }
        if (gun.getIsRaycast())
        {
            //cheack if the Raycast Bullet can penetrate enemy
            if (!gun.getCanPenetrate())
            {
                RaycastFire(direction);
            }
            //if the Raycast Bullet can penetrate enemy
            else
            {
                PenetrationRaycastFire(direction);
            }
        }
        else
        {
            PrefabBulletFire(direction);
        }
        if (!gun.getIsShotgun())
        {
            bulletsLeft--;
            IsShootingFromLeft = !IsShootingFromLeft;
        }
        bulletsShot--;

        if (allowInvoke)
        {
            Invoke("ResetShot", gun.getFireRate());
            allowInvoke = false;
        }
        if (bulletsShot > 0 && bulletsLeft > 0)
        {
            Invoke("Shoot", gun.getTimeBetweenShoots());
        }
        if (bulletsShot <= 0 && gun.getIsShotgun())
        {
            bulletsLeft--;
            IsShootingFromLeft = !IsShootingFromLeft;
        }

    }
    private void ResetShot()
    {
        readyToShoot = true;
        allowInvoke = true;
    }

    private void Reload()
    {
        reloading = true;
        Invoke("ReloadFinished", gun.getReloadTime());
    }
    private void ReloadFinished()
    {
        if (!gun.getIsHandLoaded())
        {
            bulletsLeft = gun.getMagazineSize();
            reloading = false;
        }
        else
        {
            bulletsLeft = bulletsLeft + 1;
            reloading = false;
            if (bulletsLeft < gun.getMagazineSize())
            {
                Reload();
            }
        }
    }

    //Fire fuction for 4 type of bullet
    //Raycast Bullet
    private void RaycastFire(Vector3 direction) {
        Transform shootingPoint;

        if (!dualMode)
        {
            shootingPoint = attackPoint;
        }
        else
        {
            if (IsShootingFromLeft)
            {
                shootingPoint = attackPointLeft;
            }
            else
            {
                shootingPoint = attackPointRight;
            }
        }

        if (Physics.Raycast(shootingPoint.position, direction, out rayHit, gun.getRange(), whatIsEnemy))
        {
            Target target = rayHit.transform.GetComponent<Target>();
            if (rayHit.collider.CompareTag("Enemy"))
            {
                EffectOnHit(rayHit.transform.position);
                Rigidbody rb = rayHit.transform.gameObject.GetComponent<Rigidbody>();
                direction.y = 0;
                rb.AddForce(direction.normalized * gun.getKnockbackStrength(), ForceMode.Impulse);
                target.TakeDamage(gun.getDamege());
            }
            //GFX
            Instantiate(HitEffect, rayHit.point, Quaternion.Euler(0, 180, 0));
            Instantiate(muzzleFlash, shootingPoint.position, Quaternion.identity);
        }
        else
        {
            EffectOnHit(shootingPoint.forward * 100);
            //Graphics
            Instantiate(HitEffect, shootingPoint.forward * 100, Quaternion.Euler(0, 180, 0));
            Instantiate(muzzleFlash, shootingPoint.position, Quaternion.identity);
        }
    }

    //Penetration Raycast Bullet
    private void PenetrationRaycastFire(Vector3 direction)
    {
        RaycastHit hit;
        RaycastHit[] hits;
        Target target;
        Rigidbody rb;

        Transform shootingPoint;

        if (!dualMode)
        {
            shootingPoint = attackPoint;
        }
        else
        {
            if (IsShootingFromLeft)
            {
                shootingPoint = attackPointLeft;
            }
            else
            {
                shootingPoint = attackPointRight;
            }
        }

        hits = Physics.RaycastAll(shootingPoint.position, direction, gun.getRange(), whatIsEnemy);
        if (hits != null)
        {
            for (int i = 0; i < hits.Length; i++)
            {
                hit = hits[i];
                if (hit.collider.CompareTag("Enemy"))
                {
                    target = hit.transform.GetComponent<Target>();
                    rb = hit.transform.gameObject.GetComponent<Rigidbody>();
                    direction.y = 0;
                    rb.AddForce(direction.normalized * gun.getKnockbackStrength(), ForceMode.Impulse);
                    target.TakeDamage(gun.getDamege());
                }
                EffectOnHit(hit.point);
                //Graphics
                Instantiate(HitEffect, hit.point, Quaternion.Euler(0, 180, 0));
                Instantiate(muzzleFlash, shootingPoint.position, Quaternion.identity);
            }
        }
        else
        {
            EffectOnHit(shootingPoint.forward * 100);
            //Graphics
            Instantiate(HitEffect, shootingPoint.forward * 100, Quaternion.Euler(0, 180, 0));
            Instantiate(muzzleFlash, shootingPoint.position, Quaternion.identity);
        }
    }

    //Prefab Bullet
    private void PrefabBulletFire(Vector3 direction)
    {
        Transform shootingPoint;

        if (!dualMode)
        {
            shootingPoint = attackPoint;
        }
        else
        {
            if (IsShootingFromLeft)
            {
                //Debug.Log("Left");
                shootingPoint = attackPointLeft;
            }
            else
            {
                //Debug.Log("Right");
                shootingPoint = attackPointRight;
            }
        }

        //set damage and knockback strength before Instantiate bullet
        GameObject bullet = Instantiate(gun.getBulletPrefab(), shootingPoint.position, shootingPoint.rotation);
        bullet.GetComponent<BulletPrefab>().setDamage(gun.getDamege());
        bullet.GetComponent<BulletPrefab>().setKnockbackStrength(gun.getKnockbackStrength());
        bullet.GetComponent<BulletPrefab>().setKnockbackDirection(new Vector3(direction.x,0, direction.z));
        bullet.GetComponent<BulletPrefab>().setModFunction1(gun.mod.GetEffect1());
        bullet.GetComponent<BulletPrefab>().setModFunction2(gun.mod.GetEffect2());
        bullet.GetComponent<BulletPrefab>().setModFunction3(gun.mod.GetEffect3());
        bullet.GetComponent<BulletPrefab>().setBulletSpeed(gun.getBulletSpeed());
        bullet.GetComponent<BulletPrefab>().setFirePosition(gameObject.transform.position);
        bullet.GetComponent<Rigidbody>().velocity = direction * gun.getBulletSpeed();
        Rigidbody rb = bullet.GetComponent<Rigidbody>();

    }



    //Mod
    protected virtual void EffectOnHit(Vector3 hitPoint)
    {
        switch (gun.mod.GetEffect1())
        {
            case ModFunction.Effect.Explosive:
                if (explosion != null) Instantiate(explosion, hitPoint, Quaternion.identity);
                Collider[] enemies = Physics.OverlapSphere(hitPoint, 5, whatIsEnemy);
                for (int i = 0; i < enemies.Length; i++)
                {
                    if (enemies[i].GetComponent<Rigidbody>())
                    {
                        enemies[i].GetComponent<Rigidbody>().AddExplosionForce(70, hitPoint, 5);
                    }
                }
                break;
        }

        switch (gun.mod.GetEffect2())
        {
            case ModFunction.Effect.Explosive:
                if (explosion != null) Instantiate(explosion, hitPoint, Quaternion.identity);
                Collider[] enemies = Physics.OverlapSphere(hitPoint, 5, whatIsEnemy);
                for (int i = 0; i < enemies.Length; i++)
                {
                    if (enemies[i].GetComponent<Rigidbody>())
                    {
                        enemies[i].GetComponent<Rigidbody>().AddExplosionForce(70, hitPoint, 5);
                    }
                }
                break;
        }

        switch (gun.mod.GetEffect3())
        {
            case ModFunction.Effect.Explosive:
                if (explosion != null) Instantiate(explosion, hitPoint, Quaternion.identity);
                Collider[] enemies = Physics.OverlapSphere(hitPoint, 5, whatIsEnemy);
                for (int i = 0; i < enemies.Length; i++)
                {
                    if (enemies[i].GetComponent<Rigidbody>())
                    {
                        enemies[i].GetComponent<Rigidbody>().AddExplosionForce(70, hitPoint, 5);
                    }
                }
                break;
        }

        switch (gun.mod.GetEffect3())
        {
            case ModFunction.Effect.Explosive:
                if (explosion != null) Instantiate(explosion, hitPoint, Quaternion.identity);
                Collider[] enemies = Physics.OverlapSphere(hitPoint, 5, whatIsEnemy);
                for (int i = 0; i < enemies.Length; i++)
                {
                    if (enemies[i].GetComponent<Rigidbody>())
                    {
                        enemies[i].GetComponent<Rigidbody>().AddExplosionForce(70, hitPoint, 5);
                    }
                }
                break;
        }
    }
    protected virtual void ConstantEffect()
    {
        dualMode = gun.mod.GetDualMode();
    }
}

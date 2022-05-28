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

    //Reference
    public Gun gun;
    public Transform attackPoint;
    public RaycastHit rayHit;
    public LayerMask whatIsEnemy;

    //Graphics
    public GameObject muzzleFlash, HitEffect;
    public TextMeshProUGUI text;

    //For modAttachment
    public delegate void OnBulletHitEnemyModEffect(Vector3 pos);
    public static event OnBulletHitEnemyModEffect OnBulletHitEnemy;

    public delegate void OnBulletHitModEffect(Vector3 pos);
    public static event OnBulletHitModEffect OnBulletHit;

    public delegate void OnFiringModEffect(Vector3 pos);
    public static event OnFiringModEffect OnFiring;

    private void Awake()
    {
        gun.GunUpdate();
        bulletsLeft = gun.getMagazineSize();
        readyToShoot = true;
    }

    private void Update()
    {
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

        //Calculate Direction with the spread
        Vector3 direction = attackPoint.forward + new Vector3(x, y, z); ;

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
        if (Physics.Raycast(attackPoint.position, direction, out rayHit, gun.getRange(), whatIsEnemy))
        {
            /*if (OnBulletHitEnemy != null)
            {
                OnBulletHitEnemy(rayHit.point);
            }*/
            Target target = rayHit.transform.GetComponent<Target>();
            if (rayHit.collider.CompareTag("Enemy"))
            {
                gun.mod.ModEffect1(target.transform.position);
                Rigidbody rb = rayHit.transform.gameObject.GetComponent<Rigidbody>();
                direction.y = 0;
                rb.AddForce(direction.normalized * gun.getKnockbackStrength(), ForceMode.Impulse);
                target.TakeDamage(gun.getDamege());
            }
            //GFX
            Instantiate(HitEffect, rayHit.point, Quaternion.Euler(0, 180, 0));
            Instantiate(muzzleFlash, attackPoint.position, Quaternion.identity);
        }
        else
        {
            //Graphics
            Instantiate(HitEffect, attackPoint.forward * 100, Quaternion.Euler(0, 180, 0));
            Instantiate(muzzleFlash, attackPoint.position, Quaternion.identity);
        }
    }

    //Penetration Raycast Bullet
    private void PenetrationRaycastFire(Vector3 direction)
    {
        RaycastHit hit;
        RaycastHit[] hits;
        Target target;
        Rigidbody rb;
        hits = Physics.RaycastAll(attackPoint.position, direction, gun.getRange(), whatIsEnemy);
        if (hits != null)
        {
            for (int i = 0; i < hits.Length; i++)
            {
                hit = hits[i];
                if (hit.collider.CompareTag("Enemy"))
                {
                    gun.mod.ModEffect1(hit.transform.position);
                    target = hit.transform.GetComponent<Target>();

                    rb = hit.transform.gameObject.GetComponent<Rigidbody>();
                    direction.y = 0;
                    rb.AddForce(direction.normalized * gun.getKnockbackStrength(), ForceMode.Impulse);
                    target.TakeDamage(gun.getDamege());
                }
                //Graphics
                Instantiate(HitEffect, hit.point, Quaternion.Euler(0, 180, 0));
                Instantiate(muzzleFlash, attackPoint.position, Quaternion.identity);
            }
        }
        else
        {
            //Graphics
            Instantiate(HitEffect, attackPoint.forward * 100, Quaternion.Euler(0, 180, 0));
            Instantiate(muzzleFlash, attackPoint.position, Quaternion.identity);
        }
    }

    //Prefab Bullet
    private void PrefabBulletFire(Vector3 direction)
    {
        //set damage and knockback strength before Instantiate bullet
        GameObject bullet = Instantiate(gun.getBulletPrefab(), attackPoint.position, attackPoint.rotation);
        bullet.GetComponent<BulletPrefab>().setDamage(gun.getDamege());
        bullet.GetComponent<BulletPrefab>().setKnockbackStrength(gun.getKnockbackStrength());
        bullet.GetComponent<BulletPrefab>().setKnockbackDirection(direction);
        Rigidbody rb = bullet.GetComponent<Rigidbody>();

        bullet.GetComponent<BulletPrefab>().mod = gun.mod;

        rb.AddForce(direction * gun.getBulletSpeed(), ForceMode.Impulse);
    }

}

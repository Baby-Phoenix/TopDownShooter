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
        Debug.Log(gun.getTimeBetweenShoots());
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
                if (Physics.Raycast(attackPoint.position, direction, out rayHit, gun.getRange(), whatIsEnemy))
                {
                    Target target = rayHit.transform.GetComponent<Target>();

                    if (rayHit.collider.CompareTag("Enemy"))
                    {
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
            //if the Raycast Bullet can penetrate enemy
            else
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
        }
        else
        {
            GameObject bullet = Instantiate(gun.getBulletPrefab(), attackPoint.position, attackPoint.rotation);
            Rigidbody rb = bullet.GetComponent<Rigidbody>();
            bullet.GetComponent<PrefabBullet>().setShootDirection(direction);
            rb.AddForce(direction * gun.getBulletSpeed(), ForceMode.Impulse);
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
}

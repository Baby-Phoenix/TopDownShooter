using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Gun_old : MonoBehaviour
{
    //Gun stats
    public float damage;
    //stats control by Barrel, Scopes, and Stock
    public bool IsShotgun;
    public float timeBetweenShooting, spread, range, reloadTime, timeBetweenShots;
    public int bulletsPerTap;

    float damage_B;
    float timeBetweenShooting_B, spread_B, range_B, reloadTime_B, timeBetweenShots_B;

    //stats control by Chamber
    public bool allowButtonHold;
    public bool IsHandLoaded;

    //stats control by Magazine
    public bool IsRayCast;
    public bool CanPenetrate;

    public float bulletspeed;

    public int magazineSize;
    public int magazineSize_B;


    public GameObject bulletPrefab;

    int bulletsLeft, bulletsShot;

    float KnockbackStrength;

    //stats control by Mod
    //Explosive


    //bools 
    bool shooting, readyToShoot, reloading;

    //Reference
    public Transform attackPoint;
    public RaycastHit rayHit;
    public LayerMask whatIsEnemy;

    //Graphics
    public GameObject muzzleFlash, bulletHoleGraphic;
    public TextMeshProUGUI text;

    //Attachment
    public GameObject barrel;
    public GameObject chamber;
    public GameObject scopes;
    public GameObject magazine;
    public GameObject stock;
    public GameObject currentMod;
    public GameObject mod;


    public bool allowInvoke=true;

    private void Awake()
    {
        UpdateAttachment();
        bulletsLeft = magazineSize;
        readyToShoot = true;
    }

    private void UpdateAttachment()
    {
        GetBarrel();
        GetChamber();
        GetScope();
        GetMagazine();
        GetStock();

        if (mod != currentMod&& currentMod!=null)
        {
            Debug.Log(1);
            currentMod.GetComponent<Mod>().undoMod(gameObject);
            GetMod();
            currentMod = mod;
        }
        else if (mod != currentMod)
        {
            Debug.Log(2);
            GetMod();
            currentMod = mod;
        }
        else
        {
            GetMod();
            currentMod = mod;
        }
    }
    private void Update()
    {
        MyInput();
        UpdateAttachment();
        text.SetText(bulletsLeft + " / " + magazineSize);
    }
    private void MyInput()
    {

        //Reload Ammo
        if(Input.GetKeyDown(KeyCode.R) && bulletsLeft < magazineSize && !reloading)
        {
            Reload();
        }

        //full-Auto or Semi-Auto
        if (allowButtonHold)
        {
            shooting = Input.GetKey(KeyCode.Mouse0);
        }
        else
        {
            shooting = Input.GetKeyDown (KeyCode.Mouse0);
        }

        //Shoot
        if (readyToShoot && shooting && !reloading && bulletsLeft > 0 && !IsHandLoaded)
        {
            bulletsShot = bulletsPerTap;
            Shoot();
        }else if(readyToShoot && shooting  && bulletsLeft > 0 && IsHandLoaded)
        {
            CancelInvoke("ReloadFinished");
            reloading = false;
            bulletsShot = bulletsPerTap;
            Shoot();
        }
    }

    private void Shoot()
    {
        readyToShoot = false;
        //Spread
        float x = Random.Range(-spread, spread);
        float y = Random.Range(-spread, spread);
        float z = Random.Range(-spread, spread);


        //Calculate Direction with the spread
        Vector3 direction = attackPoint.forward + new Vector3 (x, y, z);;
        //RayCast bullet
        if (IsRayCast)
        {
            if (!CanPenetrate)
            {
                if (Physics.Raycast(attackPoint.position, direction, out rayHit, range, whatIsEnemy))
                {
                    Debug.Log(rayHit.collider.name);
                    Target target = rayHit.transform.GetComponent<Target>();

                    if (rayHit.collider.CompareTag("Enemy"))
                    {
                        Rigidbody rb = rayHit.transform.gameObject.GetComponent<Rigidbody>();
                        direction.y = 0;
                        Debug.Log(KnockbackStrength);
                        rb.AddForce(direction.normalized * KnockbackStrength, ForceMode.Impulse);
                        target.TakeDamage(damage);
                    }
                    //Graphics
                    Instantiate(bulletHoleGraphic, rayHit.point, Quaternion.Euler(0, 180, 0));
                    Instantiate(muzzleFlash, attackPoint.position, Quaternion.identity);
                }
                else
                {
                    //Graphics
                    Instantiate(bulletHoleGraphic, attackPoint.forward * 100, Quaternion.Euler(0, 180, 0));
                    Instantiate(muzzleFlash, attackPoint.position, Quaternion.identity);
                }
            }
            else
            {
                RaycastHit hit;
                RaycastHit[] hits;
                Target target;
                Rigidbody rb;
                hits = Physics.RaycastAll(attackPoint.position, direction, range, whatIsEnemy);

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
                            rb.AddForce(direction.normalized * KnockbackStrength, ForceMode.Impulse);
                            target.TakeDamage(damage);
                        }
                        //Graphics
                        Instantiate(bulletHoleGraphic, hit.point, Quaternion.Euler(0, 180, 0));
                        Instantiate(muzzleFlash, attackPoint.position, Quaternion.identity);
                    }
                }
                else
                {
                    //Graphics
                    Instantiate(bulletHoleGraphic, attackPoint.forward * 100, Quaternion.Euler(0, 180, 0));
                    Instantiate(muzzleFlash, attackPoint.position, Quaternion.identity);
                }
            }
        }
        else
        {
            GameObject bullet = Instantiate(bulletPrefab, attackPoint.position, attackPoint.rotation);
            Rigidbody rb = bullet.GetComponent<Rigidbody>();
            bullet.GetComponent<PrefabBullet>().setShootDirection(direction);
            rb.AddForce(direction * bulletspeed, ForceMode.Impulse);
        }
        if (!IsShotgun)
        {
            bulletsLeft--;
        }
        bulletsShot--;

        if (allowInvoke)
        {
            Invoke("ResetShot", timeBetweenShooting);
            allowInvoke = false;
        }

        if (bulletsShot > 0 && bulletsLeft > 0)
        {
            Invoke("Shoot", timeBetweenShots);
        }

        if (bulletsShot <= 0 && IsShotgun)
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
        Invoke("ReloadFinished", reloadTime);
    }

    private void ReloadFinished()
    {
        if (!IsHandLoaded)
        {
            bulletsLeft = magazineSize;
            reloading = false;
        }
        else
        {
            bulletsLeft = bulletsLeft+1;
            reloading = false;
            if(bulletsLeft< magazineSize)
            {
                Reload();
            }
        }
    }

    private void GetBarrel()
    {
        Barrel BarrelModifer = barrel.GetComponent<Barrel>();
        damage = BarrelModifer.damage_B;
        damage_B = BarrelModifer.damage_B;

        timeBetweenShooting = BarrelModifer.timeBetweenShooting_B;
        timeBetweenShooting_B = BarrelModifer.timeBetweenShooting_B;

        spread = BarrelModifer.spread_B;
        spread_B = BarrelModifer.spread_B;

        range = BarrelModifer.range_B;
        range_B = BarrelModifer.range_B;

        reloadTime = BarrelModifer.reloadTime_B;
        reloadTime_B = BarrelModifer.reloadTime_B;

        timeBetweenShooting = BarrelModifer.timeBetweenShooting_B;
        timeBetweenShooting_B = BarrelModifer.timeBetweenShooting_B;

        bulletsPerTap = BarrelModifer.bulletsPerTap_B;

        IsShotgun = BarrelModifer.IsShotgun;
    }
    private void GetChamber()
    {
        Chamber ChamberModifer = chamber.GetComponent<Chamber>();
        damage = damage + ChamberModifer.damageModifier(damage_B);
        timeBetweenShooting = timeBetweenShooting - ChamberModifer.FiringSpeedModifier(timeBetweenShooting_B) ;
        allowButtonHold = ChamberModifer.allowButtonHold;
        IsHandLoaded = ChamberModifer.IsHandLoaded;
    }
    private void GetScope()
    {
        Scope ScopeModifer = scopes.GetComponent<Scope>();
        spread = spread - ScopeModifer.spreadModifier(spread_B);
        range = range + ScopeModifer.rangeModifier(range_B);
    }
    private void GetMagazine()
    {
        Magazine MagazineModifer = magazine.GetComponent<Magazine>();
        IsRayCast = MagazineModifer.IsRayCast;

        if (!IsRayCast)
        {
            bulletPrefab = magazine.GetComponent<PrefabMagazine>().bulletPrefab;
            bulletspeed = magazine.GetComponent<PrefabMagazine>().bulletspeed;
        }
        else
        {
            KnockbackStrength = magazine.GetComponent<Magazine>().KnockbackStrength;
            CanPenetrate = magazine.GetComponent<Magazine>().CanPenetrate;
        }

        magazineSize = MagazineModifer.getMagazineSize();
        magazineSize_B = MagazineModifer.getMagazineSize();
    }
    private void GetStock()
    {
        Stock StockModifer = stock.GetComponent<Stock>();
        spread = spread - StockModifer.spreadModifier(spread_B);
    }
    private void GetMod()
    {
        if (mod != null)
        {
            Mod modModifer = mod.GetComponent<Mod>();
            mod.GetComponent<Mod>().initMod(gameObject);
        }
    }
}

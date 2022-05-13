using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public float damage = 10f;
    public float range = 100f;
    public float fireRate = 15f;
    public Transform firePoint;

    public LineRenderer lineRenderer;

    public ParticleSystem muzzleFlash;
    public GameObject impactEffect;
    public float impactForce = 45;

    private float nextTimeToFire = 0f;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.Mouse0)&&Time.time>=nextTimeToFire)
        {
            nextTimeToFire = Time.time + 1f / fireRate;
            StartCoroutine (Shoot());
        }
    }
    IEnumerator Shoot()
    {
        muzzleFlash.Play();
        RaycastHit hit;
        if(Physics.Raycast(firePoint.position, firePoint.forward, out hit, range)){
            Debug.Log(hit.transform.name);

            Target target = hit.transform.GetComponent<Target>();
            if(target != null)
            {
                target.TakeDamage(damage);
            }

            if(hit.rigidbody != null)
            {
                hit.rigidbody.AddForce(-hit.normal * impactForce);
            }

            Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));

            lineRenderer.SetPosition(0, firePoint.position);
            lineRenderer.SetPosition(1, hit.point);
        }
        else
        {
            lineRenderer.SetPosition(0, firePoint.position);
            lineRenderer.SetPosition(1,firePoint.position+firePoint.forward*100);
        }
        lineRenderer.enabled = true;
        //wait 1 frame

        yield return new WaitForSeconds(0.02f);

        lineRenderer.enabled = false;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Orbiter : MonoBehaviour
{
	public Transform centerPoint;
	/*
	//For Rotate
	public float xSpread;
	public float zSpread;
	public float yOffset;

	public float rotSpeed;
	public bool rotateClockwise;

	float timer = 0;
	*/
	//For Shooting
	protected Collider target;
	//bools
	bool readyToShoot=true;
	bool allowInvoke = true;
	//Reference
	public Gun gun;
	public GameObject bulletPrefab;
	public Transform attackPoint;
	public RaycastHit rayHit;
	public LayerMask whatIsEnemy;
	//Graphics
	public GameObject muzzleFlash;

	// Update is called once per frame
	void Update()
	{
		LockOn();
		//timer += Time.deltaTime * rotSpeed;
		//Rotate();
	}

	/*void Rotate()
	{
		if (rotateClockwise)
		{
			float x = -Mathf.Cos(timer) * xSpread;
			float z = Mathf.Sin(timer) * zSpread;
			Vector3 pos = new Vector3(x, yOffset, z);
			transform.position = pos + centerPoint.position;
		}
		else
		{
			float x = Mathf.Cos(timer) * xSpread;
			float z = Mathf.Sin(timer) * zSpread;
			Vector3 pos = new Vector3(x, yOffset, z);
			transform.position = pos + centerPoint.position;
		}
	}
	*/
	private void PrefabBulletFire(Vector3 direction)
	{
		centerPoint = gameObject.transform.parent.transform;
		//set damage and knockback strength before Instantiate bullet
		GameObject bullet = Instantiate(bulletPrefab, attackPoint.position, attackPoint.rotation);
		bullet.GetComponent<BulletPrefab>().setDamage(10* gun.mod.DamageModifier());
		bullet.GetComponent<BulletPrefab>().setKnockbackStrength(2*gun.mod.KnockbackStrengthModifier());
		bullet.GetComponent<BulletPrefab>().setKnockbackDirection(new Vector3(centerPoint.forward.x, 0, centerPoint.forward.z));
		bullet.GetComponent<BulletPrefab>().setBulletSpeed(5);
		bullet.GetComponent<BulletPrefab>().setFirePosition(gameObject.transform.position);
		bullet.GetComponent<OrbiterBullet>().setOrbiter(gameObject);
		bullet.GetComponent<Rigidbody>().velocity = direction * 2;
		Rigidbody rb = bullet.GetComponent<Rigidbody>();
	}
	protected virtual void LockOn()
	{
		float minDist = Mathf.Infinity;
		Collider[] enemies = Physics.OverlapSphere(gameObject.transform.position, 8, whatIsEnemy);
		for (int i = 0; i < enemies.Length; i++)
		{
			if (enemies[i].tag == "Enemy")
			{
				float dist = Vector3.Distance(enemies[i].transform.position, gameObject.transform.position);
				if (dist < minDist)
				{
					target = enemies[i];
					minDist = dist;
				}
			}
		};
		Shoot(enemies);
	}
	protected virtual void Shoot(Collider[] enemies)
	{
		Debug.Log(enemies.Length);
		if (readyToShoot==true&& enemies.Length != 0)
		{
			PrefabBulletFire(transform.forward);
			readyToShoot = false;
			allowInvoke = true;
		}
        else if(readyToShoot == false && allowInvoke == true)
        {
			Invoke("Reload", 2 * gun.mod.ReloadTimeModifier());
			allowInvoke = false;
		}
	}
	protected virtual void Reload()
	{
		readyToShoot = true;
	}
	public virtual Collider getTarget()
	{
		return target;
	}
}
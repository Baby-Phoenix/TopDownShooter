using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabBullet : MonoBehaviour
{
    [HideInInspector] public float bulletspeed;
    public Rigidbody rb;
    public Vector3 dir;
    public void setBulletSpeed(float BulletSpeed)
    {
        bulletspeed = BulletSpeed;
    }
    public void setShootDirection(Vector3 Shootdir)
    {
        dir = Shootdir;
    }
    public Vector3 getShootDirection()
    {
        return dir;
    }
}

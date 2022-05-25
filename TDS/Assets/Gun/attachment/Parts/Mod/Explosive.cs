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

    }
    public override void undoMod(GameObject gun)
    {

    }
}

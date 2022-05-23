using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class LaserMagazine : Magazine
{
    public LaserMagazine()
    {
        KnockbackStrength = 0.2f;
        IsRayCast = true;
        CanPenetrate = true;
        magazineSize_B = 40;
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMagazine : Magazine
{
    public BulletMagazine(){
        KnockbackStrength = 10;
        IsRayCast = true;
        CanPenetrate = false;
        magazineSize_B = 30;
    }
}

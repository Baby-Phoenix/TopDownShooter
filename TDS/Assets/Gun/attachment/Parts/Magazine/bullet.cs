using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : Magazine
{
    public bullet(){
        KnockbackStrength = 10;
        IsRayCast = true;
        magazineSize_B = 30;
    }
}

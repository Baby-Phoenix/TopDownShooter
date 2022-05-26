using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SMG : Barrel
{   
    public SMG()
    {
        damage_B = 3;
        timeBetweenShooting_B = 0.05f;
        spread_B = 0.15f;
        range_B = 100;
        reloadTime_B= 1;
        timeBetweenShots_B = 0.1f;
        bulletsPerTap_B = 1;
        IsShotgun = false;
    }
}

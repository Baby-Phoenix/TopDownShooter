using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : Barrel
{
    public Shotgun()
    {
        damage_B = 0.5f;
        timeBetweenShooting_B = 1;
        spread_B = 0.2f;
        range_B = 100;
        reloadTime_B = 1;
        timeBetweenShots_B = 0;
        bulletsPerTap_B = 8;
        IsShotgun = true;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoltMagazine : PrefabMagazine
{
    public BoltMagazine()
    {
        IsRayCast = false;
        magazineSize_B = 15;
    }
}
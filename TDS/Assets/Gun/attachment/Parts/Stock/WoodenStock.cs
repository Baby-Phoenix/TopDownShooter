using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodenStock : Stock
{
    public WoodenStock()
    {
        spread_M = 0.2f;
    }
    public override float spreadModifier(float baseSpead)
    {
        return baseSpead * spread_M;
    }
}

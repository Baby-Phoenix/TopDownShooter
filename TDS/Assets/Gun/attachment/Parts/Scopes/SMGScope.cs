using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SMGScope : Scope
{
    public SMGScope()
    {
        spread_M = 0.2f;
        range_M = 0.2f;
    }


    public override float spreadModifier(float baseSpead)
    {
        return baseSpead * spread_M;
    }
    public override float rangeModifier(float baseRange)
    {
        return baseRange * range_M;
    }
}

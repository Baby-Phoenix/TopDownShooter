using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Stock", menuName = "Gun/Attachment/Stock")]
public class StockAttachment : ScriptableObject
{
    public float spreadModifier;// This should be a percentage betwen 0-1

    public virtual float SpreadModifier(float baseSpead) {
        return 1 - spreadModifier ;
    }
}

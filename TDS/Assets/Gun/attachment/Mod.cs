using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mod : MonoBehaviour
{
    public int TypeOfMod; //0 = apply to all, 1 = only for prefab, 2 = only for raycast
    public int level;
    public int EXP;
    public virtual void initMod(GameObject gun) {}
    public virtual void undoMod(GameObject gun) { }
}

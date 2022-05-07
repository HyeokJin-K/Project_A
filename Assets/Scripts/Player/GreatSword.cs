using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreatSword : MeleeWeapon
{    
    public float AttackRange
    {
        get
        {
            return attackRange;
        }
        set
        {
            attackRange = value;
            gameObject.transform.localScale = new Vector2(attackRange, attackRange);
        }
    }       
}

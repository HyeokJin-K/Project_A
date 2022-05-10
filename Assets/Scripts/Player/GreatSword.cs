using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreatSword : MeleeWeapon, IWeapon
{
    [SerializeField, ReadOnly]
    List<GameObject> currentSkillList;

    bool isWeaponInput = false;

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

    public override void WeaponNormalAttack()
    {
        if (isNormalAttackReady && isWeaponInput)
        {            
            for(int i = colEnemyList.Count - 1; i >= 0; i--)
            {                
                colEnemyList[i].GetComponent<IDamageable>().TakeDamage(attackPower);                
            }         

            isNormalAttackReady = false;
            StartCoroutine(WaitNormalAttackDelay());
        }
    }

    public void SetWeaponInput()
    {
        isWeaponInput = !isWeaponInput;
    }

    public void ActivateSkill()
    {
        throw new System.NotImplementedException();
    }

    public IEnumerator WaitSkillDelay()
    {
        throw new System.NotImplementedException();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreatSword : MeleeWeapon, IWeapon
{
    #region Public Field

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

    public List<GameObject> SkillList { get => currentSkillList; set => currentSkillList = value; }

    public float WeaponExp
    {
        get => weaponExp;
        set
        {
            weaponExp = value;
        }
    }

    public int WeaponLevel { get => weaponLevel; set => weaponLevel = value; }

    #endregion

    #region Private Field

    [SerializeField, ReadOnly]
    List<GameObject> currentSkillList;

    int weaponLevel;

    float weaponExp;

    bool isWeaponInput = false;

    #endregion
    
    //------------------------------------------------------------------------------------------------

    protected override void WeaponNormalAttack()
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

    public void LevelUpWeapon()
    {
        weaponLevel++;
    }

    public void AddSkillList(GameObject skillObject)
    {
        SkillList.Add(skillObject);
    }

}

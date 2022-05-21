using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthQuakeSkill : MonoBehaviour, ISkill
{

    #region Public Field

    public bool IsSkillReady => isSkillReady;

    public bool IsSkillFinish => isSkillFinish;

    #endregion

    #region Private Field

    [SerializeField, ReadOnly]
    PlayerSkillData skillData;

    [SerializeField]
    BoxCollider2D[] skillColliders;

    [SerializeField]
    List<GameObject> attackedEnemyList = new List<GameObject>();

    bool isSkillReady = true;

    bool isSkillFinish = true;

    #endregion

    //------------------------------------------------------------------------------------------------

    #region Unity LifeCycle

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!attackedEnemyList.Contains(collision.gameObject))
        {
            if(collision.CompareTag("Monster") || collision.CompareTag("BossMonster"))
            {
                collision.GetComponent<IDamageable>().TakeDamage(skillData.SkillPower);

                attackedEnemyList.Add(collision.gameObject);                
            }
        }
    }    

    #endregion

    public void ActivateSkill()
    {
        if (isSkillReady && isSkillFinish)
        {
            isSkillReady = false;

            isSkillFinish = false;

            StartCoroutine(EarthQuake());

            StartCoroutine(WaitSkillDelay());
        }
        else
        {
            print($"{this.GetType()} 쿨타임 입니다");
        }
    }

    IEnumerator EarthQuake()
    {
        transform.localPosition = Vector3.zero;

        transform.up = transform.root.GetComponentInChildren<Player>().AttackDir;

        foreach(var col in skillColliders)
        {            
            col.gameObject.SetActive(true);

            col.GetComponent<SpriteRenderer>().DoDisable(1f, SpriteDisableMode.Lerp);

            yield return new WaitForSeconds(0.05f);
        }

        isSkillFinish = true;
    }    

    public PlayerSkillData GetPlayerSkillData()
    {
        return skillData;
    }

    public void ReinforceSkill()
    {
        throw new NotImplementedException();
    }

    public IEnumerator WaitSkillDelay()
    {
        yield return new WaitForSeconds(skillData.SkillDelay);

        isSkillReady = true;

        attackedEnemyList.Clear();
    }
}

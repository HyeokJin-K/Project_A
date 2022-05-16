using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthQuakeSkill : MonoBehaviour, ISkill
{

    #region Public Field

    public bool IsSkillReady => isSkillReady;

    #endregion

    #region Private Field

    [SerializeField, ReadOnly]
    PlayerSkillData playerSkillData;

    [SerializeField]
    BoxCollider2D[] skillColliders;

    [SerializeField]
    List<GameObject> attackedEnemyList = new List<GameObject>();

    bool isSkillReady = true;

    #endregion

    //------------------------------------------------------------------------------------------------

    #region Unity LifeCycle

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!attackedEnemyList.Contains(collision.gameObject))
        {
            if(collision.CompareTag("Monster") || collision.CompareTag("BossMonster"))
            {
                collision.GetComponent<IDamageable>().TakeDamage(playerSkillData.SkillPower);

                attackedEnemyList.Add(collision.gameObject);                
            }
        }
    }    

    #endregion

    public void ActivateSkill()
    {
        if (isSkillReady)
        {
            StartCoroutine(EarthQuake());

            isSkillReady = false;

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

            StartCoroutine(col.GetComponent<SpriteRenderer>().DoDisable(5f, DisableMode.Lerp));

            yield return new WaitForSeconds(0.05f);
        }
    }    

    public PlayerSkillData GetPlayerSkillData()
    {
        return playerSkillData;
    }

    public void ReinforceSkill()
    {
        throw new NotImplementedException();
    }

    public IEnumerator WaitSkillDelay()
    {
        float t = playerSkillData.SkillDelay;

        while (t >= 0f)
        {
            t -= Time.deltaTime;

            yield return null;
        }

        isSkillReady = true;

        attackedEnemyList.Clear();
    }
}

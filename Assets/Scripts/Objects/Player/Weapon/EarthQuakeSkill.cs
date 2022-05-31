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

    HashSet<GameObject> attackedEnemyList = new HashSet<GameObject>();

    [SerializeField, ReadOnly]
    PlayerSkillData skillData;

    [SerializeField]
    CircleCollider2D[] skillColliders;    

    [SerializeField, ReadOnly]
    bool isSkillReady = true;

    [SerializeField, ReadOnly]
    bool isSkillFinish = true;

    #endregion

    //------------------------------------------------------------------------------------------------

    #region Unity LifeCycle

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (attackedEnemyList.Contains(collision.gameObject))
        {
            return;
        }

        if (collision.CompareTag("Monster") || collision.CompareTag("BossMonster"))
        {
            collision.GetComponent<IDamageable>().TakeDamage(skillData.SkillPower);

            attackedEnemyList.Add(collision.gameObject);
        }
    }

    #endregion

    public void ActivateSkill()
    {
        if (isSkillReady && isSkillFinish)
        {
            attackedEnemyList.Clear();

            isSkillReady = false;

            isSkillFinish = false;

            StartCoroutine(EarthQuake());

            StartCoroutine(WaitSkillDelay());
        }

        #region Local Method

        IEnumerator EarthQuake()
        {
            transform.localPosition = Vector3.zero;

            transform.up = transform.root.GetComponentInChildren<Player>().AttackDir;

            int count = -1;

            foreach (var col in skillColliders)
            {
                col.gameObject.transform.localPosition = new Vector3(0, (count + 1) * 1.5f, 0);

                col.gameObject.SetActive(true);

                col.GetComponent<SpriteRenderer>().DoDisable(SpriteDisableMode.Lerp);

                count++;

                yield return new WaitForSeconds(0.03f);
            }

            foreach (var col in skillColliders)
            {
                yield return new WaitUntil(() => col.GetComponent<SpriteRenderer>().color.a <= 0f);

                col.gameObject.SetActive(false);
            }

            isSkillFinish = true;
        }

        #endregion
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
    }
}

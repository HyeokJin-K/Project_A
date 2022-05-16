using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhirlWindSkill : MonoBehaviour, ISkill
{
    #region Public Field    
    
    public bool IsSkillReady => isSkillReady;

    #endregion

    #region Private Field

    [SerializeField, ReadOnly]
    PlayerSkillData playerSkillData = new PlayerSkillData();

    [SerializeField]
    BoxCollider2D skillCollider;

    [SerializeField]
    TrailRenderer skillTrailRenderer;

    List<GameObject> attackedEnemyList = new List<GameObject>();

    bool isSkillReady = true;

    #endregion

    //------------------------------------------------------------------------------------------------

    #region Unity LifeCycle

    private void Awake()
    {
        #region Caching
        skillCollider = skillCollider == null ? GetComponent<BoxCollider2D>() : skillCollider;
        skillTrailRenderer = skillTrailRenderer == null ? GetComponentInChildren<TrailRenderer>() : skillTrailRenderer;
        #endregion                        
    }    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!attackedEnemyList.Contains(collision.gameObject))
        {
            if (collision.CompareTag("Monster") || collision.CompareTag("BossMonster"))
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
            StartCoroutine(TurnCollider());

            isSkillReady = false;

            StartCoroutine(WaitSkillDelay());
        }
        else
        {
            print("쿨타임입니다");
        }
    }

    IEnumerator TurnCollider()
    {
        skillCollider.enabled = true;

        skillTrailRenderer.enabled = true;

        skillTrailRenderer.emitting = true;
        
        for(int i = 0; i < playerSkillData.RepeatCount; i++)
        {
            float rotateValue = 0;

            while (rotateValue <= 360f)
            {
                rotateValue += Time.deltaTime * 1250f;

                transform.rotation = Quaternion.Euler(0f, 0f, rotateValue);

                yield return null;
            }

            attackedEnemyList.Clear();
        }

        skillTrailRenderer.emitting = false;

        yield return new WaitForSeconds(0.2f);

        skillTrailRenderer.enabled = false;

        skillCollider.enabled = false;

        transform.rotation = Quaternion.Euler(0f, 0f, 0f);
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
    }

    public void ReinforceSkill()
    {
        throw new NotImplementedException();
    }

    public PlayerSkillData GetPlayerSkillData()
    {
        return playerSkillData;
    }
}

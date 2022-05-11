using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreatSwordSkill_1 : MonoBehaviour, ISkill
{
    #region Public Field
    public PlayerSkillData playerSkillData;
    public bool IsSkillReady => isSkillReady;
    #endregion

    #region Private Field
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
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            ActivateSkill();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Monster") || collision.CompareTag("BossMonster"))
        {
            if (!attackedEnemyList.Contains(collision.gameObject))
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
            print("��Ÿ���Դϴ�");
        }
    }

    IEnumerator TurnCollider()
    {
        skillCollider.enabled = true;
        skillTrailRenderer.enabled = true;       
        
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

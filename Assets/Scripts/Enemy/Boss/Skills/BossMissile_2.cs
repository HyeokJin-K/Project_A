using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMissile_2 : ObjectPool, IBossSkill
{
    #region Public Field
    public GameObject missilePrefab;
    public Transform bossMissile2PoolTransform;
    public bool IsSkillReady { get => isSkillReady; }
    #endregion

    #region Private Field
    [SerializeField]
    float missilePower;
    [SerializeField]
    float missileSpeed;
    [SerializeField]
    float skillDelay;
    [SerializeField, ReadOnly]
    bool isSkillReady;
    #endregion

    //------------------------------------------------------------------------------------------------

    #region Unity LifeCycle
    private void Awake()
    {
        isSkillReady = true;

        AddObjectPoolSetParent(missilePrefab, bossMissile2PoolTransform);
        GameObject player = GameObject.FindWithTag("Player");

        float angle = 0f;

        foreach(var missile in objectPool)
        {
            missile.GetComponent<IProjectile>().SetCollisionTarget(player);
            Vector3 dir = new Vector3(Mathf.Sin(Mathf.Deg2Rad * angle), Mathf.Cos(Mathf.Deg2Rad * angle), 1f);
            missile.GetComponent<IProjectile>().SetMoveTarget(dir + transform.position, IProjectile.ProjectileSpeedMode.Normal);
            missile.GetComponent<IProjectile>().SetProjectileValue(missileSpeed, missilePower, 5.0f);
            angle += 30f;
        }        
    }
    #endregion

    public void ActivateSkill()
    {
        if (isSkillReady)
        {            
            ShootMissile();
            isSkillReady = false;
            StartCoroutine(WaitSkillDelay());
        }
        else
        {
            Debug.Log($"{this.GetType()} 쿨타입 입니다");            
        }
    }

    void ShootMissile()
    {
        for (int i = 0; i < 12; i++)
        {            
            objectPool[i].transform.position = transform.position;
            objectPool[i].SetActive(true);
        }
    }

    public IEnumerator WaitSkillDelay()
    {
        float t = skillDelay;

        while (t >= 0f)
        {
            t -= Time.deltaTime;
            yield return null;
        }

        Debug.Log($"{this.GetType()} 준비 완료");
        isSkillReady = true;
    }
}

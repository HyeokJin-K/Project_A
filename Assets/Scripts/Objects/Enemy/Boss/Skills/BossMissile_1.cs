using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMissile_1 : ObjectPool, IBossSkill
{
    #region Public Field

    public GameObject missilePrefab;   
    
    public Transform bossMissile1PoolTransform;

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

    List<IProjectile> missileIProjectileList = new List<IProjectile>();

    #endregion

    //------------------------------------------------------------------------------------------------

    #region Unity LifeCycle

    private void Awake()
    {
        isSkillReady = true;

        AddObjectPoolSetParent(missilePrefab, bossMissile1PoolTransform);

        GameObject player = GameObject.FindWithTag("Player");

        int count = 0;

        foreach(var missile in objectPool)
        {
            missileIProjectileList.Add(missile.GetComponent<IProjectile>());

            missileIProjectileList[count].SetCollisionTarget(player);

            count++;
        }
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.K))
        {
            ActivateSkill();
        }
    }

    #endregion

    public void ActivateSkill()
    {
        if (isSkillReady)
        {
            StartCoroutine(ShootMissile());

            isSkillReady = false;

            StartCoroutine(WaitSkillDelay());
        }
        else
        {
            #if UNITY_EDITOR

            Debug.Log($"{this.GetType()} 쿨타입 입니다");

            #endif
        }
    }    

    public IEnumerator WaitSkillDelay()
    {
        yield return new WaitForSeconds(skillDelay);

        Debug.Log($"{this.GetType()} 준비 완료");        

        isSkillReady = true;
    }

    IEnumerator ShootMissile()      //  보스 미사일 발사
    {
        foreach (var ob in objectPool)
        {
            ob.transform.position = transform.position;

            ob.SetActive(true);

            StartCoroutine(BezierMove(ob.GetComponent<IProjectile>()));

            yield return new WaitForSeconds(0.2f);
        }
    }

    IEnumerator BezierMove(IProjectile projectile)      //  보스 탄막의 베지어 곡선 메서드
    {
        Transform player = GameObject.FindWithTag("Player").transform;

        IProjectile pro = projectile;

        pro.SetProjectileValue(missileSpeed, missilePower, 3.5f);

        Vector3 bezierPoint;        

        Vector2 handle1 = (Random.onUnitSphere * 50.0f) + transform.position;
        
        Vector3 p1, p2;

        float t = 0f;        

        while (t <= 1f)
        {
            p1 = Vector3.Lerp(transform.position, handle1, t);

            p2 = Vector3.Lerp(handle1, player.transform.position, t);

            bezierPoint = Vector3.Lerp(p1, p2, t);            

            pro.SetMoveTarget(bezierPoint, IProjectile.ProjectileSpeedMode.Acceleration);

            t += Time.deltaTime;

            yield return null;
        }
    }
}

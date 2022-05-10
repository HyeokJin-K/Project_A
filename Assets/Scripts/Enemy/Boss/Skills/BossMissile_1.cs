using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMissile_1 : ObjectPool, ISkill
{
    public GameObject missilePrefab;    
    public Transform bossMissile1PoolTransform;

    [SerializeField]
    float missilePower;
    [SerializeField]
    float missileSpeed;
    [SerializeField]
    float skillDelay;
    [SerializeField, ReadOnly]
    bool isSkillReady;

    public float SkillPower { get => missilePower; set => missilePower = value; }
    public float SkillDelay { get => skillDelay; set => skillDelay = value; }
    public bool IsSkillReady { get => isSkillReady; }  

    private void Awake()
    {
        isSkillReady = true;

        AddObjectPoolSetParent(missilePrefab, bossMissile1PoolTransform);
        GameObject player = GameObject.FindWithTag("Player");
        foreach(var missile in objectPool)
        {            
            missile.GetComponent<IProjectile>().SetCollisionTarget(player);            
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            ActivateSkill();
        }
    }

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
        float t = skillDelay;

        while (t >= 0f)
        {
            t -= Time.deltaTime;
            yield return null;
        }
        #if UNITY_EDITOR
        Debug.Log($"{this.GetType()} 준비 완료");
        #endif
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

        float t = 0f;

        Vector2 handle1 = (Random.onUnitSphere * 30.0f) + transform.position;        
        Vector3 p1;
        Vector3 p2;   
        
        while (t <= 1f)
        {
            p1 = Vector3.Lerp(transform.position, handle1, t);
            p2 = Vector3.Lerp(handle1, player.transform.position, t);
            bezierPoint = Vector3.Lerp(p1, p2, t);            

            pro.SetMoveTarget(bezierPoint, IProjectile.ProjectileSpeedMode.Acceleration);
            t += Time.deltaTime * 1.5f;
            yield return null;
        }
    }

}

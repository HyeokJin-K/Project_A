using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMissile_1 : ObjectPool, ISkill
{
    [SerializeField]
    float missilePower = 3.0f;
    [SerializeField]
    float missileSpeed = 3.0f;

    public GameObject missilePrefab;

    [SerializeField]
    Transform bossMissilePoolTransform;

    private void Awake()
    {
        AddObjectPoolSetParent(missilePrefab, bossMissilePoolTransform);
        foreach(var missile in objectPool)
        {            
            missile.GetComponent<IProjectile>().SetCollisionTarget(GameObject.FindWithTag("Player"));            
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
        StartCoroutine(ShootMissile());
    }    

    IEnumerator ShootMissile()      //  보스 미사일 발사
    {
        foreach (var ob in objectPool)
        {
            if (!ob.activeInHierarchy)
            {
                ob.transform.position = transform.position;
                ob.SetActive(true);
                
                StartCoroutine(BezierMove(ob.GetComponent<IProjectile>()));
                yield return new WaitForSeconds(0.2f);
            }
        }
    }

    IEnumerator BezierMove(IProjectile projectile)      //  보스 탄막의 베지어 곡선 메서드
    {
        Transform player = GameObject.FindWithTag("Player").transform;
        IProjectile pro = projectile;

        pro.SetProjectileValue(1f, missilePower, 3.5f);

        Vector3 bezierPoint;

        float t = 0f;

        Vector3 handle1 = Random.onUnitSphere * 30.0f;        
        Vector3 p1;
        Vector3 p2;   
        
        while (t <= 1f)
        {
            p1 = Vector3.Lerp(transform.position, handle1, t);
            p2 = Vector3.Lerp(handle1, player.transform.position, t);
            bezierPoint = Vector3.Lerp(p1, p2, t);            

            pro.SetMoveTarget(bezierPoint, IProjectile.ProjectileMode.Acceleration);
            t += Time.deltaTime * 1.5f;
            yield return null;
        }
    }
}

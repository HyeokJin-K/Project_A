using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BossAttackPattern))]
public class BossMissileSkill_1 : ObjectPool, IBossSkill
{
    #region Public Field

    public GameObject missilePrefab;

    public Transform bossMissile1PoolTransform;

    #endregion

    #region Private Field

    [SerializeField]
    float missilePower;

    [SerializeField]
    float missileSpeed;

    List<IProjectile> missileIProjectileList = new List<IProjectile>();

    #endregion

    //------------------------------------------------------------------------------------------------

    #region Unity LifeCycle

    private void Awake()
    {
        AddObjectPoolSetParent(missilePrefab, bossMissile1PoolTransform);

        GameObject player = GameObject.FindWithTag("Player");

        int count = 0;

        foreach(var missile in objectList)
        {
            missileIProjectileList.Add(missile.GetComponent<IProjectile>());

            missileIProjectileList[count].SetCollisionTarget(player);

            count++;
        }
    }

    #endregion

    public void ActivateSkill()
    {
        StartCoroutine(ShootMissile());

        #region Local Method

        IEnumerator ShootMissile()      //  보스 미사일 발사
        {
            foreach (var ob in objectList)
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

            Vector3 bezierPoint, p1, p2;

            Vector2 handle1 = (Random.onUnitSphere * 50.0f) + transform.position;

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

        #endregion
    }    
}

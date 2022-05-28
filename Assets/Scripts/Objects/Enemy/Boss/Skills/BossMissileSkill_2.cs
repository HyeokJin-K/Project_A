using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BossAttackPattern))]
public class BossMissileSkill_2 : ObjectPool, IBossSkill
{
    #region Public Field

    public GameObject missilePrefab;

    public Transform bossMissile2PoolTransform;

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
        AddObjectPoolSetParent(missilePrefab, bossMissile2PoolTransform);

        GameObject player = GameObject.FindWithTag("Player");

        float angle = 0f;

        int count = 0;

        foreach(var missile in objectList)
        {
            Vector3 dir = new Vector3(Mathf.Sin(Mathf.Deg2Rad * angle), Mathf.Cos(Mathf.Deg2Rad * angle), 1f);

            missileIProjectileList.Add(missile.GetComponent<IProjectile>());

            missileIProjectileList[count].SetCollisionTarget(player);

            missileIProjectileList[count].SetMoveTarget(dir + transform.position, IProjectile.ProjectileSpeedMode.Normal);

            missileIProjectileList[count].SetProjectileValue(missileSpeed, missilePower, 5.0f);

            angle += 360f / initPoolAmount;

            count++;
        }        
    }

    #endregion

    public void ActivateSkill()
    {
        ShootMissile();

        #region Local Method

        void ShootMissile()
        {
            for (int i = 0; i < initPoolAmount; i++)
            {
                objectList[i].transform.position = transform.position;

                objectList[i].SetActive(true);
            }
        }

        #endregion
    }
}

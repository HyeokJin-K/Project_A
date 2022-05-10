using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMissile_2 : ObjectPool, ISkill
{
    public GameObject missilePrefab;
    public Transform bossMissile2PoolTransform;

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

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            ActivateSkill();
        }
    }

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

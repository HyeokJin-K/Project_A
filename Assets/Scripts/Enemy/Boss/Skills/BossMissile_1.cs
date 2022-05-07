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

    private void Awake()
    {        
        AddObjectPool(missilePrefab);
        foreach(var missile in objectPool)
        {            
            missile.GetComponent<IProjectile>().SetCollisionTarget(GameObject.FindWithTag("Player"));
            
        }
    }

    public void ActivateSkill()
    {
        foreach(var ob in objectPool)
        {            
        }
    }
}

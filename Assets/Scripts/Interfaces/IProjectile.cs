using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IProjectile 
{
    public void SetCollisionTarget(GameObject targetObject);

    public void SetMoveToTarget(Transform targetTransform);
    public void SetProjectileValue(float speed, float power);

    public IEnumerator SetDurationMoveToTarget(Transform targetTransform, float time);
}

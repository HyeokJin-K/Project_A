using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IProjectile 
{
    public enum ProjectileMode      //  탄막의 이동 타입
    {
        Normal,
        Acceleration
    }

    public void SetCollisionTarget(GameObject targetObject); // 충돌 대상 설정

    public void SetMoveTarget(Vector3 targetPoint, ProjectileMode mode); // 추적 할 위치 설정
    public void SetProjectileValue(float speed, float power, float lifeTime); // 투사체 특성 값 설정

    public IEnumerator SetDurationMoveToTarget(Vector3 targetPoint, float pathfindingTime); //  해당 위치를 설정 값 만큼 추적
}

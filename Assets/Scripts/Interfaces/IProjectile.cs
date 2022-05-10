using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IProjectile 
{
    enum ProjectileSpeedMode      //  탄막의 이동 타입
    {
        Normal,
        Acceleration
    }

    void SetCollisionTarget(GameObject targetObject); // 충돌 대상 설정

    void SetMoveTarget(Vector3 targetPoint, ProjectileSpeedMode mode); // 추적 할 타겟 위치 설정
    
    void SetProjectileValue(float speed, float power, float lifeTime); // 투사체 특성 값 설정

    IEnumerator SetDurationMoveToTarget(Vector3 targetPoint, float pathfindingTime); //  해당 위치를 설정 값 만큼 추적
}

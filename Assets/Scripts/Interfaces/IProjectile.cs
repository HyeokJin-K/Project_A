using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IProjectile 
{
    enum ProjectileSpeedMode      //  ź���� �̵� Ÿ��
    {
        Normal,
        Acceleration
    }

    void SetCollisionTarget(GameObject targetObject); // �浹 ��� ����

    void SetMoveTarget(Vector3 targetPoint, ProjectileSpeedMode mode); // ���� �� Ÿ�� ��ġ ����
    
    void SetProjectileValue(float speed, float power, float lifeTime); // ����ü Ư�� �� ����

    IEnumerator SetDurationMoveToTarget(Vector3 targetPoint, float pathfindingTime); //  �ش� ��ġ�� ���� �� ��ŭ ����
}

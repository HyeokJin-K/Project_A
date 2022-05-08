using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IProjectile 
{
    public enum ProjectileMode      //  ź���� �̵� Ÿ��
    {
        Normal,
        Acceleration
    }

    public void SetCollisionTarget(GameObject targetObject); // �浹 ��� ����

    public void SetMoveTarget(Vector3 targetPoint, ProjectileMode mode); // ���� �� ��ġ ����
    public void SetProjectileValue(float speed, float power, float lifeTime); // ����ü Ư�� �� ����

    public IEnumerator SetDurationMoveToTarget(Vector3 targetPoint, float pathfindingTime); //  �ش� ��ġ�� ���� �� ��ŭ ����
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour, IProjectile
{
    [SerializeField, ReadOnly]
    GameObject targetObject;
    [SerializeField, ReadOnly]
    Transform targetTransform;

    Vector3 moveDir;
    [SerializeField]
    Rigidbody2D projectileRigidbody;

    float speed;
    float power;

    private void Awake()
    {
        projectileRigidbody = projectileRigidbody == null ? GetComponent<Rigidbody2D>() : projectileRigidbody; 
    }

    public void SetProjectileValue(float speed, float power)
    {
        this.speed = speed;
        this.power = power;
    }

    public void SetCollisionTarget(GameObject targetObject)
    {
        this.targetObject = targetObject;
    }

    public void SetMoveToTarget(Transform targetTransform)
    {        
        this.targetTransform = targetTransform;
        moveDir = (targetTransform.position - transform.position).normalized;
    }

    public IEnumerator SetDurationMoveToTarget(Transform targetTransform, float time)
    {
        float t = 0;
        while (t <= time)
        {
            t += Time.deltaTime;
            SetMoveToTarget(targetTransform);

            yield return null;
        }
    }

    private void FixedUpdate()
    {
        Move();        
    }

    void Move()
    {        
        print("ÃÑ¾ËÀÌµ¿");
        projectileRigidbody.velocity = moveDir * 5f;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals(targetObject.tag))
        {
            collision.GetComponent<IDamageable>()?.TakeDamage(power);
            gameObject.SetActive(false);
        }
    }    
}

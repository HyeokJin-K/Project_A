using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour, IProjectile
{
    [SerializeField, ReadOnly]
    GameObject targetObject;
    [SerializeField, ReadOnly]
    Vector3 targetPoint;

    Vector3 moveDir;
    [SerializeField]
    Rigidbody2D projectileRigidbody;    

    IProjectile.ProjectileMode mode = IProjectile.ProjectileMode.Normal;

    float speed = 1f;
    float maxSpeed;
    float power = 1f;
    float lifeTime = 5f;

    private void Awake()
    {
        projectileRigidbody = projectileRigidbody == null ? GetComponent<Rigidbody2D>() : projectileRigidbody;           
    }

    private void FixedUpdate()
    {
        Move();        
    }

    private void OnEnable()
    {
        StartCoroutine(ProjectileLifeEnd());
    }

    IEnumerator ProjectileLifeEnd()     //  탄막의 라이프 타임이 0이 되면 탄막 오브젝트 비활성화
    {
        float temp = lifeTime;

        while (lifeTime >= 0)
        {
            lifeTime -= Time.deltaTime;
            yield return null;
        }

        lifeTime = temp;
        gameObject.SetActive(false);
    }

    public void SetProjectileValue(float speed, float power, float lifeTime) 
    {
        this.speed = speed;
        maxSpeed = speed * 15f;
        this.power = power;
        this.lifeTime = lifeTime;        
    }

    public void SetCollisionTarget(GameObject targetObject)     
    {
        this.targetObject = targetObject;
    }

    public void SetMoveTarget(Vector3 targetPoint, IProjectile.ProjectileMode mode)    
    {
        this.mode = mode;
        this.targetPoint = targetPoint;
        moveDir = (this.targetPoint - transform.position).normalized;
    }

    public IEnumerator SetDurationMoveToTarget(Vector3 targetPoint, float pathfindingTime)
    {                                                                               
        float t = 0;

        while (t <= pathfindingTime)
        {
            t += Time.deltaTime;
            SetMoveTarget(targetPoint, IProjectile.ProjectileMode.Normal);

            yield return null;
        }
    }

    void Move()
    {                
        if(mode == IProjectile.ProjectileMode.Acceleration)
        {
            if(speed < maxSpeed)
            {
                speed += speed * 0.1f;
            }            
        }        

        projectileRigidbody.velocity = moveDir * speed;
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

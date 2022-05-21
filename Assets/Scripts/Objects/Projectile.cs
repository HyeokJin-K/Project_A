using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour, IProjectile
{
    #region Private Field

    [SerializeField, ReadOnly]
    GameObject targetObject;

    [SerializeField, ReadOnly]
    Vector3 targetPoint;

    Vector3 moveDir;

    [SerializeField]
    Rigidbody2D projectileRigidbody;    

    IProjectile.ProjectileSpeedMode speedMode = IProjectile.ProjectileSpeedMode.Normal;    

    float speed;

    [SerializeField]
    float maxSpeed = 20f;

    float power;

    [SerializeField]
    float lifeTime;

    #endregion

    //------------------------------------------------------------------------------------------------

    #region Unity LifeCycle

    private void Awake()
    {
        #region Caching

        projectileRigidbody = projectileRigidbody == null ? GetComponent<Rigidbody2D>() : projectileRigidbody;

        #endregion
    }

    private void FixedUpdate()
    {
        Move();        
    }

    private void OnEnable()
    {
        StartCoroutine(ProjectileLifeEnd());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(targetObject.tag))
        {
            collision.GetComponent<IDamageable>()?.TakeDamage(power);
            
            gameObject.SetActive(false);
        }
    }

    #endregion

    IEnumerator ProjectileLifeEnd()     //  탄막의 라이프 타임이 0이 되면 탄막 오브젝트 비활성화
    {
        yield return new WaitForSeconds(lifeTime);
        
        gameObject.SetActive(false);
    }

    public void SetProjectileValue(float speed, float power, float lifeTime) 
    {
        this.speed = speed;        

        this.power = power;

        this.lifeTime = lifeTime;        
    }

    public void SetCollisionTarget(GameObject targetObject)     
    {
        this.targetObject = targetObject;
    }

    public void SetMoveTarget(Vector3 targetPoint, IProjectile.ProjectileSpeedMode mode)    
    {
        this.speedMode = mode;

        this.targetPoint = targetPoint;

        moveDir = (this.targetPoint - transform.position).normalized;        
    }

    public IEnumerator SetDurationMoveToTarget(Vector3 targetPoint, float pathfindingTime)
    {                                                                               
        float t = 0;

        while (t <= pathfindingTime)
        {
            t += Time.deltaTime;

            SetMoveTarget(targetPoint, IProjectile.ProjectileSpeedMode.Normal);

            yield return null;
        }
    }

    void Move()
    {                
        if(speedMode == IProjectile.ProjectileSpeedMode.Acceleration)
        {
            if(speed < maxSpeed)
            {
                speed += speed * speed * 0.5f * Time.fixedDeltaTime;
            }            
        }        

        projectileRigidbody.velocity = moveDir * speed;

        transform.localRotation = Quaternion.LookRotation(Vector3.forward, moveDir);
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Object = System.Object;

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

    [SerializeField]
    SpriteRenderer projectileSpriteRenderer;

    Color initSpirteColor;

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

        projectileRigidbody = projectileRigidbody ? projectileRigidbody : GetComponent<Rigidbody2D>();

        projectileSpriteRenderer = projectileSpriteRenderer ? projectileSpriteRenderer : GetComponentInChildren<SpriteRenderer>();

        #endregion

        initSpirteColor = projectileSpriteRenderer.color;
    }

    private void FixedUpdate()
    {
        Move();        
    }

    private void OnEnable()
    {
        StartCoroutine(ProjectileLifeEnd());        
    }

    private void OnDisable()
    {
        projectileSpriteRenderer.color = initSpirteColor;
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

    IEnumerator ProjectileLifeEnd()     //  ź���� ������ Ÿ���� 0�� �Ǹ� ź�� ������Ʈ ��Ȱ��ȭ
    {
        yield return new WaitForSeconds(lifeTime);
        
        projectileSpriteRenderer?.DoDisable(SpriteDisableMode.Lerp);

        yield return new WaitUntil(() => projectileSpriteRenderer.color.a <= 0f);

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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    Transform target;
    Enemy targetEnemy;

    [Header("Attributes")]
    public float range = 15f;
    public float Range
    {
        get { return range; }
    }
    [SerializeField] [Range(5, 15)] float turnSpeed = 10f;

    [Header("Laser Attributes")]
    [SerializeField] bool useLaser = false;
    [SerializeField] LineRenderer lineRender;
    [SerializeField] ParticleSystem particleSystem;
    [SerializeField] Light impactLight;
    [SerializeField] float damageOverTime = 10;
    [SerializeField] float slowPct = 0.5f;

    [Header("Bullet Attributes (default)")]
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] float fireRate = 1f;
    float fireCountdown = 0f;


    [Header("System Settings")]
    [SerializeField] string enemyTag = "Enemy";
    [SerializeField] Transform[] firePoints;
    [SerializeField] Transform pivotPoint;

    void Start()
    {
        InvokeRepeating("UpdateTarget", 0f, 0.2f);
    }

    void UpdateTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;
        foreach (GameObject enemy in enemies)
        {
            float distance = Vector3.Distance(transform.position, enemy.transform.position);
            if(distance < shortestDistance)
            {
                shortestDistance = distance;
                nearestEnemy = enemy;
            }
        }
        if(nearestEnemy != null && shortestDistance <= range)
        {
            target = nearestEnemy.transform;
            targetEnemy = nearestEnemy.GetComponent<Enemy>();
        }
        else
        {
            target = null;
        }
    }

    void Update()
    {
        if (target == null)
        {
            if(useLaser && lineRender.enabled)
            {
                lineRender.enabled = false;
                particleSystem.Stop();
                impactLight.enabled = false;
            }
            return;
        }
        LockOnTarget();

        if(useLaser)
        {
            Laser();
        }
        else
        {
            if (fireCountdown <= 0f)
            {
                Shoot();
                fireCountdown = 1f / fireRate;
            }
        }

        fireCountdown -= Time.deltaTime;
    }

    void LockOnTarget()
    {
        Vector3 dir = target.position - transform.position;
        Quaternion lookRot = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Lerp(pivotPoint.rotation, lookRot, Time.deltaTime * turnSpeed).eulerAngles;
        pivotPoint.rotation = Quaternion.Euler(0f, rotation.y, 0f);
    }

    void Laser()
    {
        Transform currentFirePoint;
        int index = Random.Range(0, firePoints.Length);
        currentFirePoint = firePoints[index];

        if (!lineRender.enabled)
        {
            lineRender.enabled = true;
            particleSystem.Play();
            impactLight.enabled = true;
        }

        lineRender.SetPosition(0, currentFirePoint.position);
        lineRender.SetPosition(1, target.position);

        Vector3 dir = currentFirePoint.position - target.position;
        particleSystem.transform.rotation = Quaternion.LookRotation(dir);
        particleSystem.transform.position = target.position + dir.normalized;

        targetEnemy.TakeDamage(damageOverTime * Time.deltaTime);
        targetEnemy.Slow(slowPct);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
        if(target != null)
        {
            for(int i = 0; i < firePoints.Length; i++)
            {
                Gizmos.DrawLine(firePoints[i].position, target.position);
            }

        }
    }

    private void Shoot()
    {
        Transform currentFirePoint;
        int index = Random.Range(0, firePoints.Length);
        currentFirePoint = firePoints[index];

        GameObject bullet = (GameObject)Instantiate(bulletPrefab, currentFirePoint.position, currentFirePoint.rotation);
        Bullet s_bullet = bullet.GetComponent<Bullet>();

        if(bullet != null)
        {
            s_bullet.Find(target);
        }
    }
}

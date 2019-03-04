using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Transform target;
    public float speed = 70f;
    [SerializeField] float explosionRadius = 0f;
    [SerializeField] GameObject impactEffect;
    [SerializeField] float damage = 20f;

    public void Find (Transform _target)
    {
        target = _target;
    }

    private void Update()
    {
        if(target == null)
        {
            foreach (ParticleSystem p in transform.GetComponentsInChildren<ParticleSystem>())
            {
                p.transform.parent = null;
                p.SetEmissionDistRate(0);
            }

            Destroy(gameObject);
            return;
        }

        Vector3 dir = target.position - transform.position;
        float currentDistance = speed * Time.deltaTime;

        if(dir.magnitude <= currentDistance)
        {
            HitTarget();
            return;
        }

        transform.Translate(dir.normalized * currentDistance, Space.World);
        transform.LookAt(target);
    }

    void HitTarget()
    {
        Instantiate(impactEffect, transform.position, transform.rotation);

        if (explosionRadius > 0f)
        {
            Explode();
        }
        else
        {
            Damage(target);
        }

        foreach(ParticleSystem p in transform.GetComponentsInChildren<ParticleSystem>())
        {
            p.transform.parent = null;
            p.SetEmissionDistRate(0);
        }

        Destroy(gameObject);
    }

    void Explode()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach(Collider collider in colliders)
        {
            if(collider.CompareTag("Enemy"))
            {
                Damage(collider.transform);
            }
        }
    }

    void Damage(Transform enemy)
    {
        Enemy e = enemy.GetComponent<Enemy>();

        if (e != null)
        {
            e.TakeDamage(damage);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}

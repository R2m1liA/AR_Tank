using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShellMove : MonoBehaviour
{
    public float moveSpeed;
    public float maxLifetime = 5.0f; // �ڵ��������ʱ��
    public float explosionRadius = 2.0f; // ��ը�뾶
    public GameObject explosionEffect; // ��ըЧ��
    public int damage = 50; // �˺�ֵ
    public AudioClip explosionSound; // ��ը��Ч

    private ParticleSystem _explosion_particle;

    private float lifetime;
    // Start is called before the first frame update
    void Start()
    {
        lifetime = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.forward * Time.deltaTime * moveSpeed;
        lifetime += Time.deltaTime;

        if (lifetime >= maxLifetime)
        {
            Explode();
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        Explode();
    }

    void Explode()
    {
        // ��ʾ��ըЧ��
        if (explosionEffect != null)
        {
            Instantiate(explosionEffect, transform.position, transform.rotation).GetComponent<ParticleSystem>().Play();
            AudioSource.PlayClipAtPoint(explosionSound, transform.position);
        }

        // ��ⱬը��Χ�ڵ���������
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach (Collider nearbyObject in colliders)
        {
            // ��̹������˺�
            EnemyTankAI EnemyTankai = nearbyObject.GetComponent<EnemyTankAI>();
            if (EnemyTankai != null)
            {
                // �����ڵ�
                Destroy(gameObject);
                EnemyTankai.TakeDamage(damage);
            }

        }

        // �����ڵ�
        Destroy(gameObject);
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShellMove : MonoBehaviour
{
    public float moveSpeed;
    public float maxLifetime = 5.0f; // 炮弹飞行最大时间
    public float explosionRadius = 2.0f; // 爆炸半径
    public GameObject explosionEffect; // 爆炸效果
    public int damage = 50; // 伤害值
    public AudioClip explosionSound; // 爆炸音效

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
        // 显示爆炸效果
        if (explosionEffect != null)
        {
            Instantiate(explosionEffect, transform.position, transform.rotation).GetComponent<ParticleSystem>().Play();
            AudioSource.PlayClipAtPoint(explosionSound, transform.position);
        }

        // 检测爆炸范围内的所有物体
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach (Collider nearbyObject in colliders)
        {
            // 对坦克造成伤害
            EnemyTankAI EnemyTankai = nearbyObject.GetComponent<EnemyTankAI>();
            if (EnemyTankai != null)
            {
                // 销毁炮弹
                Destroy(gameObject);
                EnemyTankai.TakeDamage(damage);
            }

        }

        // 销毁炮弹
        Destroy(gameObject);
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private int bulletDamage = 25;

    private void Start()
    {
        Physics2D.IgnoreCollision(GetComponent<Collider2D>(), GameObject.FindWithTag("Player").GetComponentInParent<Collider2D>());
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.GetComponent<Collider2D>() != null)
        {
            IDamageable damageable = collision.collider.GetComponent<IDamageable>();

            if (damageable != null)
            {
                damageable.Damage(bulletDamage);
                Destroy(gameObject);
            }
        }
    }

}

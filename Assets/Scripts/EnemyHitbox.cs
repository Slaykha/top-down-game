using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHitbox : Collidable
{
    public int damage = 1;
    public float pushForce = 5;

    protected override void OnCollide(Collider2D col)
    {
        if(col.CompareTag("Fighter") && col.name == "Player")
        {
            Damage dmg = new Damage
            {
                damageAmount = damage,
                pushForce = pushForce,
                origin = transform.position
            };

            col.SendMessage("RecieveDamage", dmg);
        }
    }

}

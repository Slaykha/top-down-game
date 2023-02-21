using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : Collidable
{
    public int damagePoint = 1;
    public float pushForce = 2.0f;

    // Upgrade
    public int weaponLevel = 0;
    private SpriteRenderer spriteRenderer;

    // Swing
    private Animator anim;
    private float cooldown = 0.5f;
    private float lastSwing;

    protected override void Start()
    {
        base.Start();
        spriteRenderer = GetComponent<SpriteRenderer>();    
        anim = GetComponent<Animator>();
    }

    protected override void Update()
    {
        base.Update();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (Time.time - lastSwing > cooldown) 
            {
                lastSwing = Time.time;
                Swing();
            }
            
        }
    }

    protected override void OnCollide(Collider2D col)
    {
        if(col.CompareTag("Fighter"))
        {
            if (col.name == "Player")
                return;

            // Create a new damage object, then send it to the fighter we've hit.
            Damage dmg = new Damage
            {
                damageAmount = damagePoint,
                pushForce = pushForce,
                origin = transform.position
            };

            col.SendMessage("RecieveDamage", dmg);         
        }

    }

    private void Swing()
    {
        anim.SetTrigger("Swing");
    }

}

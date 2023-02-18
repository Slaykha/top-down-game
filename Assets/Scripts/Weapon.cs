using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : Collidable
{
    public int damagePoint = 1;
    public float pudhForce = 2.0f;

    // Upgrade
    public int weaponLevel = 0;
    private SpriteRenderer spriteRenderer;

    // Swing
    private float cooldown = 0.5f;
    private float lastSwing;

    protected override void Start()
    {
        base.Start();
        spriteRenderer = GetComponent<SpriteRenderer>();    

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
                pushForce = pudhForce,
                origin = transform.position
            };

            col.SendMessage("RecieveDamage", dmg);         
        }

    }

    private void Swing()
    {
        Debug.Log("Swing");
    }

}

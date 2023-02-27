using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : Collidable
{
    public int[] damagePoint = {1 ,2 ,3 ,4 ,5 ,6 ,7};
    public float[] pushForce = { 2.0f, 2.2f, 2.5f, 3f, 3.5f, 4f, 10f};

    // Upgrade
    public int weaponLevel = 0;
    private SpriteRenderer spriteRenderer;

    // Swing
    private Animator anim;
    private float cooldown = 0.5f;
    private float lastSwing;

    private void Awake()
    {
        spriteRenderer= GetComponent<SpriteRenderer>();
    }

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
                damageAmount = damagePoint[weaponLevel],
                pushForce = pushForce[weaponLevel],
                origin = transform.position
            };

            col.SendMessage("RecieveDamage", dmg);         
        }

    }

    private void Swing()
    {
        anim.SetTrigger("Swing");
    }

    public void UpgradeWeapon()
    {
        weaponLevel++;
        spriteRenderer.sprite = GameManager.instance.weaponSprite[weaponLevel];

        // Change stats
    }

    public void SetWeaponLevel(int level)
    {
        weaponLevel = level;
        spriteRenderer.sprite = GameManager.instance.weaponSprite[weaponLevel];
    }
}

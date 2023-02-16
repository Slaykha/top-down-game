using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : Collectable
{
    public Sprite emptyChest;
    public int lirasAmount = 10;

    protected override void OnCollect()
    {
        if (!collected)
        {
            collected = true;
            GetComponent<SpriteRenderer>().sprite = emptyChest;
            GameManager.instance.liras += lirasAmount;
            GameManager.instance.showText("+ " + lirasAmount + "liras!", 24, Color.yellow, transform.position, Vector3.up * 20, 1.0f);
        }
    }
}

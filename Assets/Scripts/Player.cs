using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
    
public class Player : Mover
{
    private float dashLength = 0.35f;
    private Vector3 dashDirection;

    private void FixedUpdate()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            Dash(transform.position);
            return;
        }

        UpdateMotor(new Vector3(x, y, 0));

    }

    private void Dash(Vector3 pos)
    {

        moveDelta = pos * dashLength;
        UpdateMotor(moveDelta);
    }
}

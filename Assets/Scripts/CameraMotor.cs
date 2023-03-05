using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMotor : MonoBehaviour
{
    public Transform lookAt;
    public float boundX = 0.25f;
    public float boundY = 0.15f;

    private void Start()
    {
        lookAt = GameObject.Find("Player").transform;
    }

    private void LateUpdate()
    {
        Vector3 delta = Vector3.zero;

        //Check if inside the bound
        float deltaX = lookAt.position.x - transform.position.x;
        if(deltaX > boundX || deltaX < -boundX)
        {
            //left
            if(transform.position.x < lookAt.position.x)
            {
                delta.x = deltaX - boundX;

            }
            //right
            else
            {
                delta.x = deltaX + boundX;
            }
        }

        //Check if inside the bound
        float deltaY = lookAt.position.y - transform.position.y;
        if (deltaY > boundY || deltaY < -boundY)
        {
            //top
            if (transform.position.y < lookAt.position.y)
            {
                delta.y = deltaY - boundY;

            }
            //down
            else
            {
                delta.y = deltaY + boundY;
            }
        }

        transform.position += new Vector3(delta.x, delta.y, 0);

    }

}

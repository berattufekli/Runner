using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    CharacterController cc;
    Vector3 movec = Vector3.zero;
    public float forwardForce = 10;
    public float sidewayForce = 6;
    float forceSpeed = 0;
    float sideForce = 0;
    public float smoothSpeed = 0.125f;
    private bool canMove = true;

    private float gravity = -20;
    private Vector3 worldPos;
    

    private void Start()
    {
        cc = GetComponent<CharacterController>();        
    }

    // Update is called once per frame
    void Update()
    {

        if (!cc.isGrounded)
        {
            movec.y += gravity * Time.deltaTime;
        }

        //if (Input.GetKeyDown("d"))
        //{
        //    movec.x = sideForce;
        //}

        //if (Input.GetKeyDown("a"))
        //{
        //    movec.x = -sideForce;
        //}

        //if(Input.GetKeyUp("a") || Input.GetKeyUp("d"))
        //{
        //    movec.x = 0;
        //}





        if (gameObject.CompareTag("Normal"))
        {
            forceSpeed = forwardForce;
            sideForce = sidewayForce;
        }
        if (gameObject.CompareTag("Bigger"))
        {
            forceSpeed = forwardForce - forwardForce / 10;
            sideForce = sidewayForce - sidewayForce / 10;
        }
        if (gameObject.CompareTag("Normal"))
        {
            forceSpeed = forwardForce + forwardForce / 10;
            sideForce = sidewayForce + sidewayForce / 10;    
        }

        movec.z = forceSpeed;

        cc.Move(movec * Time.deltaTime);
    }

    private void FixedUpdate()
    {

        Vector3 screenPos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10);
        worldPos = Camera.main.ScreenToWorldPoint(screenPos);
        if (worldPos.x >= -2.4f && worldPos.x <= 2.4f)
        {
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, new Vector3(worldPos.x, transform.position.y, transform.position.z), smoothSpeed);
            transform.position = smoothedPosition;
        }
    }
}

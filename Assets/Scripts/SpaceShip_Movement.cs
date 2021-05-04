using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceShip_Movement : MonoBehaviour
{
    #region Fields

    [SerializeField] private float boost;
    private Rigidbody rb;
    private float verticalAxisValue;
    private float horizontalAxisValue;
    private float jumpAxisValue;

    #endregion

    #region Methods

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        verticalAxisValue = Input.GetAxis("Vertical");
        horizontalAxisValue = Input.GetAxis("Horizontal");
        jumpAxisValue = Input.GetAxis("Jump");
        Move();
    }

    private void Move()
    {
        if (verticalAxisValue != 0)
        {
            transform.Rotate(new Vector3(0, 0, 1) * verticalAxisValue);
        }
        if (horizontalAxisValue != 0)
        {
            transform.Rotate(new Vector3(0, 0, 1) * horizontalAxisValue);
        }
        if (jumpAxisValue != 0)
        {
            rb.AddRelativeForce(Vector3.up * boost);
        }
    }

    #endregion
}

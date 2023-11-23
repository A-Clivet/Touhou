using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.GraphicsBuffer;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    Rigidbody2D rb;

    private Vector2 dir;
    private bool c_shoot = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void FixedUpdate()
    {
        rb.velocity = dir * moveSpeed;
    }

    private void Update()
    {
        if (c_shoot)
        {
            GameObject bullet = ObjectPool.SharedInstance.GetBullet();
            if (bullet != null)
            {
                bullet.transform.position = new Vector3(transform.position.x, transform.position.y+0.5f, transform.position.z);
                bullet.transform.rotation = transform.rotation;
                bullet.SetActive(true);
            }
        }
    }

    public void Move(InputAction.CallbackContext context)
    {
        dir = context.ReadValue<Vector2>();
    }

    public void Shoot(InputAction.CallbackContext context) 
    {
        if(context.performed)
        {
           c_shoot = true;
        }
        if(context.canceled)
        {
            c_shoot= false;
        }
    }
}

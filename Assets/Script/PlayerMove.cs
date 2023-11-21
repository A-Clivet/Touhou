using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] float moveSpeed;

    private Vector2 dir;

    public void Move(InputAction.CallbackContext context)
    {
        dir = context.ReadValue<Vector2>();
    }

    private void Update()
    {
        float horizontalMovement = dir.x * moveSpeed * Time.deltaTime;
        float verticalMovement = dir.y * moveSpeed * Time.deltaTime;

        transform.position += new Vector3(horizontalMovement, verticalMovement, transform.position.z);
    }
}

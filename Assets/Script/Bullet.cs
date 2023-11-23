using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    Rigidbody2D rb;
    private float moveSpeed = 25;
    private Vector2 launch;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        launch = new Vector2(0, moveSpeed);
    }

    private void Update()
    {
        rb.velocity = launch;
    }



    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("BoundingBox"))
        {
            transform.gameObject.SetActive(false);
        }
    }
}

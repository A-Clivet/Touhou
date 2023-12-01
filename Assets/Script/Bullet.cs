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
        launch = new Vector2(0, moveSpeed); // valeur de direction et vitesse de la balle
    }

    private void Update()
    {
        if (transform.gameObject.CompareTag("PlayerShot"))
        {
            rb.velocity = launch;
        }
        if (Time.timeScale <= 0)
        {
            gameObject.SetActive(false);
        }
    }
}

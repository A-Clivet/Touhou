using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.GraphicsBuffer;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] GameObject Lose;

    public float moveSpeed;
    Rigidbody2D rb;
    private Vector2 dir;
    private bool c_shoot = false;
    private bool recover = false;

    public int life = 3;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        Time.timeScale = 1;
    }
    private void FixedUpdate()
    {
        if (transform.gameObject != null)
        {
            rb.velocity = dir * moveSpeed; //direction + vitesse de deplacement du perso
        }
    }

    private void Update()
    {
        if (life == 0)
        {
            StopAllCoroutines();
            ShootingPattern.SharedInstance.Stop();
            Time.timeScale = 0f;
            Lose.SetActive(true);
            Destroy(gameObject);
        }

        if (c_shoot && transform.gameObject != null)
        {
            GameObject bullet = ObjectPool.SharedInstance.GetBullet(); // création des balles
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
        dir = context.ReadValue<Vector2>(); // donne a dir la direction selon la touche pressé
    }

    public void Shoot(InputAction.CallbackContext context) 
    {
        if(context.performed) // quand j'appuie sur la touche
        {
           c_shoot = true;
        }
        if(context.canceled) // quand je lache la touche
        {
            c_shoot= false;
        }
    }


    private IEnumerator Recovering() // rend invincible pendant 3s après avoir été touché
    {
        for(int i = 0; i < 2; i++)
        {
            recover = !recover;
            yield return new WaitForSeconds(3);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!recover && collision.CompareTag("EnemyShot"))
        {
            life -= 1;
            StartCoroutine(Recovering());
            collision.gameObject.SetActive(false);
        }
    }

}

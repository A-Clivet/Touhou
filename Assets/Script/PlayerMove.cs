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
    private bool useSpell = false;
    private bool recallSpell = true;

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
                bullet.GetComponent<Bullet>().damage = 1;
                bullet.transform.SetPositionAndRotation(new Vector3(transform.position.x, transform.position.y+0.5f, transform.position.z), transform.rotation);
                bullet.SetActive(true);
            }
        }

        if (transform.gameObject != null && useSpell)
        {
            useSpell =  false;
            GameObject s_bullet = ObjectPool.SharedInstance.GetSBullet();
            s_bullet.GetComponent<Bullet>().damage = 500;
            s_bullet.transform.SetPositionAndRotation(new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z), transform.rotation);
            s_bullet.SetActive(true);
            StartCoroutine(SpellRecover(10));
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

    public void SpecialShoot(InputAction.CallbackContext context) 
    {
        if(context.performed && recallSpell) // quand j'appuie sur la touche
        {
            recallSpell = false;
            StartCoroutine(Cast(5));
        }
    }

    private IEnumerator Cast(float time)
    {
        SpecialPrint.sharedInstance.BeginTimer();
        yield return new WaitForSeconds(time);
        useSpell = true;
    }

    private IEnumerator SpellRecover(float time)
    {
        yield return new WaitForSeconds(time);
        recallSpell = true;
        
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingPattern : MonoBehaviour
{
    public static ShootingPattern SharedInstance;
    [SerializeField] GameObject player;

    float rota;

    void Awake()
    {
        SharedInstance = this;
    }

    public void Stop()
    {
        StopAllCoroutines();
    }
    // tir toutautour de lui en même temps
    public IEnumerator Zone(int numbersOfBullet, float speedOfBullet, GameObject boss)
    {
        for (int i = 0; i < numbersOfBullet; i++)
        {
            GameObject bullet = ObjectPool.SharedInstance.GetEnemyBullet();
            if (bullet != null)
            {
                float rota = 360 / numbersOfBullet * i;
                bullet.transform.SetPositionAndRotation(new Vector3(boss.transform.position.x, boss.transform.position.y, boss.transform.position.z), boss.transform.rotation);
                bullet.SetActive(true);
                bullet.transform.rotation = Quaternion.Euler(0, 0, rota);
                Vector2 dir = bullet.transform.up;
                bullet.GetComponent<Rigidbody2D>().velocity = dir * speedOfBullet;
            }
        }
        yield return null;
    }

    // tir tout autour de lui avec du décalage pour faire une spirale
    public IEnumerator Spiral(int numbersOfBullet, float speedOfBullet, GameObject boss)
    {
        for (int i = 0; i < numbersOfBullet; i++)
        {
            GameObject bullet = ObjectPool.SharedInstance.GetEnemyBullet();
            if (bullet != null)
            {
                if (numbersOfBullet > 15)
                {
                    rota = 360 / 15 * i;
                }
                else { rota = 360 / numbersOfBullet * i; }
                bullet.transform.SetPositionAndRotation(new Vector3(boss.transform.position.x, boss.transform.position.y, boss.transform.position.z), boss.transform.rotation);
                bullet.SetActive(true);
                bullet.transform.rotation = Quaternion.Euler(0,0,rota);
                Vector2 dir = bullet.transform.up;
                bullet.GetComponent<Rigidbody2D>().velocity = dir * speedOfBullet;
            }
            yield return new WaitForSeconds(0.2f);
        }
    }

    // tir plusieurs fois dans des directions aléatoire dans un cêne dont le centre est le joueur
    public IEnumerator RapidFire(int numbersOfBullet, float speedOfBullet, int angle, GameObject boss) 
    {
        for (int i = 0; i < numbersOfBullet; i++)
        {
            GameObject bullet = ObjectPool.SharedInstance.GetEnemyBullet();
            if (bullet != null)
            {
                bullet.transform.SetPositionAndRotation(new Vector3(boss.transform.position.x, boss.transform.position.y, boss.transform.position.z), boss.transform.rotation);
                bullet.SetActive(true);
                rota = Mathf.Atan2(player.transform.position.y - bullet.transform.position.y, player.transform.position.x - bullet.transform.position.x) * Mathf.Rad2Deg;
                rota += Random.Range(-angle/2, angle/2);
                bullet.transform.rotation = Quaternion.Euler(0, 0, rota);
                Vector2 dir = bullet.transform.right;
                bullet.GetComponent<Rigidbody2D>().velocity = dir * speedOfBullet;
            }
            yield return new WaitForSeconds(0.2f);
        }
    }

    // fais 1 tire rapide en direction du jouuer
    public IEnumerator Canon(GameObject boss)
    {
        GameObject bullet = ObjectPool.SharedInstance.GetEnemyBullet();
        if (bullet != null)
        {
            bullet.transform.SetPositionAndRotation(new Vector3(boss.transform.position.x, boss.transform.position.y, boss.transform.position.z), boss.transform.rotation);
            bullet.SetActive(true);
            Vector2 dir = player.transform.position - boss.transform.position;
            bullet.GetComponent<Rigidbody2D>().velocity = dir * 5.5f;
            yield return null;
        }
    }
}

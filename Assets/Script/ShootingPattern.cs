using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingPattern : MonoBehaviour
{
    public static ShootingPattern SharedInstance;

    void Awake()
    {
        SharedInstance = this;
    }

    public void Zone(int numbersOfBullet, float speedOfBullet, GameObject boss)
    {
        for (int i = 0; i < numbersOfBullet; i++)
        {
            GameObject bullet = ObjectPool.SharedInstance.GetEnemyBullet();
            if (bullet != null)
            {
                float rota = 360 / numbersOfBullet * i;
                bullet.transform.position = new Vector3(boss.transform.position.x, boss.transform.position.y, boss.transform.position.z);
                bullet.transform.rotation = boss.transform.rotation;
                bullet.SetActive(true);
                bullet.transform.Rotate(0, 0, rota);
                Vector2 dir = bullet.transform.up;
                bullet.GetComponent<Rigidbody2D>().velocity = dir * speedOfBullet;
            }
        }
    }
}

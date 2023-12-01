using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public static ObjectPool SharedInstance;

    GameObject _specialShot;

    [Header("Mes Munitions")]
    public List<GameObject> shotLoad;
    public GameObject shot;
    public GameObject specialShot;
    public int amountToPool;

    [Header("Munitions Ennemie")]
    public List<GameObject> enemyShotLoad;
    public GameObject enemyShot;
    public int enemyAmountToPool;

    void Awake()
    {
        SharedInstance = this;
    }

    private void OnEnable()
    {
        // my classic shot - création de la list
        shotLoad = new List<GameObject>();
        GameObject tmp;
        for (int i = 0; i < amountToPool; i++)
        {
            tmp = Instantiate(shot);
            tmp.SetActive(false);
            shotLoad.Add(tmp);
        }
        _specialShot = Instantiate(specialShot);
        _specialShot.SetActive(false); 


        // Enemy shot - création de la list
        enemyShotLoad = new List<GameObject>();
        GameObject etmp;
        for (int j = 0; j < enemyAmountToPool; j++)
        {
            etmp = Instantiate(enemyShot);
            etmp.SetActive(false);
            enemyShotLoad.Add(etmp);
        }
    }


    public GameObject GetBullet() // My classic shot
    {
        for (int i = 0; i < amountToPool; i++)
        {
            if (!shotLoad[i].activeSelf)
            {
                return shotLoad[i];
            }
        }
        return null;
    }

    public GameObject GetSBullet()
    {
        return _specialShot;
    }

    public GameObject GetEnemyBullet() // Enemy classic shot
    {
        for (int i = 0; i < enemyAmountToPool; i++)
        {
            if (!enemyShotLoad[i].activeSelf)
            {
                return enemyShotLoad[i];
            }
        }
        return null;
    }


}

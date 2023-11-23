using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [Header("Mes Munitions")]
    public static ObjectPool SharedInstance;
    public List<GameObject> shotLoad;
    public GameObject objectToPool;
    public int amountToPool;

    //[Header("Munitions Ennemie")]
    //public static ObjectPool SharedInstance;
    //public List<GameObject> shotLoad;
    //public GameObject objectToPool;
    //public int amountToPool;

    void Awake()
    {
        SharedInstance = this;
    }

    void Start()
    {
        shotLoad = new List<GameObject>();
        GameObject tmp;
        for (int i = 0; i < amountToPool; i++)
        {
            tmp = Instantiate(objectToPool);
            tmp.SetActive(false);
            shotLoad.Add(tmp);
        }
    }

    public GameObject GetBullet()
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


}

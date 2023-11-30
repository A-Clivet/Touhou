using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using UnityEngine;

public class BossManage : MonoBehaviour
{

    [SerializeField] GameObject GO_boss1;
    [SerializeField] TextMeshProUGUI bossLife;

    GameObject boss1;
    bool alive = true;


    // Start is called before the first frame update
    void Start()
    {
       boss1 = Instantiate(GO_boss1,transform.position, quaternion.identity, transform);
    }

    private void Update()
    {
        if(!boss1.activeSelf)
        {
            boss1.transform.position = transform.position;
            alive = false;
            StartCoroutine(Spawn());
        }
        bossLife.text = "Boss Life : " + GetComponentInChildren<Boss1>().PV;
    }
    public IEnumerator Spawn()
    {
        yield return new WaitForSeconds(1f);
        boss1.SetActive(true);
        if(!alive)
        {
            boss1.GetComponent<Boss1>().PlayPattern(0);
            alive = true;
        }
    }
}

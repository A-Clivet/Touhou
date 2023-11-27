using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class Boss1 : MonoBehaviour
{
    [SerializeField] int PV;
    int k = 1;

    Vector3 go;

    private int phase = 1;
    List<float> borders1; // list(Top, Down, Left, Right)
    List<float> borders2;
    List<float> borders3;

    // Coroutin
    List<float> PatternSpacing;
    List<string> PatternNaming;
    private int _phase = 0;
    //----------------------------------


    // Start is called before the first frame update
    void Start()
    {
        // Def des Zone de déplacement/teleportation
        borders1 = MapZone.SharedInstance.Zone1();
        borders2 = MapZone.SharedInstance.Zone2();
        borders3 = MapZone.SharedInstance.Zone3();
        //--------------------------------------------


        
        //if (PatternSpacing.Count != PatternNaming.Count)
        //{
        //    Debug.Log("pizdyetz");
        //    this.enabled = false;
        //    return;
        //}
        //PlayPattern(0);
    }

    // Update is called once per frame
    void Update()
    {
        if (PV <= 1500) { phase = 2; }
        if (PV <= 0) { Destroy(gameObject); }
        if (k >= 1)
        {
            //Zone3TP(false);
            ShootingPattern.SharedInstance.StartCoroutine(ShootingPattern.SharedInstance.RapidFire(50, 5, 180, transform.gameObject));
            k = 0;
        }
    }


    private void ActionPattern()
    {

    }

    private void Zone1TP(bool TP)
    {
        float pos_y = Random.Range(borders1[1], borders1[0]);
        float pos_x = Random.Range(borders1[2], borders1[3]);
        Vector3 go = new Vector3(pos_x,pos_y,transform.position.z);

        if (TP) { transform.position = go; } // teleportation dans la zone
        else { /* Lerp */ } // Déplacement dans la zone
    }

    private void Zone2TP(bool TP)
    {
        float pos_y = Random.Range(borders2[1], borders2[0]);
        float pos_x = Random.Range(borders2[2], borders2[3]);
        Vector3 go = new Vector3(pos_x, pos_y, transform.position.z);

        if (TP) { transform.position = new Vector2(pos_x, pos_y); } // teleportation dans la zone
        else { /* Lerp */ } // Déplacement dans la zone
    }

    private void Zone3TP(bool TP)
    {
        float pos_y = Random.Range(borders3[1], borders3[0]);
        float pos_x = Random.Range(borders3[2], borders3[3]);
        Vector3 go = new Vector3(pos_x, pos_y, transform.position.z);

        if (TP) { transform.position = new Vector2(pos_x, pos_y); } // teleportation dans la zone
        else { /* Lerp */ } // Déplacement dans la zone
    }

    // Coroutine pour les patterns
    private void PlayPattern(int Index)
    {
        if (Index >= PatternSpacing.Count || Index < 0) return;

        _phase = Index;
        StartCoroutine(PatternTempo());
    }

    private void EndPattern()
    {
        Debug.Log("Ending : " + PatternNaming[_phase]);
        PlayPattern(_phase + 1);
    }

    IEnumerator PatternTempo()
    {
        Debug.Log("Starting : " + PatternNaming[_phase]);
        yield return new WaitForSeconds(PatternSpacing[_phase]);
        EndPattern();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerShot")) // desactive la balle si elle sort de l'ecran
        {
            PV -= 1;
            collision.transform.gameObject.SetActive(false);
        }
    }
}

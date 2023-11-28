using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class Boss1 : MonoBehaviour
{
    Vector3 go;
    Vector3 start;


    [SerializeField] int PV;
    int startPV;
    float speed = 2.0f;
    int dead = 0;
    private int phase = 1;
    List<float> borders1; // list(Top, Down, Left, Right)
    List<float> borders2;
    List<float> borders3;

    // Coroutine
    [SerializeField] List<string> ShootName;
    public List<float> PatternSpacing;
    public List<Action> PatternActions;

    private int _phase = 0;
    //----------------------------------
    private void Awake()
    {
        PatternActions = new List<Action>();
    }

    // Start is called before the first frame update
    void Start()
    {
        startPV = PV;

        // Def des Zone de déplacement/teleportation
        borders1 = MapZone.SharedInstance.Zone1();
        borders2 = MapZone.SharedInstance.Zone2();
        borders3 = MapZone.SharedInstance.Zone3();
        //--------------------------------------------

        // Implémentation du pattern
        foreach(string name in ShootName)
        {
            if (name == "Zone" || name == "zone")
            {
                PatternActions.Add(() =>
                {
                    ShootingPattern.SharedInstance.StartCoroutine(ShootingPattern.SharedInstance.Zone(50 + dead, 5 + dead, transform.gameObject));
                });
            }

            if (name == "Spiral" || name == "spiral")
            {
                PatternActions.Add(() =>
                {
                    ShootingPattern.SharedInstance.StartCoroutine(ShootingPattern.SharedInstance.Spiral(50 + dead, 5 + dead, transform.gameObject));
                });
            }
            if (name == "RapidFire" || name == "rapidfire")
            {
                PatternActions.Add(() =>
                {
                    ShootingPattern.SharedInstance.StartCoroutine(ShootingPattern.SharedInstance.RapidFire(50 + dead, 5 + dead, 90, transform.gameObject));
                });
            }

            if (name == "Canon" || name == "canon")
            {
                PatternActions.Add(() =>
                {
                    ShootingPattern.SharedInstance.StartCoroutine(ShootingPattern.SharedInstance.Canon(transform.gameObject));
                });
            }
        }


        
        PlayPattern(0);
    }

    // Update is called once per frame
    void Update()
    {        
        if (PV <= 1500) { phase = 2; speed = 4.0f; }
        if (PV <= 0) 
        {
            ShootingPattern.SharedInstance.Stop();
            gameObject.SetActive(false);
            phase = 1; speed = 2; PV = startPV;
            dead += 1;
        }
    }

    private void Zone1TP(bool TP)
    {
        float pos_y = Random.Range(borders1[1], borders1[0]);
        float pos_x = Random.Range(borders1[2], borders1[3]);

        start = transform.position;
        go = new Vector3(pos_x, pos_y, transform.position.z);

        if (TP) { transform.position = go; } // teleportation dans la zone
        else { StartCoroutine(Move(start, go, speed)); } // Déplacement dans la zone
    }

    private void Zone2TP(bool TP)
    {
        float pos_y = Random.Range(borders2[1], borders2[0]);
        float pos_x = Random.Range(borders2[2], borders2[3]);

        start = transform.position;
        go = new Vector3(pos_x, pos_y, transform.position.z);

        if (TP) { transform.position = new Vector2(pos_x, pos_y); } // teleportation dans la zone
        else { StartCoroutine(Move(start, go, speed)); } // Déplacement dans la zone
    }

    private void Zone3TP(bool TP)
    {
        float pos_y = Random.Range(borders3[1], borders3[0]);
        float pos_x = Random.Range(borders3[2], borders3[3]);

        start = transform.position;
        go = new Vector3(pos_x, pos_y, transform.position.z);
         
        if (TP) { transform.position = new Vector2(pos_x, pos_y); } // teleportation dans la zone
        else { StartCoroutine(Move(start, go, speed)); } // Déplacement dans la zone
    }

    IEnumerator Move(Vector3 start, Vector3 go, float timeToMove)
    {
        float t = 0;
        while (t < 1)
        {
            transform.position = Vector3.Lerp(start, go, t);
            t += Time.deltaTime / timeToMove;
            yield return new WaitForEndOfFrame();
        }
        transform.position = go;
    }


    // Coroutine pour les patterns
    private void PlayPattern(int Index)
    {
        if (Index >= PatternSpacing.Count || Index < 0) Index = 0;

        _phase = Index;
        StartCoroutine(PatternTempo());
    }

    private void EndPattern()
    {
        Debug.Log("Ending : " + PatternActions[_phase]);
        PlayPattern(_phase + 1);
    }

    IEnumerator PatternTempo()
    {
        Action action = PatternActions[_phase];
        action.Invoke();
        //Debug.Log("Starting : " + PatternNaming[_phase]);
        yield return new WaitForSeconds(PatternSpacing[_phase]);
        EndPattern();
    }
    // -----------------------------------------------------------


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerShot")) // eneleve 1 PV et desactive la balle si elle touche
        {
            PV -= 1;
            collision.transform.gameObject.SetActive(false);
        }
    }
}

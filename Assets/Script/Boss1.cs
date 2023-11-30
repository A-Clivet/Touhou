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
    private int _phase = 0;
    private int phase = 1;

    List<float> borders1; // list(Top, Down, Left, Right)
    List<float> borders2;
    List<float> borders3;

    // Coroutine
    [SerializeField] List<TypeShotEnum> ShootName;
    public List<float> PatternSpacing;
    public List<Action> PatternActions;

    // Enums
    public enum TypeShotEnum
    {
        Zone,
        Spiral,
        RapidFire,
        Canon,
        TPzone1,
        Zone1,
        TPzone2,
        Zone2,
        TPzone3,
        Zone3,

    }

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

        // Egalisation des list
        if(PatternSpacing.Count > ShootName.Count)
        {
            int j = PatternSpacing.Count - ShootName.Count;
            for (int i = j;  i > 0; i--)
            {
                PatternSpacing.RemoveAt(PatternSpacing.Count - 1);
            }
        }
        if(PatternActions.Count < ShootName.Count)
        {
            int j = ShootName.Count - PatternSpacing.Count;
            for (int i = j; i > 0; i--)
            {
                PatternSpacing.Add(2);
            }
        }


        // Implémentation du pattern
        foreach(var name in ShootName)
        {
            switch (name)
            {
                case TypeShotEnum.Zone:
                    PatternActions.Add(() =>
                    {
                        ShootingPattern.SharedInstance.StartCoroutine(ShootingPattern.SharedInstance.Zone(50 + dead, 5 + dead, transform.gameObject));
                    });
                    break;

                case TypeShotEnum.Spiral:
                    PatternActions.Add(() =>
                    {
                        ShootingPattern.SharedInstance.StartCoroutine(ShootingPattern.SharedInstance.Spiral(50 + dead, 5 + dead, transform.gameObject));
                    });
                    break;

                case TypeShotEnum.RapidFire:
                    PatternActions.Add(() =>
                    {
                        ShootingPattern.SharedInstance.StartCoroutine(ShootingPattern.SharedInstance.RapidFire(50 + dead, 5 + dead, 90, transform.gameObject));
                    });
                    break;

                case TypeShotEnum.Canon:
                    PatternActions.Add(() =>
                    {
                        ShootingPattern.SharedInstance.StartCoroutine(ShootingPattern.SharedInstance.Canon(transform.gameObject));
                    });
                    break;

                case TypeShotEnum.TPzone1:
                    PatternActions.Add(() =>
                    {
                        Zone1TP(true);
                    });
                    break;

                case TypeShotEnum.Zone1:
                    PatternActions.Add(() =>
                    {
                        Zone1TP(false);
                    });
                    break;

                case TypeShotEnum.TPzone2:
                    PatternActions.Add(() =>
                    {
                        Zone2TP(true);
                    });
                    break;

                case TypeShotEnum.Zone2:
                    PatternActions.Add(() =>
                    {
                        Zone2TP(false);
                    });
                    break;

                case TypeShotEnum.TPzone3:
                    PatternActions.Add(() =>
                    {
                        Zone2TP(true);
                    });
                    break;

                case TypeShotEnum.Zone3:
                    PatternActions.Add(() =>
                    {
                        Zone3TP(false);
                    });
                    break;

                default:
                    PatternActions.Add(() =>
                    {
                        ShootingPattern.SharedInstance.StartCoroutine(ShootingPattern.SharedInstance.Zone(50 + dead, 5 + dead, transform.gameObject));
                    });
                    break;
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
    public void PlayPattern(int Index)
    {
        if (Index >= PatternSpacing.Count || Index < 0) Index = 1;

        _phase = Index;
        StartCoroutine(PatternTempo());
    }

    private void EndPattern()
    {
        PlayPattern(_phase + 1);
    }

    IEnumerator PatternTempo()
    {
        yield return new WaitForSeconds(PatternSpacing[_phase]);
        Action action = PatternActions[_phase];
        action.Invoke();
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

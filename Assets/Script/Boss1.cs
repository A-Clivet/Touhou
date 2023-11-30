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

    [SerializeField] GameObject player;
    [SerializeField] int PV;
    int startPV;
    float moveSpeed = 2.0f;
    float shotSpeed = 5.0f;
    int dead = 0;
    private int _phase = 0;
    private int phase = 1;
    private bool phase2 = false;

    List<float> borders1; // list(Top, Down, Left, Right)
    List<float> borders2;
    List<float> borders3;

    // Coroutine
    [Header("Phase 1")]
    [SerializeField] List<TypeShotEnum> ShootNamePhase1;
    public List<float> PatternSpacingPhase1;
    public List<Action> PatternActionsPhase1;

    [Header("Phase 2")] 
    [SerializeField] List<TypeShotEnum> ShootNamePhase2;
    public List<float> PatternSpacingPhase2;
    public List<Action> PatternActionsPhase2;

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
        PatternActionsPhase1 = new List<Action>();
        PatternActionsPhase2 = new List<Action>();
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
        // phase 1
        if(PatternSpacingPhase1.Count > ShootNamePhase1.Count)
        {
            int j = PatternSpacingPhase1.Count - ShootNamePhase1.Count;
            for (int i = j;  i > 0; i--)
            {
                PatternSpacingPhase1.RemoveAt(PatternSpacingPhase1.Count - 1);
            }
        }
        if(PatternActionsPhase1.Count < ShootNamePhase1.Count)
        {
            int j = ShootNamePhase1.Count - PatternSpacingPhase1.Count;
            for (int i = j; i > 0; i--)
            {
                PatternSpacingPhase1.Add(2);
            }
        }
        // phase 2
        if(PatternSpacingPhase2.Count > ShootNamePhase2.Count)
        {
            int j = PatternSpacingPhase2.Count - ShootNamePhase2.Count;
            for (int i = j;  i > 0; i--)
            {
                PatternSpacingPhase2.RemoveAt(PatternSpacingPhase2.Count - 1);
            }
        }
        if(PatternActionsPhase2.Count < ShootNamePhase2.Count)
        {
            int j = ShootNamePhase2.Count - PatternSpacingPhase2.Count;
            for (int i = j; i > 0; i--)
            {
                PatternSpacingPhase2.Add(2);
            }
        }


        // Implémentation du pattern
        foreach (var name in ShootNamePhase1)
        {
            switch (name)
            {
                case TypeShotEnum.Zone:
                    PatternActionsPhase1.Add(() =>
                    {
                        ShootingPattern.SharedInstance.StartCoroutine(ShootingPattern.SharedInstance.Zone(50 + dead, shotSpeed + dead, transform.gameObject));
                    });
                    break;

                case TypeShotEnum.Spiral:
                    PatternActionsPhase1.Add(() =>
                    {
                        ShootingPattern.SharedInstance.StartCoroutine(ShootingPattern.SharedInstance.Spiral(50 + dead, shotSpeed + dead, transform.gameObject));
                    });
                    break;

                case TypeShotEnum.RapidFire:
                    PatternActionsPhase1.Add(() =>
                    {
                        ShootingPattern.SharedInstance.StartCoroutine(ShootingPattern.SharedInstance.RapidFire(50 + dead, shotSpeed + dead, 90, transform.gameObject));
                    });
                    break;

                case TypeShotEnum.Canon:
                    PatternActionsPhase1.Add(() =>
                    {
                        ShootingPattern.SharedInstance.StartCoroutine(ShootingPattern.SharedInstance.Canon(transform.gameObject));
                    });
                    break;

                case TypeShotEnum.TPzone1:
                    PatternActionsPhase1.Add(() =>
                    {
                        Zone1TP(true);
                    });
                    break;

                case TypeShotEnum.Zone1:
                    PatternActionsPhase1.Add(() =>
                    {
                        Zone1TP(false);
                    });
                    break;

                case TypeShotEnum.TPzone2:
                    PatternActionsPhase1.Add(() =>
                    {
                        Zone2TP(true);
                    });
                    break;

                case TypeShotEnum.Zone2:
                    PatternActionsPhase1.Add(() =>
                    {
                        Zone2TP(false);
                    });
                    break;

                case TypeShotEnum.TPzone3:
                    PatternActionsPhase1.Add(() =>
                    {
                        Zone2TP(true);
                    });
                    break;

                case TypeShotEnum.Zone3:
                    PatternActionsPhase1.Add(() =>
                    {
                        Zone3TP(false);
                    });
                    break;

                default:
                    PatternActionsPhase1.Add(() =>
                    {
                        ShootingPattern.SharedInstance.StartCoroutine(ShootingPattern.SharedInstance.Zone(50 + dead, shotSpeed + dead, transform.gameObject));
                    });
                    break;
            }
        }

        foreach (var name in ShootNamePhase2)
        {
            switch (name)
            {
                case TypeShotEnum.Zone:
                    PatternActionsPhase2.Add(() =>
                    {
                        ShootingPattern.SharedInstance.StartCoroutine(ShootingPattern.SharedInstance.Zone(50 + dead, shotSpeed + dead, transform.gameObject));
                    });
                    break;

                case TypeShotEnum.Spiral:
                    PatternActionsPhase2.Add(() =>
                    {
                        ShootingPattern.SharedInstance.StartCoroutine(ShootingPattern.SharedInstance.Spiral(50 + dead, shotSpeed + dead, transform.gameObject));
                    });
                    break;

                case TypeShotEnum.RapidFire:
                    PatternActionsPhase2.Add(() =>
                    {
                        ShootingPattern.SharedInstance.StartCoroutine(ShootingPattern.SharedInstance.RapidFire(10 + dead, shotSpeed + dead, 90, transform.gameObject));
                    });
                    break;

                case TypeShotEnum.Canon:
                    PatternActionsPhase2.Add(() =>
                    {
                        ShootingPattern.SharedInstance.StartCoroutine(ShootingPattern.SharedInstance.Canon(transform.gameObject));
                    });
                    break;

                case TypeShotEnum.TPzone1:
                    PatternActionsPhase2.Add(() =>
                    {
                        Zone1TP(true);
                    });
                    break;

                case TypeShotEnum.Zone1:
                    PatternActionsPhase2.Add(() =>
                    {
                        Zone1TP(false);
                    });
                    break;

                case TypeShotEnum.TPzone2:
                    PatternActionsPhase2.Add(() =>
                    {
                        Zone2TP(true);
                    });
                    break;

                case TypeShotEnum.Zone2:
                    PatternActionsPhase2.Add(() =>
                    {
                        Zone2TP(false);
                    });
                    break;

                case TypeShotEnum.TPzone3:
                    PatternActionsPhase2.Add(() =>
                    {
                        Zone2TP(true);
                    });
                    break;

                case TypeShotEnum.Zone3:
                    PatternActionsPhase2.Add(() =>
                    {
                        Zone3TP(false);
                    });
                    break;

                default:
                    PatternActionsPhase2.Add(() =>
                    {
                        ShootingPattern.SharedInstance.StartCoroutine(ShootingPattern.SharedInstance.Zone(50 + dead, shotSpeed + dead, transform.gameObject));
                    });
                    break;
            }
        }


        PlayPattern(0);
    }

    // Update is called once per frame
    void Update()
    {
        if (!phase2 && PV <= 1500) 
        {
            phase2 = true;
            StopAllCoroutines();
            ShootingPattern.SharedInstance.Stop();
            phase = 2; moveSpeed = 3.0f; shotSpeed = 6.0f;
            player.GetComponent<PlayerMove>().moveSpeed *= 1.5f;
            PlayPattern(0);
        }
        if (PV <= 0)
        {
            ShootingPattern.SharedInstance.Stop();
            gameObject.SetActive(false);
            phase = 1; moveSpeed = 2; PV = startPV;
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
        else { StartCoroutine(Move(start, go, moveSpeed)); } // Déplacement dans la zone
    }

    private void Zone2TP(bool TP)
    {
        float pos_y = Random.Range(borders2[1], borders2[0]);
        float pos_x = Random.Range(borders2[2], borders2[3]);

        start = transform.position;
        go = new Vector3(pos_x, pos_y, transform.position.z);

        if (TP) { transform.position = new Vector2(pos_x, pos_y); } // teleportation dans la zone
        else { StartCoroutine(Move(start, go, moveSpeed)); } // Déplacement dans la zone
    }

    private void Zone3TP(bool TP)
    {
        float pos_y = Random.Range(borders3[1], borders3[0]);
        float pos_x = Random.Range(borders3[2], borders3[3]);

        start = transform.position;
        go = new Vector3(pos_x, pos_y, transform.position.z);

        if (TP) { transform.position = new Vector2(pos_x, pos_y); } // teleportation dans la zone
        else { StartCoroutine(Move(start, go, moveSpeed)); } // Déplacement dans la zone
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
        if (Index >= PatternSpacingPhase1.Count || Index < 0)
        {
            if(phase == 2)
            {
                Index = 4;
            }
            else Index = 1;
        }

        _phase = Index;
        StartCoroutine(PatternTempo());
    }

    private void EndPattern()
    {
        PlayPattern(_phase + 1);
    }

    IEnumerator PatternTempo()
    {
        if (phase == 2)
        {
            yield return new WaitForSeconds(PatternSpacingPhase2[_phase]);
            Action action = PatternActionsPhase2[_phase];
            action.Invoke();
        }
        if (phase == 1)
        {
            yield return new WaitForSeconds(PatternSpacingPhase1[_phase]);
            Action action = PatternActionsPhase1[_phase];
            action.Invoke();
        }
        
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

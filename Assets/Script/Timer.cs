
using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timer_Txt;
    public TextMeshProUGUI finalTime;
    [SerializeField] private float sectionCurrentTime;

    private bool isTimerGoing;
    private string timePlaying_Str;
    private TimeSpan timePlaying;

    private Coroutine coUpdateTimer;

    void Start()
    {
        isTimerGoing = false;
        timer_Txt.text = "00:00.0";
        finalTime.text = "00:00.0";
        BeginTimer();
    }


    void BeginTimer()
    {
        isTimerGoing = false;
        if (coUpdateTimer != null)
        {
            StopCoroutine(coUpdateTimer);
        }

        sectionCurrentTime = 0f;
        isTimerGoing = true;
        coUpdateTimer = StartCoroutine(UpdateTimer());
    }

    private IEnumerator UpdateTimer()
    {
        while (isTimerGoing)
        {
            sectionCurrentTime += Time.deltaTime;

            timePlaying = TimeSpan.FromSeconds(sectionCurrentTime);
            timePlaying_Str = timePlaying.ToString("mm':'ss'.'f");
            timer_Txt.text = ("Time : " + timePlaying_Str);
            finalTime.text = ("You survived : " + timePlaying_Str + " min");

            yield return null;
        }
    }

}

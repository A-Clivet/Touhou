using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SpecialPrint : MonoBehaviour
{
    public static SpecialPrint sharedInstance;

    TextMeshProUGUI reload;
    [SerializeField] TextMeshProUGUI charge;

    float sectionCurrentTime;
    private string timePlaying_Str;
    private float timePlaying;

    private Coroutine coUpdateTimer;

    private void Awake()
    {
        sharedInstance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        reload = GetComponent<TextMeshProUGUI>();
        reload.text = "spell reloded";
        charge.text = "special charge : 0%";
    }

    public void BeginTimer()
    {
        if (coUpdateTimer != null)
        {
            StopCoroutine(coUpdateTimer);
        }

        sectionCurrentTime = 0f;
        coUpdateTimer = StartCoroutine(PrintCharge());
    }

    private IEnumerator PrintCharge()
    {
        while (sectionCurrentTime < 5f)
        {
            sectionCurrentTime += Time.deltaTime;

            timePlaying = sectionCurrentTime * 20;
            if (timePlaying > 100.0f) { timePlaying = 100.0f; }

            timePlaying_Str = timePlaying.ToString("###");
            charge.text = ("special charge : " + timePlaying_Str + "%");

            yield return null;
        }
        charge.text = "Spell Reload";
        StartCoroutine(PrintReload());

    }

    private IEnumerator PrintReload()
    {
        sectionCurrentTime = 0f;
        while (sectionCurrentTime < 10f)
        {
            sectionCurrentTime += Time.deltaTime;

            timePlaying = sectionCurrentTime * 10;
            if (timePlaying > 100.0f) { timePlaying = 100.0f; }

            timePlaying_Str = timePlaying.ToString("###.#");
            reload.text = ("special reload : " + timePlaying_Str + "%");

            yield return null;
        }
        charge.text = "special charge : 0%";
        reload.text = "spell reloded";
    }

}

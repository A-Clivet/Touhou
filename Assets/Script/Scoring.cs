using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Scoring : MonoBehaviour
{
    public static Scoring sharedInstance;

    [SerializeField] TextMeshProUGUI finalScore;
    TextMeshProUGUI score_Txt;

    [NonSerialized] public int score = 0;

    private void Awake()
    {
        sharedInstance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        score_Txt = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        finalScore.text = "Your score : \n" + score + " pts";
        score_Txt.text = "Score : " + score;
    }
}

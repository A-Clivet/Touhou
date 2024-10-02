using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Life : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI life;
    [SerializeField] GameObject player;

    public static Life test;
    // Start is called before the first frame update
    void Start()
    {
        test = this;
        life = GetComponent<TextMeshProUGUI>();
    }

    public void LifeUI(int _life)
    {
        if ( player != null && _life > 0)
        {
            life.text = "Life : " + _life;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Life : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI life;
    [SerializeField] GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        life = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        if ( player != null && player.GetComponent<PlayerMove>().life > 0)
        {
           life.text = "Life : " + player.GetComponent<PlayerMove>().life;
        }
    }
}

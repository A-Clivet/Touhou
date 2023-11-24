using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Void : MonoBehaviour
{
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("EnemyShot")) // desactive la balle si elle sort de l'ecran
        {
            collision.transform.gameObject.SetActive(false);
        }
        if (collision.CompareTag("PlayerShot")) // desactive la balle si elle sort de l'ecran
        {
            collision.transform.gameObject.SetActive(false);
        }
    }
}

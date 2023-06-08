using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemCollector : MonoBehaviour
{
    private int _cherries = 0;
    
    [SerializeField] private AudioSource collectItemSorceEffect;

    [SerializeField] private Text cherriesText;
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Cherry"))
        {
            collectItemSorceEffect.Play();
            Destroy(col.gameObject);
            _cherries++;
            cherriesText.text = "Cherries: " + _cherries;
        }
    }
}

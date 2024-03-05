using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class Showtext : MonoBehaviour
{
    
    public GameObject[] text;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            for (int i = 0; i < text.Length; i++)
            {
                text[i].SetActive(true);
            }
        }
    }

    private void OnCollisionExit(Collision other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            for (int i = 0; i < text.Length; i++)
            {
                text[i].SetActive(false);
            }
        }
    }
}

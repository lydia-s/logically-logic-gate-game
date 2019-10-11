﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridComponent : MonoBehaviour
{
    public bool outputType;
    public Sprite sprite_1, sprite_0;
    // Start is called before the first frame update
    void Start()
    {

        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

   

    public void ChangeOutputType(bool newOutput) {
        outputType = newOutput;
        if (newOutput == true)
        {
            this.gameObject.GetComponent<SpriteRenderer>().sprite = sprite_1;
        }
        else
        {
            this.gameObject.GetComponent<SpriteRenderer>().sprite = sprite_0;
        }
    }
}
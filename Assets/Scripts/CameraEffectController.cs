﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraEffectController : MonoBehaviour
{
    [HideInInspector]
    public Vector3 position;
    [HideInInspector]
    public Vector3 rotation;
    [HideInInspector]
    public CameraEffect effect;
    float timer;
    public float zoom;

    public void Process()
    {
        if (effect!=null)
        {
            if (effect.timeLimit > timer)
            {
                effect.Apply(this);
            }
           
        }
        timer +=Time.deltaTime;
    }
    public void Reset()
    {
        position = Vector3.zero;
        rotation = Vector3.zero;
        timer = 0f;
        zoom = 0f;
    }
}

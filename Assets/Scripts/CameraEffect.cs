using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CameraEffect :ScriptableObject
{
    public float timeLimit=20f;
    public abstract void Apply(CameraEffectController effectController);
}

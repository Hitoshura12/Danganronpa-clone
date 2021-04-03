using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class SpriteChange
{
    public Actor actor;
    public int expression;
    public int onScreenImageID;
}
[Serializable]
public class BackgroundChange
{
    public Sprite sprite;
    public int backgroundImageID;
}
[Serializable]
public class DialogueLine
{
    public Actor actor;
    public string line;
    public List<SpriteChange> spriteChanges;
    public List<BackgroundChange> backgroundChanges;
    
}
[CreateAssetMenu(menuName="Dialogue/DialogueContainer")]
public class DialogueContainer : ScriptableObject
{
    public List<DialogueLine> lines;
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TextLine
{
    public string text;
    public List<TextEffect> textEffect;
    public Vector3 spawnOffset;
    public Vector3 scale = Vector3.one;
    public float ttl;
    public TextLine()
    {
        text = "";
        textEffect = new List<TextEffect>();
        spawnOffset = Vector3.zero;
        ttl = 1f;
    }
}

[System.Serializable]
public class DialogueNode 
{
    public Rect nodeRect;
    public string title;

    public DrawNode drawNode;

    public string text;
    public Character character;
    public CameraEffect cameraEffect;
    public List<TextLine> textLines;
    public AudioClip voiceLine;
    public Evidence evidence;
    public string statement;
    public Color statementColor;
    public CharacterState expression;
    public DialogueNode( DrawNode _drawNode)
    {
        drawNode = _drawNode;
        textLines = new List<TextLine>();

    }

    public void DrawNode()
    {
        if (drawNode != null)
        {
            drawNode.DrawWindow(this);
        }
    }
}

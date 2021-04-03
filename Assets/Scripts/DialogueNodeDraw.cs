using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

[CreateAssetMenu(menuName = "Behaviour Editor/Draw/Dialogue Node Draw")]
public class DialogueNodeDraw : DrawNode
{
    public override void DrawWindow(DialogueNode b)
    {
        b.nodeRect.height = 180;
        b.nodeRect.width = 200;
        //b.text = GUILayout.TextField(b.text);
        b.cameraEffect = (CameraEffect)EditorGUILayout.ObjectField(b.cameraEffect, typeof(CameraEffect), false);
        b.character = (Character)EditorGUILayout.ObjectField(b.character, typeof(Character), false);
        b.voiceLine = (AudioClip)EditorGUILayout.ObjectField(b.voiceLine, typeof(AudioClip), false);
        b.evidence = (Evidence)EditorGUILayout.ObjectField(b.evidence, typeof(Evidence), false);
        b.statement = EditorGUILayout.TextField(b.statement);
        b.statementColor = EditorGUILayout.ColorField(b.statementColor);
        b.expression = (CharacterState) EditorGUILayout.EnumPopup(b.expression);

        if (b.textLines == null)
        {
            return;
        }
        for (int i = 0; i < b.textLines.Count; i++) // change i to 0
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.Separator();
            GUILayout.Label("Line#" + i.ToString());
            if (GUILayout.Button("X", GUILayout.Width(20)))
            {
                b.textLines.RemoveAt(i);
                continue;
            }
            EditorGUILayout.EndHorizontal();
            b.textLines[i].text = EditorGUILayout.TextField(b.textLines[i].text);
            b.textLines[i].spawnOffset = EditorGUILayout.Vector3Field("Spawn Offset", b.textLines[i].spawnOffset);
            b.textLines[i].ttl = EditorGUILayout.FloatField("Time", b.textLines[i].ttl);
            b.textLines[i].scale = EditorGUILayout.Vector3Field("Scale", b.textLines[i].scale);
            b.nodeRect.height += 145;
            EditorGUILayout.Separator();
            b.nodeRect.height += 16;
            ShowTextEffect(ref b.textLines[i].textEffect, ref b);
        }

    }


    private void ShowTextEffect(ref List<TextEffect> textEffect, ref DialogueNode b)
    {
        for (int i = 0; i < textEffect.Count; i++)
        {
            EditorGUILayout.BeginHorizontal();
            textEffect[i] = (TextEffect)EditorGUILayout.ObjectField(textEffect[i], typeof(TextEffect), false);
            if (GUILayout.Button("X", GUILayout.Height(20)))
            {
                textEffect.RemoveAt(i);
                continue;
            }
            b.nodeRect.height += 20;
            EditorGUILayout.EndHorizontal();
        }

        if (GUILayout.Button("Add Text Effect"))
        {
            textEffect.Add(null);
        }
        b.nodeRect.height += 20;

    }
}

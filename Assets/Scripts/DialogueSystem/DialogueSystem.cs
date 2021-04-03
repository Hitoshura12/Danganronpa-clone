using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class DialogueSystem : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI text;
    [SerializeField] TextMeshProUGUI nameTag;
    DialogueContainer currentDialogue;
    [SerializeField] SpriteManager spriteManager;
    [SerializeField] SpriteManager backgroundManager;

    [SerializeField]
    [Range(0,1)]
    float visibleTextPercent;
    [SerializeField]
    float timePerLetter = 0.05f;
    float currentTime, totalTimeToType;
    [SerializeField] float skipTextWaitTime = 0.1f;
    Coroutine skipTextCoroutine;

    string lineToShow;
    int index;

    [SerializeField] DialogueContainer debugDialogueContainer;

    private void Start()
    {
        if (debugDialogueContainer!=null)
        {
            InitiateDialogue(debugDialogueContainer);
        }
       // CycleLine();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            PushText();
        }
      
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            skipTextCoroutine = StartCoroutine(SkipText());
        }
        if (Input.GetKeyUp(KeyCode.LeftControl)) 
        {
            if (skipTextCoroutine!=null)
            {
                StopCoroutine(skipTextCoroutine);
            }
            
        }
        TypeOutText();
    }

    private void TypeOutText()
    {
        if (visibleTextPercent >=1f)
        {
            return;
        }
        currentTime += Time.deltaTime;
        visibleTextPercent = currentTime / totalTimeToType;
        visibleTextPercent = Mathf.Clamp(visibleTextPercent, 0f, 1f);
        UpdateText();
        
    }
    void UpdateText()
    {
        int totalLetterToShow = (int)(lineToShow.Length * visibleTextPercent);
        text.text = lineToShow.Substring(0, totalLetterToShow);
    }
    void InitiateDialogue(DialogueContainer dialogueContainer)
    {
        currentDialogue = dialogueContainer;
        index = 0;
        CycleLine();
    }
    void CycleLine()
    {
        totalTimeToType = lineToShow.Length * timePerLetter;
        currentTime = 0f;
        visibleTextPercent = 0f;

        text.text = "";
        index += 1;

        if (index>= currentDialogue.lines.Count)
        {
            Debug.Log("There are no more lines to show!");
            return;
        }
        DialogueLine line = currentDialogue.lines[index];
        lineToShow = line.line;
        // ACTOR SPRITES
        if (line.spriteChanges!=null)
        {
            for (int i = 0; i < line.spriteChanges.Count; i++)
            {
                if (line.spriteChanges[i].actor==null)
                {
                    spriteManager.Hide(line.spriteChanges[i].onScreenImageID);
                    continue;
                }
                int expressionIndex = line.spriteChanges[i].expression;
                spriteManager.SetSprite(
                    line.spriteChanges[i].actor.sprites[expressionIndex],
                    line.spriteChanges[i].onScreenImageID);
            }
        }

        // BACKGROUND SPRITES
        if (line.backgroundChanges != null)
        {
            for (int i = 0; i < line.backgroundChanges.Count; i++)
            {
                if (line.backgroundChanges[i].sprite == null)
                {
                    backgroundManager.Hide(line.backgroundChanges[i].backgroundImageID);
                    continue;
                }
              
                backgroundManager.SetSprite(
                    line.backgroundChanges[i].sprite,
                    line.backgroundChanges[i].backgroundImageID);
            }
        }

        if (line.actor!=null)
        {
            nameTag.text = line.actor.Name;
        }

     
    }
    private void PushText()
    {
        if (visibleTextPercent <1)
        {
            visibleTextPercent = 1;
            UpdateText();
            return;
        }
        CycleLine();
    }
    IEnumerator SkipText()
    {
        yield return null;
        while (true)
        {
            PushText();
            yield return new WaitForSeconds(skipTextWaitTime);

        }
    }
}

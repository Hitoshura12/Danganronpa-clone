using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameLoop : MonoBehaviour
{
    public class TextLine
    {
        public GameObject textGo;
        public RectTransform textRT;
        public List<TextEffect> textEffect;
        public TextMeshPro textMeshPro;
        public float timeToLive;

        public TextLine(GameObject textGo, RectTransform textRT, List<TextEffect> textEffect,
            float timeToLive, TextMeshPro tmp)
        {
            this.textMeshPro = tmp;
            this.textGo = textGo;
            this.textRT = textRT;
            this.textEffect = textEffect;
            this.timeToLive = timeToLive;
        }

        internal void Apply()
        {
            for (int i = 0; i < textEffect.Count; i++)
            {
                textEffect[i].Apply(textRT);
            }
        }
    }

    [SerializeField] Stage stage;
    [SerializeField] CameraController cameraController;
    [SerializeField] List<CharacterStand> characterStands;
    [SerializeField] Transform textPivot;
    [SerializeField] GameObject textPrefab;
    [SerializeField] CameraEffectController effectController;
    [SerializeField] AudioManager audioManager;
    [SerializeField] EvidenceManager evidenceManager;
    [SerializeField] MusicManager musicManager;
    [SerializeField] TMP_Text timerText;

    float timer;
    float stageTimer;
    float defaultStageTime = 600f;
    int textIndex;
    public List<TextLine> textLines;
    CharacterStand characterStand;
    Evidence correctEvidence;
    int correctTMPIndex, correctCharacterIndexBegin, correctCharacterIndexEnd;
    bool pause;
    bool finished;

    private void Start()
    {
        stageTimer = defaultStageTime;
        textLines = new List<TextLine>();
        evidenceManager.ShowEvidence(stage.evidences);
        musicManager.Play(stage.audioClip);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SetPause(!pause);
        }
        if (pause == true|| finished== true)
        {
            return;
        }
        if (stage.dialogueNodes.Count <= textIndex) { textIndex = 0; }

        timer += Time.deltaTime;
        stageTimer -= Time.deltaTime;
        TimeSpan timeSpan = TimeSpan.FromSeconds(stageTimer);
        timerText.text = timeSpan.ToString(@"mm\:ss\.fff");

        if (stageTimer<0f)
        {
            GameOver();
        }


        if (textLines.Count == 0)
        {
            SpawnText(textIndex);
            PlayVoiceClip(textIndex);
            timer = 0;
            textIndex++;
        }
        int index = 0;
        while (textLines.Count > index)
        {
            if (timer > textLines[index].timeToLive)
            {
                Destroy(textLines[index].textGo);
                textLines.RemoveAt(index);
                index--;
            }
            index++;
        }
        if (textLines.Count > 0)
        {
            for (int i = 0; i < textLines.Count; i++)
            {
                textLines[i].Apply();
            }
        }
        effectController.Process();
        HandleMouseControl();
    }

    private void GameOver()
    {
        finished = true;
    }

    void HandleMouseControl()
    {
        if (Input.GetMouseButtonDown(0))
        {
            for (int i = 0; i < textLines.Count; i++)
            {
                if (correctTMPIndex!=i)
                {
                    continue;
                }
                if (correctTMPIndex==i)
                {
                    int index = TMP_TextUtilities.FindIntersectingCharacter(
                                      textLines[i].textMeshPro, Input.mousePosition, Camera.main, true);
                    if (index != -1)
                    {
                        if (index >= correctCharacterIndexBegin && index < correctCharacterIndexEnd)
                        {
                            Hit();
                        }
                    }
                }
              

            }

        }
       
    }
    void Hit()
    {
        if (evidenceManager.Check(correctEvidence))
        {
            CorrectChoice();
        }
    }

    private void CorrectChoice()
    {
        finished = true;
        Debug.Log("You clicked the correct statement! at" + Time.time);
    }

    void SetPause(bool _pause)
    {
        pause = _pause;
        if (pause==true)
        {
            audioManager.Pause();
        }
        else
        {
            audioManager.UnPause();
        }
    }

    private void PlayVoiceClip(int textIndex)
    {
        if (stage.dialogueNodes[textIndex].voiceLine != null)
        {
            audioManager.Play(stage.dialogueNodes[textIndex].voiceLine);
        }

    }

    void SpawnText(int dialogueNodeIndex)
    {
        correctTMPIndex = -1;
        correctCharacterIndexBegin = -1;
        correctCharacterIndexEnd = -1;
        DialogueNode nextDialogueNode = stage.dialogueNodes[dialogueNodeIndex];
        correctEvidence = nextDialogueNode.evidence;
        if (nextDialogueNode.character != null)

        {
            characterStand = characterStands.Find(
            stand => stand.character == nextDialogueNode.character);
            cameraController.target = characterStand.spriteRenderer.transform;
            textPivot = characterStand.textPivot;
        }
        if (characterStand!=null)
        {
            characterStand.state = stage.dialogueNodes[dialogueNodeIndex].expression;
            characterStand.SetSprite();
        }
        for (int i = 0; i < nextDialogueNode.textLines.Count; i++)
        {
            GameObject go = Instantiate(textPrefab);
            go.transform.position = textPivot.position;
            go.transform.position += nextDialogueNode.textLines[i].spawnOffset;
            go.transform.localScale = nextDialogueNode.textLines[i].scale;
            go.transform.rotation = textPivot.rotation;

            TextMeshPro tmp = go.GetComponent<TextMeshPro>();
            string str = nextDialogueNode.textLines[i].text;
            int indexOf = str.IndexOf("{0}");
            if (indexOf != -1)
            {
                correctTMPIndex = i;
                correctCharacterIndexBegin = indexOf;
                correctCharacterIndexEnd = indexOf + nextDialogueNode.statement.Length;
                str = string.Format(nextDialogueNode.textLines[i].text,
                   "<color=#" 
                   + ColorUtility.ToHtmlStringRGBA(nextDialogueNode.statementColor) + ">"
                   + nextDialogueNode.statement + "</color>") ;
               
            }


            tmp.text = str;
            TextLine textLine = new TextLine(
                go,
                go.GetComponent<RectTransform>(),
                nextDialogueNode.textLines[i].textEffect,
                nextDialogueNode.textLines[i].ttl,
                go.GetComponent<TextMeshPro>()
                );
            textLines.Add(textLine);

        }
        effectController.effect = nextDialogueNode.cameraEffect;
        effectController.Reset();

    }
}

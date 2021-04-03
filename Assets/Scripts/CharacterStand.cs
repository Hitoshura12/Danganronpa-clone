using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStand : MonoBehaviour
{
    public Character character;
    public CharacterState state;
    public SpriteRenderer spriteRenderer;
    public Transform textPivot;
       

    private void Start()
    {
        spriteRenderer.transform.localScale = character.scale;
        SetSprite();
    }
    private void Update()
    {
        SetSprite();
    }

    public void SetSprite()
    {
        int stateIndex = (int)state;
        if (character.sprites.Count < stateIndex)
        {
            spriteRenderer.sprite = character.sprites[0];
            return;
        }
        if (character.sprites[stateIndex]==null)
        {
            spriteRenderer.sprite = character.sprites[0];
            return;
        }
        spriteRenderer.sprite = character.sprites[(int)state];
    }
}

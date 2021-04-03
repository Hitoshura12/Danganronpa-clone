using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CharacterState
{
    Idle = 0,
    Surprised=1,
    Angry=2,
    Deduction=3,
    Blush=4
}
[CreateAssetMenu(menuName ="Data/Character")]
public class Character : ScriptableObject
{
    public string characterName;
    public List<Sprite> sprites;
    public Vector3 scale;

  
}

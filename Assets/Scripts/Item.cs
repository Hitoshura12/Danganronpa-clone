using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Data/Item")]
public class Item : ScriptableObject
{
    public string itemName;
    public Sprite picture;
    [TextArea]
    public string description;
}

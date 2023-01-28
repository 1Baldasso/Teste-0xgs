using System;
using UnityEngine;

[CreateAssetMenu(fileName = "CardObject",menuName ="Card", order = 0)]
public class Card : ScriptableObject
{
    public String Name;
    public Int16 Attack;
    public Int16 Defense;
    public Texture2D Image;
}
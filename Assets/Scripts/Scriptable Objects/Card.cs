using System;
using UnityEngine;

//Scriptable Objects are Easier to handle when creating new cards, specially when there is no effect trigger associated
//Also, its possible to add enums in order to start handling effects
[CreateAssetMenu(fileName = "CardObject",menuName = "Card", order = 0)]
public class Card : ScriptableObject
{
    public String Name;
    public Int16 Attack;
    public Int16 Defense;
    public Texture2D Image;
    
}
using System;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CardScript : MonoBehaviour
{
    public Card Card;
    [HideInInspector]public short Attack;
    [HideInInspector]public short Defense;

    public GameObject KillCard()
    {
        this.Defense = 0;
        this.Card = null;
        return this.gameObject;
    }

    public void CreateCard()
    {
        this.Attack = Card.Attack;
        this.Defense = Card.Defense;
        gameObject.GetComponent<Canvas>().worldCamera = Camera.main;
        gameObject.GetComponentsInChildren<RawImage>()
            .FirstOrDefault(x=>x.gameObject.CompareTag("CardImage")).texture = Card.Image;
        gameObject.GetComponentsInChildren<TextMeshProUGUI>()
            .FirstOrDefault(x => x.gameObject.CompareTag("Attack")).text = this.Attack.ToString();
        gameObject.GetComponentsInChildren<TextMeshProUGUI>()
            .FirstOrDefault(x => x.gameObject.CompareTag("Defense")).text = this.Defense.ToString();
    }

    private void OnMouseDown()
    {
        
    }
}

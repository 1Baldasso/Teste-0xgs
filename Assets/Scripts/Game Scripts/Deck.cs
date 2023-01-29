using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Deck : MonoBehaviour
{
    public List<Card> PlayerDeck;
    public List<Card> EnemyDeck;
    public static Deck Instance;

    private void Awake()
    {
        Instance = this;
        Shuffle(PlayerDeck);
        Shuffle(EnemyDeck);
    }

    //Passing deck by reference makes changes directly to the deck
    private void Shuffle(List<Card> deck)
    {
        System.Security.Cryptography.RNGCryptoServiceProvider provider = new System.Security.Cryptography.RNGCryptoServiceProvider();
        int n = deck.Count;
        while (n > 1)
        {
            byte[] box = new byte[1];
            do provider.GetBytes(box);
            while (!(box[0] < n * (Byte.MaxValue / n)));
            int k = (box[0] % n);
            n--;
            Card value = deck[k];
            deck[k] = deck[n];
            deck[n] = value;
        }
    }

    public Card Draw(Managers.PlayerManager.PlayerEnum player)
    {
        Card card = null;
        switch (player)
        {
            case Managers.PlayerManager.PlayerEnum.Player:
                card = PlayerDeck.FirstOrDefault();
                PlayerDeck.Remove(card);
                break;
            case Managers.PlayerManager.PlayerEnum.Enemy:
                card = EnemyDeck.FirstOrDefault();
                EnemyDeck.Remove(card);
                break;
        }
        return card;
    }
}
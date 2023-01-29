using System.Collections;
using UnityEngine;

namespace Managers
{
    public class AnimationManager : MonoBehaviour
    {
        void Start()
        {
            CombatManager.Instance.OnUnitDie += OnUnitDie;
        }

        private void OnUnitDie(GameObject obj)
        {
            CardScript card = obj.GetComponent<CardScript>();
            Debug.LogFormat("Card: {0} died", card.Card.name);
            Debug.LogFormat("Card Health was: {0}", card.Defense);
            GameObject.Destroy(obj);
        }
    }
}
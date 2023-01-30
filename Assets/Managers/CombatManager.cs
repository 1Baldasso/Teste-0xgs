using System;
using UnityEngine;
namespace Managers
{
    public class CombatManager : MonoBehaviour
    {
        /* Draft Combat Style:
         * 
         * Following Squid Game's Hierarchy:
         * 
         * Square - Supervisor
         * Triangle - Soldier
         * Circle - Minion
         * 
         * If you attack a unit higher on hierarchy scale while there is a lower unit on board,
         * combat will be declared first with lower unit and them with the unit you attacked
         *      FoE: If you attack a Triangle while there is two circles on board, combat will be
         * against Circles first, them against Triangle
         * 
         * Also, if you have lower level units on board, they will be sent to combat before its superiors
         *      FoE: If you have one Triangle and three Circles, your Circles will be sent to attack,
         * and if they all die, Triangle will be sent to combat
         * 
         */

        //Following requirement documentation:

        //This event will trigger Animations
        public event Action<GameObject> OnUnitDie;
        public event Action<GameObject, GameObject> OnCombatDeclared;

        //Getting a CardScript as Instances not to change ScriptableObject Properties.
        private CardScript AttackingUnit { get; set; }
        private CardScript DefendingUnit { get; set; }
        public static CombatManager Instance;
        private void Awake()
        {
            Instance = this;
        }

        public void DeclareCombat(CardScript attacking, CardScript defending)
        {
            AttackingUnit= attacking; 
            DefendingUnit= defending;
            OnCombatDeclared?.Invoke(attacking.gameObject,defending.gameObject);
            CombatResolve();
        }

        public void CombatResolve()
        {
            AttackingUnit.Defense -= DefendingUnit.Attack;
            DefendingUnit.Defense -= AttackingUnit.Attack;
            if (DefendingUnit.Defense <= 0)
                OnUnitDie?.Invoke(DefendingUnit.KillCard());
            if (AttackingUnit.Defense <= 0)
                OnUnitDie?.Invoke(AttackingUnit.KillCard());
            AttackingUnit = null;
            DefendingUnit = null;
        }
    }
}
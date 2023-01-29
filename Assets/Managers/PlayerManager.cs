using System;
using UnityEngine;
namespace Managers
{
    public class PlayerManager : MonoBehaviour
    {
        public static PlayerManager Instance;
        public enum PlayerEnum
        {
            Player,
            Enemy
                
        }
        private void Awake()
        {
            Instance = this;
        }
        private void Start()
        {
            Managers.GameManager.Instance.OnTurnStart += OnTurnStart;
            Managers.GameManager.Instance.ChangePlayer += ChangeGameView;
            ActivePlayer = PlayerEnum.Player;
            PlayerHand = GameObject.FindGameObjectWithTag("Player Hand");
            PlayerBoard = GameObject.FindGameObjectWithTag("Player Board");
            EnemyHand = GameObject.FindGameObjectWithTag("Enemy Hand");
            EnemyBoard = GameObject.FindGameObjectWithTag("Enemy Board");
            FieldPositionDown.HandPosition = PlayerHand.transform.position;
            FieldPositionDown.BoardPosition = PlayerBoard.transform.position;
            FieldPositionUp.HandPosition = EnemyHand.transform.position;
            FieldPositionUp.BoardPosition = EnemyBoard.transform.position;
            DrawPlayerCard();
            DrawEnemyCard();
        }
        private void OnDestroy()
        {
            Managers.GameManager.Instance.OnTurnStart -= OnTurnStart;
            Managers.GameManager.Instance.ChangePlayer -= ChangeGameView;
        }

        struct FieldPosition
        {
            public Vector3 HandPosition;
            public Vector3 BoardPosition;
        }
        [SerializeField] private GameObject CardPrefab;
        public PlayerEnum ActivePlayer { get; private set; }
        private FieldPosition FieldPositionDown;
        private FieldPosition FieldPositionUp;
        private GameObject PlayerHand;
        private GameObject EnemyHand;
        private GameObject PlayerBoard;
        private GameObject EnemyBoard;
        public event Action PlayerChanged;

        private void OnTurnStart()
        {
            switch(ActivePlayer)
            {
                case PlayerEnum.Player:
                    DrawPlayerCard();
                    break;
                case PlayerEnum.Enemy:
                    DrawEnemyCard();
                    break;
            }
        }

        private void DrawPlayerCard()
        {
            var playerCardPrefab = Instantiate(CardPrefab, GameObject.FindGameObjectWithTag("Player Hand").transform);
            var playerCardScript = playerCardPrefab.GetComponent<CardScript>();
            playerCardScript.Card = Deck.Instance.Draw(PlayerEnum.Player);
            playerCardScript.CreateCard();
        }
        private void DrawEnemyCard()
        {
            var enemyCardPrefab = Instantiate(CardPrefab, GameObject.FindGameObjectWithTag("Enemy Hand").transform);
            var enemyCardScript = enemyCardPrefab.GetComponent<CardScript>();
            enemyCardScript.Card = Deck.Instance.Draw(PlayerEnum.Enemy);
            enemyCardScript.CreateCard();
        }

        public void ChangeGameView()
        {
            InvertPositions(ActivePlayer);
        }

        private void InvertPositions(PlayerEnum playerEnum)
        {
            switch(playerEnum)
            {
                case PlayerEnum.Enemy:
                    PlayerHand.transform.SetPositionAndRotation(FieldPositionDown.HandPosition, Quaternion.identity);
                    PlayerBoard.transform.SetPositionAndRotation(FieldPositionDown.BoardPosition, Quaternion.identity);
                    EnemyHand.transform.SetPositionAndRotation(FieldPositionUp.HandPosition, Quaternion.identity);
                    EnemyBoard.transform.SetPositionAndRotation(FieldPositionUp.BoardPosition, Quaternion.identity);
                    ActivePlayer = PlayerEnum.Player;

                    break;
                case PlayerEnum.Player:
                    PlayerHand.transform.SetPositionAndRotation(FieldPositionUp.HandPosition, Quaternion.identity);
                    PlayerBoard.transform.SetPositionAndRotation(FieldPositionUp.BoardPosition, Quaternion.identity);
                    EnemyHand.transform.SetPositionAndRotation(FieldPositionDown.HandPosition, Quaternion.identity);
                    EnemyBoard.transform.SetPositionAndRotation(FieldPositionDown.BoardPosition, Quaternion.identity);
                    ActivePlayer = PlayerEnum.Enemy;
                    break;
            }
            PlayerChanged?.Invoke();
        }
    }
}

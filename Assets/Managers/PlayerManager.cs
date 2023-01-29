using UnityEngine;
namespace Managers
{
    public class PlayerManager : MonoBehaviour
    {
        public enum PlayerEnum
        {
            Player,
            Enemy
        }

        private void Start()
        {
            Managers.GameManager.Instance.OnTurnStart += OnTurnStart;
            Managers.GameManager.Instance.ChangePlayer += ChangeGameView;
        }
        private void OnDestroy()
        {
            Managers.GameManager.Instance.OnTurnStart -= OnTurnStart;
        }

        struct FieldPosition
        {
            public Vector3 HandPosition;
            public Vector3 BoardPosition;
        }
        [SerializeField] private GameObject CardPrefab;
        private PlayerEnum ActivePlayer;
        private FieldPosition FieldPositionDown;
        private FieldPosition FieldPositionUp;
        private GameObject PlayerHand;
        private GameObject EnemyHand;
        private GameObject PlayerBoard;
        private GameObject EnemyBoard;

        private void OnTurnStart()
        {
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


        private void ChangeGameView()
        {
            if(ActivePlayer == PlayerEnum.Player)
            {
                PlayerHand.transform.SetPositionAndRotation(FieldPositionUp.HandPosition,Quaternion.identity);
                EnemyHand.transform.SetPositionAndRotation(FieldPositionDown.HandPosition,Quaternion.identity);
            }
            if(ActivePlayer == PlayerEnum.Enemy)
            {
                PlayerHand.transform.SetPositionAndRotation(FieldPositionDown.HandPosition,Quaternion.identity);
                EnemyHand.transform.SetPositionAndRotation(FieldPositionUp.HandPosition,Quaternion.identity);
            }
        }
    }
}

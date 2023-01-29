using System;
using System.Collections;
using UnityEngine;

namespace Managers
{
    public class GameManager : MonoBehaviour
    {
        public event Action OnTurnStart;
        public event Action ChangePlayer;
        public static GameManager Instance;
        private void Awake()
        {
            Instance = this;
        }
        public void Start()
        {
            OnTurnStart?.Invoke();
        }
    }
}
using System;
using System.Collections;
using UnityEngine;

namespace Managers
{
    public class GameManager : MonoBehaviour
    {
        public event Action OnTurnStart;
        public event Action ChangePlayer;
        public bool ActivateControls;
        public static GameManager Instance;
        private void Awake()
        {
            Instance = this;
        }
        public void Start()
        {
            CardScript.OnCardPlay += () => ChangePlayer?.Invoke();
            CardScript.OnCardPlay += () => OnTurnStart?.Invoke();
            ActivateControls = true;
        }
        public void Update()
        {
            if(Input.GetKey(KeyCode.Escape))
            {
                GameObject.FindGameObjectWithTag("Pause Menu").GetComponent<Canvas>().enabled = true;
                ActivateControls = false;
            }
        }
    }
}
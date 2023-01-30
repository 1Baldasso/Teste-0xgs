using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

namespace Managers
{
    public class AnimationManager : MonoBehaviour
    {
        [SerializeField] private AudioSource audioSource;
        [SerializeField] private AudioClip audioClip;
        void Start()
        {
            CombatManager.Instance.OnUnitDie += OnUnitDie;
            //CombatManager.Instance.OnCombatDeclared+= OnCombatDeclared;
        }

        //private void OnCombatDeclared(GameObject card1, GameObject card2)
        //{
        //    card1.gameObject.transform.localScale *= 2.5f;
        //    card2.gameObject.transform.localScale *= 2.5f;
        //}

        private IEnumerator CardMove(GameObject obj, Vector3 position)
        {
            yield return null;
            while(obj.transform.position.y < position.y)
                obj.transform.Translate(Vector3.up * 2 * Time.deltaTime);
        }

        private void OnUnitDie(GameObject obj)
        {
            obj.GetComponent<ParticleSystem>().Play();
            gameObject.GetComponent<AudioSource>().Play();
        }
    }
}
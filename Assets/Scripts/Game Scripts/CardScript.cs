using Managers;
using System;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CardScript : MonoBehaviour
{
    public static event Action OnCardPlay;
    public Card Card;
    [HideInInspector]public short Attack;
    [HideInInspector]public short Defense;
    private CardState cardState;

    private void Start()
    {
        PlayerManager.Instance.PlayerChanged += ChangeCardView;
        ChangeCardView();
    }

    private void OnDestroy()
    {
        PlayerManager.Instance.PlayerChanged -= ChangeCardView;
    }

    private void Update()
    {
        if(cardState == CardState.OnHand)
            GrabCard();
        else
            TryDeclareAttack();
        //Disable mouse controls over cards by disabling its collider
        GetComponent<Collider2D>().enabled = GameManager.Instance.ActivateControls;

    }

    private void ChangeCardView()
    {
        var activePlayer = PlayerManager.Instance.ActivePlayer.ToString();
        gameObject.GetComponentsInChildren<RawImage>().FirstOrDefault(x => x.name == "Card Back").enabled = !gameObject.transform.parent.CompareTag(activePlayer + " Hand") && !gameObject.transform.parent.tag.Contains("Board");

    }

    private GameObject cardObject;
    private GameObject targetCard;
    private void TryDeclareAttack()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if(Input.GetMouseButtonDown(0))
        {
            Collider2D collider = Physics2D.OverlapPointAll(mousePosition).FirstOrDefault(x => x.gameObject == this.gameObject);
            if(collider!=null)
            {
                cardObject = collider.gameObject;
            }
            else
            {
                cardObject = null;
            }
        }
        if(Input.GetMouseButtonUp(0))
        {
            Collider2D targetCardCollider = Physics2D.OverlapPointAll(mousePosition).FirstOrDefault(x=>x.CompareTag("Card Played"));
            if(targetCardCollider != null && targetCardCollider.gameObject != gameObject)
            {
                targetCard = targetCardCollider.gameObject;
            }
            else
            {
                targetCard = null;
                cardObject = null;
            }
        }
        if(cardObject != null && targetCard != null) 
        {
            CombatManager.Instance.DeclareCombat(
                cardObject.GetComponent<CardScript>(),
                targetCard.GetComponent<CardScript>()
                );
            cardObject = null;
            targetCard = null;
        }
    }

    public GameObject KillCard()
    {
        Defense = 0;
        return this.gameObject;
    }

    public void CreateCard()
    {
        Attack = Card.Attack;
        Defense = Card.Defense;
        gameObject.GetComponent<Canvas>().worldCamera = Camera.main;
        gameObject.GetComponentsInChildren<RawImage>()
            .FirstOrDefault(x=>x.gameObject.CompareTag("CardImage")).texture = Card.Image;
        gameObject.GetComponentsInChildren<TextMeshProUGUI>()
            .FirstOrDefault(x => x.gameObject.CompareTag("Attack")).text = this.Attack.ToString();
        gameObject.GetComponentsInChildren<TextMeshProUGUI>()
            .FirstOrDefault(x => x.gameObject.CompareTag("Defense")).text = this.Defense.ToString();
        _objectDefaultTransform = gameObject.transform.parent;
        cardState = CardState.OnHand;
    }


    private bool canScale = true;
    private void OnMouseOver()
    {
        if(canScale)
        {
            gameObject.transform.localScale *= 1.25f;
            canScale = false;
        }
    }

    private void OnMouseExit()
    {
        gameObject.transform.localScale /= 1.25f;
        canScale = true;
    }


    private Vector3 offset;
    private Transform _objectDefaultTransform;
    private GameObject _selectedObject;
    private void GrabCard()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if(Input.GetMouseButtonDown(0))
        {
            Collider2D targetObject = Physics2D.OverlapPoint(mousePosition);
            if(targetObject != null)
            {
                if(targetObject == gameObject.GetComponent<Collider2D>())
                {
                    _selectedObject = targetObject.transform.gameObject;
                    _selectedObject.transform.SetParent(null, false);
                }
            }
        }
        if(_selectedObject != null)
        {
            _selectedObject.transform.position = mousePosition;
            _selectedObject.transform.position += Vector3.forward * 3;

        }
        if(Input.GetMouseButtonUp(0) && _selectedObject != null) 
        {
            Collider2D[] overlapping = Physics2D.OverlapPointAll(mousePosition);
            Transform targetBoard = overlapping.FirstOrDefault(x=>x.gameObject.CompareTag(PlayerManager.Instance.ActivePlayer.ToString() + " Board"))?.transform;
            if(targetBoard != null)
            {
                _selectedObject.transform.SetParent(targetBoard, false);
                _selectedObject = null;
                PlayCard();
            }
            else
            {
                _selectedObject.transform.SetParent(_objectDefaultTransform, false);
                _selectedObject = null;
            }
        }
    }

    private void PlayCard()
    {
        cardState = CardState.OnBoard;
        gameObject.tag = "Card Played";
        OnCardPlay?.Invoke();
    }

    private enum CardState
    {
        OnHand,
        OnBoard
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackgroundProvider : MonoBehaviour
{
    [SerializeField] private Texture2D Background;
    void Start()
    {
        gameObject.GetComponent<Image>().sprite = Sprite.Create(Background, new Rect(0f,0f,Background.width,Background.height), new Vector2(0f,0f));
    }
}

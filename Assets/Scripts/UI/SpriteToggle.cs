using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpriteToggle : MonoBehaviour
{
    [SerializeField] private Sprite sprite;
    private Toggle toggle;
    private Image image;

    // Start is called before the first frame update
    void Start()
    {
        image = GetComponent<Image>();
        toggle = GetComponent<Toggle>();
        toggle.onValueChanged.AddListener((b) => 
        {
            var currSprite = image.sprite;
            image.sprite = sprite;
            sprite = currSprite;
        });
    }
}

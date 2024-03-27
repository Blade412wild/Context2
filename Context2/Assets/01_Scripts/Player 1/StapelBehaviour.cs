using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StapelBehaviour : MonoBehaviour
{
    [SerializeField] private List<Sprite> sprites = new List<Sprite>();
    [SerializeField] private Sprite emptySprite;

    [SerializeField] private SpriteRenderer spriteRenderer;


    [SerializeField] private int counter = 0;
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void StapelSetup(int _choiceListCount)
    {
        if (spriteRenderer == null)
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }

        counter = sprites.Count - _choiceListCount;
        Debug.Log(sprites[counter].name);
        Debug.Log(spriteRenderer);
        spriteRenderer.sprite = sprites[counter];

    }

    public void NextSprite()
    {
        counter++;
        if (sprites[counter] != null && spriteRenderer != null)
        {
            spriteRenderer.sprite = sprites[counter];
        }
        else
        {
            spriteRenderer.sprite = emptySprite;
        }
    }
}

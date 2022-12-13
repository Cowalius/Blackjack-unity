using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardScript : MonoBehaviour
{
    public int value = 0;

    public int GetValueOfCArd()
    {
        return value;
    }

    public void SetValue(int newvalue)
    {
        value = newvalue;
    }

    public void SetSprite(Sprite newSprite)
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = newSprite;
    }

    public string GetName()
    {
        return GetComponent<SpriteRenderer>().sprite.name;
    }

    public void ResetCard()
    {//tu byl deckcontroller
        Sprite back = GameObject.Find("Deck").GetComponent<DeckScript>().GetCardBack();
        gameObject.GetComponent<SpriteRenderer>().sprite = back;
        value = 0;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckScript : MonoBehaviour
{

    public Sprite[] cardSprites;
    int[] cardValues = new int[53];
    int Currentindex = 0;

    // Start is called before the first frame update
    void Start()
    {
        GetCardValues();
    }

    // Update is called once per frame
    void GetCardValues()
    {
        int num = 0;
        for(int i =0;i<cardSprites.Length;i++)
        {
            num = i;
            num = num % 13;
            if(num>10 ||num==0)
            {
                num = 10;
            }
            cardValues[i] = num++;
        }
        
    }

    public void Shuffle()
    {
        for(int i = cardSprites.Length-1;i>0;--i) { 
            //array swapping
            int j = Mathf.FloorToInt(Random.Range(0.0f, 1.0f) * cardSprites.Length - 1) + 1;
            Sprite face = cardSprites[i];
            cardSprites[i] = cardSprites[j];
            cardSprites[j] = face;

            int value = cardValues[i];
            cardValues[i] = cardValues[j];
            cardValues[j] = value;
        }
        Currentindex = 1;
    }
    public int DealCard(CardScript cardScript)
    {
        cardScript.SetSprite(cardSprites[Currentindex]);
        cardScript.SetValue(cardValues[Currentindex++]);
        return cardScript.GetValueOfCArd();
   }
   public Sprite GetCardBack()
    {
        return cardSprites[0];
    }
}

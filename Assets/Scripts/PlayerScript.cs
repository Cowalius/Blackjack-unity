using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{

    public CardScript cardscript;
    public DeckScript deckScript;
    // Start is called before the first frame update
    public int handvalue = 0;
    private int money = 1000;
    public GameObject[] hand;
    public int cardindex = 0;
    
    List<CardScript> acelist=new List<CardScript>();
    public void StartHand()
    {
        GetCard();
        GetCard();
    }

    public int GetCard()
    {
        int cardvalue = deckScript.DealCard(hand[cardindex].GetComponent<CardScript>());

        hand[cardindex].GetComponent<Renderer>().enabled = true;
        handvalue += cardvalue;
        if (cardvalue == 1)
        {
            acelist.Add(hand[cardindex].GetComponent<CardScript>());
        }
        AceCheck();
        cardindex++;
        return handvalue;

    }

    public void AceCheck()
    {
        foreach(CardScript ace in acelist)
        {
            if(handvalue>10// && ace.GetValueOfCArd() ==1
                               ) {
                ace.SetValue(1);
                handvalue -= 10;
            
            }else if(handvalue<10// && ace.GetValueOfCArd()==11
                                 ) {
                ace.SetValue(11);
                handvalue += 10;    
            }
        }
    }

    //liczy ile kasy
    public void AdjustMoney(int amount)
    {
        money += amount;
    }
    
    //zwraca kase
    public int GetMoney()
    {
        return money;
    }

    public void ResetHand()
    {
        for (int i = 0; i < hand.Length; i++)
        {
            hand[i].GetComponent<CardScript>().ResetCard();
            hand[i].GetComponent<Renderer>().enabled = false;
        }
        cardindex= 0;
        handvalue= 0;
        acelist= new List<CardScript>();


    }

   

}

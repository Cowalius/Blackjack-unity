using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Button dealbtn;
    public Button hitbtn;
    public Button standbtn;
    public Button betbtn;
    public Text standbtnText;
    public PlayerScript player;
    public PlayerScript dealer;
    private int StandClicks = 0;
    public Text maintext;
    public Text scroteText;
    public Text dealerscoreText;
    public Text cashText;
    public Text betsText;

    

    //karta ukryta( 2 karta dealera)
    public GameObject hidecard;
    //bet
    int pot = 0;

    // Start is called before the first frame update
    void Start()
    {
        //add listeners
        dealbtn.onClick.AddListener(() => DealClicked());
        hitbtn.onClick.AddListener(() => HitClicked());
        standbtn.onClick.AddListener(() => StandClicked());
        betbtn.onClick.AddListener(() => BetClicked());
    }

  

    private void HitClicked(){ 
    
    //czy jest miejsce na netx card
        if (player.GetCard() <= 10)
        {
            player.GetCard();
            scroteText.text = "Hand: " + player.handvalue + "nowe value";
            if(player.handvalue > 20)
            {
                RoundOver();
            }
        }
    }
    public void previousScene()
    {
        //zaladowanie gry
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex -1);
    }

    //bety z zamiana z  textu na liczby 
    void BetClicked()
    {
        player.AdjustMoney(-20);
        Text newbet = betbtn.GetComponentInChildren(typeof(Text)) as Text;
        int intbet = int.Parse(newbet.text.ToString().Remove(0, 1));
//player.AdjustMoney(-intbet);
        cashText.text=player.GetMoney().ToString();
        pot += (20 * 2);
        betsText.text = pot.ToString();

    }


    private void DealClicked()
    {
        //reset 
        player.ResetHand();
        dealer.ResetHand();
        maintext.gameObject.SetActive(false);
        dealerscoreText.gameObject.SetActive(false);
        GameObject.Find("Deck").GetComponent<DeckScript>().Shuffle();
        player.StartHand();
        dealer.StartHand();
       // dealer.GetCard();
       // dealer.GetCard();
        standbtnText.text = "call";
        //update score
        scroteText.text = " Tokens ";//Hand: "+player.handvalue +"po rozdaniu
        dealerscoreText.text = "Hand: " + dealer.handvalue.ToString();
        hidecard.GetComponent<Renderer>().enabled = true;
        //hidding buttons
        dealbtn.gameObject.SetActive(false);
        standbtn.gameObject.SetActive(true);
        hitbtn.gameObject.SetActive(true);
        standbtnText.text = "Stand";
        pot = 40;
        betsText.text = pot.ToString();
        player.AdjustMoney(-20);
       cashText.text = player.GetMoney().ToString();
    }

    private void HitDealer()
    {
        while(dealer.handvalue< 16 && dealer.cardindex <10) {
            dealer.GetCard();
            dealerscoreText.text ="Hand: "+ dealer.handvalue.ToString();
            if(dealer.handvalue > 20)
            {

            }

        }
    }

    private void StandClicked()
    {
       // hidecard.gameObject.SetActive(false);
        StandClicks++;
        if (StandClicks > 1) RoundOver();
        HitDealer();
    }

    //chec kwinner and loser
    void RoundOver()
    {
        bool playerbust = player.handvalue > 21;
        bool dealerbust = dealer.handvalue > 21;
        bool player21 = player.handvalue ==21;
        bool dealer21 = dealer.handvalue == 21;
        if (StandClicks < 2 && !playerbust && !dealerbust && !player21 && !dealer21) return;
        bool roundover = true;
        if(playerbust && dealerbust)
        {
            maintext.text = "All bust: bets returned";
            //tyle bo tylko polwoa kasy w zakladzie jest gracza
            player.AdjustMoney(pot / 2);
        }else if(playerbust || (dealer.handvalue > player.handvalue && !dealerbust))
        {
            maintext.text = "Dealer won !";
        }else if(dealerbust || player.handvalue> dealer.handvalue)
        {
            maintext.text = " You won !";
            player.AdjustMoney(pot);
        }
        else if(player.handvalue == dealer.handvalue)
        {
            maintext.text = "bets returned";
            player.AdjustMoney(pot/2);
        }
        else
        {
            roundover= false;
        }

        //reset rundy
        if(roundover)
        {
            hitbtn.gameObject.SetActive(false);
            standbtn.gameObject.SetActive(false);
            dealbtn.gameObject.SetActive(true);
            maintext.gameObject.SetActive(true);
            dealerscoreText.gameObject.SetActive(true);
            hidecard.GetComponent<Renderer>().enabled = false;
            cashText.text = player.GetMoney().ToString();
            StandClicks = 0;
        }

    }
}

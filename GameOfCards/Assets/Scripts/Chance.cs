﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class Chance : MonoBehaviour
{
    
    // These are game objects
    public CardStack dealer;
    public CardStack player;
    public CardStack deck;

    // Create these buttons in Chance scene
    public Button playAgainButton;
    public Text winnerText;
    public Text playerScore;
    public Text dealerScore; 


    public Button endTurnButton;
    public Button swapCardButton;

    int roundWonByPlayer = 0;
    int roundWonByDealer = 0;


    /*
     * 3 Cards dealt to each player
     * First player reveal the card
     * Add up all three cards and take the mod 10
     * Compare the 2 cards
     * you win if you have higher hand
     * 3 face cards (Jack/Jack/Jack) or Queen/Jack/King have the highest value
     */


    #region Public Methods

    public void swapCard()
    {
        int tempCard = player.Draw();
        player.push(dealer.Draw());
        dealer.push(tempCard);

    }

    public void endTurn()
    {

        endTurnButton.interactable = false;
        swapCardButton.interactable = false;


        if (dealer.ChanceHandValue() > player.ChanceHandValue())
        {
            winnerText.text = "Sorry-- you lose";
            roundWonByDealer++;
            dealerScore.text = roundWonByDealer.ToString();
        }
        else if (player.ChanceHandValue() > dealer.ChanceHandValue())
        {
            winnerText.text = "You win";
            roundWonByPlayer++;
            playerScore.text = roundWonByPlayer.ToString();
        }
        else
        {
            winnerText.text = "Draw";
        }

        playAgainButton.interactable = true;
    }

    #endregion

    // need to complete
    public void PlayAgain()
    {
        playAgainButton.interactable = false;


        // PROBLEM: THE CARD RESET INSTEAD OF RUNNING OUT OF THE STACK
        // Probably happen during shuffle function
        // no swap so it's okay(?)
        //deck.Shuffle();
        winnerText.text = "";

        //continueButton.interactable = true;
        //revealButton.interactable = true;

        //dealersFirstCard = -1;

        StartGame();
    }

    void Start()
    {
        playerScore.text = "0";
        dealerScore.text = "0";
        StartGame();
    }

    void StartGame()
    {
        endTurnButton.interactable = true;
        swapCardButton.interactable = true;


        if (player.HasCards && dealer.HasCards)
        {
            for (int i = 0; i < 3; i++)
            {
                player.Draw();
                dealer.Draw();
            }
        }
        for (int i = 0; i < 3; i++)
        {
            player.push(deck.Draw());
            dealer.push(deck.Draw());
        }
    }

    void OnGUI()
    {
        if (GUI.Button(new Rect(Screen.width/2 - 100, Screen.height/2 - 15, 200, 30), "Draw"))
        {
            if (player.HasCards && dealer.HasCards)
            {
               for (int i = 0; i < 3; i++)
                {
                    player.Draw();
                    dealer.Draw();
                }
            }
            for (int i = 0; i < 3; i++)
            {
                player.push(deck.Draw());
                dealer.push(deck.Draw());
            }
            if (dealer.ChanceHandValue() > player.ChanceHandValue())
            {
                winnerText.text = "Sorry-- you lose";
            }
            else if (player.ChanceHandValue() > dealer.ChanceHandValue())
            {
                winnerText.text = "You win";
            }
            else
            {
                winnerText.text = "Draw";
            }
        }
        if (GUI.Button(new Rect(Screen.width / 2 + 270, Screen.height / 10 - 50, 200, 30), "Back"))
        {
            SceneManager.LoadScene(1);
        }

    }

}

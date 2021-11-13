using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public delegate void CheckCardEventHandler();
public class Card : MonoBehaviour
{
    public Image Image;
    public Sprite backImage;
    Sprite oldImage;
    public Card CardsPair;
    public bool blockInput;
    CardAnimatorControler cardAnimatorControler;
    bool CardRevealed;
    public bool CardRemoved = false;

    private void Start()
    {
        Debug.Log("Card is :" + CardRemoved);
        StartPalyer();
        cardAnimatorControler = GetComponent<CardAnimatorControler>();
        GameEvents.current.startPlayer += StartPalyer;
        GameEvents.current.stopPlayer += stopPlayer;
        //cardId = backImage.name;
    }

    public void AiFlipTheCard()
    {
        //GameEvents.current 
        CardRevealed = true;
        GetComponent<CardAnimatorControler>().subscribeToGameEvents();
        GameEvents.current.StartFilpAni();     
    }

    public void FlipTheCard()
    {
        if (blockInput)
        {
            CardRevealed = true;
            GetComponent<CardAnimatorControler>().subscribeToGameEvents();
            GameEvents.current.StartFilpAni();
            //GameEvents.current.PlayerFoundBug();
            GameEvents.current.changeImage += ChangeImage;
            GameEvents.current.changeImageBack += ChangeImageBack;
            GameEvents.current.StopAi();
        }
    }

    /*
    public void CheckCard()
    {
        GameEvents.current.CheckCard(this);
    }
    */

    public void ChangeImage()
    {
        oldImage = Image.sprite;
        Image.sprite = backImage;
        GameEvents.current.changeImage -= ChangeImage;
    }

    public void ChangeImageBack()
    {       
        Image.sprite = oldImage;
        GameEvents.current.changeImageBack -= ChangeImageBack;
    }


    void stopPlayer()
    {
        blockInput = false;
        Image.GetComponent<Button>().enabled = false;
    }

    public void StartPalyer()
    {
        blockInput = true;
        Image.GetComponent<Button>().enabled = true;
    }

    public void TurnCard()
    {
        cardAnimatorControler.CoverUpAni();
    }

    public void Remove()
    {
        CardRemoved = true;
        cardAnimatorControler.RemoveCardAni();
    }
}

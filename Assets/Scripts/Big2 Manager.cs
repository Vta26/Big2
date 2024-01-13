using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ClassCard;

public class Big2Manager : MonoBehaviour
{
    public bool isSingle = false;
    public bool isPair = false;
    public bool isTriple = false;
    public bool isFive = false;
    public bool isStraight = false;
    public bool isFlush = false;
    public bool isFullHouse = false;
    public bool isFourOfAKind = false;
    public bool isStraightFlush = false;

    public class inPlay
    {
        public float Value;
        public Card Card1;
        public Card Card2;
        public Card Card3;
        public Card Card4;
        public Card Card5;
        public int CardCount;

        public inPlay()
        {
            Value = 0.0f;
            Card1 = Card2 = Card3 = Card4 = Card5 = null;
            CardCount = 0;
        }

        public void PlayCard(Card FirstCard)
        {
            Value = FirstCard.Value;
            Card1 = FirstCard;
            Card2 = null;
            Card3 = null;
            Card4 = null;
            Card5 = null;
            CardCount = 1;
        }

        public void PlayCard(Card FirstCard, Card SecondCard)
        {
            Value = Mathf.Max(FirstCard.Value, SecondCard.Value);
            Card1 = FirstCard;
            Card2 = SecondCard;
            Card3 = null;
            Card4 = null;
            Card5 = null;
            CardCount = 2;
        }

        public void PlayCard(Card FirstCard, Card SecondCard, Card ThirdCard)
        {
            Value = FirstCard.Value + SecondCard.Value + ThirdCard.Value;
            Card1 = FirstCard;
            Card2 = SecondCard;
            Card3 = ThirdCard;
            Card4 = null;
            Card5 = null;
            CardCount = 3;
        }

        public void PlayCard(Card FirstCard, Card SecondCard, Card ThirdCard, Card FourthCard, Card FifthCard, string Type)
        {
            Card1 = FirstCard;
            Card2 = SecondCard;
            Card3 = ThirdCard;
            Card4 = FourthCard;
            Card5 = FifthCard;
            CardCount = 5;
            float[] temparray = new float[5];
            temparray[0] = FirstCard.Value;
            temparray[1] = SecondCard.Value;
            temparray[2] = ThirdCard.Value;
            temparray[3] = FourthCard.Value;
            temparray[4] = FifthCard.Value;
            switch(Type)
            {
                case "Straight":
                    Value = Mathf.Max(temparray);
                    break;
                case "Flush":
                    Value = FirstCard.Value - Mathf.Floor(FirstCard.Value) + 20.0f;
                    break;
                case "FullHouse":
                    Value = Mathf.Max(Mathf.Max(Mathf.Floor(FirstCard.Value),Mathf.Floor(SecondCard.Value)),Mathf.Floor(ThirdCard.Value)) + 40.0f;
                    break;
                case "FourOfAKind":
                    Value = FirstCard.Value + 60.0f;
                    break;
                case "StraightFlush":
                    Value = Mathf.Max(temparray) + 80.0f;
                    break;
                default:
                    Value = 0;
                    break;

            }
        }

        public void ResetPlay()
        {
            Value = 0.0f;
            Card1 = Card2 = Card3 = Card4 = Card5 = null;
            CardCount = 0;
        }
    }

    public inPlay CurrentlyInPlay;

    public void Start()
    {
        CurrentlyInPlay = new inPlay();
    }

}

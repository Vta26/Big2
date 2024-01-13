using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ClassCard{
    public class Card
    {
        public float Value;
        public string Number;
        public string Suit;
        public string Color;

        public Card(float Val, string Num, string suit, string color)
        {
            Value = Val;
            Number = Num;
            Suit = suit;
            Color = color;
        }

        public Card()
        {
            Value = 0.0f;
            Number = "";
            Suit = "";
            Color = "";
        }

        public string Name()
        {
            return (Number + " of " + Suit + "s");
        }
    }
}    

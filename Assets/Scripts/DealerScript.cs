using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using ClassCard;

public class DealerScript : MonoBehaviour
{
    public string CurrentPlayer = "Player 1";
    public bool Waiting = false;
    private bool GameStarted = false;
    private List<string> Players = new List<string>{"Player 1","Player 2", "Player 3","Player 4"};
    private List<string> Numbers = new List<string>{"Three","Four","Five","Six","Seven","Eight","Nine","Ten","Jack","Queen","King","Ace","Two"};
    private List<string> Suits = new List<string>{"Diamond","Club","Heart","Spade"};
    private List<string> Colors = new List<string>{"Red","Black","Red","Black"};

    public GameObject CardObject;
    public Transform DeckStart;
    public Transform LeftTarget;
    public Transform UpTarget;
    public Transform RightTarget;
    public Transform PlayerTarget;
    public TextMeshProUGUI LeftHandCount; //-405x -85y
    public TextMeshProUGUI UpHandCount; //0x 175y
    public TextMeshProUGUI RightHandCount; //570x -85y
    public TextMeshProUGUI PlayerHandCount; //0x -180y

    public List<Card> Deck = new List<Card>();
    public List<string> DeckNames = new List<string>();
    private List<GameObject> Cards = new List<GameObject>();

    // Start is called before the first frame update
    public void BuildDeck()
    {
        List<Card> StarterDeck = new List<Card>();
        float CardValue = 0.0f;
        float SuitValue = 0.0f;

        //52 Cards in a deck
        for (int i = 0;i < 13; i++)
        {
            for (int k = 0;k < 4; k++)
            {
                Card newCard = new Card(CardValue + SuitValue, Numbers[i], Suits[k], Colors[k]);
                SuitValue += 0.1f;
                Deck.Add(newCard);
            }
            CardValue += 1.0f;
            SuitValue = 0.0f;   
        }

        for (int i = 0; i < Deck.Count; i++)
        {
            DeckNames.Add(Deck[i].Name());
        }
    }

    public List<GameObject> PlayerHand = new List<GameObject>();
    public List<GameObject> LeftPlayerHand = new List<GameObject>();
    public List<GameObject> UpPlayerHand = new List<GameObject>();
    public List<GameObject> RightPlayerHand = new List<GameObject>();

    public List<string> PlayerHandNames = new List<string>();
    public List<string> LeftPlayerHandNames = new List<string>();
    public List<string> UpPlayerHandNames = new List<string>();
    public List<string> RightPlayerHandNames = new List<string>();

    public string CurrentTurn = "Player";

    IEnumerator Deal()
    {
        print("Deal Started");
        int DealTarget = 0;
        Card tempCard;
        for (int i = 0; i < 52; i++)
        {
            if (DealTarget > 3){
                DealTarget = 0;
            }
            tempCard = Deck[Random.Range(0, Deck.Count)];
            GameObject TempCard;
            TempCard = Instantiate(CardObject);
            TempCard.transform.position = DeckStart.position;
            Cards.Add(TempCard);
            TempCard.GetComponent<CardScript>().DealMove(DealTarget);
            TempCard.GetComponent<CardScript>().SetCardValue(tempCard.Value, tempCard.Number, tempCard.Suit, tempCard.Color);
            switch(DealTarget)
            {
                case 0:
                    PlayerHand.Add(TempCard);
                    PlayerHandNames.Add(tempCard.Name());
                    break;
                case 1:
                    LeftPlayerHand.Add(TempCard);
                    LeftPlayerHandNames.Add(tempCard.Name());
                    break;
                case 2:
                    UpPlayerHand.Add(TempCard);
                    UpPlayerHandNames.Add(tempCard.Name());
                    break;
                default:
                    RightPlayerHand.Add(TempCard);
                    RightPlayerHandNames.Add(tempCard.Name());
                    break;
            }
            Deck.Remove(tempCard);
            DealTarget++;
            yield return new WaitForSeconds(0.1f);
        }
        GameStarted = true;
    }

    IEnumerator PlayerPlayCard()
    {
        //print("Playing Cards");
        //Test Run Player Plays 1 Card Rn
        for (int i = 0;i<4;i++)
        {
            Cards[i].GetComponent<CardScript>().DealMove(4);
            yield return new WaitForSeconds(0.1f);
        }
    }

    public void PlayCard(string SelectedPlayer)
    {
        switch(SelectedPlayer)
        {
            case "1":
                PlayerHand[0].GetComponent<CardScript>().DealMove(4);
                PlayerHand.Remove(PlayerHand[0]);
                PlayerHandNames.Remove(PlayerHandNames[0]);
                break;
            case "2":
                LeftPlayerHand[0].GetComponent<CardScript>().DealMove(4);
                LeftPlayerHand.Remove(LeftPlayerHand[0]);
                LeftPlayerHandNames.Remove(LeftPlayerHandNames[0]);
                break;
            case "3":
                UpPlayerHand[0].GetComponent<CardScript>().DealMove(4);
                UpPlayerHand.Remove(UpPlayerHand[0]);
                UpPlayerHandNames.Remove(UpPlayerHandNames[0]);
                break;
            case "4":
                RightPlayerHand[0].GetComponent<CardScript>().DealMove(4);
                RightPlayerHand.Remove(RightPlayerHand[0]);
                RightPlayerHandNames.Remove(RightPlayerHandNames[0]);
                break;
            default:
                print("Error Playing Card");
                break;
        }
    }

    public void DisplayHand(string SelectedPlayer)
    {
        List<string> tempHand;
        switch(SelectedPlayer)
        {
            case "1":
                tempHand = PlayerHandNames;
                break;
            case "2":
                tempHand = LeftPlayerHandNames;
                break;
            case "3":
                tempHand = UpPlayerHandNames;
                break;
            case "4":
                tempHand = RightPlayerHandNames;
                break;
            default:
                tempHand = new List<string>();
                break;
        }
        if (tempHand.Count == 0 && GameStarted)
        {
            print("You Win!");
            return;
        }
        string HandString = "";
        for (int i = 0; i < tempHand.Count; i++)
        {
            HandString = HandString + tempHand[i];
            if (i != tempHand.Count - 1)
            {
                HandString = HandString + ", ";
            }
        }
        print("Your Current Hand Is: " + HandString);
    }

    void Start()
    {
        BuildDeck();
        //Deal();
        print("Please Press Space to Start");
        Time.timeScale = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && Time.timeScale == 0)
        {
            Time.timeScale = 1;
            StartCoroutine(Deal());
        }
        LeftHandCount.text = LeftPlayerHand.Count.ToString();
        UpHandCount.text = UpPlayerHand.Count.ToString();
        RightHandCount.text = RightPlayerHand.Count.ToString();
        PlayerHandCount.text = PlayerHand.Count.ToString();
        if (Input.GetKeyDown(KeyCode.Return))
        {
            StartCoroutine(PlayerPlayCard());
        }
        if (!Waiting && GameStarted)
        {
            switch(CurrentPlayer)
            {
                case "Player 1":
                    DisplayHand("1");
                    break;
                case "Player 2":
                    DisplayHand("2");
                    break;
                case "Player 3":
                    DisplayHand("3");
                    break;
                case "Player 4":
                    DisplayHand("4");
                    break;
                default:
                    break;
            }
            Waiting = true;
        }
        else
        {
            if (Input.GetKeyDown("1"))
            {
                switch(CurrentPlayer)
                {
                    case "Player 1":
                        PlayCard("1");
                        CurrentPlayer = "Player 2";
                        break;
                    case "Player 2":
                        PlayCard("2");
                        CurrentPlayer = "Player 3";
                        break;
                    case "Player 3":
                        PlayCard("3");
                        CurrentPlayer = "Player 4";
                        break;
                    case "Player 4":
                        PlayCard("4");
                        CurrentPlayer = "Player 1";
                        break;
                    default:
                        break;
                }
                Waiting = false;
            }
        }
        //if (Input.GetKeyDown("1"))
        //{
            //DisplayHand("1");
        //}
        //if (Input.GetKeyDown("2"))
        //{
            //DisplayHand("2");
        //}
        //if (Input.GetKeyDown("3"))
        //{
            //DisplayHand("3");
        //}
        //if (Input.GetKeyDown("4"))
        //{
            //DisplayHand("4");
        //}
    }
}

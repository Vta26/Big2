using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ClassCard;

public class CardScript : MonoBehaviour
{
    private List<string> Numbers = new List<string>{"Three","Four","Five","Six","Seven","Eight","Nine","Ten","Jack","Queen","King","Ace","Two"};
    private List<string> Suits = new List<string>{"Diamond","Club","Heart","Spade"};
    private List<string> Colors = new List<string>{"Red","Black","Red","Black"};
    
    public Transform DeckStart;
    public Transform LeftTarget;
    public Transform UpTarget;
    public Transform RightTarget;
    public Transform PlayerTarget;
    public int TargetPos = 5;
    public float Speed = 50.0f;
    private Transform HandPosition;
    //Quaternion _MyQuaternion = new Quaternion();

    public Card _ThisCard;

    public void SetCardValue(float val, string num, string suit, string color)
    {
        _ThisCard = new Card(val, num, suit, color);
    }

    public void DealMove(int DealPos)
    {
        TargetPos = DealPos;
    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        switch (TargetPos)
        {
            case 0:
                transform.position = Vector3.MoveTowards(transform.position, PlayerTarget.position, Speed * Time.deltaTime);
                HandPosition = PlayerTarget;
                break;
            case 1:
                transform.position = Vector3.MoveTowards(transform.position, LeftTarget.position, Speed * Time.deltaTime);
                HandPosition = LeftTarget;
                break;
            case 2:
                transform.position = Vector3.MoveTowards(transform.position, UpTarget.position, Speed * Time.deltaTime);
                HandPosition = UpTarget;
                break;
            case 3:
                transform.position = Vector3.MoveTowards(transform.position, RightTarget.position, Speed * Time.deltaTime);
                HandPosition = RightTarget;
                break;
            case 4:
                if (transform.eulerAngles.y >= 180 && transform.position == DeckStart.position)
                {
                    //print("Stopped");
                    TargetPos = 5;
                }
                else
                {
                    if (transform.eulerAngles.y < 180)
                    {
                        transform.Rotate(0,800.0f*Time.deltaTime,0);
                    }
                    if (transform.position != DeckStart.position)
                    {
                        transform.position = Vector3.MoveTowards(transform.position, DeckStart.position, Speed * Time.deltaTime);
                    }
                }
                break;
            default:
                break;
        }
    }
}

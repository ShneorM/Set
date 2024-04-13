using Set.Model;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;

namespace Set.ViewModel;

public class SetViewModel : INotifyPropertyChanged
{
    private DB dB;
    List<Card> userSetList;
    bool[] chosenCard;

    //Ctor
    public SetViewModel()
    {
        dB = new DB();
        userSetList = new List<Card>();
        chosenCard = new bool[12];//Cards that the user chooses in the game itself so that after choosing 3 cards we will check if it is a set
        optionalSet = new bool[12];//A set that the computer recommends
        loadData();//fill the deck

        AddCard0 = new relayCommand(execute => handlingTheSelectedCard(0, execute));
        AddCard1 = new relayCommand(execute => handlingTheSelectedCard(1, execute));
        AddCard2 = new relayCommand(execute => handlingTheSelectedCard(2, execute));
        AddCard3 = new relayCommand(execute => handlingTheSelectedCard(3, execute));
        AddCard4 = new relayCommand(execute => handlingTheSelectedCard(4, execute));
        AddCard5 = new relayCommand(execute => handlingTheSelectedCard(5, execute));
        AddCard6 = new relayCommand(execute => handlingTheSelectedCard(6, execute));
        AddCard7 = new relayCommand(execute => handlingTheSelectedCard(7, execute));
        AddCard8 = new relayCommand(execute => handlingTheSelectedCard(8, execute));
        AddCard9 = new relayCommand(execute => handlingTheSelectedCard(9, execute));
        AddCard10 = new relayCommand(execute => handlingTheSelectedCard(10, execute));
        AddCard11 = new relayCommand(execute => handlingTheSelectedCard(11, execute));
        Hint = new relayCommand(execute => { findingSetToHint(); onPropertyChanged("OptionalSet"); });
    }

    //RelayCommands
    public relayCommand AddCard0 { get; set; }
    public relayCommand AddCard1 { get; set; }
    public relayCommand AddCard2 { get; set; }
    public relayCommand AddCard3 { get; set; }
    public relayCommand AddCard4 { get; set; }
    public relayCommand AddCard5 { get; set; }
    public relayCommand AddCard6 { get; set; }
    public relayCommand AddCard7 { get; set; }
    public relayCommand AddCard8 { get; set; }
    public relayCommand AddCard9 { get; set; }
    public relayCommand AddCard10 { get; set; }
    public relayCommand AddCard11 { get; set; }
    public relayCommand Hint { get; set; }




    //A function that handles the card the user has selected on the board
    void handlingTheSelectedCard(int placementOnTheBoard, object _path)
    {
        string path = _path.ToString();
        //Find the name of the card
        string nameCard = pathToCard(path);
        //if the Card not exist in the List Add him. else Remove him.
        Card card = nameToCard(nameCard);
        if (GamePlay.contain(card, userSetList) == false)
        {
            userSetList.Add(card);
            chosenCard[placementOnTheBoard] = true;
        }
        else
        {
            userSetList.Remove(card);
            chosenCard[placementOnTheBoard] = false;
            return;//Surely there is no Set, so we will finish
        };

        //Check if the user choose three cards
        if (userSetList.Count == 3)
        {
            //if the cards Set Do....
            if (GamePlay.checkSet(userSetList) == true)
            {
                for (int i = 0; i < 12; ++i)
                {
                    if (chosenCard[i] == true)
                    {
                        GamePlay.moveOneCardToTheGameBoard(UnUsedCards, UsedCards, CurrentCards, i);
                        //Manual activation of the change 
                        onPropertyChanged("UnUsedCards");
                        onPropertyChanged("UsedCards");
                        onPropertyChanged("CurrentCards");

                        optionalSet = new bool[12];
                        onPropertyChanged("OptionalSet");

                        //Check if the game is over
                        if (UnUsedCards.Count <= 16 && findingSetToHint() == false)
                            MessageBox.Show("Well done!\nyou won!");


                    }
                }
            }
            else//Do Something Bad
            {

            }

            //reset the choose of user
            userSetList = new List<Card>();
            chosenCard = new bool[12];
            optionalSet = new bool[12];
        }
    }



    public event PropertyChangedEventHandler? PropertyChanged;
    private void onPropertyChanged(string propertyName)
    {
        if (propertyName != null)
            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
    }


    #region Property
    private List<Card> unUsedCards;
    public List<Card> UnUsedCards
    {
        get { return unUsedCards; }
        set { unUsedCards = value; onPropertyChanged("UnUsedCards"); }
    }

    private List<Card> usedCards;
    public List<Card> UsedCards
    {
        get { return usedCards; }
        set { usedCards = value; onPropertyChanged("UsedCards"); }
    }

    private List<Card> currentCards;
    public List<Card> CurrentCards
    {
        get { return currentCards; }
        set { currentCards = value; onPropertyChanged("CurrentCards"); }
    }
    private bool[] optionalSet;

    public bool[] OptionalSet
    {
        get { return optionalSet; }
        set { optionalSet = value; onPropertyChanged("OptionalSet"); }
    }

    #endregion

    #region Load Data 
    void loadData()
    {
        unUsedCards = dB.Cards;//all the Cards
        usedCards = new List<Card>();//all the used Cards
        //The first 12 cards
        currentCards = new List<Card>(new Card[12]);
        for (int i = 0; i < 12; ++i)
        {
            GamePlay.moveOneCardToTheGameBoard(unUsedCards, usedCards, currentCards, i);
        }


    }
    #endregion

    #region Auxiliary functions

    //Convert path to Card object
    private string pathToCard(string path)
    {
        string cardName = ReverseString(path);
        int i = cardName.IndexOf("\\");
        cardName = cardName.Substring(0, i);
        cardName = ReverseString(cardName);
        cardName = cardName.Replace(".png", "");
        return cardName;
    }

    //Revers the String
    static string ReverseString(string input)
    {
        char[] charArray = input.ToCharArray();
        int length = input.Length;
        for (int i = 0; i < length / 2; i++)
        {
            char temp = charArray[i];
            charArray[i] = charArray[length - i - 1];
            charArray[length - i - 1] = temp;
        }
        return new string(charArray);
    }

    //Convert name to Card object
    Card nameToCard(string name)
    {
        string Shape = extractUpper(name);
        name = name.Replace(Shape, "");
        string Fill = extractUpper(name);
        name = name.Replace(Fill, "");
        string Color = extractUpper(name);
        name = name.Replace(Color, "");
        int Number = int.Parse(name);
        Card card = new Card()
        {
            shape = Shape,
            fill = Fill,
            color = Color,
            number = Number
        };
        return card;
    }

    //A function that accepts a string and returns (0, first capital letter || number)
    string extractUpper(string input)
    {
        string output = input[0] + "";
        for (int i = 1; i < input.Length; ++i)
        {
            if (char.IsUpper(input[i]) || char.IsDigit(input[i]))
            {
                return output;
            }
            output = output + input[i];
        }
        return output;
    }

    //Finding a set for a hint to the user
    bool findingSetToHint()
    {
        optionalSet = new bool[12];//Reset the previous selection
        var recommendedSet = GamePlay.findSet(currentCards);//Find a recommended set for hint 
        if (recommendedSet == null)
            return false;
        for (int i = 0; i < 12; ++i)
        {
            if (currentCards[i].ToString() == recommendedSet[0].ToString() ||
                currentCards[i].ToString() == recommendedSet[1].ToString() ||
                currentCards[i].ToString() == recommendedSet[2].ToString())
            {
                optionalSet[i] = true;
            }
        }
        return true;
    }
    #endregion
}
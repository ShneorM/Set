using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Set.Model;

public static class GamePlay
{

    public static bool checkSet(List<Card> cards)
    {
        if (cards.Count != 3) ;
        //זריקת חריגה
        if (!(cards[0].shape == cards[1].shape && cards[1].shape == cards[2].shape ||
            cards[0].shape != cards[1].shape && cards[1].shape != cards[2].shape && cards[0].shape != cards[2].shape))
        {
            return false;
        }
        if (!(cards[0].fill == cards[1].fill && cards[1].fill == cards[2].fill ||
            cards[0].fill != cards[1].fill && cards[1].fill != cards[2].fill && cards[0].fill != cards[2].fill))
        {
            return false;
        }
        if (!(cards[0].color == cards[1].color && cards[1].color == cards[2].color ||
            cards[0].color != cards[1].color && cards[1].color != cards[2].color && cards[0].color != cards[2].color))
        {
            return false;
        }
        if (!(cards[0].number == cards[1].number && cards[1].number == cards[2].number ||
            cards[0].number != cards[1].number && cards[1].number != cards[2].number && cards[0].number != cards[2].number))
        {
            return false;
        }

        return true;
    }

    //check if EndGame
    //..


    public static void moveOneCardToTheGameBoard(List<Card> unUsedCards, List<Card> usedCards, List<Card> currentCards, int index)
    {
        if (unUsedCards.Count < 1)
            return;//זרוק חריגה
        Random rnd = new Random();
        int _rnd = rnd.Next(0, unUsedCards.Count);
        Card card = unUsedCards[_rnd];//נבחר קלף חדש מהרשימה
        currentCards[index] = card;//נציב אותו במקום המתאים בלוח משחק
        usedCards.Add(card);//נשים אותו בקלפים המשומשים
        unUsedCards.RemoveAt(_rnd);//נמחק אותו מהקלפים שבקופה

    }

    public static bool contain(Card card, List<Card> cards)
    {
        foreach (Card aCard in cards)
        {
            if (card.shape == aCard.shape && card.fill == aCard.fill &&
                card.color == aCard.color && card.number == aCard.number)
                return true;
        }
        return false;
    }

    public static List<Card> findSet(List<Card> cards)
    {
        for (int i = 0; i < 12; ++i)
            for (int j = 0; j < 12; ++j)
                for (int k = 0; k < 12; ++k)
                {
                    if (i != j && j != k && i != k && checkSet(new List<Card>() { cards[i], cards[j], cards[k] }) == true)
                        return new List<Card>() { cards[i], cards[j], cards[k] };
                }
        return null;
    }

}
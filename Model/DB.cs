using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Set.Model;

enum Shape { Diamond, Squiggles, Oval }
enum Fill { Solid, Striped, Open }
enum Color { Red, Green, Purple }

public class DB
{
    public List<Card> Cards { get; } = new List<Card>();

    //Ctor
    public DB()
    {
        initCards();
    }

    //Init Cards list
    void initCards()
    {
        for (int i = 0; i < 3; ++i)//shape 
            for (int j = 0; j < 3; ++j)//fill
                for (int k = 0; k < 3; ++k)//color 
                    for (int w = 1; w < 4; ++w)//number 
                    {
                        Card card = new Card()
                        {
                            shape = ((Shape)i).ToString(),
                            fill = ((Fill)j).ToString(),
                            color = ((Color)k).ToString(),
                            number = w
                        };
                        Cards.Add(card);
                    }
        ShuffleCard();

    }//End init Cards
    void ShuffleCard()
    {
        Random rand = new Random();
        for (int i = Cards.Count - 1; i >= 1; i--)
        {
            int j = rand.Next(0, i + 1);
            Card temp = Cards[i];
            Cards[i] = Cards[j];
            Cards[j] = temp;
        }
    }

}

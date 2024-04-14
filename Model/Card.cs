using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Set.Model;

public class Card
{
    public string shape { get; set; }
    public string fill { get; set; }
    public string color { get; set; }
    public int number { get; set; }

    public override string ToString()
    {
        return shape + fill + color + number;
    }
    public override bool Equals(object? obj)
    {
        Card card = obj as Card;
        return (card.shape == this.shape && card.fill == this.fill &&
                card.color == this.color && card.number == this.number);
    }
}

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
}

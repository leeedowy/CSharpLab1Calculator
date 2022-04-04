using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Calculator
{
    public enum Operators
    {
        [Description("+")]
        Addition = '+',
        [Description("-")]
        Subtraction = '-',
        [Description("*")]
        Multiplication = '*',
        [Description("/")]
        Division = '/'
    }
}

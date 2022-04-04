using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator
{
    class Expression
    {
        private decimal? _firstPart;
        private decimal? _secondPart;
        private Operators? _operator;
        private Operators? _previousOperation;
        private decimal? _previousSecondPart;
        private bool _equalsClicked;

        public Expression()
        {
            Clear();
        }

        public void AddDigit(string digit)
        {
            if (_equalsClicked)
            {
                Clear();
            }

            if (_operator == null)
            {
                _firstPart = _firstPart == 0 ?
                    Convert.ToDecimal(digit) : Convert.ToDecimal(Convert.ToString(_firstPart) + digit);
            }
            else
            {
                _secondPart = _secondPart == 0 ?
                    Convert.ToDecimal(digit) : Convert.ToDecimal(Convert.ToString(_secondPart) + digit);
            }
        }

        public void AddOperator(string oparator)
        {
            if (_firstPart != null)
            {
                _equalsClicked = false;
                if (_secondPart != null)
                {
                    Evaluate();
                }
                _operator = (Operators?)char.Parse(oparator);
                _previousOperation = null;
            }
        }

        public void Clear()
        {
            _firstPart = null;
            _secondPart = null;
            _operator = null;
            _previousOperation = null;
            _previousSecondPart = null;
            _equalsClicked = false;
        }

        public void Evaluate()
        {
            if ((_operator != null && _secondPart != null) || _previousOperation != null)
            {
                Operators? currentOperator = _equalsClicked ? _previousOperation : _operator;
                decimal? currentSecondPart = _equalsClicked ? _previousSecondPart : _secondPart;
                switch (currentOperator)
                {
                    case Operators.Addition:
                        _firstPart += currentSecondPart;
                        break;
                    case Operators.Subtraction:
                        _firstPart -= currentSecondPart;
                        break;
                    case Operators.Multiplication:
                        _firstPart *= currentSecondPart;
                        break;
                    case Operators.Division:
                        _firstPart /= currentSecondPart;
                        break;
                    default:
                        break;
                }
                _previousOperation = currentOperator;
                _operator = null;
                _firstPart = Math.Round((decimal)_firstPart, 10).Normalize();
                _previousSecondPart = currentSecondPart;
                _secondPart = null;
            }
        }

        public bool EqualsClicked
        {
            set => _equalsClicked = value;
        }

        public override string ToString()
        {
            string expression = "";
            expression += _firstPart == null ? "" : Convert.ToString(_firstPart);
            if (!_equalsClicked)
            {
                expression += _operator == null ? "" : ((Operators)_operator).GetDescription();
                expression += _secondPart == null ? "" : Convert.ToString(_secondPart);
            }
            return expression;
        }
    }
}

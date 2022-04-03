using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Calculator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string _lastOperation;

        public static readonly IList<string> Operators = new ReadOnlyCollection<string>
        (new List<string> { "+", "-", "*", "/" });

        public MainWindow()
        {
            InitializeComponent();
            _lastOperation = "";
        }

        private void ButtonZero_Click(object sender, RoutedEventArgs e)
        {
            string zero = (sender as Button).Content.ToString();

            if (CalculationsTextBox.Text != "0")
            {
                CalculationsTextBox.Text += zero;
            }
        }

        private void ButtonNumber_Click(object sender, RoutedEventArgs e)
        {
            string number = (sender as Button).Content.ToString();

            if (ExpressionContainsOperator(CalculationsTextBox.Text))
            {
                if (SecondPartOfExpression(CalculationsTextBox.Text) == "0")
                {
                    CalculationsTextBox.Text =
                        CalculationsTextBox.Text.Remove(CalculationsTextBox.Text.Length - 1);
                }
            }
            else       
            {
                if (CalculationsTextBox.Text == "0")
                {
                    CalculationsTextBox.Clear();
                }
            }
            CalculationsTextBox.Text += number;
        }

        private void ButtonOperation_Click(object sender, RoutedEventArgs e)
        {
            string operator_ = (sender as Button).Content.ToString();
            
            if (CalculationsTextBox.Text != "")
            {
                if (ExpressionEndsWithOperator(CalculationsTextBox.Text))
                {
                    CalculationsTextBox.Text =
                        CalculationsTextBox.Text.Remove(CalculationsTextBox.Text.Length - 1, 1) + operator_;
                }
                else if (ExpressionContainsOperator(CalculationsTextBox.Text))
                {
                    CalculationsTextBox.Text = Evaluate(CalculationsTextBox.Text) + operator_;
                }
                else
                {
                    CalculationsTextBox.Text += operator_;
                }
            }
        }

        private void ButtonClear_Click(object sender, RoutedEventArgs e)
        {
            CalculationsTextBox.Clear();
        }

        private void ButtonEquals_Click(object sender, RoutedEventArgs e)
        {
            if (CalculationsTextBox.Text != "" &&
                !ExpressionEndsWithOperator(CalculationsTextBox.Text))
            {
                if (ExpressionContainsOperator(CalculationsTextBox.Text))
                {
                    CalculationsTextBox.Text = Evaluate(CalculationsTextBox.Text);
                }
            }
        }

        private string Evaluate(string calculation)
        {
            double firstPart = Convert.ToDouble(FirstPartOfExpression(CalculationsTextBox.Text));
            double secondPart = Convert.ToDouble(SecondPartOfExpression(CalculationsTextBox.Text));
            double result = 0;

            switch (OperatorOfExpression(CalculationsTextBox.Text))
            {
                case "+":
                    result = firstPart + secondPart;
                    break;
                case "-":
                    result = firstPart - secondPart;
                    break;
                case "*":
                    result = firstPart * secondPart;
                    break;
                case "/":
                    result = firstPart / secondPart;
                    break;
                default:
                    break;
            }

            return result.ToString();
        }

        private bool ExpressionEndsWithOperator(string expression)
        {
            return Operators.Any(op => expression.EndsWith(op));
        }

        private bool ExpressionContainsOperator(string expression)
        {
            return Operators.Any(op => expression.Contains(op));
        }

        private string FirstPartOfExpression(string expression)
        {
            return expression.Split(Operators.ToArray(), StringSplitOptions.None)[0];
        }

        private string SecondPartOfExpression(string expression)
        {
            return expression.Split(Operators.ToArray(), StringSplitOptions.None)[1];
        }

        private string OperatorOfExpression(string expression)
        {
            char[] operators = Operators.Select(c => char.Parse(c)).ToArray();
            int indexOfOperator = expression.IndexOfAny(operators);
            return expression[indexOfOperator].ToString();
        }
    }
}

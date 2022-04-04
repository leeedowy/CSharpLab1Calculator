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
    public partial class MainWindow : Window
    {
        private Expression _expression;

        public MainWindow()
        {
            InitializeComponent();
            _expression = new Expression();
        }

        private void ButtonNumber_Click(object sender, RoutedEventArgs e)
        {
            string number = (sender as Button).Content.ToString();
            _expression.AddDigit(number);
            DisplayExpression(_expression);
        }

        private void ButtonOperation_Click(object sender, RoutedEventArgs e)
        {
            string operator_ = (sender as Button).Content.ToString();
            _expression.AddOperator(operator_);
            DisplayExpression(_expression);
        }

        private void ButtonClear_Click(object sender, RoutedEventArgs e)
        {
            _expression.Clear();
            DisplayExpression(_expression);
        }

        private void ButtonEquals_Click(object sender, RoutedEventArgs e)
        {
            _expression.Evaluate(true);
            DisplayExpression(_expression);
        }

        private void DisplayExpression(Expression expression)
        {
            CalculationsTextBox.Text = expression.ToString();
        }
    }
}

using System;
using System.Windows;
using System.Windows.Controls;

namespace Calculator
{
    
    public partial class MainWindow : Window
    {
        double firstNumber = 0;
        double secondNumber = 0;
        string operation = "";
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btn_Click(object sender, RoutedEventArgs e)
        {
            Button senderBtn = sender as Button;
            double btnContent = double.Parse(senderBtn.Content.ToString());
            if (operation == "")
            {
                firstNumber = firstNumber * 10 + btnContent;
                txtDisplay.Text = firstNumber.ToString();
            }
            else
            {
                int numLength = secondNumber.ToString().Length;
                string text = txtDisplay.Text.ToString();
                if (secondNumber != 0 && numLength > 0)
                {
                    txtDisplay.Text = text.Substring(0, text.Length - numLength);
                }
                secondNumber = secondNumber * 10 + btnContent;
                txtDisplay.Text += secondNumber.ToString();
            }
        }

        private void Calculate()
        {
            double result = 0;
            if (operation == "/" && secondNumber == 0)
            {
                txtDisplay.Text = "Invalid operation";
                firstNumber = 0;
            }
            else
            {
                switch (operation)
                {
                    case "+":
                        result = firstNumber + secondNumber;
                        break;
                    case "-":
                        result = firstNumber - secondNumber;
                        break;
                    case "/":
                        result = firstNumber / secondNumber;
                        break;
                    case "*":
                        result = firstNumber * secondNumber;
                        break;
                    default:
                        result = firstNumber;
                        break;
                }
                txtDisplay.Text = result.ToString();
                firstNumber = result;
            }
            secondNumber = 0;
            operation = "";
        }
        private void btnEqual_Click(object sender, RoutedEventArgs e)
        {
            this.Calculate();
        }

        private void CheckUncalculatedStuff()
        {
            if ((operation != "" && secondNumber != 0) || (operation == "/" && secondNumber == 0))
            {
                this.Calculate();
            }
        }

        private void btnServ_Click(object sender, RoutedEventArgs e)
        {
            this.CheckUncalculatedStuff();
            Button senderBtn = sender as Button;
            string result = senderBtn.Content.ToString();
            operation = result;
            txtDisplay.Text += operation;
        }

        private void btnSqrt_Click(object sender, RoutedEventArgs e)
        {
            this.CheckUncalculatedStuff();
            double result = Math.Sqrt(firstNumber);
            txtDisplay.Text = result.ToString();
            firstNumber = result;
        }

        private string CalculateTan(double value)
        {
            double radianValue = value * Math.PI / 180;
            if (value == 90 || value == 270)
            {
                return "Incorrect value";
            }
            return Math.Tan(radianValue).ToString();
        }

        private void btnMath_Click(object sender, RoutedEventArgs e)
        {
            this.CheckUncalculatedStuff();
            Button senderBtn = sender as Button;
            string result = senderBtn.Content.ToString();
            double mathResult = 0;
            string stringResult = "";
            switch (result)
            {
                case "sin":
                    mathResult = Math.Sin(firstNumber * Math.PI / 180);
                    stringResult = mathResult.ToString();
                    break;
                case "cos":
                    mathResult = Math.Cos(firstNumber * Math.PI / 180);
                    stringResult = mathResult.ToString();
                    break;
                case "tg":
                    stringResult = this.CalculateTan(firstNumber);
                    mathResult = stringResult == "Incorrect value" ? 0 : double.Parse(stringResult);
                    break;
                default:
                    mathResult = 0;
                    break;
            }
            txtDisplay.Text = stringResult;
            firstNumber = mathResult;
        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            firstNumber = 0;
            secondNumber = 0;
            operation = "";
            txtDisplay.Text = "";
        }
    }
}

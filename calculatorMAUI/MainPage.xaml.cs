using Microsoft.Maui.Controls;

namespace calculatorMAUI
{
    public partial class MainPage : ContentPage
    {
        private string _currentInput = ""; // Stores the current user input
        private double _lastResult = 0;    // Stores the last calculated result
        private string _currentOperator = ""; // Stores the current operator (+, -, *, /, %)
        private string _history = "";      // Stores the history of operations

        public MainPage()
        {
            InitializeComponent(); // Initializes the UI components
        }

        // Handles number button clicks
        private void OnNumberClicked(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            _currentInput += button.Text;  // Append the clicked number to the current input
            Result.Text = _currentInput;   // Display the updated input in the result field
        }

        // Handles operator button clicks (+, -, *, /, %)
        private void OnOperatorClicked(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            if (_currentInput != "")
            {
                if (_currentOperator != "")
                {
                    // If an operator is already set, calculate the partial result
                    OnEqualClicked(sender, e);
                }
                _lastResult = double.Parse(_currentInput); // Store the current input as the last result
                _currentInput = ""; // Clear the input for the next number
            }
            _currentOperator = button.Text; // Store the clicked operator

            // Update the history with the last result and operator
            _history += $"{_lastResult} {_currentOperator} ";
            HistoryDisplay.Text = _history;
        }

        // Handles the equals button click (=)
        private void OnEqualClicked(object sender, EventArgs e)
        {
            if (_currentInput != "" && _currentOperator != "")
            {
                double currentValue = double.Parse(_currentInput); // Parse the current input
                switch (_currentOperator)
                {
                    case "+":
                        _lastResult += currentValue; // Addition
                        break;
                    case "-":
                        _lastResult -= currentValue; // Subtraction
                        break;
                    case "*":
                        _lastResult *= currentValue; // Multiplication
                        break;
                    case "/":
                        if (currentValue != 0)
                        {
                            _lastResult /= currentValue; // Division (avoid division by zero)
                        }
                        else
                        {
                            Result.Text = "Error"; // Display error for division by zero
                            return;
                        }
                        break;
                    case "%":
                        _lastResult = _lastResult % currentValue; // Modulus
                        break;
                }
                Result.Text = _lastResult.ToString(); // Display the final result
                _currentInput = _lastResult.ToString(); // Update input with the result for further operations
                _currentOperator = ""; // Reset the operator

                // Update the history with the full calculation
                _history += $"{currentValue} = {_lastResult}\n";
                HistoryDisplay.Text = _history;
            }
        }

        // Handles the Clear (C) button click
        private void OnClearClicked(object sender, EventArgs e)
        {
            _currentInput = ""; // Clear the current input
            Result.Text = "0";   // Reset the display to "0"
        }

        // Handles the All Clear (AC) button click
        private void OnAllClearClicked(object sender, EventArgs e)
        {
            _currentInput = ""; // Clear the current input
            _lastResult = 0;   // Reset the last result
            _currentOperator = ""; // Reset the operator
            Result.Text = "0";  // Reset the display to "0"
            _history = "";      // Clear the history
            HistoryDisplay.Text = ""; // Clear the history display
        }

        // Handles the decimal point button click
        private void OnDecimalClicked(object sender, EventArgs e)
        {
            if (!_currentInput.Contains(".")) // Ensure only one decimal point is added
            {
                _currentInput += "."; // Append the decimal point
                Result.Text = _currentInput; // Update the display
            }
        }
    }
}
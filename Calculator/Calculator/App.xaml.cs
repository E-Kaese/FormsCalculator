using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace Calculator
{
    public partial class App : Application
    {
        static Label _calculationLabel;

        public App()
        {
            InitializeComponent();

            #region Button styles

            var numButton = new Style(typeof(Button))
            {
                Setters =
                {
                    new Setter{Property = Button.BackgroundColorProperty, Value=Color.White},
                    new Setter{Property = Button.FontSizeProperty, Value=32},
                    new Setter{Property = Button.CornerRadiusProperty, Value=0}
                }
            };

            var functionButton = new Style(typeof(Button))
            {
                Setters =
                {
                    new Setter{Property = Button.BackgroundColorProperty, Value = Color.Orange},
                    new Setter{Property = Button.FontSizeProperty, Value=32},
                    new Setter{Property = Button.CornerRadiusProperty, Value=0}
                }
            };

            var darkerButton = new Style(typeof(Button))
            {
                Setters =
                {
                    new Setter{Property = Button.BackgroundColorProperty, Value = Color.DarkGray},
                    new Setter{Property = Button.FontSizeProperty, Value=32},
                    new Setter{Property = Button.CornerRadiusProperty, Value=0}
                }
            };

            #endregion

            #region Grid Setup
            var grid = new Grid { RowSpacing = 1, ColumnSpacing = 1, BackgroundColor = Color.Black };
            grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
            grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
            grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
            grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
            grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });


            _calculationLabel = new Label
            {
                BackgroundColor = Color.Transparent,
                Text = "0",
                TextColor = Color.White,
                HorizontalTextAlignment = TextAlignment.End,
                VerticalTextAlignment = TextAlignment.Center,
                FontSize = 50,
                LineBreakMode = LineBreakMode.HeadTruncation
            };

            grid.Children.Add(_calculationLabel, 0, 0);
            Grid.SetColumnSpan(_calculationLabel, 4);

            var sevenButton = new Button { Text = "7", Style = numButton };
            sevenButton.Clicked += (s, e) => AppendNum("7");
            grid.Children.Add(sevenButton, 0, 1);
            var eightButton = new Button { Text = "8", Style = numButton };
            eightButton.Clicked += (s, e) => AppendNum("8");
            grid.Children.Add(eightButton, 1, 1);
            var nineButton = new Button { Text = "9", Style = numButton };
            nineButton.Clicked += (s, e) => AppendNum("9");
            grid.Children.Add(nineButton, 2, 1);
            var divideButton = new Button { Text = "/", Style = functionButton };
            divideButton.Clicked += (s, e) => AddToEquation("/");
            grid.Children.Add(divideButton, 3, 1);

            var fourButton = new Button { Text = "4", Style = numButton };
            fourButton.Clicked += (s, e) => AppendNum("4");
            grid.Children.Add(fourButton, 0, 2);
            var fiveButton = new Button { Text = "5", Style = numButton };
            fiveButton.Clicked += (s, e) => AppendNum("5");
            grid.Children.Add(fiveButton, 1, 2);
            var sixButton = new Button { Text = "6", Style = numButton };
            sixButton.Clicked += (s, e) => AppendNum("6");
            grid.Children.Add(sixButton, 2, 2);
            var multiplyButton = new Button { Text = "*", Style = functionButton };
            multiplyButton.Clicked += (s, e) => AddToEquation("*");
            grid.Children.Add(multiplyButton, 3, 2);

            var oneButton = new Button { Text = "1", Style = numButton };
            oneButton.Clicked += (s, e) => AppendNum("1");
            grid.Children.Add(oneButton, 0, 3);
            var twoButton = new Button { Text = "2", Style = numButton };
            twoButton.Clicked += (s, e) => AppendNum("2");
            grid.Children.Add(twoButton, 1, 3);
            var threeButton = new Button { Text = "3", Style = numButton };
            threeButton.Clicked += (s, e) => AppendNum("3");
            grid.Children.Add(threeButton, 2, 3);
            var minusButton = new Button { Text = "-", Style = functionButton };
            minusButton.Clicked += (s, e) => AddToEquation("-");
            grid.Children.Add(minusButton, 3, 3);

            var zeroButton = new Button { Text = "0", Style = numButton };
            zeroButton.Clicked += (s, e) => AppendNum("0");
            grid.Children.Add(zeroButton, 0, 4);
            Grid.SetColumnSpan(zeroButton, 3);
            var plusButton = new Button { Text = "+", Style = functionButton };
            plusButton.Clicked += (s, e) => AddToEquation("+");
            grid.Children.Add(plusButton, 3, 4);


            var clearButton = new Button { Text = "C", Style = darkerButton };
            clearButton.Clicked += DeleteNum;
            grid.Children.Add(clearButton, 0, 5);
            var equalsButton = new Button { Text = "=", Style = functionButton };
            equalsButton.Clicked += Equate;
            grid.Children.Add(equalsButton, 1, 5);
            Grid.SetColumnSpan(equalsButton, 3);

            #endregion

            MainPage = new ContentPage
            {
                Content = grid
            };
        }

        void AppendNum(string num)
        {
            if (_calculationLabel.Text == "0")
            {
                _calculationLabel.Text = num;
            }
            else
            {
                _calculationLabel.Text += num;
            }
        }

        void DeleteNum(object sender, EventArgs e)
        {
            if (_calculationLabel.Text.Length != 1)
            {
                string text = _calculationLabel.Text;
                _calculationLabel.Text = text.Substring(0, text.Length - 1);

            }
            else
            {
                _calculationLabel.Text = "0";
            }
        }

        void AddToEquation(string function)
        {
            if (_calculationLabel.Text != "0")
            {
                List<string> list = new List<string>(_calculationLabel.Text.Split(' '));

                if (string.IsNullOrWhiteSpace(list[list.Count - 1]))
                {
                    list.RemoveAt(list.Count - 1);
                    list.RemoveAt(list.Count - 1);
                    _calculationLabel.Text = "";
                    foreach (var item in list)
                    {
                        _calculationLabel.Text += item + " ";
                    }
                }
                else
                {
                    _calculationLabel.Text += " ";
                }
                _calculationLabel.Text += function + " ";
            }
        }

        void Equate(object sender, EventArgs e)
        {
            List<string> list = new List<string>(_calculationLabel.Text.Split(' '));

            if (string.IsNullOrWhiteSpace(list[list.Count - 1]))
            {
                list.RemoveAt(list.Count - 1);
                list.RemoveAt(list.Count - 1);
            }

            if (list.Count > 1)
            {
                double total = double.Parse(list[0]);
                for (int i = 1; i < list.Count; i++)
                {
                    switch (list[i])
                    {
                        case "/":
                            total = Math.Round(total / int.Parse(list[i + 1]), 2);
                            i++;
                            break;
                        case "*":
                            total = Math.Round(total * int.Parse(list[i + 1]), 2);
                            i++;
                            break;
                        case "+":
                            total = Math.Round(total + int.Parse(list[i + 1]), 2);
                            i++;
                            break;
                        case "-":
                            total = Math.Round(total - int.Parse(list[i + 1]), 2);
                            i++;
                            break;
                    }
                }
                _calculationLabel.Text = total.ToString();
            }
        }
    }
}

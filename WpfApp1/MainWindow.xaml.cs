using System;
using System.Data.SQLite;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;

namespace WpfApp1
{ 
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            OutputText.Text = "";
            char s = ' ';
            int i = 0, d = 0, tsp = 0, ts = 0;
            // Переменная для хранения найденного числа
            string foundNumber = string.Empty;

            // Проверка наличия символа разделителя
            bool Slash = artikul.Text.Contains('/');
            bool Underscore = artikul.Text.Contains('_');
            bool BS = artikul.Text.Contains(' ');
            bool flag= false;

            if (Slash) {  s = '/'; }
            else if (Underscore) { s = '_'; }
            else if (BS) { s = ' '; }

            // Разделить строку по символу '/'
            string[] parts = artikul.Text.Split(s);

            // Найти первое число в каждой части
            foreach (var part in parts)
            {
                i++;
                Regex regex = new Regex(@"\d+"); // Поиск числа
                Match match = regex.Match(part);

                if (match.Success)
                {
                    if (!(i % 2 == 0))
                    {
                        ts += int.Parse(match.Value);
                        d++;
                    }
                    tsp += int.Parse(match.Value);
                }
            }
            OutputText.Text = (d-1).ToString() + " - Камерный \n";
            OutputText.Text += "Толщина СП = " + tsp +'\n';
            OutputText.Text += "Толщина стекла = " + ts + '\n';
        }
    }
}
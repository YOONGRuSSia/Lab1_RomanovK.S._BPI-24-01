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
using System.Windows.Shapes;

namespace Lab1_RomanovK.S._BPI_24_01
{
    /// <summary>
    /// Логика взаимодействия для Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        public ObservableCollection<Rabotnik> workers = new ObservableCollection<Rabotnik>();
        public Window1()
        {
            InitializeComponent();
            if (App.CurrentTheme == AppTheme.Dark)
                rbDark.IsChecked = true;
            else
                rbLight.IsChecked = true;
        }
        private void Theme_Checked(object sender, RoutedEventArgs e)
        {
            if (!(sender is RadioButton rb)) return;
            if (rb.Tag == null) return;

            AppTheme theme = rb.Tag.ToString() == "Dark"
                ? AppTheme.Dark
                : AppTheme.Light;

            App.ApplyTheme(theme);
        }
        private void Minimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        private void TitleBar_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left && e.ClickCount == 2)
            {
                WindowState = WindowState == WindowState.Maximized
                    ? WindowState.Normal
                    : WindowState.Maximized;
            }
        }

        private void TxtFamiliya_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            char c = e.Text[0];
            if (!char.IsLetter(c) && c != '-' && c != ' ')
            {
                e.Handled = true;
            }
        }

        private void TxtDolzhnost_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            char c = e.Text[0];
            if (!char.IsLetter(c) && c != ' ')
            {
                e.Handled = true;
            }
        }

        private void TxtOklad_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            char c = e.Text[0];
            if (!char.IsDigit(c) && c != ',' && c != '.')
            {
                e.Handled = true;
            }

            if ((c == ',' || c == '.') && ((TextBox)sender).Text.Contains(",") || ((TextBox)sender).Text.Contains("."))
            {
                e.Handled = true;
            }
        }

        private void TxtOklad_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
            {
                e.Handled = true;
            }
        }

        private void BtnAdd_Clickk(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtFamiliya.Text) ||
                string.IsNullOrWhiteSpace(txtDolzhnost.Text) ||
                !decimal.TryParse(txtOklad.Text, out decimal oklad))
            {
                MessageBox.Show("Пожалуйста, заполните все поля корректно. Оклад должен быть числом.");
                return;
            }

            var newWorker = new Rabotnik
            {
                Familiya = txtFamiliya.Text,
                Dolzhnost = txtDolzhnost.Text,
                Oklad = oklad
            };

            workers.Add(newWorker);
            MessageBox.Show("Работник успешно добавлен.");

            MainWindow secondWindow = new MainWindow(workers);
            secondWindow.Show();
            this.Close();
        }

        
    }
}

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

namespace Lab1_RomanovK.S._BPI_24_01
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
       public ObservableCollection<Rabotnik> workers { get; set; }
        public MainWindow() : this(new ObservableCollection<Rabotnik>())
        {
            
        }

        public MainWindow(ObservableCollection<Rabotnik> incomingWorkers)
        {
            InitializeComponent();

            /* workers.Add(new Rabotnik { Familiya = "Иванов", Dolzhnost = "Слесарь", Oklad = 50000 });
             workers.Add(new Rabotnik { Familiya = "Петров", Dolzhnost = "Токарь", Oklad = 45000 });
             workers.Add(new Rabotnik { Familiya = "Иванченко", Dolzhnost = "Менеджер", Oklad = 60000 });
             workers.Add(new Rabotnik { Familiya = "Сидоров", Dolzhnost = "Электрик", Oklad = 48000 });
             workers.Add(new Rabotnik { Familiya = "Иванова", Dolzhnost = "Бухгалтер", Oklad = 55000 });*/

            workers = incomingWorkers;
            listBoxWorkers.ItemsSource = workers;
            if (App.CurrentTheme == AppTheme.Dark)
                rbDark.IsChecked = true;
            else
                rbLight.IsChecked = true;
        }

        // ============ ТЕМА через RadioButton ============
        private void Theme_Checked(object sender, RoutedEventArgs e)
        {
            if (!(sender is RadioButton rb)) return;
            if (rb.Tag == null) return;

            AppTheme theme = rb.Tag.ToString() == "Dark"
                ? AppTheme.Dark
                : AppTheme.Light;

            App.ApplyTheme(theme);
        }

        // ============ СИСТЕМНЫЕ КНОПКИ ОКНА ============
        private void Minimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void Maximize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState == WindowState.Maximized
                ? WindowState.Normal
                : WindowState.Maximized;
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        // ============ ПЕРЕТАСКИВАНИЕ ОКНА ЗА TITLEBAR ============
        private void TitleBar_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left && e.ClickCount == 2)
            {
                Maximize_Click(sender, e);
            }
            // DragMove() не нужен — WindowChrome сам перетаскивает окно
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


        private void ListBoxWorkers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (listBoxWorkers.SelectedItems.Count == 1 &&
                listBoxWorkers.SelectedItem is Rabotnik selectedWorker)
            {
                txtFamiliya.Text = selectedWorker.Familiya;
                txtDolzhnost.Text = selectedWorker.Dolzhnost;
                txtOklad.Text = selectedWorker.Oklad.ToString();
            }
        }



        private void BtnAdd_Click(object sender, RoutedEventArgs e)
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
            ClearInputs();
            MessageBox.Show("Работник успешно добавлен.");
        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (listBoxWorkers.SelectedItems.Count != 1)
            {
                MessageBox.Show("Для изменения выберите РОВНО одного работника.");
                return;
            }

            if (listBoxWorkers.SelectedItem is Rabotnik selectedWorker)
            {
                if (string.IsNullOrWhiteSpace(txtFamiliya.Text) ||
                    string.IsNullOrWhiteSpace(txtDolzhnost.Text) ||
                    !decimal.TryParse(txtOklad.Text, out decimal oklad))
                {
                    MessageBox.Show("Пожалуйста, заполните все поля корректно. Оклад должен быть числом.");
                    return;
                }

                selectedWorker.Familiya = txtFamiliya.Text;
                selectedWorker.Dolzhnost = txtDolzhnost.Text;
                selectedWorker.Oklad = oklad;

                listBoxWorkers.Items.Refresh();
                MessageBox.Show("Данные работника обновлены.");
            }
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (listBoxWorkers.SelectedItems.Count == 0)
            {
                MessageBox.Show("Выберите работника(ов) из списка для удаления.");
                return;
            }

            int count = listBoxWorkers.SelectedItems.Count;

            string message = count == 1
                ? "Вы действительно хотите уволить выбранного работника?"
                : $"Вы действительно хотите уволить {count} работников?";

            MessageBoxResult result = MessageBox.Show(message, "Подтверждение увольнения", MessageBoxButton.YesNo, MessageBoxImage.Warning);

            if (result == MessageBoxResult.Yes)
            {
                var toDelete = listBoxWorkers.SelectedItems.Cast<Rabotnik>().ToList();

                foreach (var worker in toDelete)
                {
                    workers.Remove(worker);
                }

                ClearInputs();
                MessageBox.Show($"Уволено работников: {count}.");
            }
            else
            {
                MessageBox.Show("Прощены.");
            }
        }

        private void ClearInputs()
        {
            txtFamiliya.Clear();
            txtDolzhnost.Clear();
            txtOklad.Clear();
        }


        private void BtnIncreaseSalary_Click(object sender, RoutedEventArgs e)
        {
            int affectedCount = 0;

            if (listBoxWorkers.SelectedItems.Count == 0)
            {
                foreach (var worker in workers)
                {
                    worker.Oklad *= 1.15m;
                    affectedCount++;
                }

                listBoxWorkers.Items.Refresh();
                MessageBox.Show($"Оклады ВСЕХ работников ({affectedCount} чел.) увеличены на 15%.");
            }
            else
            {
                foreach (var item in listBoxWorkers.SelectedItems)
                {
                    if (item is Rabotnik worker)
                    {
                        worker.Oklad *= 1.15m;
                        affectedCount++;
                    }
                }

                listBoxWorkers.Items.Refresh();
                MessageBox.Show($"Оклады ВЫБРАННЫХ работников ({affectedCount} чел.) увеличены на 15%.");
            }
        }

        private void BtnAssignEngineer_Click(object sender, RoutedEventArgs e)
        {
            int count = 0;
            foreach (var worker in workers)
            {
                if (worker.Familiya.StartsWith("Иван", StringComparison.OrdinalIgnoreCase))
                {
                    worker.Dolzhnost = "Инженер";
                    count++;
                }
            }
            listBoxWorkers.Items.Refresh();

            if (count > 0)
                MessageBox.Show($"Должность 'Инженер' присвоена {count} работникам.");
            else
                MessageBox.Show("Работников с фамилией, начинающейся на 'Иван', не найдено.");
        }
    }
}

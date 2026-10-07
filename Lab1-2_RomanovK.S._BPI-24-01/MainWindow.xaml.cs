using System;
using System.Collections.Generic;
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

namespace Lab1_2_RomanovK.S._BPI_24_01
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ArcsinFunction arcsin;
        private ArccosFunction arccos;

        public MainWindow()
        {
            InitializeComponent();

            arcsin = new ArcsinFunction();
            arccos = new ArccosFunction();

            txtInput.Focus();
        }

        private void TxtInput_LostFocus(object sender, RoutedEventArgs e)
        {
            PerformCalculation(showMessageBox: false);
        }

        private void BtnCalculate_Click(object sender, RoutedEventArgs e)
        {
            PerformCalculation(showMessageBox: true);
        }

        private void PerformCalculation(bool showMessageBox)
        {
            txtOutput.Text = "";

            if (!double.TryParse(txtInput.Text, out double x))
            {
                string error = "Пожалуйста, введите корректное число.";
                txtOutput.Text = $"Ошибка: {error}";
                if (showMessageBox)
                    MessageBox.Show(error, "Ошибка ввода", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                txtOutput.Text += $"=== Результаты для числа x = {x} ===\n\n";

                arcsin.X = x;
                arccos.X = x;

                txtOutput.Text += $"{arcsin.Name}:\n";
                txtOutput.Text += $"   Значение: {arcsin.Calculate():F2}\n\n";

                txtOutput.Text += $"{arccos.Name}:\n";
                txtOutput.Text += $"   Значение: {arccos.Calculate():F2}\n\n";

                Function arcsinDeriv = arcsin.GetDerivative();
                Function arccosDeriv = arccos.GetDerivative();

                arcsinDeriv.X = x;
                arccosDeriv.X = x;

                txtOutput.Text += $"{arcsinDeriv.Name}:\n";
                txtOutput.Text += $"   Значение: {arcsinDeriv.Calculate():F2}\n\n";

                txtOutput.Text += $"{arccosDeriv.Name}:\n";
                txtOutput.Text += $"   Значение: {arccosDeriv.Calculate():F2}\n\n";
            }
            catch (ArgumentOutOfRangeException ex)
            {
                string error = ex.Message;
                txtOutput.Text = $"Ошибка: {error}";
                if (showMessageBox)
                    MessageBox.Show(error, "Ошибка диапазона", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                string error = $"Произошла ошибка: {ex.Message}";
                txtOutput.Text = error;
                if (showMessageBox)
                    MessageBox.Show(error, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BtnInfo_Click(object sender, RoutedEventArgs e)
        {
            txtOutput.Text = "";
            txtOutput.Text += "=== Информация о программе ===\n\n";
            txtOutput.Text += "Эта программа вычисляет значения арксинуса и арккосинуса, а также их производных.\n\n";

            txtOutput.Text += "Описание функций:\n";
            txtOutput.Text += $"— {arcsin.Describe()}\n";
            txtOutput.Text += $"— {arccos.Describe()}\n\n";

            txtOutput.Text += "Демонстрация перегрузки метода:\n";
            txtOutput.Text += $"— {arcsin.Describe(true)}\n";
            txtOutput.Text += $"— {arcsin.Describe("-> ")}\n";
        }
    }
}

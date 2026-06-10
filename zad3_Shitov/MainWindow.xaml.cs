using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace zad3_Shitov
{
    public partial class MainWindow : Window
    {
        // Две коллекции
        private ObservableCollection<RoadWork.RoadWork> roadWorks = new ObservableCollection<RoadWork.RoadWork>();
        private List<RoadWork.ReinforcedRoadWork> reinforcedRoadWorks = new List<RoadWork.ReinforcedRoadWork>();

        public MainWindow()
        {
            InitializeComponent();

            lstRoadWorks.ItemsSource = roadWorks;
            lstReinfRoads.ItemsSource = reinforcedRoadWorks;
        }

        //Добавление обычной дороги
        private void btnAddRoadWork_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtContractor.Text))
                {
                    MessageBox.Show("Введите название организатора!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                double width = double.Parse(txtWidth.Text);
                double length = double.Parse(txtLength.Text);
                double mass = double.Parse(txtMass.Text);
                int workersCount = int.Parse(txtWorkersCount.Text);

                // Проверки
                if (width <= 1 || width > 100)
                {
                    MessageBox.Show("Ширина должна быть от 1 до 50 м!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
                if (length <= 100 || length > 100000)
                {
                    MessageBox.Show("Длина должна быть от 100 до 100000 м!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
                if (mass <= 1 || mass > 1000)
                {
                    MessageBox.Show("Масса должна быть от 1 до 1000 кг/м²!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
                if (workersCount <= 1 || workersCount > 50)
                {
                    MessageBox.Show("Количество рабочих должно быть от 1 до 50!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                roadWorks.Add(new RoadWork.RoadWork(width, length, mass, txtContractor.Text, workersCount));
                UpdateStats();
                ClearRoadForm();
            }
            catch (FormatException)
            {
                MessageBox.Show("Введите корректные числовые значения!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        //Удаление выбранной дороги
        private void btnDeleteSelectedRoad_Click(object sender, RoutedEventArgs e)
        {
            if (lstRoadWorks.SelectedItem is RoadWork.RoadWork selected)
                roadWorks.Remove(selected);
            UpdateStats();
        }
        private void btnDeleteSelectedReinRoad_Click(object sender, RoutedEventArgs e)
        {
            if (lstReinfRoads.SelectedItem is RoadWork.ReinforcedRoadWork selected)
            {
                reinforcedRoadWorks.Remove(selected);
                lstReinfRoads.Items.Refresh();
                UpdateStats(); 
            }
            else
            {
                MessageBox.Show("Выберите объект для удаления!", "Внимание",
                                MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        // Очистка всех дорог
        private void btnClearAllRoad_Click(object sender, RoutedEventArgs e)
        {
            roadWorks.Clear();
            UpdateStats();
        }

        //Добавление укреплённой дороги
        private void btnAddReinfRoad_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtReinfContractor.Text))
                {
                    MessageBox.Show("Введите название организатора!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                double width = double.Parse(txtReinfWidth.Text);
                double length = double.Parse(txtReinfLength.Text);
                double mass = double.Parse(txtReinfMass.Text);
                int workersCount = int.Parse(txtReinfWorkersCount.Text);
                int coefficient = int.Parse((cmbCoefficient.SelectedItem as ComboBoxItem).Content.ToString());
                string type = (cmbType.SelectedItem as ComboBoxItem).Content.ToString();
                double price = double.Parse(txtPrice.Text);

                // Проверки
                if (width <= 1 || width > 50)
                {
                    MessageBox.Show("Ширина должна быть от 1 до 50 м!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
                if (length <= 10 || length > 100000)
                {
                    MessageBox.Show("Длина должна быть от 10 до 100000 м!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
                if (mass <= 1 || mass > 1000)
                {
                    MessageBox.Show("Масса должна быть от 1 до 1000 кг/м²!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
                if (workersCount <= 1 || workersCount > 50)
                {
                    MessageBox.Show("Количество рабочих должно быть от 1 до 50!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
                if (price <= 100 || price > 10000)
                {
                    MessageBox.Show("Стоимость должна быть от 100 до 10000 тыс.руб!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                reinforcedRoadWorks.Add(new RoadWork.ReinforcedRoadWork(width, length, mass, txtReinfContractor.Text,
                    workersCount, coefficient, type, price));
                lstReinfRoads.Items.Refresh();
                UpdateStats();
                ClearReinfForm();
            }
            catch (FormatException)
            {
                MessageBox.Show("Введите корректные числовые значения!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        //Удаление укреплённой дороги
        private void btnDeleteSelectedReinf_Click(object sender, RoutedEventArgs e)
        {
            if (lstReinfRoads.SelectedItem is RoadWork.ReinforcedRoadWork selected)
                reinforcedRoadWorks.Remove(selected);
            lstReinfRoads.Items.Refresh();
        }

        // Очистка всех укреплённых дорог
        private void btnClearAllReinf_Click(object sender, RoutedEventArgs e)
        {
            reinforcedRoadWorks.Clear();
            lstReinfRoads.Items.Refresh();
            UpdateStats();
        }

        // Очистка формы обычной дороги
        private void ClearRoadForm()
        {
            txtContractor.Text = txtWidth.Text = txtLength.Text = txtMass.Text = txtWorkersCount.Text = "";
        }

        // Очистка формы укреплённой дороги
        private void ClearReinfForm()
        {
            txtReinfContractor.Text = txtReinfWidth.Text = txtReinfLength.Text = txtReinfMass.Text =
            txtReinfWorkersCount.Text = txtPrice.Text = "";
            cmbCoefficient.SelectedIndex = 2;
            cmbType.SelectedIndex = 0;
        }

        // Статистика через LINQ
        private void UpdateStats()
        {
            string stats = "";

            if (roadWorks.Count > 0)
            {
                stats += $"ДОРОЖНЫЕ РАБОТЫ:\n";
                stats += $"Всего: {roadWorks.Count}\n";
                stats += $"Сумма Q: {roadWorks.Sum(x => x.Quality()):F2}\n";
                stats += $"Макс Q: {roadWorks.Max(x => x.Quality()):F2}\n\n";
            }

            if (reinforcedRoadWorks.Count > 0)
            {
                stats += $"УКРЕПЛЁННЫЕ ДОРОГИ:\n";
                stats += $"Всего: {reinforcedRoadWorks.Count}\n";
                stats += $"Сумма Qp: {reinforcedRoadWorks.Sum(x => x.Quality()):F2}\n";
                stats += $"Макс Qp: {reinforcedRoadWorks.Max(x => x.Quality()):F2}\n";
            }

            txtStats.Text = stats == "" ? "Нет данных" : stats;
        }

        /*// Очистка формы
        private void btnClearRoadWork_Click(object sender, RoutedEventArgs e) => ClearRoadForm();
        private void btnClearReinfForm_Click(object sender, RoutedEventArgs e) => ClearReinfForm();
*/
        private void btnTestAddOverload_Click(object sender, RoutedEventArgs e)
        {
            var test = new RoadWork.RoadWork(0, 0, 0, "", 0);
            test.AddData(10, 10, 100, "Тест", 10);
            roadWorks.Add(test);
            UpdateStats();
            MessageBox.Show(test.GetInfo());
        }

        private void btnTestClearData_Click(object sender, RoutedEventArgs e)
        {
            if (lstRoadWorks.SelectedItem is RoadWork.RoadWork selected)
            {
                selected.ClearData();
                lstRoadWorks.Items.Refresh();
                UpdateStats();
            }
        }
    }
}

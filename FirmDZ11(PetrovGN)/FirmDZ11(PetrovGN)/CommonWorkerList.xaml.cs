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

namespace FirmDZ11_PetrovGN_
{
    /// <summary>
    /// Логика взаимодействия для CommonWorkerList.xaml
    /// </summary>
    public partial class CommonWorkerList : Window
    {
        ObservableCollection<Worker> workers;
        public CommonWorkerList(ObservableCollection<Worker> list)
        {
            InitializeComponent();
            if (list.Count == 0)
                MessageBox.Show("Записей не найдено");
            
            workers = list;
            dtWorkers.ItemsSource = workers;
        }

        /// <summary>
        /// Показывает сотрудника с самой маленькой зп
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnLowerSalary_Click(object sender, RoutedEventArgs e)
        {
            Worker rez = workers[0];
            foreach (var i in workers)
            {
                if (i.Salary <= rez.Salary)
                    rez = i;
            }

            MessageBox.Show(rez.WorkerInfo());
        }


        /// <summary>
        /// Показывает сотрудника с самой большой ЗП
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnHigherSalary_Click(object sender, RoutedEventArgs e)
        {
            Worker rez = workers[0];
            foreach (var i in workers)
            {
                if (i.Salary >= rez.Salary)
                    rez = i;
            }

            MessageBox.Show(rez.WorkerInfo());
        }

        /// <summary>
        /// Показ сообщения о среденйЗП
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnMediumSalary_Click(object sender, RoutedEventArgs e)
        {
            if (workers.Count == 0)
            {
                MessageBox.Show("Список сотрудников пуст");
            }
            else
            {
                int rez = 0;

                foreach (var i in workers)
                {
                    rez += i.Salary;
                }

                rez = rez / workers.Count;
                MessageBox.Show($"Средняя ЗП {rez} рупий");
            }
        }

        /// <summary>
        /// Сортировка по параметрам
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnSort_Click(object sender, RoutedEventArgs e)
        {
            if (radID.IsChecked == true) //сортировка по ID
            {
                dtWorkers.ItemsSource = from t in workers
                                        orderby t.WorkerID
                                        select t;
            }
            else if (radName.IsChecked == true) // сортировка по фамилии
            {
                dtWorkers.ItemsSource = from t in workers
                                        orderby t.LastName
                                        select t;
            }
            else  // сортировка по ЗП
            {
                dtWorkers.ItemsSource = from t in workers
                                        orderby t.Salary
                                        select t;
            }
        }

        /// <summary>
        /// показать всех директоров
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnShowDirector_Click(object sender, RoutedEventArgs e)
        {
            dtWorkers.ItemsSource = from t in workers
                                    where t is Director
                                    select t;
        }
    }
}

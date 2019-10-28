using System;
using System.Diagnostics;
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

namespace FirmDZ11_PetrovGN_
{
    /// <summary>
    /// Логика взаимодействия для PageAddWorker.xaml
    /// </summary>
    public partial class PageAddWorker : Page
    {
        public PageAddWorker()
        {
            InitializeComponent();
            ID.Text = MainWindow.winReference.MainDepartment[0].FindFreeWorkerID().ToString();
        }               

        /// <summary>
        /// Выбор радио копки сокрытие полей
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RadWorker_Click(object sender, RoutedEventArgs e)
        {
            MasterID.Visibility = Visibility.Hidden;
            lblMaster.Visibility = Visibility.Hidden;
        }
        /// <summary>
        /// Выбор радио копки открытие полей
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RadIntern_Click(object sender, RoutedEventArgs e)
        {
            MasterID.Visibility = Visibility.Visible;
            lblMaster.Visibility = Visibility.Visible;
        }

        /// <summary>
        /// добавление нового сотрудника к выбранному департаменту
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // проверяем выбран ли в дереве департамент
                if (((TreeViewItem)MainWindow.winReference.MainTree.SelectedItem).Tag is Department)
                {

                    if (radWorker.IsChecked == true)
                    {
                        //Создаем штатного
                        Employee temp = new Employee(Convert.ToInt32(ID.Text), newName.Text, Last.Text);  //новый сотрудник
                        Department capturer = ((TreeViewItem)MainWindow.winReference.MainTree.SelectedItem).Tag as Department; // принимающий департамент
                        capturer.workers.Add(temp); // добавляем к списку
                        ID.Text = MainWindow.winReference.MainDepartment[0].FindFreeWorkerID().ToString(); // генерируем новый ИД
                        newName.Text = string.Empty;  // отчищаем                        
                        Last.Text = string.Empty;
                    }
                    else// создаем стажера
                    {
                        Intern temp = new Intern(Convert.ToInt32(ID.Text), newName.Text, Last.Text,Convert.ToInt32(MasterID.Text));  //новый сотрудник
                        Department capturer = ((TreeViewItem)MainWindow.winReference.MainTree.SelectedItem).Tag as Department; // принимающий департамент
                        capturer.workers.Add(temp); // добавляем к списку
                        ID.Text = MainWindow.winReference.MainDepartment[0].FindFreeWorkerID().ToString(); // генерируем новый ИД
                        newName.Text = string.Empty;  // отчищаем                        
                        Last.Text = string.Empty;
                    }                    
                }
                else  // если выбран сотрудник. то месадж
                {
                    MessageBox.Show("Выбран не депортамент!");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        private void BtnClear_Click(object sender, RoutedEventArgs e)
        {
            ID.Text = MainWindow.winReference.MainDepartment[0].FindFreeWorkerID().ToString();
            newName.Text = string.Empty;
            Last.Text = string.Empty;
        }
    }
}

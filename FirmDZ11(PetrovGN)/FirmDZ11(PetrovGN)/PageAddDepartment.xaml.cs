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
    /// Логика взаимодействия для PageAddDepartment.xaml
    /// </summary>
    public partial class PageAddDepartment : Page
    {
        public PageAddDepartment()
        {
            InitializeComponent();
            DepID.Text = MainWindow.winReference.MainDepartment[0].FindFreeDepartmentID().ToString();
        }

        /// <summary>
        /// отчистка формы
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnClear_Click(object sender, RoutedEventArgs e)
        {
            DepID.Text = MainWindow.winReference.MainDepartment[0].FindFreeDepartmentID().ToString();
            DepName.Text = string.Empty;
            DirName.Text = string.Empty;
            DirLast.Text = string.Empty;
        }

        /// <summary>
        /// Добавление нового депортамента в дерево
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            //Department temp = new Department(Convert.ToInt32(DepID.Text), 777, DirName.Text, DirLast.Text, DepName.Text);
            try
            {
                // проверяем выбран ли в дереве департамент
                if (((TreeViewItem)MainWindow.winReference.MainTree.SelectedItem).Tag is Department)  
                {
                    // новый экземпляр департамента
                    Department temp = new Department(Convert.ToInt32(DepID.Text), MainWindow.winReference.MainDepartment[0].FindFreeWorkerID(),
                        DirName.Text, DirLast.Text, DepName.Text);  
                    // выбираем список депортамента из selectedItem
                    Department capturer = ((TreeViewItem)MainWindow.winReference.MainTree.SelectedItem).Tag as Department;
                    capturer.Departments.Add(temp);  // добавляем

                    DepID.Text = MainWindow.winReference.MainDepartment[0].FindFreeDepartmentID().ToString();  
                    DepName.Text = string.Empty;  // отчищаем
                    DirName.Text = string.Empty;
                    DirLast.Text = string.Empty;
                }
                else  // если сотрудник. то месадж
                {
                    MessageBox.Show("Выбран не депортамент!");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            //MainWindow.winReference.MainTree.R
        }
    }
}

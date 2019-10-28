using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
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
using Newtonsoft.Json.Linq;

namespace FirmDZ11_PetrovGN_
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //PageAddWorker pageAddWorker;
        PageAddDepartment pageAddDepartment;
        public ObservableCollection<Department> MainDepartment;  // Корневой каталог фирмы

        public static MainWindow winReference { get; private set; }  // Это ссылка на MainWindow
        public MainWindow()
        {
            InitializeComponent();
            MessageBox.Show("Удаление в контекстном меню правой таблицы");

            winReference = this; // для доступа извне ссылка на окно
            //начальная дата-инициализация
            string file = string.Empty;
            if (File.Exists("JSONSave.json"))  //наличие файла сохранения
            {               
                JToken jt = JToken.Parse(File.ReadAllText("JSONSave.json"));
                MainDepartment = new ObservableCollection<Department>();
                MainDepartment.Add(Department.DeserializeJson(jt));  // длбавляем десериализованный объект
            }
            else   
            {
                // Если файла нет. то инициализируем 1 объект корень фирмы
                MainDepartment = CommonMethods.CreateTempDep();
            }             

            //Инициализация первого элемента в дерево
            //Инициализируктся только 1й объект department
            //Остальные будут отображаться когда будет событие разворачивания дерева
            foreach (var i in MainDepartment)
            {                
                TreeViewItem item = new TreeViewItem();  // элекмент дерева
                item.Tag = i;  // содержание т.е. объект (он по идее 1)
                item.Header = string.Concat(i.DepName + " " + i.DID);  // текст для оотбражения
                item.Items.Add("*");  // то что будет в содержаниии . сгенерируется при событии
                MainTree.Items.Add(item);  // добавление этого всего
            }
        }

        /// <summary>
        /// Вызов формы добавления департамента
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnAddDepartment_Click(object sender, RoutedEventArgs e)
        {
            pageAddDepartment =  new PageAddDepartment(); // отображение страницы добавлнеия
            Frame1.Content = pageAddDepartment;
        }


        /// <summary>
        /// Обраотчик выбора элемента в дереве
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainTree_Expanded(object sender, RoutedEventArgs e)
        {
            TreeViewItem item = (TreeViewItem)e.OriginalSource;
            Department dep = item.Tag as Department;

            item.Items.Clear();
            
            txtDir.Text = dep.GetDirector();    
            foreach (var i in dep.Departments)  // Строки для департамента
            {
                TreeViewItem newItem = new TreeViewItem();
                newItem.Tag = i;
                newItem.Header = i.DepInfo();
                newItem.Items.Add("*");
                item.Items.Add(newItem);
            }

            // отображение руководителя в списке
            TreeViewItem newHeader = new TreeViewItem();
            newHeader.Tag = dep.director;
            newHeader.Header = "Руководитель: " + dep.director.FirstName + " " + dep.director.LastName;
            item.Items.Add(newHeader);

            foreach (var i in dep.workers)  // Строки для сотрудника
            {
                TreeViewItem newItem = new TreeViewItem();
                newItem.Header = i.FirstName + " " + i.LastName;
                newItem.Tag = i;
                item.Items.Add(newItem);
            }
        }

        /// <summary>
        /// Отображение данных о выбранном пользователе или разделе
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainTree_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {


            if (((TreeViewItem)e.NewValue).Tag is Department)  // если выбран департамент
            {
                // дата гриду присваиваем источик
                dgWorkers.ItemsSource = (((TreeViewItem)e.NewValue).Tag as Department).workers;
                
                // отображаем справа информацию о директоре
                txtDir.Text = (((TreeViewItem)e.NewValue).Tag as Department).GetDirector();
                // отображаем грид и скрываем инфо о работнике
                viewWorker.Visibility = Visibility.Hidden;
                dgWorkers.Visibility = Visibility.Visible;
            }
            else  // если работник
            {
                txtDir.Text = string.Empty;
                TreeViewItem item = (TreeViewItem)e.NewValue;
                //показываем лист и скрываем грид
                dgWorkers.Visibility = Visibility.Hidden;
                viewWorker.Visibility = Visibility.Visible;

                if (item.Tag is Employee)  // отображение штатного
                {
                    Employee wrk = item.Tag as Employee;


                    viewWorker.Items.Clear();
                    viewWorker.Items.Add(new TextBlock() { Text = $"ID Работника {wrk.WorkerID}" });
                    viewWorker.Items.Add(new TextBlock() { Text = $"Имя {wrk.FirstName}" });
                    viewWorker.Items.Add(new TextBlock() { Text = $"Фамилия {wrk.LastName}" });
                    viewWorker.Items.Add(new TextBlock() { Text = $"ЗП {wrk.Salary}" });
                    viewWorker.Items.Add(new TextBlock() { Text = $"Отработано {wrk.HoursWorked}" });
                }
                else if (item.Tag is Intern) // оторажение интерна
                {
                    Intern wrk = item.Tag as Intern;


                    viewWorker.Items.Clear();
                    viewWorker.Items.Add(new TextBlock() { Text = $"ID Работника {wrk.WorkerID}" });
                    viewWorker.Items.Add(new TextBlock() { Text = $"Имя {wrk.FirstName}" });
                    viewWorker.Items.Add(new TextBlock() { Text = $"Фамилия {wrk.LastName}" });
                    viewWorker.Items.Add(new TextBlock() { Text = $"ЗП {wrk.Salary}" });
                    viewWorker.Items.Add(new TextBlock() { Text = $"ID наставника {wrk.Mentor}" });
                }
                else // отображение директора
                {
                    Director wrk = item.Tag as Director;


                    viewWorker.Items.Clear();
                    viewWorker.Items.Add(new TextBlock() { Text = $"ID Директора {wrk.WorkerID}" });
                    viewWorker.Items.Add(new TextBlock() { Text = $"Имя {wrk.FirstName}" });
                    viewWorker.Items.Add(new TextBlock() { Text = $"Фамилия {wrk.LastName}" });
                    viewWorker.Items.Add(new TextBlock() { Text = $"ЗП {wrk.Salary}" });
                }
            }
        }

        /// <summary>
        /// Нажатие кнопки добавления сотрудника
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnAddWorker_Click(object sender, RoutedEventArgs e)
        {
            PageAddWorker pageAddWorker = new PageAddWorker();
            Frame1.Content = pageAddWorker;  // показываем страницу
        }
        /// <summary>
        /// Начисление ЗП
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnAddSalary_Click(object sender, RoutedEventArgs e)
        {
            // часы начисляются всем сотрудникам одинаково. Зп считается на исходя из типа работника
            if (int.TryParse(txtHours.Text, out int hours)) // перобразование текстового поля
            {
                MainDepartment[0].AddHours(hours);   // добавление часов
                MainDepartment[0].CalculateZP();    // вычисление ЗП на основе часов
            }
            else
            {
                MessageBox.Show("Неверный ввод количества часов");
                txtHours.Text = string.Empty;
            }

        }

        /// <summary>
        /// сохранение файла
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            string file = MainDepartment[0].SerializeJson().ToString();
            File.WriteAllText("JSONSave.json", file);
        }

        /// <summary>
        /// Показать результаты поиска в базе
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnSearch_Click(object sender, RoutedEventArgs e)
        {
            string request = txtSearch.Text;            
            viewWorker.Visibility = Visibility.Hidden;
            dgWorkers.Visibility = Visibility.Visible;
            dgWorkers.ItemsSource = MainDepartment[0][request];  // реализован индексатор через строку или число
        }

        /// <summary>
        /// Показать в отдельном окне сотрудников департамента
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnShowAll_Click(object sender, RoutedEventArgs e)
        {
            //string input = xtxDep.Text
            bool flag = int.TryParse(xtxDep.Text, out int ID);
            //Debug.WriteLine(txtDir.Text + " " + ID);
            //&& MainDepartment[0].GetWorkersByDep(ID).Count != 0
            if (flag)
            {
                ObservableCollection<Worker> arr = MainDepartment[0].GetWorkersByDep(ID);
                CommonWorkerList common = new CommonWorkerList(arr);
                common.Show();                          
            }
            else
            {
                CommonWorkerList common = new CommonWorkerList(MainDepartment[0][""]);
                common.Show();
            }
        }
        /// <summary>
        /// Удаление сотрудника(контекстное меню)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnDeleteWorker_Click(object sender, RoutedEventArgs e)
        {
            MainDepartment[0].RemoveWorker((dgWorkers.SelectedItem as Worker));
            MessageBox.Show("Сотрудник удален.");
        }
    }
}

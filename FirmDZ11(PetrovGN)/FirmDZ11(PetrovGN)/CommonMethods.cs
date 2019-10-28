using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirmDZ11_PetrovGN_
{
    /// <summary>
    /// Статические методы загрузки выгрузки
    /// </summary>
    public sealed class CommonMethods
    {
        /// <summary>
        /// Конвертер представления департамента в дерево
        /// </summary>
        public static ObservableCollection<Department> CreateTempDep()
        {

            ObservableCollection<Department> temp = new ObservableCollection<Department>();
            temp.Add(new Department(1, 1, "Петров", "Петров", "Фирма"));

            //Department temp = new Department(0, 0, "Петров", "Петров", "Фирма");           
            //temp.AddWorker(02, "1c", "1c", true);
            //temp.AddWorker(03, "2c", "2c", true);
            //temp.AddWorker(04, "3c", "3c", false);

            //temp.Departments.Add(new Department(1, 6, "Иван", "Иванов"));
            //temp.Departments[0].AddWorker(10, "3c", "3c", true);
            //temp.Departments[0].AddWorker(11, "4c", "4c", true);
            //temp.Departments[0].AddWorker(12, "5c", "5c", true);

            //temp.Departments.Add(new Department(3, 76, "Марина", "Кари"));
            //temp.Departments[0].AddWorker(10, "3c", "3c", true);
            //temp.Departments[0].AddWorker(11, "4c", "4c", true);
            //temp.Departments[0].AddWorker(12, "5c", "5c", true);
                        

            return temp;

        }

    }
}

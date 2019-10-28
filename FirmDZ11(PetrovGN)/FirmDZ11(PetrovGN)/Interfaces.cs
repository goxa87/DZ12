using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirmDZ11_PetrovGN_
{
    interface IWorker
    {
        /// <summary>
        /// Уникальный идентификатор работника
        /// </summary>
        int WorkerID { get; set; }
        /// <summary>
        /// Зарплата сотрудника
        /// </summary>
        int Salary { get; set; }
        /// <summary>
        /// Имя сотрудника
        /// </summary>
        string FirstName { get; set; }
        /// <summary>
        /// Фамилия сотрудника
        /// </summary>
        string LastName { get; set; }
        /// <summary>
        /// Начисление зарплаты
        /// </summary>
        void AddZP();
        /// <summary>
        /// Перемещение в департамент
        /// </summary>
        void MoveToDepartment(int depId);
    }

    interface IDirector
    {
      // уникальные методы для руководителя 
    }

    interface IEmployee
    {
        /// <summary>
        /// Количество отработанных часов
        /// </summary>
        int HoursWorked { get; set; }
    }

    interface IIntern
    {
        /// <summary>
        /// ID наставника
        /// </summary>
        int Mentor { get; set; }

        /// <summary>
        /// Повышение стажера до штатного сотрудника
        /// </summary>
        void Increase();
    }

    interface IDepartment
    {
        /// <summary>
        /// Идентификатор депортамента
        /// </summary>
        int DID { get; set; }

        /// <summary>
        /// ID руководителя отдела
        /// </summary>
        int DirectorID { get; set; }

        /// <summary>
        /// Вложенные поддепартапменты
        /// </summary>
        ObservableCollection<Department> Departments { set; get; }
    }
}

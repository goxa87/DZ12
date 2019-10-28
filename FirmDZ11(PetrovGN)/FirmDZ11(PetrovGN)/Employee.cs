using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirmDZ11_PetrovGN_
{

    /// <summary>
    /// Представляет штатного сотрудника
    /// </summary>
    public class Employee : Worker, IEmployee
    {
        /// <summary>
        /// инициализация
        /// </summary>
        /// <param name="ID">идентификатор</param>
        /// <param name="first">Имя</param>
        /// <param name="last">Фамилия</param>
        public Employee(int ID, string first, string last)
        {
            this.WorkerID = ID;
            this.FirstName = first;
            this.LastName = last;
            this.HoursWorked = 0;
            this.Salary = 0;
        }

        /// <summary>
        /// Отработанные часы
        /// </summary>
        public int HoursWorked { get;set; }

        /// <summary>
        /// Расчет ЗП исходя из часов
        /// </summary>
        public override void AddZP()
        {
            this.Salary = 15 * this.HoursWorked;
        }

        /// <summary>
        /// Онформация в виде строки
        /// </summary>
        /// <returns></returns>
        public override string WorkerInfo()
        {
            return $"ID сотрудника: {WorkerID}. Имя: {FirstName}. Фамилия: {LastName}. Отработано {HoursWorked} часов. ЗП {Salary}";
        }
    }
}

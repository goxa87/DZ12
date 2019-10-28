using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirmDZ11_PetrovGN_
{
    /// <summary>
    /// представляет руководителя отдела или фирмы
    /// </summary>
    public class Director : Worker, IDirector
    {
        /// <summary>
        /// Начисление зарплаты (15% от ЗП подчиненных сотрудников)
        /// </summary>
        public override void AddZP() { }

        /// <summary>
        /// Инициализация
        /// </summary>
        /// <param name="ID">id</param>
        /// <param name="first">имя</param>
        /// <param name="last">фамилия</param>
        public Director(int ID, string first, string last)
        {
            this.WorkerID = ID;
            this.FirstName = first;
            this.LastName = last;
            this.Salary = 0;            
        }

        /// <summary>
        /// конструктор по умолчанию
        /// </summary>
        public Director() : this(0, "", "")
        { }

        /// <summary>
        /// Онформация в виде строки
        /// </summary>
        /// <returns></returns>
        public override string WorkerInfo()
        {
            return $"ID сотрудника: {WorkerID}. Имя: {FirstName}. Фамилия: {LastName}. ЗП {Salary}";
        }

    }
}

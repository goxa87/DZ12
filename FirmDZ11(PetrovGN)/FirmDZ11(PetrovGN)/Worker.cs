using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirmDZ11_PetrovGN_
{
    public abstract class Worker : IWorker,IComparer<Worker>,IEquatable<Worker>
    {
        /// <summary>
        /// Уникальный идентификатор работника
        /// </summary>
        public int WorkerID { get; set; }
        /// <summary>
        /// Зарплата сотрудника
        /// </summary>
        public int Salary { get; set; }
        /// <summary>
        /// Имя сотрудника
        /// </summary>
        public string FirstName { get; set; }
        /// <summary>
        /// Фамилия сотрудника
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Начисление зарплаты
        /// </summary>
        public abstract void AddZP();

        /// <summary>
        /// Сравнение 2 работников по фамилии
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public int Compare(Worker x, Worker y)
        {
            if (x.LastName.CompareTo(y.LastName) == 0)
                return 0;
            else if (x.LastName.CompareTo(y.LastName) == 1)
                return 1;
            else
                return -1;
        }


        /// <summary>
        /// Сравнивает 2 сотрудника по фамлии и имени если и то и другое совпадет то true
        /// </summary>
        /// <param name="other">объект для сравнения</param>
        /// <returns></returns>
        public bool Equals(Worker other)
        {
            if (this.FirstName == other.FirstName && this.LastName == other.LastName)
                return true;
            else
                return
                    false;
        }

        /// <summary>
        /// Перемещение работника в новый департамент
        /// </summary>
        public void MoveToDepartment(int depId)
        {
            
        }

        /// <summary>
        /// получение информации о сотруднике в виде строки
        /// </summary>
        /// <returns></returns>
        public virtual string WorkerInfo()
        {
            return $"{this.WorkerID}: {this.LastName} {this.FirstName} ЗП{this.Salary}";
        }
    }
}

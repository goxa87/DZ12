using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirmDZ11_PetrovGN_
{
    /// <summary>
    /// представляет стажера- сотрудника
    /// </summary>
    class Intern : Worker, IIntern
    {
        /// <summary>
        /// инициализация
        /// </summary>
        /// <param name="ID">Идентификатор</param>
        /// <param name="first">имя</param>
        /// <param name="last">фамилия</param>
        /// <param name="MentorID">ид ментора</param>
        public Intern(int ID, string first, string last, int MentorID)
        {
            this.WorkerID = ID;
            this.FirstName = first;
            this.LastName = last;
            this.Mentor = MentorID;
            this.Salary = 0;
        }

        /// <summary>
        /// ID наставника
        /// </summary>
        public int Mentor { get ; set; }

        /// <summary>
        /// Начисление фиксированной ЗП
        /// </summary>
        public override void AddZP()
        {
            this.Salary = 500;
        }
        /// <summary>
        /// Повышение. Перевод в штатные сотрудники
        /// </summary>
        public void Increase()
        {
            // пока не реализован
        }

        /// <summary>
        /// Онформация в виде строки
        /// </summary>
        /// <returns></returns>
        public override string WorkerInfo()
        {
            return $"ID сотрудника: {WorkerID}. Имя: {FirstName}. Фамилия: {LastName}. Ментор {Mentor}. ЗП {Salary}";
        }
    }
}

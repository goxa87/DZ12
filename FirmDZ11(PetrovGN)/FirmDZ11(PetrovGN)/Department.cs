using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.IO;
using System.Diagnostics;

namespace FirmDZ11_PetrovGN_
{
    /// <summary>
    /// класс отображает основную структурную единицу организации
    /// </summary>
    public class Department : IDepartment
    {
        /// <summary>
        /// идентификатор департамента
        /// </summary>
        public int DID { get; set; }

        /// <summary>
        /// название отдела
        /// </summary>
        public string DepName { get; set; }

        /// <summary>
        /// Вложенные поддепартапменты
        /// </summary>
        public ObservableCollection<Department> Departments { get; set; }
        /// <summary>
        /// Список сотрудников этого отдела
        /// </summary>
        public ObservableCollection<Worker> workers { get; set; }
        /// <summary>
        /// ID руководителя отдела
        /// </summary>
        public int DirectorID { get; set; }

        /// <summary>
        /// Директор этого отдела
        /// </summary>
        public Director director { get; set; }

        /// <summary>
        /// Инициализация (создается департамент вместе с реководителем)
        /// </summary>
        /// <param name="ID">id отдела</param>
        /// <param name="DirID">id директора</param>
        /// <param name="dirName">имя директора</param>
        /// <param name="dirLastName">фамилия директора</param>
        /// <param name="name"> название отдела</param>
        public Department(int ID, int DirID, string dirName = "noname", string dirLastName = "NoName", string name = "DepNoName")
        {
            this.DID = ID;
            this.DepName = name;
            this.DirectorID = DirID;
            director = new Director(DirID, dirName, dirLastName);
            Departments = new ObservableCollection<Department>();
            workers = new ObservableCollection<Worker>();
        }

        /// <summary>
        /// инициализация поумолчанию
        /// </summary>
        public Department() : this(0, 0)
        {

        }

        /// <summary>
        /// Возвращает строку о директоре
        /// </summary>
        /// <returns></returns>
        public string GetDirector()
        {
            return $"Руководитель: {director.WorkerID} {director.LastName} {director.LastName} {director.Salary}рублей.";
        }

        /// <summary>
        /// возвращает строку информации о депортаменте
        /// </summary>
        /// <returns></returns>
        public string DepInfo()
        {
            return $"ID-{this.DID}: {this.DepName} кол. подотделов: {this.Departments.Count} " +
                $"сотрудников: {this.workers.Count}";
        }

        /// <summary>
        /// Добавление сотрудника в этот отдел (метод не применяется)
        /// </summary>
        /// <param name="id">Идентификатор</param>
        /// <param name="first">Имя сотрудника</param>
        /// <param name="last"> Фамилия сотрудника</param>
        /// <param name="employee"> true - штатный сотрудник, False - стажер </param>
        public void AddWorker(int id, string first, string last, bool employee)
        {
            if (employee)
            {
                workers.Add(new Employee(id, first, last));
            }
            else
            {
                workers.Add(new Intern(id, first, last, 0001));
            }
        }

        /// <summary>
        /// Определяет, есть ли такой id уже в базе(рекурсия)
        /// </summary>
        /// <param name="n">поиск n<0/param>
        /// <returns></returns>
        public bool FindID(int n)
        {
            bool flag = false;
            if (DirectorID == n)  // сравнение с ИД директора
                return true;
            foreach (var e in workers) // проверка списка сотрудников текущего департамента
            {
                if ((e.WorkerID == n))
                {
                    return true;  // если нашли выходим
                }
            }
            if (Departments.Count == 0)
                return false;
            else
                foreach (var e in Departments) // поиск во вложенных департамментах
                {
                    flag = e.FindID(n);
                }
            return flag;
        }

        /// <summary>
        /// Возвращает свободный ID
        /// </summary>
        /// <returns></returns>
        public int FindFreeWorkerID()
        {
            int ID = 0;
            bool flag = true;
            while (flag)
            {
                ID++;
                flag = FindID(ID);        // когда метод вернет ложь, цикл закончится       
            }
            return ID;
        }
        /// <summary>
        /// Определяет. есть ли такой id депортамента в базе (рекурсия)
        /// </summary>
        /// <param name="n">искомый id</param>
        /// <returns></returns>
        public bool FindDID(int n)
        {
            bool flag = false;

            foreach (var i in Departments)
            {
                if (i.DID == n)
                    return true;
            }

            if (Departments.Count == 0)
            {
                return false;
            }
            else
            {
                foreach (var i in Departments)
                {
                    flag = i.FindDID(n);  // во вложенных 
                }
            }
            return flag;
        }
        /// <summary>
        /// Возвращает свободный ID для департамента
        /// </summary>
        /// <returns></returns>
        public int FindFreeDepartmentID()
        {
            int ID = 0;
            bool flag = true;
            while (flag)
            {
                ID++;
                flag = FindDID(ID);        // когда метод вернет ложь, цикл закончится       
            }
            return ID;
        }

        /// <summary>
        /// Начисление ЗП всем поровну в соответствии со статусом (Директор 15% рабочий 15$ за час интерн 500 $)
        /// </summary>
        public void CalculateZP()
        {

            foreach (var i in workers)  // ЗП сотрудников
            {
                i.AddZP();
            }
            foreach (var i in Departments) // зП во вложеных депатментах
            {
                i.CalculateZP();
            }
            int DirZP = DirectorZP() * 15 / 100;
            director.Salary = (DirZP < 1300) ? 1300 : (DirZP);  // зп директора не меньше 1300
        }

        /// <summary>
        /// Расчет зп руководителя данного отдела
        /// </summary>
        public int DirectorZP()
        {
            int ZP = 0;
            foreach (var i in workers) // ЗП Сотрудников в этом департаменте
            {
                ZP += i.Salary;
            }
            foreach (var i in Departments) // ЗП директоров во вложениях
            {
                ZP += i.DirectorZP();
            }
            return ZP;
        }


        /// <summary>
        /// Добавление часов всем штатным сотрудникам (рекурсия)
        /// </summary>
        /// <param name="n"></param>
        public void AddHours(int n)
        {
            foreach (var i in workers)
            {
                if (i is Employee)  // если штатный добавляем
                {
                    (i as Employee).HoursWorked = n;
                }
            }

            foreach (var i in Departments)  // запускаем во вложенных
            {
                i.AddHours(n);
            }

        }

        /// <summary>
        /// формирование JObject для сохранения в формате json  (рекурсия)
        /// </summary>
        public JObject SerializeJson()
        {
            //string file = JsonConvert.SerializeObject(this);
            JObject main = new JObject();

            //одиночные поля департамента
            main["DID"] = DID;
            main["DepName"] = DepName;
            main["DirectorID"] = DirectorID;
            //поле директор
            JObject dir = new JObject();
            dir["WorkerID"] = director.WorkerID;
            dir["FirstName"] = director.FirstName;
            dir["LastName"] = director.LastName;
            dir["Salary"] = director.Salary;
            main["director"] = dir;

            // массив сотрудников этого депортамента
            JArray array1 = new JArray();
            foreach (var e in workers)
            {
                JObject worker = new JObject();
                worker["WorkerID"] = e.WorkerID.ToString();
                worker["FirstName"] = e.FirstName;
                worker["LastName"] = e.LastName;
                worker["Salary"] = e.Salary;

                if (e is Employee)
                {
                    worker["HoursWorked"] = (e as Employee).HoursWorked.ToString();
                }
                else
                {
                    worker["HoursWorked"] = ""; // Это поле нужно чтобы при десериализации разграничить штатных и интернов
                    worker["Mentor"] = (e as Intern).Mentor.ToString();
                }
                array1.Add(worker);
            }
            main["workers"] = array1;  // добавляем как массив

            JArray array2 = new JArray();
            // рекурсия для массива вложенных департаментов
            
            foreach (var e in Departments)
            {
                array2.Add(e.SerializeJson());
            }
            main["Departments"] = array2;

            return main;
        }

        /// <summary>
        /// десериализация из файла
        /// </summary>
        /// <param name="path"></param>
        public static Department DeserializeJson(JToken dJO)
        {

            Department rez = new Department();  // это будет возвращено
            //одиночные поля
            rez.DID = Convert.ToInt32(dJO["DID"]); 
            rez.DepName = dJO["DepName"].ToString();
            rez.DirectorID = Convert.ToInt32(dJO["DirectorID"]);

            //Добавление объекта директор
            Director dir = new Director(Convert.ToInt32(dJO["director"]["WorkerID"]), dJO["director"]["FirstName"].ToString(), dJO["director"]["LastName"].ToString());
            dir.Salary = Convert.ToInt32(dJO["director"]["Salary"]);  // этого нет в конструкторе
            rez.director = dir;
            // добавление списка сотрудников
            var arrW = dJO["workers"].ToArray();
            foreach (var i in arrW)
            {
                // по полю workedHours выбирает объект типа employee или Intern
                Worker temp;
                if (i["HoursWorked"].ToString() == string.Empty)  // если количество отработаных часов ПУСТО, то это интерн
                {
                    temp = new Intern(Convert.ToInt32(i["WorkerID"]), i["FirstName"].ToString(), i["LastName"].ToString(), Convert.ToInt32(i["Mentor"]));
                }
                else
                {
                    temp = new Employee(Convert.ToInt32(i["WorkerID"]), i["FirstName"].ToString(), i["LastName"].ToString())
                        { HoursWorked=Convert.ToInt32(i["HoursWorked"]) };                                   
                }
                
                temp.Salary = Convert.ToInt32(i["Salary"]); // зарплаты нет в конструкторе
                rez.workers.Add(temp);
            }
            // десериализация набора депаортаментов. рекурсия
            var arrD = dJO["Departments"].ToArray();

            foreach (var e in arrD)
            {
                rez.Departments.Add(DeserializeJson(e));
            }

            return rez;
        }

        //**************************************************************  DZ 12

        /// <summary>
        /// Получение ссылки на Работника по его ID (должен быть 1 в базе)
        /// </summary>
        /// <param name="ID">ID работника</param>
        /// <returns></returns>
        ObservableCollection<Worker> GetWorkerById(int ID)
        {
            ObservableCollection<Worker> rez = new ObservableCollection<Worker>();
            //проверка в этом департпменте
            foreach (var e in workers)
            {
                if (e.WorkerID == ID)
                {
                    rez.Add(e);
                    break;
                }
            }
            // если уже нашли возвращем
            if (rez.Count!=0)
                return rez;
            // продолжаем во вложенных
            foreach (var e in Departments)
            {
                rez = e.GetWorkerById(ID);
            }
            //если достиг этого места, значит не нашел
            return rez;
        }

        /// <summary>
        /// Возвращает ссылку на сотрудника по его фамилии 
        /// </summary>
        /// <param name="LN">часть в фамилии сотрудника</param>
        /// <returns></returns>
        ObservableCollection<Worker> GetWorkerByLastName(string LN)
        {
            ObservableCollection<Worker> rez = new ObservableCollection<Worker>();
            LN = LN.ToLower();
            // поиск в этом департаменте
            //директор
            string last = director.LastName.ToLower();
            if (last.Contains(LN))
                rez.Add(director);
            //сорудники
            foreach (var e in workers)
            {
                last = e.LastName.ToLower();
                if (last.Contains(LN))
                {
                    rez.Add(e);
                }
                
            }           
            // поиск во влож департаментах
            foreach (var e in Departments)
            {
                // добавить к результату сотрудников
                foreach (var i in e.GetWorkerByLastName(LN))
                    rez.Add(i);
            }
            return rez;
        }



            /// <summary>
            /// получение списка работников по ID или фамилии
            /// </summary>
            /// <param name="index">параметр поиска(может быть строка или число)</param>
            /// <returns></returns>
        public ObservableCollection<Worker> this[string index]
        {

            get
            {
                ObservableCollection<Worker> rez = new ObservableCollection<Worker>();

                bool flag = int.TryParse(index,  out int Index);
                if (flag)
                {
                    // число
                    rez = this.GetWorkerById(Index);
                }
                else
                {
                    // строка
                    rez = this.GetWorkerByLastName(index);
                }
                return rez;
            }
        }

        /// <summary>
        /// возвращает список сотрудников по ID департамента 
        /// </summary>
        /// <param name="index">id департамента</param>
        /// <returns></returns>
        public ObservableCollection<Worker> GetWorkersByDep(int index)
        {
            ObservableCollection<Worker> rez = new ObservableCollection<Worker>();
            if (this.DID == index)
            {

                rez = workers;
                rez.Add(this.director);
                return rez;
            }
            else
            {
                foreach (var e in Departments)
                {
                    foreach (var i in e.GetWorkersByDep(index))
                        rez.Add(i);
                }
            }
            return rez;
        }

        /// <summary>
        /// Удаляет сотрудника из базы
        /// </summary>
        /// <param name="wrk">экземпляр работника (Получить по актуальной ссылке) </param>
        public void RemoveWorker(Worker wrk)
        {
            bool flag = workers.Remove(wrk);
            if (!flag)  // если удалил не успешно то ищем во вложенных 
            {
                foreach (var e in Departments)
                {
                    e.RemoveWorker(wrk);
                }
            }
        }
    }
}

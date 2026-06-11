using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoadWork
{
    public class RoadWork
    {
        public override string ToString()
        {
            return $"{Contractor}: Кол-во рабочих {WorkersCount}чел. | Ширина:{Width}м | Длина:{Length}м | Q={Quality():F2}";
        }
        // Базовые поля
        public double Width { get; set; }
        public double Length { get; set; } 
        public double Mass { get; set; }
        //Дополнительные поля
        public string Contractor { get; set; } 
        public int WorkersCount { get; set; } 

        // Конструктор
        public RoadWork(double width, double length, double mass, string contractor, int workersCount)
        {
            Width = width;
            Length = length;
            Mass = mass;
            Contractor = contractor;
            WorkersCount = workersCount;
        }

        //Функция качества Q
        public virtual double Quality()
        {
            return (Width * Length * Mass) / 1000;
        }

        //Вывод информации
        public virtual string GetInfo()
        {
            return $"Организация: {Contractor}\n" +
                   $"Количество рабочих: {WorkersCount} чел.\n" +
                   $"Ширина: {Width} м\n" +
                   $"Длина: {Length} м\n" +
                   $"Масса на 1 кв.м: {Mass} кг\n" +
                   $"Качество Q: {Quality():F2}";
        }

        // Методы добавления (перегрузка 1 - через параметры)
        public void AddData(double width, double length, double mass, string contractor, int workersCount)
        {
            Width = width;
            Length = length;
            Mass = mass;
            Contractor = contractor;
            WorkersCount = workersCount;
        }

        // Перегрузка 2 - через объект
        public void AddData(RoadWork other)
        {
            Width = other.Width;
            Length = other.Length;
            Mass = other.Mass;
            Contractor = other.Contractor;
            WorkersCount = other.WorkersCount;
        }

        // Методы удаления (перегрузка 1 - сброс к значениям по умолчанию)
        public void ClearData()
        {
            Width = 0;
            Length = 0;
            Mass = 0;
            Contractor = "";
            WorkersCount = 0;
        }

        // Перегрузка 2 - удаление конкретного поля
        public void ClearData(string fieldName)
        {
            switch (fieldName.ToLower())
            {
                case "width":
                    Width = 0;
                    break;
                case "length":
                    Length = 0;
                    break;
                case "mass":
                    Mass = 0;
                    break;
                case "contractor":
                    Contractor = "";
                    break;
                case "workers":
                    WorkersCount = 0;
                    break;
            }
        }
    }

    ///КЛАСС ПОТОМОК
    public class ReinforcedRoadWork : RoadWork
    {
        public override string ToString()
        {
            return $"{Contractor} | Ширина:{Width}м | Длина:{Length}м | P={Coefficient} | Qp={Quality():F2}";
        }
        // Дополнительное поле P - коэффициент прочности
        public int Coefficient { get; set; } 

        // Дополнительные свойства (2 новых)
        public string Type { get; set; }
        public double Price { get; set; }  

        // Конструктор
        public ReinforcedRoadWork(double width, double length, double mass,
                                  string contractor, int workersCount,
                                  int coefficient, string type, double price)
            : base(width, length, mass, contractor, workersCount)
        {
            Coefficient = coefficient;
            Type = type;
            Price = price;
        }

        // Перекрытие функции качества Qp
        public override double Quality()
        {
            double Q = base.Quality();

            if (Coefficient >= 5 && Coefficient <= 8)
            {
                return Q * 1.1;
            }
            else if (Coefficient == 3 || Coefficient == 4 ||
                     Coefficient == 9 || Coefficient == 10)
            {
                return Q * 1.6;
            }
            else
            {
                return Q * 1.9;
            }
        }

        // Перекрытие вывода информации
        public override string GetInfo()
        {
            return base.GetInfo() +
                   $"\nКоэффициент прочности P: {Coefficient}\n" +
                   $"Тип техники: {Type}\n" +
                   $"Стоимость за метр: {Price:F2} тыс. руб\n" +
                   $"Качество Qp: {Quality():F2}";
        }
    }
}

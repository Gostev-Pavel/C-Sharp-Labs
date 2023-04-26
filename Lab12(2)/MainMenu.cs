using System;
using System.Collections.Generic;
using System.Text;
using MyLib10;

namespace Lab12_2_
{
    class MainMenu
    {
        MyLinkedList<Production> productions;
        MyLinkedList<Car> cars;
        public MainMenu()
        {
            productions = new MyLinkedList<Production>();
            cars = new MyLinkedList<Car>();
        }

        public static int GetInt(string message)
        {
            int number;
            bool isCorrect;
            do
            {
                Console.Write(message);
                isCorrect = int.TryParse(Console.ReadLine(), out number);
                if (!isCorrect)
                    Console.WriteLine("Введено недопустимое значение!");
            } while (!isCorrect);
            return number;
        }

        public static Char GetChar(string message)
        {
            Char symbol;
            bool isCorrect;
            do
            {
                Console.Write(message);
                isCorrect = Char.TryParse(Console.ReadLine(), out symbol);
                if (!isCorrect)
                    Console.WriteLine("Введено недопустимое значение!");
            } while (!isCorrect);
            return symbol;
        }
        //генерация случайного объекта из иерархии классов Production
        public object GenerateProduction()
        {
            Random rand = new Random();
            int type = rand.Next(1, 4);
            switch (type)
            {
                case 1:
                    Production newProd = new Production();
                    newProd.RandomInit();
                    return newProd;
                case 2:
                    Factory newFactory = new Factory();
                    newFactory.RandomInit();
                    return newFactory;
                case 3:
                    Department newDep = new Department();
                    newDep.RandomInit();
                    return newDep;
                default:
                    throw new FormatException("Невозможно создать случайный объект Production!");
            }
        }
        //генерация случайного объекта из иерархии классов Car
        public object GenerateCar()
        {
            Random rand = new Random();
            int type = rand.Next(1, 3);
            switch (type)
            {
                case 1:
                    Car newCar = new Car();
                    newCar.RandomInit();
                    return newCar;
                case 2:
                    Engine newEngine = new Engine();
                    newEngine.RandomInit();
                    return newEngine;
                default:
                    throw new FormatException("Невозможно создать случайный объект Car!");
            }
        }
        //Создание ручным вводом объекта иерархии Production для поиска/удаления этого объекта из списка
        public object CreateProduction()
        {
            int type = GetInt("Укажите желаемый тип объекта:\n1 - Production  2 - Factory  3 - Department: ");
            while (type > 3 || type < 1)
                type = GetInt("Введена недопустимая команда! Повторите ввод: ");
            switch (type)
            {
                case 1:
                    Console.Write("\nВведите название производства: ");
                    string newName = Console.ReadLine();
                    int newYear = GetInt("Укажите год основания производства: ");
                    while (newYear < 0)
                        newYear = GetInt("Год не может быть отрицательным! Повторите ввод: ");
                    Console.Write("\nВведите тип производства: ");
                    string newType = Console.ReadLine();
                    Production production = new Production(newName, newYear, newType);
                    return production;
                case 2:
                    Factory factory = new Factory();
                    Console.Write("\nВведите название завода: ");
                    string newNameFac = Console.ReadLine();
                    int newYearFac = GetInt("Укажите год основания завода: ");
                    while (newYearFac < 0)
                        newYearFac = GetInt("Год не может быть отрицательным! Повторите ввод: ");
                    Console.Write("\nВведите тип производства: ");
                    string newTypeFac = Console.ReadLine();
                    int number = GetInt("\nВведите количество профессий в цеху: ");
                    while (number <= 0)
                        number = GetInt("Количество профессий не может быть <= 0! Повторите ввод: ");
                    for (int i = 0; i < number; i++)
                    {
                        Console.Write("\nВведите название профессии: ");
                        string nameWorker = Console.ReadLine();
                        factory.AddWorker(nameWorker);
                    }
                    factory.EditName = newNameFac;
                    factory.EditYear = newYearFac;
                    factory.EditType = newTypeFac;
                    return factory;
                case 3:
                    Department department = new Department();
                    Console.Write("\nВведите название цеха: ");
                    string newNameDep = Console.ReadLine();
                    int newYearDep = GetInt("\nУкажите год основания цеха: ");
                    while (newYearDep < 0)
                        newYearDep = GetInt("Год не может быть отрицательным! Повторите ввод: ");
                    Console.Write("\nВведите тип производства: ");
                    string newTypeDep = Console.ReadLine();
                    int numberWork = GetInt("\nВведите количество профессий в цеху: ");
                    while (numberWork <= 0)
                        numberWork = GetInt("Количество профессий не может быть <= 0! Повторите ввод: ");
                    for (int i = 0; i < numberWork; i++)
                    {
                        Console.Write("\nВведите название профессии: ");
                        string nameWorker = Console.ReadLine();
                        department.AddWorker(nameWorker);
                    }
                    int numberNameWork = GetInt("\nВведите количество работников в цеху: ");
                    while (numberNameWork <= 0)
                        numberNameWork = GetInt("Количество работников не может быть <= 0! Повторите ввод: ");
                    for (int i = 0; i < numberNameWork; i++)
                    {
                        Console.Write("\nВведите имя работника: ");
                        string nameWorker = Console.ReadLine();
                        department.AddWorker(nameWorker);
                    }
                    department.EditName = newNameDep;
                    department.EditYear = newYearDep;
                    department.EditType = newTypeDep;
                    return department;
                default:
                    throw new FormatException("Создать объект иерархии классов Production невозможно!");
            }
        }
        //Создание ручным вводом объекта иерархии Car для поиска/удаления этого объекта из списка
        public object CreateCar()
        {
            int type = GetInt("Укажите желаемый тип объекта:\n1 - Car  2 - Engine: ");
            while (type > 2 || type < 1)
                type = GetInt("Введена недопустимая команда! Повторите ввод: ");
            switch (type)
            {
                case 1:
                    Console.Write("\nВведите марку авто: ");
                    string newBrand = Console.ReadLine();
                    Console.Write("\nВведите модель авто: ");
                    string newModel = Console.ReadLine();
                    int newYear = GetInt("\nУкажите год выпуска авто: ");
                    while (newYear < 0)
                        newYear = GetInt("Год не может быть отрицательным! Повторите ввод: ");
                    Car car = new Car(newBrand, newModel, newYear);
                    return car;
                case 2:
                    Console.Write("\nВведите марку авто: ");
                    string newBrandEngine = Console.ReadLine();
                    Console.Write("\nВведите модель авто: ");
                    string newModelEngine = Console.ReadLine();
                    int newYearEngine = GetInt("\nУкажите год выпуска авто: ");
                    while (newYearEngine < 0)
                        newYearEngine = GetInt("Год не может быть отрицательным! Повторите ввод: ");
                    Car carEngine = new Car(newBrandEngine, newModelEngine, newYearEngine);
                    Console.Write("\nВведите название двигателя авто: ");
                    string newEngineName = Console.ReadLine();
                    int newPower = GetInt("\nВведите у двигателя количество лошадиных сил: ");
                    while (newPower <= 0)
                        newPower = GetInt("Лошадиные силы не могут быть <= 0! Повторите ввод: ");
                    Engine engine = new Engine(newEngineName, newPower, carEngine);
                    return engine;
                default:
                    throw new FormatException("Создать объект иерархии классов Car невозможно!");
            }
        }
        public void MenuLogic()
        {
            bool workFlag = true;
            while (workFlag)
            {
                Console.Write("Список доступных команд:\t\n" +
                    "0 - Сгенерировать связный список с элементами иерархии классов Production\t\n" +
                    "1 - Сгенерировать связный список с элементами иерархии классов Car\t\n" +
                    "2 - Вывести список с элементами иерархии классов Production\t\n" +
                    "3 - Вывести список с элементами иерархии классов Car\t\n" +
                    "4 - Демонстрация поверхностного копирования на списке с элементами Car с использованием дополнительного списка\t\n" +
                    "5 - Демонстрация глубокого копирования списка на списке с элементами Car с использованием дополнительного списка\t\n" +
                    "6 - Добавить случайный новый элемент в конец списка с элементами иерархии классов Production\t\n" +
                    "7 - Добавить случайный новый элемент в конец списка с элементами иерархии классов Car\t\n" +
                    "8 - Удалить заданный элемент из списка с элементами иерархии классов Production\t\n" +
                    "9 - Удалить заданный элемент из списка с элементами иерархии классов Car\t\n" +
                    "10 - Поиск заданного элемента по списку с элементами иерархии классов Production\t\n" +
                    "11 - Поиск заданного элемента по списку с элементами иерархии классов Car\t\n" +
                    "12 - Добавить случайный новый элемент на указанную позицию в списке с элементами иерархии классов Production\t\n" +
                    "13 - Добавить случайный новый элемент на указанную позицию в списке с элементами иерархии классов Car\t\n" +
                    "14 - Очистить список с элементами иерархии классов Production\t\n" +
                    "15 - Очистить список с элементами иерархии классов Car\t\n" +
                    "16 - Закрыть программу\t\n\n");
                int num = GetInt("Выберите номер команды: ");
                while (num > 16 || num < 0)
                    num = GetInt("Неверный ввод! Выберите повторно номер команды: ");
                switch (num)
                {
                    case 0:
                        productions = new MyLinkedList<Production>(MyLinkedList<Production>.GenerateListProduction());
                        Console.Write("Генерация списка с элементами иерархии классов Production выполнена\n");
                        break;
                    case 1:
                        cars = new MyLinkedList<Car>(MyLinkedList<Car>.GenerateListCar());
                        Console.Write("Генерация списка с элементами иерархии классов Car выполнена\n");
                        break;
                    case 2:
                        productions.Show();
                        break;
                    case 3:
                        cars.Show();
                        break;
                    case 4:
                        if (cars.Count != 0)
                        {
                            MyLinkedList<Car> copyList = new MyLinkedList<Car>();
                            copyList = cars;
                            Console.Write("Список с поверхностно скопированными элементами (резервный):\n");
                            copyList.Show();
                            copyList.Clear();
                            Console.Write("Оригинальный список после очистки элементов методом Clear в резервном списке:\n");
                            cars.Show();
                            Console.Write("Резервный список после очистки элементов методом Clear:\n");
                            copyList.Show();
                            Console.Write("\n");
                        }
                        else
                            Console.Write("Список с элементами иерархии классов Car пуст!\n");
                        break;
                    case 5:
                        MyLinkedList<Car> deepCopy = (MyLinkedList<Car>)cars.Clone();
                        Console.Write("Глубокая копия списка с элементами иерархии классов Car:\n");
                        deepCopy.Show();
                        deepCopy.Clear();
                        Console.Write("Оригинальный список после очистки элементов методом Clear в глубокой копии:\n");
                        cars.Show();
                        Console.Write("Глубокая копия после очистки элементов методом Clear:\n");
                        deepCopy.Show();
                        Console.Write("\n");
                        break;
                    case 6:
                        object newProd = GenerateProduction();
                        productions.Add((Production)newProd);
                        break;
                    case 7:
                        object newCar = GenerateCar();
                        cars.Add((Car)newCar);
                        break;
                    case 8:
                        object removeProd = CreateProduction();
                        if (productions.Remove((Production)removeProd))
                            Console.Write("Объект успешно удален\n");
                        else
                            Console.Write("Указанный объект не найден!\n");
                        break;
                    case 9:
                        object removeCar = CreateCar();
                        if (cars.Remove((Car)removeCar))
                            Console.Write("Объект успешно удален\n");
                        else
                            Console.Write("Указанный объект не найден!\n");
                        break;
                    case 10:
                        object searchProd = CreateProduction();
                        if (productions.Contains((Production)searchProd))
                            Console.Write("Указанный объект находится в списке\n");
                        else
                            Console.Write("Указанный объект не найден в списке!\n");
                        break;
                    case 11:
                        object searchCar = CreateCar();
                        if (cars.Contains((Car)searchCar))
                            Console.Write("Указанный объект находится в списке\n");
                        else
                            Console.Write("Указанный объект не найден в списке!\n");
                        break;
                    case 12:
                        object newProdIndex = GenerateProduction();
                        int indexProd = GetInt("Укажите желаемую позицию для добавления нового элемента в список: ");
                        productions.AddToIndex((Production)newProdIndex, indexProd - 1);
                        Console.Write($"Новый случайный элемент успешно добавлен в список на позицию {indexProd}\n");
                        break;
                    case 13:
                        object newCarIndex = GenerateCar();
                        int indexCar = GetInt("Укажите желаемую позицию для добавления нового элемента в список: ");
                        cars.AddToIndex((Car)newCarIndex, indexCar - 1);
                        Console.Write($"Новый случайный элемент успешно добавлен в список на позицию {indexCar}\n");
                        break;
                    case 14:
                        if (productions.Count > 0)
                        {
                            productions.Clear();
                            Console.Write("Список с элементами иерархии классов Production очищен\n");
                        }
                        else
                            Console.Write("Текущий список уже пустой!\n");
                        break;
                    case 15:
                        if (cars.Count > 0)
                        {
                            cars.Clear();
                            Console.Write("Список с элементами иерархии классов Car очищен\n");
                        }
                        else
                            Console.Write("Текущий список уже пустой!\n");
                        break;
                    case 16:
                        workFlag = false;
                        break;
                }
            }
        }
    }
}

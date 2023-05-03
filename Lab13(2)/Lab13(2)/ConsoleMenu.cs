using System;
using System.Collections.Generic;
using System.Text;
using MyLib10;

namespace Lab13_2_
{
    class ConsoleMenu
    {
        MyLinkedListEvent<Production> listOne = new MyLinkedListEvent<Production>("Список-1");
        MyLinkedListEvent<Production> listTwo = new MyLinkedListEvent<Production>("Список-2");
        public ConsoleMenu()
        {
            listOne = GenerateProductionList("Список-1");
            listTwo = GenerateProductionList("Список-2");
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

        //генерация случайного списка с иерархией классов Production
        public static MyLinkedListEvent<Production> GenerateProductionList(string newName)
        {
            Random rand = new Random();
            int length = rand.Next(1, 31);
            MyLinkedListEvent<Production> newList = new MyLinkedListEvent<Production>(newName);
            for (int i = 0; i < length; i++)
            {
                int type = rand.Next(1, 4);
                switch (type)
                {
                    case 1:
                        Production newProd = new Production();
                        newProd.RandomInit();
                        newList.Add(newProd);
                        break;
                    case 2:
                        Factory newFactory = new Factory();
                        newFactory.RandomInit();
                        newList.Add(newFactory);
                        break;
                    case 3:
                        Department newDep = new Department();
                        newDep.RandomInit();
                        newList.Add(newDep);
                        break;
                }
            }
            return newList;
        }
        //регенерация нового связного списка с объектами иерархии классов Production
        public static void RegenerateListProduction(MyLinkedListEvent<Production> regList)
        {
            regList.Clear();
            Random rand = new Random();
            int length = rand.Next(1, 31);
            for (int i = 0; i < length; i++)
            {
                int type = rand.Next(1, 4);
                switch (type)
                {
                    case 1:
                        Production newProd = new Production();
                        newProd.RandomInit();
                        regList.Add(newProd);
                        break;
                    case 2:
                        Factory newFactory = new Factory();
                        newFactory.RandomInit();
                        regList.Add(newFactory);
                        break;
                    case 3:
                        Department newDep = new Department();
                        newDep.RandomInit();
                        regList.Add(newDep);
                        break;
                }
            }
        }
        //генерация случайного объекта из иерархии классов Production
        public static object GenerateProduction()
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

        public void MenuLogic()
        {
            //создание первого журнала и подписка его на события изменений ReferenceChanged и CountChanged первого списка
            Journal<Production> journalOne = new Journal<Production>();
            listOne.MyLinkedListCountChanged += journalOne.AddEntry;
            listOne.MyLinkedReferenceChanged += journalOne.AddEntry;
            //создание второго журнала и подписка его на события изменений ReferenceChanged первого и второго списка
            Journal<Production> journalTwo = new Journal<Production>();
            listOne.MyLinkedReferenceChanged += journalTwo.AddEntry;
            listTwo.MyLinkedReferenceChanged += journalTwo.AddEntry;
            bool workFlag = true;
            while (workFlag)
            {
                Console.Write("Список доступных команд:\t\n" +
                    "0 - Вывести текущий первый список\t\n" +
                    "1 - Вывести текущий второй список\t\n" +
                    "2 - Сгенерировать новый первый список\t\n" +
                    "3 - Сгенерировать новый второй список\t\n" +
                    "4 - Добавить новый случайный элемент в первый список\t\n" +
                    "5 - Добавить новый случайный элемент во второй список\t\n" +
                    "6 - Удалить по индексу элемент из первого списка\t\n" +
                    "7 - Удалить по индексу элемент из второго списка\t\n" +
                    "8 - Заменить элемент по индексу в первом списке\t\n" +
                    "9 - Заменить элемент по индексу во втором списке\t\n" +
                    "10 - Вывести первый журнал\t\n" +
                    "11 - Вывести второй журнал\t\n" +
                    "12 - Закрыть программу\t\n\n");
                int num = GetInt("Выберите номер команды: ");
                while (num > 12 || num < 0)
                    num = GetInt("Неверный ввод! Выберите повторно номер команды: ");
                switch (num)
                {
                    case 0:
                        listOne.Show();
                        break;
                    case 1:
                        listTwo.Show();
                        break;
                    case 2:
                        ConsoleMenu.RegenerateListProduction(listOne);
                        break;
                    case 3:
                        ConsoleMenu.RegenerateListProduction(listTwo);
                        break;
                    case 4:
                        Production newProdOne = (Production)ConsoleMenu.GenerateProduction();
                        listOne.Add(newProdOne);
                        break;
                    case 5:
                        Production newProdTwo = (Production)ConsoleMenu.GenerateProduction();
                        listTwo.Add(newProdTwo);
                        break;
                    case 6:
                        if (listOne.Count > 0)
                        {
                            int remIndexOne = GetInt("Введите индекс удаляемого элемента: ");
                            listOne.RemoveOnIndex(remIndexOne);
                        }
                        else
                            Console.Write("Список пуст!\n");
                        break;
                    case 7:
                        if (listTwo.Count > 0)
                        {
                            int remIndexTwo = GetInt("Введите индекс удаляемого элемента: ");
                            listTwo.RemoveOnIndex(remIndexTwo);
                        }
                        else
                            Console.Write("Список пуст!\n");
                        break;
                    case 8:
                        if (listOne.Count > 0)
                        {
                            int repIndexOne = GetInt("Введите индекс заменяемого элемента: ");
                            while (repIndexOne < 0 || repIndexOne > listOne.Count)
                                repIndexOne = GetInt("Недопустимый индекс! Повторите ввод: ");
                            listOne[repIndexOne] = (Production)ConsoleMenu.GenerateProduction();
                        }
                        else
                            Console.Write("Список пуст!\n");
                        break;
                    case 9:
                        if (listTwo.Count > 0)
                        {
                            int repIndexTwo = GetInt("Введите индекс заменяемого элемента: ");
                            while (repIndexTwo < 0 || repIndexTwo > listTwo.Count)
                                repIndexTwo = GetInt("Недопустимый индекс! Повторите ввод: ");
                            listOne[repIndexTwo] = (Production)ConsoleMenu.GenerateProduction();
                        }
                        else
                            Console.Write("Список пуст!\n");
                        break;
                    case 10:
                        Console.Write(journalOne);
                        break;
                    case 11:
                        Console.Write(journalTwo);
                        break;
                    case 12:
                        workFlag = false;
                        break;
                }
            }
        }
    }
}

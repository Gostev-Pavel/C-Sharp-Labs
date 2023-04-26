using System;
using System.Collections.Generic;
using System.Collections;
using MyLib10;

namespace Lab12_2_
{
    public class MyLinkedList<T> : IEnumerable<T>, ICollection<T>, ICloneable, IDisposable
    {
        //класс для реализации связанных узлов списка
        protected class Node
        {
            public T data;
            public Node nextNode;

            public Node(T newData)
            {
                data = newData;
            }
            public Node(T newData, Node newNextNode)
            {
                data = newData;
                nextNode = newNextNode;
            }
        }
        //класс для реализации нумератора коллекции
        protected class MyEnumerator : IEnumerator<T>
        {
            private Node currentNode { get; set; } = null; //текущий узел (позиция нумератора)
            private Node startNode { get; set; } = null; //стартовый узел

            public MyEnumerator(Node newStart)
            {
                this.startNode = newStart;
            }
            //реализация метода Reset из IEnumerator для сброса положения нумератора до изначального состояния (то есть начала коллекции)
            public void Reset()
            {
                currentNode = null;
            }
            //реализация метода MoveNext из IEnumerator для передвижения нумератора по коллекции
            public bool MoveNext()
            {
                if (startNode is null) //если коллекция пуста
                    return false;
                if (currentNode is null) //если в коллекции только один узел
                {
                    currentNode = startNode;
                    return true;
                }
                else
                {
                    if (currentNode.nextNode is null)
                    {
                        Reset();
                        return false;
                    }
                    else
                    {
                        currentNode = currentNode.nextNode;
                        return true;
                    }
                }
            }
            //реализация свойства для получения данных из текущего узла типом T
            public T Current
            {
                get
                {
                    return currentNode.data;
                }
            }
            //реализация свойства для получения данных из текущего узла типом object
            object IEnumerator.Current
            {
                get
                {
                    return Current;
                }
            }
            //реализация метода Dispose из IEnumerator для удаления неуправляемых ресурсов нумератора
            public void Dispose() { }
        }

        protected Node initialNode = null; //начальный узел
        public int Count { get; protected set; } = 0; //количество узлов в списке
        public bool IsReadOnly { get; private set; } = false;
        private bool disposed = false;

        public MyLinkedList() { }
        public MyLinkedList(int newAmount)
        {
            Count = newAmount;
        }
        public MyLinkedList(MyLinkedList<T> copyList)
        {
            foreach (var obj in copyList)
            {
                this.Add(obj);
            }
        }
        //реализация метода Add из ICollection для добавления новых узлов в конец списка
        public virtual void Add(T newData)
        {
            if (newData is null) //если пытаются добавить в список пустое значение
            {
                throw new NullReferenceException("Добавить null в список невозможно!");
            }
            Node newNode = new Node(newData);
            if (initialNode is null) //если в списке нет ни одного узла
                initialNode = newNode;
            else //в противном случае двигаемся в конец списка по всем узлам и добавляем новый узел в конец
            {
                Node nodeCurrent = initialNode;
                while (nodeCurrent.nextNode != null)
                    nodeCurrent = nodeCurrent.nextNode;
                nodeCurrent.nextNode = newNode;
            }
            ++Count;
        }
        //реализация метода Clear из ICollection, который удаляет все элементы коллекции (списка) и сбрасывает количество элементов до 0
        public virtual void Clear()
        {
            Count = 0;
            initialNode = null;
        }
        //реализация метода Contains из ICollection, который ищет данное значение в коллекции (списке)
        public bool Contains(T searchObject)
        {
            if (searchObject is null) //если искомое значение пустое
            {
                Console.Write("Пустое значение (null) не может содержаться в списке!\n");
                return false;
            }
            else
                foreach (var obj in this) //если искомое значение чему-то равно
                {
                    if (searchObject.Equals(obj))
                        return true;
                }
            return false; //если по итогу перебора ничего не найдено в коллекции с искомым значением
        }
        //реализация метода Remove из ICollection, который удаляет первое вхождение данного элемента в коллекцию (если таковой имеется)
        public virtual bool Remove(T searchObject)
        {
            if (Contains(searchObject)) //если искомый элемент содержится вообще в коллекции
            {
                    if (Count != 1) //если есть больше одного узла в списке
                    {
                        if (initialNode.data.Equals(searchObject)) //если удаляемый элемент является начальным узлом в списке
                            initialNode = initialNode.nextNode;
                        else
                        {
                            Node nodeCurrent = initialNode;
                            while (!searchObject.Equals(nodeCurrent.nextNode.data))
                                nodeCurrent = nodeCurrent.nextNode;
                            nodeCurrent.nextNode = nodeCurrent.nextNode.nextNode;
                        }
                    }
                    else
                        initialNode = null; //если узел один, то искомый для удаления узел точно начальный (то есть удаляем его)
                    --Count;
                    return true;
            }
            else
                return false;
        }
        //Реализация необобщенного метода GetEnumerator из ICollection, который возвращает объект-нумератор
        IEnumerator IEnumerable.GetEnumerator()
        {
            return (IEnumerator)new MyEnumerator(initialNode);
        }
        //Реализация обобщенного метода GetEnumerator из ICollection, который возвращает обобщенный объект-нумератор
        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            return (IEnumerator<T>)new MyEnumerator(initialNode);
        }
        //Реализация CopyTo из ICollection, который поверхностно копирует элементы коллекции в массив
        public void CopyTo(T[] array, int index)
        {
            if (array.Length < index + Count)
            {
                throw new ArgumentOutOfRangeException("В массиве недостаточно места для переноса всех элементов списка!");
            }
            if (index < 0)
            {
                throw new ArgumentOutOfRangeException("Введен недопустимый отрицательный индекс!");
            }
            foreach (var obj in this)
            {
                array[index] = obj;
                ++index;
            }
        }
        //реализация метода добавления нового элемента в коллекцию по заданной позиции (индексу)
        public virtual void AddToIndex(T data, int index)
        {
            if (index < 0 || index > Count)
            {
                throw new ArgumentOutOfRangeException("Значение позиции должно быть не меньше 0 и не больше количества узлов!");
            }
            if (index == 0) //если хотят добавить в самое начало списка
            {
                initialNode = new Node(data, initialNode);
                ++Count;
            }
            else if (index == Count) //если хотят добавить в конец списка
                Add(data);
            else
            {
                Node nodeCurrent = initialNode;
                while (index > 1)
                {
                    nodeCurrent = nodeCurrent.nextNode;
                    --index;
                }
                Node newNode = new Node(data, nodeCurrent.nextNode);
                nodeCurrent.nextNode = newNode;
                ++Count;
            }
        }
        //Глубокое копирование текущего объекта
        public object Clone()
        {
            return new MyLinkedList<T>(this);
        }
        //Вывод списка
        public void Show()
        {
            if (initialNode != null)
            {
                foreach (var obj in this)
                {
                    Console.Write($"{obj.ToString()}\n");
                }
            }
            else
                Console.Write("Текущий список пуст!\n");
        }
        //Генерация случайного списка с иерархией классов Production
        public static MyLinkedList<Production> GenerateListProduction()
        {
            Random rand = new Random();
            int length = rand.Next(1, 31);
            MyLinkedList<Production> newList = new MyLinkedList<Production>();
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
        //Генерация случайного списка с иерархией классов Production
        public static MyLinkedList<Car> GenerateListCar()
        {
            Random rand = new Random();
            int length = rand.Next(3, 31);
            MyLinkedList<Car> newList = new MyLinkedList<Car>();
            Engine car = new Engine();
            car.RandomInit();
            newList.Add(car);
            for (int i = 1; i < length; i++)
            {
                int type = rand.Next(1, 3);
                switch (type)
                {
                    case 1:
                        Car newCar = new Car();
                        newCar.RandomInit();
                        newList.Add(newCar);
                        break;
                    case 2:
                        Engine newEngine = new Engine();
                        newEngine.RandomInit();
                        newList.Add(newEngine);
                        break;
                }
            }
            return newList;
        }
        //реализация метода Dispose из IDisposable, с помощью которого удаляются неконтролируемые ресурсы
        public void Dispose()
        {
            Dispose(true); //освобождение неуправляемых ресурсов
            GC.SuppressFinalize(this); //запрет системе применить на текущий объект Finalize, чтобы предотвратить избыточную сборку мусора
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed) //выполняется, если ресурсы ещё не были освобождены
            {
                if (disposing) //если вызов осуществляется из метода Dispose(), то освободить управляемые ресурсы
                {
                    Console.WriteLine("Удаление управляемых ресурсов");
                    if (this != null)
                        this.Dispose();
                }
            }
            //освобождение неуправляемых ресурсов
            disposed = true;
        }
        ~MyLinkedList()
        {
            Dispose(false);
        }
    }
}

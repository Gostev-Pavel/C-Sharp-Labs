using System;
using System.Collections.Generic;
using System.Text;
using MyLib10;

namespace Lab13_2_
{
    //класс NewMyLinkedList с событиями
    public class MyLinkedListEvent<T>: NewMyLinkedList<T>
    {
        public delegate void MyLinkedListHandler(MyLinkedListHandlerEventArgs args);
        public event MyLinkedListHandler MyLinkedListCountChanged = null;
        public event MyLinkedListHandler MyLinkedReferenceChanged = null;
        //переопределение индексатора из NewMyLinkedList
        public override T this[int index]
        {
            get
            {
                return base[index];
            }
            set
            {
                MyLinkedReferenceChanged?.Invoke(new MyLinkedListHandlerEventArgs(Name, MyLinkedListHandlerEventArgs.types[3], base[index], this));
                base[index] = value;
            }
        }
        //конструкторы
        public MyLinkedListEvent(string newName) : base(newName)
        {
        }
        //переопределение Add из MyLinkedList
        public override void Add(T newData)
        {
            MyLinkedListCountChanged?.Invoke(new MyLinkedListHandlerEventArgs(Name, MyLinkedListHandlerEventArgs.types[0], newData, this));
            base.Add(newData);
        }
        //переопределение AddToIndex из MyLinkedList
        public override void AddToIndex(T newData, int index)
        {
            MyLinkedListCountChanged?.Invoke(new MyLinkedListHandlerEventArgs(Name, MyLinkedListHandlerEventArgs.types[0], newData, this));
            base.AddToIndex(newData, index);
        }
        //переопределение Clear из MyLinkedList
        public override void Clear()
        {
            MyLinkedListCountChanged?.Invoke(new MyLinkedListHandlerEventArgs(Name, MyLinkedListHandlerEventArgs.types[2], this, this));
            base.Clear();
        }
        //переопределение Remove из MyLinkedList
        public override bool Remove(T searchObject)
        {
            if (Contains(searchObject))
            {
                MyLinkedListCountChanged?.Invoke(new MyLinkedListHandlerEventArgs(Name, MyLinkedListHandlerEventArgs.types[1], searchObject, this));
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
            {
                MyLinkedReferenceChanged?.Invoke(new MyLinkedListHandlerEventArgs(Name, MyLinkedListHandlerEventArgs.types[1], null, this));
                return false;
            }
        }
        //переопределение RemoveOnIndex из NewMyLinkedList
        public override bool RemoveOnIndex(int index)
        {
            if (index >= 0 && index < Count)
            {
                MyLinkedListCountChanged?.Invoke(new MyLinkedListHandlerEventArgs(Name, MyLinkedListHandlerEventArgs.types[1], this[index], this));
                if (Count != 1)
                {
                    if (index == 0) //если нужно удалить начальный узел
                        initialNode = initialNode.nextNode;
                    else
                    {
                        Node nodeCurrent = initialNode;
                        while (index-- > 0)
                            nodeCurrent = nodeCurrent.nextNode;
                        nodeCurrent.nextNode = nodeCurrent.nextNode.nextNode;
                    }
                }
                else
                    initialNode = null;
                --Count;
                return true;
            }
            else
            {
                MyLinkedListCountChanged?.Invoke(new MyLinkedListHandlerEventArgs(Name, MyLinkedListHandlerEventArgs.types[1], null, this));
                return false;
            }
        }
    }
}
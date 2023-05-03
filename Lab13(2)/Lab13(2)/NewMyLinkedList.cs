using System;
using System.Collections.Generic;
using System.Text;
using MyLib10;
using Lab12_2_;

namespace Lab13_2_
{
    public class NewMyLinkedList<T>: MyLinkedList<T>
    {
        public string Name { get; set; } //открытое автореализуемое свойство типа string с названием коллекции
        //индексатор с целочисленным индексом для доступа к элементам
        public virtual T this[int index]
        {
            get
            {
                if (index >= 0 && index < Count)
                {
                    Node nodeCurrent = initialNode;
                    while (index-- > 0)
                        nodeCurrent = nodeCurrent.nextNode;
                    return nodeCurrent.data;
                }
                else
                    throw new ArgumentOutOfRangeException("Индекс выходит за пределы коллекции");
            }
            set
            {
                if (index >= 0 && index < Count)
                {
                    Node nodeCurrent = initialNode;
                    while (index-- > 0)
                        nodeCurrent = nodeCurrent.nextNode;
                    nodeCurrent.data = value;
                }
                else
                    throw new ArgumentOutOfRangeException("Индекс выходит за пределы коллекции");
            }
        }
        //конструкторы
        public NewMyLinkedList(string newName)
        {
            this.Name = newName;
        }
        public NewMyLinkedList(NewMyLinkedList<T> copyList)
        {
            this.Name = copyList.Name;
            foreach (var obj in copyList)
            {
                this.Add(obj);
            }
        }
        //реализация метода RemoveOnIndex для удаления элемента с указанной позиции (если таковой имеется)
        public virtual bool RemoveOnIndex(int index)
        {
            if (index >= 0 && index < Count)
            {
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
                return false;
        }
    }
}

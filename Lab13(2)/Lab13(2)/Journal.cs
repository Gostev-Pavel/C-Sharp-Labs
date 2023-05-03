using System;
using System.Collections.Generic;
using System.Text;

namespace Lab13_2_
{
    //класс-журнал для записи всех изменений в коллекциях
    public class Journal<T>
    {
        //аналог JournalEntry, так как MyLinkedListHandlerEventArgs содержит уже все необходимые свойства для ведения лога событий коллекций
        public List<MyLinkedListHandlerEventArgs> entries { get; private set; } = new List<MyLinkedListHandlerEventArgs>();
        //конструктор
        public Journal()
        {
        }
        //метод, который будет подписывать журнал на события коллекций
        public void AddEntry(MyLinkedListHandlerEventArgs newEntry)
        {
            entries.Add(newEntry);
        }
        //перегрузка ToString
        public override string ToString()
        {
            if (entries.Count > 0)
            {
                StringBuilder result = new StringBuilder();
                result.Append("Журнал изменений:\n");
                int count = 0;
                foreach (var item in entries)
                {
                    result.Append($"{count++})\n{item}\n");
                }
                return result.ToString();
            }
            else
                return "Журнал пуст!\n";
        }
    }
}

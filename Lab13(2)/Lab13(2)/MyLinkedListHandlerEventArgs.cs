using System;
using System.Collections.Generic;
using System.Text;

namespace Lab13_2_
{
    //класс для хранения и передачи информации о событиях
    public class MyLinkedListHandlerEventArgs: EventArgs
    {
        public static string[] types = new string[] { "Added", "Removed", "RemovedAll", "Updated" }; //типы изменений коллекции
        public string collectionName { get; private set; } //название коллекции, в которой произошло событие
        public string changeType { get; private set; } //конкретный тип изменения коллекции
        public object changedObject { get; private set; } = null; //объект коллекции, подвергшийся изменениям
        public object caller { get; private set; } //объект коллекции, вызвавший событие в коллекции
        //конструктор
        public MyLinkedListHandlerEventArgs(string newName, string newType, object newChanged, object newCaller)
        {
            collectionName = newName;
            changeType = newType;
            changedObject = newChanged;
            caller = newCaller;
        }
        //переопределение ToString
        public override string ToString()
        {
            return $"Изменившийся объект:\n{changedObject.ToString() ?? "NULL"}\nТип изменения коллекции:\n{changeType.ToString()}\n" +
                $"Имя коллекции, в которой произошло событие:\n{collectionName.ToString()}\n";
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BugTrackingSys_EPAMTest
{
    // ИСКЛЮЧЕНИЯ
    // не совпадение по количеству атрибутов
    public class NotMatchTableEx : Exception
    {
        public NotMatchTableEx() : base("Количество атрибутов во входной таблице не совпадает с количеством атрибутов текущей!") { }
    }
    // пустая входная таблица
    public class NullTableEx : Exception
    {
        public NullTableEx() : base("Входная таблица пуста!") { }
    }
    // пустой элемент или не уникальный ключ
    public class NullElem : Exception
    {
        public NullElem(string message) : base(message) { }
    }
    // не уникальный элемент
    public class NotUniqueKeyInTableEx : Exception
    {
        public NotUniqueKeyInTableEx(string message) : base(message) { }
    }
    // нарушение связности таблиц
    public class LinkedTableEx : Exception
    {
         public LinkedTableEx(string message) : base(message) { }
    }


    // КЛАССЫ ТАБЛИЦ СИСТЕМЫ
    // Предок
    public abstract class TTable
    {
        // структура для организации связи между таблицами
        public struct TableLink
        {
            public bool Type;  //true: Tab - Target, false: Tab - Linked 
            public TTable Tab;
            public int FK;
        }

        // ПОЛЯ
        protected      List<string>        TableHead;  // заголовок таблицы
        protected      List<string>[]      TableBody;  // тело таблицы
        protected   List<TableLink>     Links;      // связь с другими таблицами
        protected   int                 NumAtr;     // количество атрибутов
        //

        // КОНСТРУКТОРЫ
        // конструктор, создающий пустую таблицу
        public TTable(int NumAttribute)
        {
            NumAtr = NumAttribute;
            TableBody = new List<string>[NumAttribute];
        }

        // коструктор, создающий таблицу c данными из Table
        public TTable(int NumAttribute, string[,] Table)
        {
            NumAtr = NumAttribute;
            if (Table != null)
            {
                if (Table.GetLength(0) != NumAttribute)
                {
                    // ошибка: количество столбцов входного массива больше числа атрибутов в таблице (для блока try..catch)
                }

                TableBody = new List<string>[NumAttribute];
                for (int i = 0; i < NumAttribute; i++)
                {
                    TableBody[i] = new List<string>();
                    for (int j = 0; j < Table.GetLength(1); j++)
                        TableBody[i].Add(Table[i, j]);
                }
            }
            else
            {
                TableBody = new List<string>[NumAtr];
            }
        }
        //

        // МЕТОДЫ
        // процедура связывания таблиц
        public abstract void LinkTable(List<TTable> Tables);

        // добавление элементов в таблицу
        public virtual void Add(string[,] InsTable)
        {    
            // добавление данных в таблицу           
            for (int i = 0; i < TableHead.Count; i++)
            {
                if (TableBody[i] == null)
                {
                    TableBody[i] = new List<string>();
                }
                for (int j = 0; j < InsTable.GetLength(1); j++)
                    TableBody[i].Add(InsTable[i, j]);
            }
        }

        // удаление элементов из таблицы
        public virtual void Delete(string[] PK)
        {
            for (int i = 0; i < PK.Length; i++)
            {
                int DelInd = TableBody[0].IndexOf(PK[i]);
                for(int j = 0; j < TableHead.Count; j++)
                {
                    TableBody[j].RemoveAt(DelInd);
                }
            }
        }

        // проверка таблицы перед вставкой
        protected void BeforeAddCheck(string[,] InsTable)
        {
            // проверка на равенство количества столбцов AddTable количеству атрибутов в TableHead
            if (InsTable.GetLength(0) != TableHead.Count)
            {
                throw new NotMatchTableEx();
            }

            // проверка на пустую входную таблицу или значения в ней
            if (InsTable == null)
            {
                throw new NullTableEx();
            }

            // проверка на пустые элементы в таблице
            for (int i = 0; i < InsTable.GetLength(0); i++)
                for (int j = 0; j < InsTable.GetLength(1); j++)
                {
                    if (!IsNotNull(InsTable[i, j]))
                        throw new NullElem("Входная таблица имеет пустые элементы!");
                }

            if (TableBody[0] != null)
            {
                // проверка на уникальность ключей в InsTable
                for (int i = 0; i < InsTable.GetLength(1); i++)
                {
                    if (!IsUnique(TableHead[0], InsTable[0, i]))
                        throw new NotUniqueKeyInTableEx("Входная таблица содержит не уникальный ключ!");
                }
            }

            // проверка на связность при добавлении
            object[] PK = new object[InsTable.GetLength(1)];
            foreach (TableLink tableLink in Links)
            {
                if (tableLink.Type)
                {
                    for (int i = 0; i < InsTable.GetLength(1); i++)
                    {
                        PK[i] = InsTable[tableLink.FK, i];
                    }

                    bool IsEq = false;
                    foreach (string str in tableLink.Tab.TableBody[0])
                    {
                        for (int i = 0; i < PK.Length; i++)
                        {
                            if (String.Equals(str, PK[i]))
                            {
                                IsEq = true;
                                break;
                            }
                        }
                        if (IsEq) break;
                    }

                    if(!IsEq) throw new LinkedTableEx("Добавляемого элемента нет в связной таблице!");
                }
            }
        }

        // проверка таблицы перед удалением
        protected void BeforeDeleteCheck(string[] PK)
        {
            // проверка на пустую входную таблицу или значения в ней
            if (PK == null)
            {
                throw new NullTableEx();
            }

            // проверка на пустые элементы в таблице
            for (int i = 0; i < PK.Length; i++)
                if (!IsNotNull(PK[i]))
                    throw new NullElem("Входная таблица содержит пустые элементы!");

            // проверка на отсутствующие элементы
            if (TableBody[0] != null)
            {
                for (int i = 0; i < PK.Length; i++)
                {
                    if (IsUnique(TableHead[0], PK[i]))
                        throw new NotUniqueKeyInTableEx("Часть элементов не найдено в таблице!");
                }
            }

            // исключение повторяющихся элементов
            PK = PK.Distinct().ToArray();

            // проверка на связность при удалении
            foreach(TableLink tableLink in Links)
            {
                if(!tableLink.Type)
                {
                    foreach (string str in tableLink.Tab.TableBody[tableLink.FK])
                    {
                        for (int i = 0; i < PK.Length; i++)
                        {
                            if (String.Equals(str, PK[i]))
                            {
                                throw new LinkedTableEx("Попытка удаления связной строки!");
                            }
                        }
                    }
                }
            }
        }

        // проверка атрибута на уникальность       
        protected bool IsUnique(string Attrib, string Item)
        {
            int AtrInd = TableHead.IndexOf(Attrib);

            foreach(string str in TableBody[AtrInd])
            {
                if (String.Equals(Item, str)) return false;
            }

            return true;
        }

        // проверка элемента на непустое значение (null или пустая строка)
        protected bool IsNotNull(string Item)
        {
            if (String.IsNullOrWhiteSpace(Item) ||
                String.IsNullOrEmpty(Item))
                    return false;
            return true;
        }

        // получение строк по заданному значению атрибута
        public string[][] GetRows(List<string> Attributes, List<string> AtrQue, List<string> AttrValue)
        {

            string[][] Res = new string[Attributes.Count][];
            for(int i = 0; i < Res.Length; i++)
            {
                Res[i] = new string[0];
            }

            for(int i = 0; i < TableBody[0].Count; i++)
            {
                bool IsEq = true;
                for(int j = 0; j < AtrQue.Count; j++)
                {
                    int ind = TableHead.IndexOf(AtrQue[j]);
                    IsEq &= AttrValue[j] == TableBody[ind][i];
                }

                if (IsEq)
                {
                    for (int j = 0; j < Attributes.Count; j++)
                    {
                        Array.Resize(ref Res[j], Res[j].Length + 1);
                        Res[j][Res[j].Length - 1] = TableBody[TableHead.IndexOf(Attributes[j])][i];
                    }
                }
            }

            return Res;
        }
        //

        // СВОЙСТВА
        // элемент таблицы
        public string this[string Attribute, int index]
        {
            get { return TableBody[TableHead.IndexOf(Attribute)][index]; }
        }

        // количество атрибутов в таблице
        public int AttributeCount
        {
            get { return NumAtr; }
        }

        // количество строк таблицы
        public int RowCount
        {
            get { return TableBody[0].Count; }
        }

        // заголовок таблицы
        public List<string> Head
        {
            get { return TableHead; }
        }
    }

    // Класс для представления таблицы с пользователями 
    public class TUsers : TTable
    {
        // конструкторы
        public TUsers() : base(4)
        {
            TableHead = new List<string>() { "ID", "Фамилия", "Имя", "Отчество" };
        }
        public TUsers(string[,] Table) : base(4, Table)
        {
            TableHead = new List<string>() { "ID", "Фамилия", "Имя", "Отчество" };
        }

        // методы
        public override void LinkTable(List<TTable> Tables)
        {
            Links = new List<TableLink>() { new TableLink() { Type = false, Tab = Tables[0], FK = 1},
                                            new TableLink() { Type = false, Tab = Tables[1], FK = 4}};
        }

        public override void Add(string[,] InsTable)
        {
            BeforeAddCheck(InsTable);
            base.Add(InsTable);
        }
        public override void Delete(string[] PK)
        {
            BeforeDeleteCheck(PK);
            base.Delete(PK);
        }
    }

    //Класс для представления таблицы с проектами
    public class TProjects : TTable
    {
        // конструкторы
        public TProjects() : base(3)
        {
            TableHead = new List<string>() { "Наименование", "Руководитель", "Описание" };
        }
        public TProjects(string[,] Table) : base(3, Table)
        {
            TableHead = new List<string>() { "Наименование", "Руководитель", "Описание" };
        }

        // методы
        public override void LinkTable(List<TTable> Tables)
        {
            Links = new List<TableLink>() { new TableLink() { Type = true, Tab = Tables[0],  FK = 1 },
                                            new TableLink() { Type = false, Tab = Tables[1], FK = 5 }};
        }

        public override void Add(string[,] InsTable)
        {
            BeforeAddCheck(InsTable);
            base.Add(InsTable);
        }
        public override void Delete(string[] PK)
        {
            BeforeDeleteCheck(PK);
            base.Delete(PK);
        }
    }

    //Класс для представления таблицы с задачами
    public class TTasks : TTable
    {
        //конструкторы
        public TTasks() : base(7)
        {
            TableHead = new List<string>() { "Номер", "Тема", "Тип", "Приоритет", "Исполнитель", "Проект", "Описание" };
        }
        public TTasks(string[,] Table) : base(7, Table)
        {
            TableHead = new List<string>() { "Номер", "Тема", "Тип", "Приоритет", "Исполнитель", "Проект", "Описание" };
        }

        
        // методы
        protected void BeforeAddCheck(ref string[,] InsTable)
        {
            base.BeforeAddCheck(InsTable);

            // проверка числового значения приоритета (от 1 до 10)
            int ind = TableHead.IndexOf("Приоритет");
            for(int i = 0; i < InsTable.GetLength(1); i++)
            {
                if (Convert.ToInt32(InsTable[ind, i]) > 10) InsTable[ind, i] = "10";
                else if (Convert.ToInt32(InsTable[ind, i]) < 1) InsTable[ind, i] = "1";
            }
        }
        public override void Add(string[,] InsTable)
        {
            BeforeAddCheck(ref InsTable);
            base.Add(InsTable);
        }
        public override void Delete(string[] PK)
        {
            BeforeDeleteCheck(PK);
            base.Delete(PK);
        }
        public override void LinkTable(List<TTable> Tables)
        {
            Links = new List<TableLink>() { new TableLink() { Type = true, Tab = Tables[0], FK = 4 },
                                            new TableLink() { Type = true, Tab = Tables[1], FK = 5 }};
        }
    }
}

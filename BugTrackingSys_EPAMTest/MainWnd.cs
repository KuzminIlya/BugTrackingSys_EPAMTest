using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SQLite;
using System.IO;

namespace BugTrackingSys_EPAMTest
{
    public partial class MainWnd : Form
    {
        public MainWnd()
        {
            InitializeComponent();
        }

        // Объекты с таблицами
        TTable  Users,      // Пользователи 
                Projects,   // Проекты
                Tasks;      // Задачи

        // Окна для ввода доп. информации
        EditTablesWnd editTablesWnd; // добавление, удаление элементов из таблиц и выдача запросов
        CreatedbWnd createdbWnd;     // создание новых таблиц


        public string               FileName;
        public string               LogFileName;
        public string               FilePath;
        public SQLiteConnection     db_Connection;      
        public SQLiteCommand        db_Command;

        public StreamWriter         WriteLog;

        public bool IsInsert  = false,                  // Флаги для добавления и удаления
                    IsDelete  = false;                  // элементов из таблиц
        public bool IsChanged = false;                  // флаг изменения таблиц

        public List<string>     TablesList;
        public string           TableName;                      // имя текущей таблицы
        public TTable           table;                          // текущая таблица
        public string           Commands;                       // Запросы к БД


        // -------------- ВСПОМАГАТЕЛЬНЫЕ ФУНКЦИИ И ПРОЦЕДУРЫ ---------------------
        // Процедура добавления нового сообщения в логи
        public void AddLog(string LogMessage)
        {
            rchtxtbx_Logs.Text += System.DateTime.Now.ToString() + ":  " + LogMessage + ";\n";
        }

        // Процедура блокировки некоторых пунктов главного меню
        public void LockUnlockMenu(bool lck)
        {
            mmenu_FileClose.Enabled = lck;
            mmenuFileSave.Enabled = lck;
            mmenuTablesAdd.Enabled = lck;
            mmenuTablesDelete.Enabled = lck;
            mmenuQueriesShowTable.Enabled = lck;
            mmenuQueriesShowTaskListProject.Enabled = lck;
            mmenuQueriesShowTaskListUser.Enabled = lck;
        }

        // Процедура создания нового файла БД
        public void CreateDB(string FileName)
        {
            db_Connection = new SQLiteConnection();
            db_Command = new SQLiteCommand();

            // Создание файла БД
            SQLiteConnection.CreateFile(FileName);
            db_Connection = new SQLiteConnection("Data Source = " + FileName + "; Version=3;");
            db_Connection.Open();
            db_Command.Connection = db_Connection;

            // Создание таблиц в базе
            db_Command.CommandText = "CREATE TABLE Пользователи" +
                                    "(" +
                                        "ID                     TEXT        NOT NULL    UNIQUE, " +
                                        "Фамилия                TEXT        NOT NULL, " +
                                        "Имя                    TEXT        NOT NULL," +
                                        "Отчество               TEXT        NOT NULL," +

                                        "PRIMARY KEY (ID)" +
                                     ");";

            db_Command.CommandText += " CREATE TABLE Проекты" +
                                     "(" +
                                        "Наименование            TEXT    NOT NULL    UNIQUE, " +
                                        "Руководитель            TEXT    NOT NULL, " +
                                        "Описание                TEXT," +

                                        "PRIMARY KEY (Наименование)," +
                                        "FOREIGN KEY (Руководитель) REFERENCES Пользователи(ID)" +
                                      ");";

            db_Command.CommandText += " CREATE TABLE Задачи" +
                                      "(" +
                                        "Номер              TEXT        NOT NULL," +
                                        "Тема               TEXT        NOT NULL," +
                                        "Тип                TEXT        NOT NULL," +
                                        "Приоритет          TEXT        NOT NULL," +
                                        "Исполнитель        TEXT        NOT NULL," +
                                        "Проект             TEXT        NOT NULL," +
                                        "Описание           TEXT," +

                                        "PRIMARY KEY (Номер)," +
                                        "FOREIGN KEY (Исполнитель)  REFERENCES Пользователи(ID)," +
                                        "FOREIGN KEY (Проект)       REFERENCES Проекты(Наименование)" +
                                       ");";

            db_Command.ExecuteNonQuery();
            db_Connection.Close();
        }

        // Функция Загрузки данных из файла БД
        public string[,] LoadDB(SQLiteConnection DB, string Table_Name, int NumAttr)
        {
            DataTable DB_Table = new DataTable();
            SQLiteDataAdapter DB_Adapter = new SQLiteDataAdapter("SELECT * FROM " + Table_Name, DB);
            DB_Adapter.Fill(DB_Table);

            string[,] Arr = new string[NumAttr, DB_Table.Rows.Count];

            for (int i = 0; i < DB_Table.Rows.Count; i++)
            {
                for (int j = 0; j < NumAttr; j++)
                {
                    Arr[j, i] = DB_Table.Rows[i].ItemArray[j].ToString();
                }
            }

            return Arr;
        }

        // Сохранение изменений в файл БД
        public void SaveAll()
        {
            SQLiteConnection DB_Connection = new SQLiteConnection();
            SQLiteCommand DB_Command = new SQLiteCommand();

            try
            {
                DB_Connection = new SQLiteConnection("Data Source = " + FileName + "; Version=3;");
                DB_Connection.Open();
                DB_Command.Connection = DB_Connection;
                DB_Command.CommandText = Commands;
                DB_Command.ExecuteNonQuery();
                DB_Connection.Close();

                IsChanged = false;
                Commands = "";

                AddLog("Данные сохранены в файл БД - \' " + FileName + "\'");
            }
            catch (SQLiteException SqEx)
            {
                AddLog("Не удалось сохранить изменения в файл БД - \' " + FileName + "\'");
                MessageBox.Show(SqEx.Message, "Ошибка сохранения в файл базы данных!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                DB_Connection.Close();
            }
        }

        // При несохраненных изменениях
        public bool OnChanged()
        {
            // Сохранение текущих изменений в файл (с запросом)
            if (IsChanged)
            {
                switch (MessageBox.Show("Сохранить текущие изменения в таблицах?",
                                "Некоторые таблицы изменены!", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning))
                {
                    case DialogResult.Yes:
                        SaveAll();
                        return true;

                    case DialogResult.No:
                        return true;

                    case DialogResult.Cancel:
                        return false;
                }
            }
            else
            {
                return true;
            }
            return false;
        }

        // Запрос на вставку в БД
        public string GetInsertStr(string TabName, List<string> tabHead, object[,] Arr)
        {
            string Fields = "(";
            for (int i = 0; i < tabHead.Count - 1; i++)
            {
                Fields += tabHead[i] + ", ";
            }
            Fields += tabHead[tabHead.Count - 1] + ")";

            // формирование ряда запросов INSERT в соответствии с заданным количеством добавляемых элементов
            string Command = "";
            for (int i = 0; i < Arr.GetLength(1); i++)
            {
                Command += "INSERT INTO " + TableName + " " + Fields + " VALUES (";
                for (int j = 0; j < Arr.GetLength(0) - 1; j++)
                {
                    Command += "\'" + Arr[j, i].ToString() + "\', ";
                }
                Command += "\'" + Arr[Arr.GetLength(0) - 1, i].ToString() + "\');";
            }

            return Command;
        }

        // Запрос на удаление из БД
        public string GetDeleteStr(string TabName, string Head, object[] PK)
        {
            // Прохождение по введенным первичным ключам и формирование запроса на удаление для каждого из них
            string Command = "";
            for (int i = 0; i < PK.Length; i++)
            {
                Command += "DELETE FROM " + TabName + " WHERE " + Head + " = ";
                Command += "\'" + PK[i].ToString() + "\';";
            }

            return Command;
        }

        // для запросов
        public void CreateQueries(TTable table, List<string> HeadQue, List<string> QueAtr, List<string> QueVal)
        {
            string[][] Que = table.GetRows(HeadQue,
                                           QueAtr,
                                           QueVal);

            dtGrdV_TablesView.Columns.Clear();
            dtGrdV_TablesView.Rows.Clear();

            dtGrdV_TablesView.ColumnCount = Que.Length;
            dtGrdV_TablesView.RowCount = Que[0].Length;
            for (int i = 0; i < dtGrdV_TablesView.ColumnCount; i++)
                dtGrdV_TablesView.Columns[i].HeaderText = HeadQue[i];

            for (int i = 0; i < dtGrdV_TablesView.RowCount; i++)
            {
                for (int j = 0; j < dtGrdV_TablesView.ColumnCount; j++)
                {
                    dtGrdV_TablesView[j, i].Value = Que[j][i];
                }
            }
        }


        // -------------- ОБРАБОТЧИКИ СОБЫТИЙ ---------------------
        private void MainWnd_Load(object sender, EventArgs e)
        {
            // создание лог файла (если такового нет в рабочем каталоге)
            string LogFilePath = Environment.CurrentDirectory;
            if (!File.Exists(LogFilePath + "\\BTS_Logs.txt"))
            {
                File.CreateText(LogFilePath + "\\BTS_Logs.txt");
            }
            LogFileName = LogFilePath + "\\BTS_Logs.txt";

            LockUnlockMenu(false);
            TablesList = new List<string>() { "Пользователи", "Проекты", "Задачи" };
        }

        // Пункт меню - Файл
        private void mmenu_FileCreate_Click(object sender, EventArgs e)                         // Создание пустых таблиц
        {
            //запрос сохранения
            if (!OnChanged())
                return;

            // Вызов окна ввода имени файла и пути сохранения
            createdbWnd = new CreatedbWnd();
            if (createdbWnd.ShowDialog(this) == DialogResult.Cancel)
                return;

            // Создание файла БД с таблицами
            try
            {
                FileName = createdbWnd.File_Path;
                CreateDB(FileName);

                // Создание пустых таблиц
                Users = new TUsers();
                Projects = new TProjects();
                Tasks = new TTasks();

                // связывание созданных таблиц
                Users.LinkTable(new List<TTable>() { Projects, Tasks });
                Projects.LinkTable(new List<TTable>() { Users, Tasks });
                Tasks.LinkTable(new List<TTable>() { Users, Projects });

                // установка вида окна в состояние после создания таблиц/загрузки данных
                dtGrdV_TablesView.Rows.Clear();
                dtGrdV_TablesView.Columns.Clear();
                LockUnlockMenu(true);               

                AddLog("Создан файл БД - \' " + FileName + "\'");
            }
            catch (SQLiteException SqEx)
            {
                LockUnlockMenu(false);
                AddLog("Не удалось создать файл с БД - \' " + FileName + "\'");
                MessageBox.Show(SqEx.Message, "Ошибка создания файла БД!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                db_Connection.Close();
            }
        }

        private void mmenu_FileLoad_Click(object sender, EventArgs e)                           // Загрузка таблиц из файла
        {
            TTable  oldUsers = Users,
                    oldProjects = Projects,
                    oldTasks = Tasks;
            string oldFileName = FileName;

            //запрос сохранения
            if (!OnChanged())
                return;

            // вызов окна выбора файла БД
            if (opnFlDlg_LoadDataSource.ShowDialog() == DialogResult.Cancel)
                return;

            try
            {
                // Соединение с БД
                db_Connection = new SQLiteConnection("Data Source = " + opnFlDlg_LoadDataSource.FileName + "; Version=3;");
                db_Connection.Open();

                FileName = opnFlDlg_LoadDataSource.FileName;

                // Загрузка таблиц из БД
                Users = new TUsers(LoadDB(db_Connection, "Пользователи", 4));
                Projects = new TProjects(LoadDB(db_Connection, "Проекты", 3));
                Tasks = new TTasks(LoadDB(db_Connection, "Задачи", 7));

                // Связывание таблиц
                Users.LinkTable(new List<TTable>() { Projects, Tasks });
                Projects.LinkTable(new List<TTable>() { Users, Tasks });
                Tasks.LinkTable(new List<TTable>() { Users, Projects });

                // установка вида окна
                dtGrdV_TablesView.Rows.Clear();
                dtGrdV_TablesView.Columns.Clear();
                LockUnlockMenu(true);

                AddLog("Данные загружены из файла БД - \' " + FileName + "\'");
                db_Connection.Close();
            }
            catch (SQLiteException SqEx)
            {
                LockUnlockMenu(false);
                MessageBox.Show(SqEx.Message, "Ошибка соединения с БД!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                AddLog("Не удалось загрузить данные из файла БД - \' " + opnFlDlg_LoadDataSource.FileName + "\'");

                Users = oldUsers;
                Projects = oldProjects;
                Tasks = oldTasks;

                FileName = oldFileName;

                db_Connection.Close();
            }
        }

        private void mmenuFileSave_Click(object sender, EventArgs e)                            // Сохранение изменений в файл
        {
            if(IsChanged)
            {
                SaveAll();
            }
        }

        private void mmenu_FileClose_Click(object sender, EventArgs e)                          // Запись таблиц в файл и очистка текущих объектов
        {
            //запрос сохранения
            if (!OnChanged())
                return;

            // Очистка элементов
            dtGrdV_TablesView.Rows.Clear();
            dtGrdV_TablesView.Columns.Clear();
            LockUnlockMenu(false);
            IsInsert = false;
            IsDelete = false;
            IsChanged = false;
            AddLog("Файл БД - \'" + FileName + "\' закрыт");
            FileName = "";
            TableName = "";
            Commands = "";           
        }

        private void mmenu_FileExit_Click(object sender, EventArgs e)                           // Запись таблиц в файл и выход
        {
            //запрос сохранения
            if (!OnChanged())
                return;

            if(!File.Exists(LogFileName))
            {
                File.CreateText(LogFileName);
            }
            using (StreamWriter sw = File.AppendText(LogFileName))
            {
                foreach (string str in rchtxtbx_Logs.Lines)
                    sw.WriteLine(str);
            }              

            Application.Exit();
        }


        // Пункт меню - Таблицы
        private void mmenuTablesAdd_Click(object sender, EventArgs e)                           // Формирование массивов для добавление полей    
        {
            // добавление элементов в таблицы
            //вызов окна с выбором таблицы и количества элементов
            editTablesWnd = new EditTablesWnd();
            editTablesWnd.Text = "Добавление данных в таблицу";
            if (editTablesWnd.ShowDialog(this) == DialogResult.Cancel)
                return;
            
            try
            {
                // режим редактирования
                IsInsert = true;
                LockUnlockMenu(false);
                mmenuTablesRecCanсel.Enabled = true;
                mmenuTablesRecEdit.Enabled = true;
                TableName = editTablesWnd.cmbBx_TablesList.Text;
                dtGrdV_TablesView.Columns.Clear();
                dtGrdV_TablesView.Rows.Clear();
                dtGrdV_TablesView.ReadOnly = false;
                switch (editTablesWnd.cmbBx_TablesList.Text)
                {
                    case "Пользователи":
                        table = Users;
                        break;
                    case "Проекты":
                        table = Projects;
                        break;
                    case "Задачи":
                        table = Tasks;
                        break;
                }

                //Вывод таблицы с заданным количеством строк и заголовком выбранной таблицы
                int rowsnum = Convert.ToInt32(editTablesWnd.txtBx_StrNum.Text);
                if (rowsnum == 0)
                    throw new FormatException("Количество строк не должно быть нулевым!", null);

                foreach (string colcapt in table.Head)
                {
                    dtGrdV_TablesView.Columns.Add(colcapt, colcapt);
                }
                dtGrdV_TablesView.RowCount = rowsnum;

                AddLog("Добавление новых данных в таблицу \'" + TableName + "\'");
            }
            catch (FormatException fex)
            {
                LockUnlockMenu(true);
                mmenuTablesRecCanсel.Enabled = false;
                mmenuTablesRecEdit.Enabled = false;
                dtGrdV_TablesView.Columns.Clear();
                dtGrdV_TablesView.Rows.Clear();
                dtGrdV_TablesView.ReadOnly = true;

                AddLog("Добавление данных в \'" + TableName + "\' не выполнено");
                MessageBox.Show(fex.Message, "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                IsInsert = false;
            }
        }

        private void mmenuTablesDelete_Click(object sender, EventArgs e)                        // Формирование массивов для удаление полей 
        {
            //удаление данных из таблицы
            // вызов окна с выбором таблицы и количества удаляемых элементов
            editTablesWnd = new EditTablesWnd();
            editTablesWnd.Text = "Удаление данных";
            if (editTablesWnd.ShowDialog(this) == DialogResult.Cancel)
                return;

            try
            {
                // установка окна программы в режим редактирования базы
                IsDelete = true;
                TableName = editTablesWnd.cmbBx_TablesList.Text;
                LockUnlockMenu(false);
                mmenuTablesRecCanсel.Enabled = true;
                mmenuTablesRecEdit.Enabled = true;
                dtGrdV_TablesView.Columns.Clear();
                dtGrdV_TablesView.Rows.Clear();
                dtGrdV_TablesView.ReadOnly = false;
                switch (editTablesWnd.cmbBx_TablesList.Text)
                {
                    case "Пользователи":
                        table = Users;
                        break;
                    case "Проекты":
                        table = Projects;
                        break;
                    case "Задачи":
                        table = Tasks;
                        break;
                }

                // вывод столбца с ключом текущей таблицы
                int rowsnum = Convert.ToInt32(editTablesWnd.txtBx_StrNum.Text);
                if (rowsnum == 0)
                    throw new FormatException("Количество строк не должно быть нулевым!", null);
                dtGrdV_TablesView.Columns.Add(table.Head[0], table.Head[0]);
                dtGrdV_TablesView.RowCount = rowsnum;

                AddLog("Удаление данных из таблицы \'" + TableName + "\'");
            }
            catch (FormatException fex)
            {
                LockUnlockMenu(true);
                mmenuTablesRecCanсel.Enabled = false;
                mmenuTablesRecEdit.Enabled = false;
                dtGrdV_TablesView.Columns.Clear();
                dtGrdV_TablesView.Rows.Clear();
                dtGrdV_TablesView.ReadOnly = true;

                AddLog("Удаление данных из \'" + TableName + "\' не выполнено");
                MessageBox.Show(fex.Message, "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                IsDelete = false;
            }

        }           

        private void mmenuTablesRecEdit_Click(object sender, EventArgs e)                       // Внесение изменений в таблицы
        {
            // запись результатов добавления и удаления элементов в таблицы
            string TableName = editTablesWnd.cmbBx_TablesList.Text;
            
            try
            {
                if (IsInsert) // при добавлении
                {
                    string[,] Arr;

                    // Создание таблицы для добавления
                    Arr = new string[table.AttributeCount, dtGrdV_TablesView.RowCount];
                    for(int i = 0; i < dtGrdV_TablesView.ColumnCount; i++)
                        for (int j = 0; j < dtGrdV_TablesView.RowCount; j++)
                        {
                            if (dtGrdV_TablesView[i, j].Value != null)
                            {
                                Arr[i, j] = dtGrdV_TablesView[i, j].Value.ToString();
                            }
                            else
                            {
                                Arr[i, j] = "";
                            }
                        }

                    table.Add(Arr);

                    if (!IsChanged)
                    {
                        IsChanged = true;
                        Commands = "";
                    }
                    Commands += GetInsertStr(TableName, table.Head, Arr);
                    IsInsert = false;
                    AddLog("Данные записаны в таблицу \'" + TableName + "\'");
                }

                if(IsDelete) // для удаления
                {
                    string[] PK;
                    PK = new string[dtGrdV_TablesView.RowCount];
                    for (int i = 0; i < dtGrdV_TablesView.RowCount; i++)
                    {
                        if (dtGrdV_TablesView[0, i].Value != null)
                        {
                            PK[i] = dtGrdV_TablesView[0, i].Value.ToString();
                        }
                        else
                        {
                            PK[i] = "";
                        }
                    }

                    table.Delete(PK);

                    if (!IsChanged)
                    {
                        IsChanged = true;
                        Commands = "";
                    }

                    Commands += GetDeleteStr(TableName, table.Head[0], PK);
                    IsDelete = false;
                    AddLog("Данные удалены из таблицы - \'" + TableName + "\'");
                }

                // возврат окна программы в начальный вид
                LockUnlockMenu(true);
                mmenuTablesRecCanсel.Enabled = false;
                mmenuTablesRecEdit.Enabled = false;
            }
            catch (NotMatchTableEx Ex)
            {
                AddLog("Входная таблица не совпадает с таблицей - \' " + TableName);
                MessageBox.Show(Ex.Message);              
                LockUnlockMenu(false);
                mmenuTablesRecCanсel.Enabled = true;
                mmenuTablesRecEdit.Enabled = true;
            }
            catch(NullTableEx Ex)
            {
                AddLog("Входная таблица пустая;");
                MessageBox.Show(Ex.Message);
                LockUnlockMenu(false);
                mmenuTablesRecCanсel.Enabled = true;
                mmenuTablesRecEdit.Enabled = true;
            }
            catch(NullElem Ex)
            {
                AddLog(Ex.Message);
                MessageBox.Show(Ex.Message);
                LockUnlockMenu(false);
                mmenuTablesRecCanсel.Enabled = true;
                mmenuTablesRecEdit.Enabled = true;
            }
            catch (NotUniqueKeyInTableEx Ex)
            {
                AddLog(Ex.Message);
                MessageBox.Show(Ex.Message);
                LockUnlockMenu(false);
                mmenuTablesRecCanсel.Enabled = true;
                mmenuTablesRecEdit.Enabled = true;
            }
            catch(LinkedTableEx Ex)
            {
                AddLog(Ex.Message);
                MessageBox.Show(Ex.Message);
                LockUnlockMenu(false);
                mmenuTablesRecCanсel.Enabled = true;
                mmenuTablesRecEdit.Enabled = true;
            }
            catch (FormatException)
            {
                AddLog("Неверные данные в числовых полях;");
                MessageBox.Show("Неверные данные в числовых полях!");
                LockUnlockMenu(false);
                mmenuTablesRecCanсel.Enabled = true;
                mmenuTablesRecEdit.Enabled = true;
            }
        }                   

        private void mmenuTablesRecCansel_Click(object sender, EventArgs e)                     // Отмена изменений в таблицах
        {
            if (IsInsert) AddLog("Отмена операции вставки");
            if (IsDelete) AddLog("Отмена операции удаления");
            IsInsert = IsDelete = false;

            dtGrdV_TablesView.Columns.Clear();
            dtGrdV_TablesView.Rows.Clear();
            dtGrdV_TablesView.ReadOnly = true;

            LockUnlockMenu(true);
            mmenuTablesRecCanсel.Enabled = false;
            mmenuTablesRecEdit.Enabled = false;
        }


        // Пункт меню - Запросы
        private void mmenuQueriesShowTable_Click(object sender, EventArgs e)                    // Показать таблицы целиком на выбор
        {
            // выдача всех данных из выбранной таблицы
            // вызов окна с выбором таблицы
            editTablesWnd = new EditTablesWnd();
            editTablesWnd.Text = "Показать данные таблицы";
            editTablesWnd.txtBx_StrNum.Visible = false;
            editTablesWnd.lblStrNum.Visible = false;
            if (editTablesWnd.ShowDialog(this) == DialogResult.Cancel)
                return;

            // формирование запроса и заполнение DataGridView
            TableName = editTablesWnd.cmbBx_TablesList.Text;
            dtGrdV_TablesView.Columns.Clear();
            dtGrdV_TablesView.Rows.Clear();
            switch (TableName)
            {
                case "Пользователи":
                    table = Users;
                    break;
                case "Проекты":
                    table = Projects;
                    break;
                case "Задачи":
                    table = Tasks;
                    break;
            }

            dtGrdV_TablesView.ColumnCount = table.AttributeCount;
            dtGrdV_TablesView.RowCount = table.RowCount;
            for (int i = 0; i < table.AttributeCount; i++)
                dtGrdV_TablesView.Columns[i].HeaderText = table.Head[i];

            for (int i = 0; i < table.AttributeCount; i++)
            {
                for (int j = 0; j < table.RowCount; j++)
                {
                    dtGrdV_TablesView[i, j].Value = table[table.Head[i], j];
                }
            }

            dtGrdV_TablesView.ReadOnly = true;
            AddLog("Выполнен запрос на выдачу таблицы \'" + editTablesWnd.cmbBx_TablesList.Text + "\'");
        }

        private void mmenuQueriesShowTaskListProject_Click(object sender, EventArgs e)          // Показать список задач в проекте
        {
            editTablesWnd = new EditTablesWnd();
            editTablesWnd.Text = "Показать данные таблицы";
            editTablesWnd.txtBx_StrNum.Visible = true;
            editTablesWnd.lblStrNum.Visible = true;
            editTablesWnd.lblTables.Visible = false;
            editTablesWnd.cmbBx_TablesList.Visible = false;
            editTablesWnd.lblStrNum.Text = "Значение\n поля";
            if (editTablesWnd.ShowDialog(this) == DialogResult.Cancel)
                return;

            CreateQueries(Tasks, new List<string>() { "Номер", "Тема", "Тип", "Приоритет", "Исполнитель", "Описание" },
                                 new List<string>() { "Проект" },
                                 new List<string>() { editTablesWnd.txtBx_StrNum.Text });

            AddLog("Выполнен запрос к таблице \'Задачи\' на выдачу списка задач в проекте - " + editTablesWnd.txtBx_StrNum.Text + ";");
        }

        private void mmenuQueriesShowTaskListUser_Click(object sender, EventArgs e)             // Показать список задач на исполнителе
        {
            editTablesWnd = new EditTablesWnd();
            editTablesWnd.Text = "Показать данные таблицы";
            editTablesWnd.txtBx_StrNum.Visible = true;
            editTablesWnd.lblStrNum.Visible = true;
            editTablesWnd.lblTables.Visible = false;
            editTablesWnd.cmbBx_TablesList.Visible = false;
            editTablesWnd.lblStrNum.Text = "Значение\n поля";
            if (editTablesWnd.ShowDialog(this) == DialogResult.Cancel)
                return;

            CreateQueries(Tasks, new List<string>() { "Номер", "Тема", "Тип", "Приоритет", "Проект", "Описание" },
                                 new List<string>() { "Исполнитель" },
                                 new List<string>() { editTablesWnd.txtBx_StrNum.Text });

            AddLog("Выполнен запрос к таблице \'Задачи\' на выдачу списка задач на пользователе - " + editTablesWnd.txtBx_StrNum.Text + ";");
        }
    }
}

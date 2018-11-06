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

namespace BugTrackingSys_EPAMTest
{
    public partial class MainWnd : Form
    {
        public MainWnd()
        {
            InitializeComponent();           
        }

        EditTablesWnd editTablesWnd;
        CreatedbWnd createdbWnd;

        public string               db_FileName;        
        public SQLiteConnection     db_Connection; 
        public SQLiteCommand        db_Command;
        public SQLiteDataAdapter    db_Adapter;
        public DataTable            db_Tab;

        public List<string>         TablesList;         // Список таблиц БД

        public bool IsInsert  = false,                  // Флаши редактирования,
                    IsDelete  = false,                  // добавления и удаления элементов
                    IsEdit    = false;                  // из БД

        public string TableName;                        // Имя текущей БД

        // Процедура добавления нового сообщения в логи
        public void AddLog(string LogMessage, RichTextBox Logs)
        {
            Logs.Text += System.DateTime.Now.ToString() + ":  " + LogMessage + ";\n";
        }

        // Процедура формирования списка с заголовками всех таблиц в базе
        public void GetTablesList(List<string> ts, SQLiteConnection con)
        {
            db_Adapter = new SQLiteDataAdapter("SELECT name FROM sqlite_master WHERE type = 'table' AND name <> 'sqlite_sequence'",
                                               con);
            DataTable tab = new DataTable();
            db_Adapter.Fill(tab);

            for (int i = 0; i < tab.Rows.Count; i++)
            {
                for(int j = 0; j < tab.Rows[i].ItemArray.Length; j++)
                {
                    ts.Add((string)tab.Rows[i].ItemArray[j]);
                }
            }
        }

        // Процедура блокировки некоторых пунктов главного меню
        public void LockUnlockMenu(bool lck)
        {
            mmenu_FileClose.Enabled = lck;

            mmenuTablesAdd.Enabled = lck;
            mmenu_TablesEdit.Enabled = lck;
            mmenuTablesDelete.Enabled = lck;

            mmenuQueriesShowTable.Enabled = lck;
            mmenuQueriesShowTaskListProject.Enabled = lck;
            mmenuQueriesShowTaskListUser.Enabled = lck;
        }


        // -------------- Обработка событий ---------------------
        private void MainWnd_Load(object sender, EventArgs e)
        {
            db_Connection = new SQLiteConnection();
            db_Command = new SQLiteCommand();
            db_Tab = new DataTable();
            TablesList = new List<string>();
        }

        // Пункт меню - Файл
        private void mmenu_FileCreate_Click(object sender, EventArgs e)                         // Создание новой БД
        {
            // Вызов окна ввода имени БД и пути сохранения файла базы
            createdbWnd = new CreatedbWnd();
            if (createdbWnd.ShowDialog(this) == DialogResult.Cancel)
                return;

            try
            {
                // Закрытие соединения с текущей базой
                if (!String.IsNullOrEmpty(db_FileName))
                {
                    db_Connection.Close();
                    db_Tab.Columns.Clear();
                    db_Tab.Rows.Clear();
                    dtGrdV_TablesView.Columns.Clear();
                    dtGrdV_TablesView.Rows.Clear();
                    AddLog("Закрыто соединение с базой данных - \' " + db_FileName + "\'", rchtxtbx_Logs);
                }

                // Создание файла БД и установка соединения
                SQLiteConnection.CreateFile(createdbWnd.DB_Path);
                db_Connection = new SQLiteConnection("Data Source = " + createdbWnd.DB_Path + "; Version=3;");
                db_Connection.Open();
                db_Command.Connection = db_Connection;

                db_Command.CommandText = "PRAGMA foreign_keys=on;";
                db_Command.ExecuteNonQuery();

                // Создание таблиц в базе
                db_Command.CommandText = "CREATE TABLE Пользователи" +
                                        "(" +
                                            "ID_Пользователя        TEXT        NOT NULL    UNIQUE, "           +
                                            "Фамилия                TEXT        NOT NULL, "                     +
                                            "Имя                    TEXT        NOT NULL,"                      +
                                            "Отчество               TEXT        NOT NULL,"                      +

                                            "PRIMARY KEY (ID_Пользователя)" +
                                         ");";

                db_Command.CommandText += " CREATE TABLE Проекты" +
                                         "(" +
                                            "Наименование            TEXT    NOT NULL    UNIQUE, "   +
                                            "Руководитель            TEXT    NOT NULL, "             +
                                            "Описание                TEXT,"                          +

                                            "PRIMARY KEY (Наименование)," +
                                            "FOREIGN KEY (Руководитель) REFERENCES Пользователи(ID_Пользователя)" +
                                          ");";

                db_Command.CommandText += " CREATE TABLE Задачи" +
                                          "(" +
                                            "Номер          INTEGER     NOT NULL    PRIMARY KEY     AUTOINCREMENT ,"                    +
                                            "Тема           TEXT        NOT NULL,"                                                      +
                                            "Тип            TEXT        NOT NULL,"                                                      +
                                            "Приоритет      INTEGER     NOT NULL    CHECK (Приоритет >= 0 AND Приоритет <= 10),"        +
                                            "Исполнитель    TEXT        NOT NULL,"                                                      +
                                            "Проект         TEXT        NOT NULL,"                                                      +
                                            "Описание       TEXT,"                                                                      +

                                            "FOREIGN KEY (Исполнитель) REFERENCES Пользователи(ID_Пользователя)," +
                                            "FOREIGN KEY (Проект)  REFERENCES Проекты(Наименование)" +
                                           ");";

                db_Command.ExecuteNonQuery();

                db_FileName = createdbWnd.DB_Path;

                TablesList.Clear();
                GetTablesList(TablesList, db_Connection);

                dtGrdV_TablesView.Rows.Clear();
                dtGrdV_TablesView.Columns.Clear();

                LockUnlockMenu(true);

                AddLog("Создана новая база данных - \' " + db_FileName + "\'", rchtxtbx_Logs);
                AddLog("Соединение с базой - \' " + db_FileName + "\' установлено", rchtxtbx_Logs);
            }
            catch (SQLiteException SqEx)
            {
                LockUnlockMenu(false);
                AddLog("Не удалось создать файл с базой данных - \' " + createdbWnd.DB_Path + "\'", rchtxtbx_Logs);
                MessageBox.Show(SqEx.Message, "Ошибка создания базы данных!", MessageBoxButtons.OK, MessageBoxIcon.Error);

                if (!String.IsNullOrEmpty(db_FileName))
                {
                    db_Connection = new SQLiteConnection("Data Source = " + db_FileName + "; Version=3;");
                    db_Connection.Open();
                    db_Command.Connection = db_Connection;
                    GetTablesList(TablesList, db_Connection);
                    LockUnlockMenu(true);
                    AddLog("Соединение с базой - \' " + db_FileName + "\' восстановлено", rchtxtbx_Logs);
                }
            }
            
        }

        private void mmenu_FileLoad_Click(object sender, EventArgs e)                           // Загрузка БД из файла
        {
            if (opnFlDlg_LoadDataSource.ShowDialog() == DialogResult.Cancel)
                return;           

            try
            {
                if (!String.IsNullOrEmpty(db_FileName))
                {
                    db_Connection.Close();
                    db_Tab.Columns.Clear();
                    db_Tab.Rows.Clear();
                    dtGrdV_TablesView.Columns.Clear();
                    dtGrdV_TablesView.Rows.Clear();
                    AddLog("Закрыто соединение с базой данных - \' " + db_FileName + "\'", rchtxtbx_Logs);
                }

                db_Connection = new SQLiteConnection("Data Source = " + opnFlDlg_LoadDataSource.FileName + "; Version=3;");
                db_Connection.Open();
                db_Command.Connection = db_Connection;
                db_FileName = opnFlDlg_LoadDataSource.FileName;

                db_Command.CommandText = "PRAGMA foreign_keys=on;"; // Включение контроля внешних ключей
                db_Command.ExecuteNonQuery();

                TablesList.Clear();
                GetTablesList(TablesList, db_Connection);

                dtGrdV_TablesView.Rows.Clear();
                dtGrdV_TablesView.Columns.Clear();

                LockUnlockMenu(true);

                AddLog("Соединение с базой - \' " + db_FileName + "\' установлено", rchtxtbx_Logs);
            }
            catch (SQLiteException SqEx)
            {
                LockUnlockMenu(false);
                MessageBox.Show(SqEx.Message, "Ошибка соединения с БД!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                AddLog("Не удалось установить соединение с базой - \' " + opnFlDlg_LoadDataSource.FileName + "\'", rchtxtbx_Logs);

                if (!String.IsNullOrEmpty(db_FileName))
                {
                    db_Connection = new SQLiteConnection("Data Source = " + db_FileName + "; Version=3;");
                    db_Connection.Open();
                    db_Command.Connection = db_Connection;
                    GetTablesList(TablesList, db_Connection);
                    LockUnlockMenu(true);
                    AddLog("Соединение с базой - \' " + db_FileName + "\' восстановлено", rchtxtbx_Logs);
                }
            }
        }

        private void mmenu_FileClose_Click(object sender, EventArgs e)                          // Закрытие текущего соединения
        {
            if (!String.IsNullOrEmpty(db_FileName))
            {
                LockUnlockMenu(false);
                db_Connection.Close();
                db_Tab.Columns.Clear();
                db_Tab.Rows.Clear();
                dtGrdV_TablesView.Columns.Clear();
                dtGrdV_TablesView.Rows.Clear();
                AddLog("Закрыто соединение с базой данных - \' " + db_FileName + "\'", rchtxtbx_Logs);
            }
        }      

        private void mmenu_FileExit_Click(object sender, EventArgs e)                           // Закрытие текущего соединения и выход
        {
            if (!String.IsNullOrEmpty(db_FileName))
            {
                db_Connection.Close();
                AddLog("Закрыто соединение с базой данных - \' " + db_FileName + "\'", rchtxtbx_Logs);
            }

            Application.Exit();
        }


        // Пункт меню - Таблицы
        private void mmenuTablesAdd_Click(object sender, EventArgs e)                           // Добавление полей    
        {
            editTablesWnd = new EditTablesWnd();
            editTablesWnd.Text = "Добавление данных";
            if (editTablesWnd.ShowDialog(this) == DialogResult.Cancel)
                return;

            try
            {
                IsInsert = true;
                LockUnlockMenu(false);
                mmenuTablesRecCanсel.Enabled = true;
                mmenuTablesRecEdit.Enabled = true;

                TableName = editTablesWnd.cmbBx_TablesList.Text;

                db_Tab.Columns.Clear();
                db_Tab.Rows.Clear();
                dtGrdV_TablesView.Columns.Clear();
                dtGrdV_TablesView.Rows.Clear();

                db_Adapter = new SQLiteDataAdapter("SELECT * FROM " + TableName,
                                       db_Connection);
                db_Adapter.Fill(db_Tab);

                foreach (DataColumn column in db_Tab.Columns)
                {
                    if (!(editTablesWnd.cmbBx_TablesList.Text == "Задачи" && column.Caption == "Номер"))
                        dtGrdV_TablesView.Columns.Add(column.ColumnName, column.Caption);
                }

                dtGrdV_TablesView.RowCount = Convert.ToInt32(editTablesWnd.txtBx_StrNum.Text);

                dtGrdV_TablesView.ReadOnly = false;

                AddLog("Добавление новых данных в таблицу \'" + TableName + "\'", rchtxtbx_Logs);
            }
            catch (SQLiteException SqEx)
            {
                LockUnlockMenu(true);
                mmenuTablesRecCanсel.Enabled = false;
                mmenuTablesRecEdit.Enabled = false;

                AddLog("Добавление данных в таблицу \'" + TableName + "\' не выполнено", rchtxtbx_Logs);
                MessageBox.Show(SqEx.Message, "Ошибка записи в базу!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                IsInsert = false;
            }
        }               

        private void mmenuTablesDelete_Click(object sender, EventArgs e)                        // Удаление полей 
        {
            editTablesWnd = new EditTablesWnd();
            editTablesWnd.Text = "Удаление данных";
            if (editTablesWnd.ShowDialog(this) == DialogResult.Cancel)
                return;
            try
            {
                IsDelete = true;
                LockUnlockMenu(false);
                mmenuTablesRecCanсel.Enabled = true;
                mmenuTablesRecEdit.Enabled = true;

                TableName = editTablesWnd.cmbBx_TablesList.Text;

                db_Tab.Columns.Clear();
                db_Tab.Rows.Clear();
                dtGrdV_TablesView.Columns.Clear();
                dtGrdV_TablesView.Rows.Clear();

                db_Adapter = new SQLiteDataAdapter("SELECT * FROM " + TableName,
                                       db_Connection);
                db_Adapter.Fill(db_Tab);

                dtGrdV_TablesView.Columns.Add(db_Tab.Columns[0].ColumnName, db_Tab.Columns[0].Caption);

                dtGrdV_TablesView.RowCount = Convert.ToInt32(editTablesWnd.txtBx_StrNum.Text);

                dtGrdV_TablesView.ReadOnly = false;

                AddLog("Удаление данных из таблицы \'" + TableName + "\'", rchtxtbx_Logs);
            }
            catch (SQLiteException SqEx)
            {
                LockUnlockMenu(true);
                mmenuTablesRecCanсel.Enabled = false;
                mmenuTablesRecEdit.Enabled = false;

                AddLog("Удаление данных из \'" + TableName + "\' не выполнено", rchtxtbx_Logs);
                MessageBox.Show(SqEx.Message, "Ошибка удаления из базы!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                IsDelete = false;
            }

        }           

        private void mmenu_TablesEdit_Click(object sender, EventArgs e)                         // Редактирование полей
        {
            editTablesWnd = new EditTablesWnd();
            editTablesWnd.Text = "Изменение данных";
            editTablesWnd.txtBx_StrNum.Visible = true;
            editTablesWnd.lblStrNum.Visible = true;
            if (editTablesWnd.ShowDialog(this) == DialogResult.Cancel)
                return;
        }            

        private void mmenuTablesRecEdit_Click(object sender, EventArgs e)                       // Внесение изменений в БД
        {
            string TableName = editTablesWnd.cmbBx_TablesList.Text, Fields = "(";
            try
            {
                if (IsInsert)
                {
                    for (int i = 0; i < dtGrdV_TablesView.Columns.Count - 1; i++)
                    {
                        Fields += dtGrdV_TablesView.Columns[i].HeaderText + ", ";
                    }

                    Fields += dtGrdV_TablesView.Columns[dtGrdV_TablesView.Columns.Count - 1].HeaderText + ")";

                    for (int i = 0; i < dtGrdV_TablesView.Rows.Count; i++)
                    {
                        db_Command.CommandText = "INSERT INTO " + TableName + " " + Fields + " VALUES (";
                        for (int j = 0; j < dtGrdV_TablesView.Columns.Count - 1; j++)
                        {
                            if (dtGrdV_TablesView.Columns[j].HeaderText != "Приоритет")
                                db_Command.CommandText += "\'" + dtGrdV_TablesView[j, i].Value.ToString() + "\', ";
                            else
                                db_Command.CommandText += dtGrdV_TablesView[j, i].Value.ToString() + ", ";
                        }
                        db_Command.CommandText += "\'" + dtGrdV_TablesView[dtGrdV_TablesView.Columns.Count - 1, i].Value.ToString() + "\');";
                        db_Command.ExecuteNonQuery();
                    }

                    mmenuTablesDelete.Enabled = !mmenuTablesDelete.Enabled;
                    mmenu_TablesEdit.Enabled = !mmenu_TablesEdit.Enabled;

                    AddLog("Данные записаны в таблицу \'" + TableName + "\'", rchtxtbx_Logs);
                }

                if(IsDelete)
                {
                    for (int i = 0; i < dtGrdV_TablesView.Rows.Count; i++)
                    {
                        Fields = dtGrdV_TablesView.Columns[0].HeaderText;
                        db_Command.CommandText = "DELETE FROM " + TableName + " WHERE " + Fields + " = ";

                        if ((dtGrdV_TablesView.Columns[0].HeaderText != "Приоритет") && (dtGrdV_TablesView.Columns[0].HeaderText != "Номер"))
                            db_Command.CommandText += "\'" + dtGrdV_TablesView[0, i].Value.ToString() + "\';";
                        else
                            db_Command.CommandText += dtGrdV_TablesView[0, i].Value.ToString() + ";";
                        
                        db_Command.ExecuteNonQuery();
                    }

                    AddLog("Данные удалены из таблицы \'" + TableName + "\'", rchtxtbx_Logs);
                }

                if(IsEdit)
                {

                }

                LockUnlockMenu(true);
                mmenuTablesRecCanсel.Enabled = false;
                mmenuTablesRecEdit.Enabled = false;
            }
            catch (SQLiteException SqEx)
            {
                AddLog("Не удалось изменить данные в таблице - \' " + TableName, rchtxtbx_Logs);
                MessageBox.Show(SqEx.Message);
                
                LockUnlockMenu(false);
                mmenuTablesRecCanсel.Enabled = true;
                mmenuTablesRecEdit.Enabled = true;
            }
        }                   

        private void mmenuTablesRecCansel_Click(object sender, EventArgs e)                     // Отмена изменений в БД
        {
            IsInsert = IsDelete = IsEdit = false;

            db_Tab.Columns.Clear();
            db_Tab.Rows.Clear();
            dtGrdV_TablesView.Columns.Clear();
            dtGrdV_TablesView.Rows.Clear();

            LockUnlockMenu(true);
            mmenuTablesRecCanсel.Enabled = false;
            mmenuTablesRecEdit.Enabled = false;
        }


        // Пункт меню - Запросы
        private void mmenuQueriesShowTable_Click(object sender, EventArgs e)                    // Показать таблицы целиком на выбор
        {
            editTablesWnd = new EditTablesWnd();
            editTablesWnd.Text = "Показать данные таблицы";
            editTablesWnd.txtBx_StrNum.Visible = false;
            editTablesWnd.lblStrNum.Visible = false;
            if (editTablesWnd.ShowDialog(this) == DialogResult.Cancel)
                return;

            try
            {
                TableName = editTablesWnd.cmbBx_TablesList.Text;
                db_Tab.Columns.Clear();
                db_Tab.Rows.Clear();
                db_Adapter = new SQLiteDataAdapter("SELECT * FROM " + TableName,
                                       db_Connection);
                db_Adapter.Fill(db_Tab);

                dtGrdV_TablesView.Columns.Clear();
                dtGrdV_TablesView.Rows.Clear();

                dtGrdV_TablesView.ColumnCount = db_Tab.Columns.Count;
                dtGrdV_TablesView.RowCount = db_Tab.Rows.Count;
                for (int i = 0; i < db_Tab.Columns.Count; i++)
                    dtGrdV_TablesView.Columns[i].HeaderText = db_Tab.Columns[i].Caption;

                for (int i = 0; i < db_Tab.Rows.Count; i++)
                {
                    for (int j = 0; j < db_Tab.Columns.Count; j++)
                    {
                        dtGrdV_TablesView[j, i].Value = db_Tab.Rows[i].ItemArray[j];
                    }
                }

                dtGrdV_TablesView.ReadOnly = true;

                AddLog("Выполнен запрос на выдачу таблицы \'" + editTablesWnd.cmbBx_TablesList.Text + "\'", rchtxtbx_Logs);
            }
            catch (SQLiteException SqEx)
            {
                AddLog("Запрос на выдачу таблицы \'" + editTablesWnd.cmbBx_TablesList.Text + "\' не выполнен", rchtxtbx_Logs);
                MessageBox.Show(SqEx.Message, "Неверный запрос к базе!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void mmenuQueriesShowTaskListProject_Click(object sender, EventArgs e)          // Показать список задач в проекте
        {
            editTablesWnd = new EditTablesWnd();
            editTablesWnd.Text = "Показать данные таблицы";
            editTablesWnd.txtBx_StrNum.Visible = true;
            editTablesWnd.lblStrNum.Visible = true;
            editTablesWnd.lblStrNum.Text = "Значение поля";
            if (editTablesWnd.ShowDialog(this) == DialogResult.Cancel)
                return;

            try
            {
                TableName = editTablesWnd.cmbBx_TablesList.Text;
                string QueField = editTablesWnd.txtBx_StrNum.Text;

                db_Tab.Columns.Clear();
                db_Tab.Rows.Clear();
                dtGrdV_TablesView.Columns.Clear();
                dtGrdV_TablesView.Rows.Clear();

                db_Adapter = new SQLiteDataAdapter("SELECT Тема, Тип, Приоритет, Исполнитель, Проект, Описание FROM Задачи WHERE Проект = " + "\'" + QueField + "\';",
                                       db_Connection);
                db_Adapter.Fill(db_Tab);

                dtGrdV_TablesView.ColumnCount = db_Tab.Columns.Count;
                dtGrdV_TablesView.RowCount = db_Tab.Rows.Count;
                for (int i = 0; i < db_Tab.Columns.Count; i++)
                    dtGrdV_TablesView.Columns[i].HeaderText = db_Tab.Columns[i].Caption;

                for (int i = 0; i < db_Tab.Rows.Count; i++)
                {
                    for (int j = 0; j < db_Tab.Columns.Count; j++)
                    {
                        dtGrdV_TablesView[j, i].Value = db_Tab.Rows[i].ItemArray[j];
                    }
                }

                dtGrdV_TablesView.ReadOnly = true;

                AddLog("Выполнен запрос на выдачу списка задач в проекте \'" + QueField + "\'", rchtxtbx_Logs);
            }
            catch (SQLiteException SqEx)
            {
                AddLog("Запрос к таблице \'" + editTablesWnd.cmbBx_TablesList.Text + "\' не выполнен", rchtxtbx_Logs);
                MessageBox.Show(SqEx.Message, "Неверный запрос к базе!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void mmenuQueriesShowTaskListUser_Click(object sender, EventArgs e)             // Показать список задач на исполнителе
        {

        }
    }
}

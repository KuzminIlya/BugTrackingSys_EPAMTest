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

        public List<string>         TablesList;

        public bool IsInsert  = false, 
                    IsDelete  = false, 
                    IsEdit    = false;
        public string TableName;

        public void AddLog(string LogMessage, RichTextBox Logs)
        {
            Logs.Text += System.DateTime.Now.ToString() + ":  " + LogMessage + ";\n";
        }

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
                    // вставить проверку непроведенных транзакций
                    db_Connection.Close();
                    AddLog("Закрыто соединение с базой данных - \' " + db_FileName + "\'", rchtxtbx_Logs);
                }

                // Создание файла БД и установка соединения
                SQLiteConnection.CreateFile(createdbWnd.DB_Path);
                db_Connection = new SQLiteConnection("Data Source = " + createdbWnd.DB_Path + "; Version=3;");
                db_Connection.Open();
                db_Command.Connection = db_Connection;

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

                AddLog("Создана новая база данных - \' " + db_FileName + "\'", rchtxtbx_Logs);
                AddLog("Соединение с базой - \' " + db_FileName + "\' установлено", rchtxtbx_Logs);
            }
            catch (SQLiteException SqEx)
            {
                AddLog("Не удалось создать файл с базой данных - \' " + createdbWnd.DB_Path + "\'", rchtxtbx_Logs);
                MessageBox.Show(SqEx.Message, "Ошибка создания базы данных!", MessageBoxButtons.OK, MessageBoxIcon.Error);

                if (!String.IsNullOrEmpty(db_FileName))
                {
                    db_Connection = new SQLiteConnection("Data Source = " + db_FileName + "; Version=3;");
                    db_Connection.Open();
                    db_Command.Connection = db_Connection;
                    GetTablesList(TablesList, db_Connection);

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
                    // вставить проверку на непроведенные транзакции
                    db_Connection.Close();
                    AddLog("Закрыто соединение с базой данных - \' " + db_FileName + "\'", rchtxtbx_Logs);
                }

                db_Connection = new SQLiteConnection("Data Source = " + opnFlDlg_LoadDataSource.FileName + "; Version=3;");
                db_Connection.Open();
                db_Command.Connection = db_Connection;
                db_FileName = opnFlDlg_LoadDataSource.FileName;

                TablesList.Clear();
                GetTablesList(TablesList, db_Connection);

                dtGrdV_TablesView.Rows.Clear();
                dtGrdV_TablesView.Columns.Clear();

                AddLog("Соединение с базой - \' " + db_FileName + "\' установлено", rchtxtbx_Logs);
            }
            catch (SQLiteException SqEx)
            {
                MessageBox.Show(SqEx.Message, "Ошибка соединения с БД!", MessageBoxButtons.OK, MessageBoxIcon.Error);

                AddLog("Не удалось установить соединение с базой - \' " + opnFlDlg_LoadDataSource.FileName + "\'", rchtxtbx_Logs);

                if (!String.IsNullOrEmpty(db_FileName))
                {
                    db_Connection = new SQLiteConnection("Data Source = " + db_FileName + "; Version=3;");
                    db_Connection.Open();
                    db_Command.Connection = db_Connection;
                    GetTablesList(TablesList, db_Connection);

                    AddLog("Соединение с базой - \' " + db_FileName + "\' восстановлено", rchtxtbx_Logs);
                }
            }
        }

        private void mmenu_FileClose_Click(object sender, EventArgs e)                          // Закрытие текущего соединения
        {
            if (!String.IsNullOrEmpty(db_FileName))
            {
                // вставить проверку на непроведенные транзакции
                db_Connection.Close();
                AddLog("Закрыто соединение с базой данных - \' " + db_FileName + "\'", rchtxtbx_Logs);
            }
        }      

        private void mmenu_FileExit_Click(object sender, EventArgs e)                           // Закрытие текущего соединения и выход
        {
            if (!String.IsNullOrEmpty(db_FileName))
            {
                // вставить проверку на непроведенные транзакции
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
            editTablesWnd.txtBx_StrNum.Visible = true;
            editTablesWnd.lblStrNum.Visible = true;
            if (editTablesWnd.ShowDialog(this) == DialogResult.Cancel)
                return;

            try
            {
                IsInsert = true;
                TableName = editTablesWnd.cmbBx_TablesList.Text;
                mmenuTablesDelete.Enabled = !mmenuTablesDelete.Enabled;
                mmenu_TablesEdit.Enabled = !mmenu_TablesEdit.Enabled;

                db_Tab.Columns.Clear();
                db_Tab.Rows.Clear();
                db_Adapter = new SQLiteDataAdapter("SELECT * FROM " + TableName,
                                       db_Connection);
                db_Adapter.Fill(db_Tab);

                dtGrdV_TablesView.Columns.Clear();
                dtGrdV_TablesView.Rows.Clear();

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
                AddLog("Добавление данных в таблицу \'" + TableName + "\' не выполнено", rchtxtbx_Logs);
                MessageBox.Show(SqEx.Message, "Ошибка записи в базу!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                IsInsert = false;
                mmenuTablesDelete.Enabled = !mmenuTablesDelete.Enabled;
                mmenu_TablesEdit.Enabled = !mmenu_TablesEdit.Enabled;
            }
        }               

        private void mmenuTablesDelete_Click(object sender, EventArgs e)                        // Удаление полей 
        {

        }           

        private void mmenu_TablesEdit_Click(object sender, EventArgs e)                         // Редактирование полей
        {

        }            

        private void mmenuTablesRecEdit_Click(object sender, EventArgs e)
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
                }

                if(IsDelete)
                {

                }

                if(IsEdit)
                {

                }
            }
            catch (SQLiteException SqEx)
            {
                AddLog("Не удалось изменить данные в таблице - \' " + TableName, rchtxtbx_Logs);
                MessageBox.Show(SqEx.Message);

                mmenuTablesDelete.Enabled = true;
                mmenu_TablesEdit.Enabled = true;
            }
        }

        private void mmenuTablesRecCansel_Click(object sender, EventArgs e)
        {
            IsInsert = IsDelete = IsEdit = false;
            dtGrdV_TablesView.Columns.Clear();
            dtGrdV_TablesView.Rows.Clear();
            mmenuTablesDelete.Enabled = true;
            mmenu_TablesEdit.Enabled = true;
        }

        // Пункт меню - Запросы
        private void mmenuQueriesShowTable_Click(object sender, EventArgs e)                    // Показать таблицы целиком
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

        }

        private void mmenuQueriesShowTaskListUser_Click(object sender, EventArgs e)             // Показать список задач на исполнителе
        {

        }
    }
}

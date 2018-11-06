namespace BugTrackingSys_EPAMTest
{
    partial class MainWnd
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.mmenu_File = new System.Windows.Forms.ToolStripMenuItem();
            this.mmenu_FileCreate = new System.Windows.Forms.ToolStripMenuItem();
            this.mmenu_FileLoad = new System.Windows.Forms.ToolStripMenuItem();
            this.mmenu_FileClose = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.mmenu_FileExit = new System.Windows.Forms.ToolStripMenuItem();
            this.mmenuTables = new System.Windows.Forms.ToolStripMenuItem();
            this.mmenuTablesAdd = new System.Windows.Forms.ToolStripMenuItem();
            this.mmenuTablesDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.mmenuTablesRecEdit = new System.Windows.Forms.ToolStripMenuItem();
            this.mmenuTablesRecCanсel = new System.Windows.Forms.ToolStripMenuItem();
            this.mmenuQueries = new System.Windows.Forms.ToolStripMenuItem();
            this.mmenuQueriesShowTable = new System.Windows.Forms.ToolStripMenuItem();
            this.mmenuQueriesShowTaskListUser = new System.Windows.Forms.ToolStripMenuItem();
            this.mmenuQueriesShowTaskListProject = new System.Windows.Forms.ToolStripMenuItem();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.dtGrdV_TablesView = new System.Windows.Forms.DataGridView();
            this.rchtxtbx_Logs = new System.Windows.Forms.RichTextBox();
            this.opnFlDlg_LoadDataSource = new System.Windows.Forms.OpenFileDialog();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtGrdV_TablesView)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mmenu_File,
            this.mmenuTables,
            this.mmenuQueries});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(784, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // mmenu_File
            // 
            this.mmenu_File.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mmenu_FileCreate,
            this.mmenu_FileLoad,
            this.mmenu_FileClose,
            this.toolStripMenuItem1,
            this.mmenu_FileExit});
            this.mmenu_File.Name = "mmenu_File";
            this.mmenu_File.Size = new System.Drawing.Size(48, 20);
            this.mmenu_File.Text = "Файл";
            // 
            // mmenu_FileCreate
            // 
            this.mmenu_FileCreate.Name = "mmenu_FileCreate";
            this.mmenu_FileCreate.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.mmenu_FileCreate.Size = new System.Drawing.Size(177, 22);
            this.mmenu_FileCreate.Text = "Создать...";
            this.mmenu_FileCreate.Click += new System.EventHandler(this.mmenu_FileCreate_Click);
            // 
            // mmenu_FileLoad
            // 
            this.mmenu_FileLoad.Name = "mmenu_FileLoad";
            this.mmenu_FileLoad.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.L)));
            this.mmenu_FileLoad.Size = new System.Drawing.Size(177, 22);
            this.mmenu_FileLoad.Text = "Загрузить...";
            this.mmenu_FileLoad.Click += new System.EventHandler(this.mmenu_FileLoad_Click);
            // 
            // mmenu_FileClose
            // 
            this.mmenu_FileClose.Enabled = false;
            this.mmenu_FileClose.Name = "mmenu_FileClose";
            this.mmenu_FileClose.Size = new System.Drawing.Size(177, 22);
            this.mmenu_FileClose.Text = "Закрыть";
            this.mmenu_FileClose.Click += new System.EventHandler(this.mmenu_FileClose_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(174, 6);
            // 
            // mmenu_FileExit
            // 
            this.mmenu_FileExit.Name = "mmenu_FileExit";
            this.mmenu_FileExit.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.F4)));
            this.mmenu_FileExit.Size = new System.Drawing.Size(177, 22);
            this.mmenu_FileExit.Text = "Выход";
            this.mmenu_FileExit.Click += new System.EventHandler(this.mmenu_FileExit_Click);
            // 
            // mmenuTables
            // 
            this.mmenuTables.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mmenuTablesAdd,
            this.mmenuTablesDelete,
            this.toolStripMenuItem2,
            this.mmenuTablesRecEdit,
            this.mmenuTablesRecCanсel});
            this.mmenuTables.Name = "mmenuTables";
            this.mmenuTables.Size = new System.Drawing.Size(69, 20);
            this.mmenuTables.Text = "Таблицы";
            // 
            // mmenuTablesAdd
            // 
            this.mmenuTablesAdd.Enabled = false;
            this.mmenuTablesAdd.Name = "mmenuTablesAdd";
            this.mmenuTablesAdd.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.A)));
            this.mmenuTablesAdd.Size = new System.Drawing.Size(191, 22);
            this.mmenuTablesAdd.Text = "Добавить...";
            this.mmenuTablesAdd.Click += new System.EventHandler(this.mmenuTablesAdd_Click);
            // 
            // mmenuTablesDelete
            // 
            this.mmenuTablesDelete.Enabled = false;
            this.mmenuTablesDelete.Name = "mmenuTablesDelete";
            this.mmenuTablesDelete.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D)));
            this.mmenuTablesDelete.Size = new System.Drawing.Size(191, 22);
            this.mmenuTablesDelete.Text = "Удалить...";
            this.mmenuTablesDelete.Click += new System.EventHandler(this.mmenuTablesDelete_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(188, 6);
            // 
            // mmenuTablesRecEdit
            // 
            this.mmenuTablesRecEdit.Enabled = false;
            this.mmenuTablesRecEdit.Name = "mmenuTablesRecEdit";
            this.mmenuTablesRecEdit.ShortcutKeys = System.Windows.Forms.Keys.F5;
            this.mmenuTablesRecEdit.Size = new System.Drawing.Size(191, 22);
            this.mmenuTablesRecEdit.Text = "Обновить";
            this.mmenuTablesRecEdit.Click += new System.EventHandler(this.mmenuTablesRecEdit_Click);
            // 
            // mmenuTablesRecCanсel
            // 
            this.mmenuTablesRecCanсel.Enabled = false;
            this.mmenuTablesRecCanсel.Name = "mmenuTablesRecCanсel";
            this.mmenuTablesRecCanсel.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Alt) 
            | System.Windows.Forms.Keys.C)));
            this.mmenuTablesRecCanсel.Size = new System.Drawing.Size(191, 22);
            this.mmenuTablesRecCanсel.Text = "Очистить";
            this.mmenuTablesRecCanсel.Click += new System.EventHandler(this.mmenuTablesRecCansel_Click);
            // 
            // mmenuQueries
            // 
            this.mmenuQueries.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mmenuQueriesShowTable,
            this.mmenuQueriesShowTaskListUser,
            this.mmenuQueriesShowTaskListProject});
            this.mmenuQueries.Name = "mmenuQueries";
            this.mmenuQueries.Size = new System.Drawing.Size(68, 20);
            this.mmenuQueries.Text = "Запросы";
            // 
            // mmenuQueriesShowTable
            // 
            this.mmenuQueriesShowTable.Enabled = false;
            this.mmenuQueriesShowTable.Name = "mmenuQueriesShowTable";
            this.mmenuQueriesShowTable.Size = new System.Drawing.Size(239, 22);
            this.mmenuQueriesShowTable.Text = "Вся таблица...";
            this.mmenuQueriesShowTable.Click += new System.EventHandler(this.mmenuQueriesShowTable_Click);
            // 
            // mmenuQueriesShowTaskListUser
            // 
            this.mmenuQueriesShowTaskListUser.Enabled = false;
            this.mmenuQueriesShowTaskListUser.Name = "mmenuQueriesShowTaskListUser";
            this.mmenuQueriesShowTaskListUser.Size = new System.Drawing.Size(239, 22);
            this.mmenuQueriesShowTaskListUser.Text = "Список задач на исполнителе";
            this.mmenuQueriesShowTaskListUser.Click += new System.EventHandler(this.mmenuQueriesShowTaskListUser_Click);
            // 
            // mmenuQueriesShowTaskListProject
            // 
            this.mmenuQueriesShowTaskListProject.Enabled = false;
            this.mmenuQueriesShowTaskListProject.Name = "mmenuQueriesShowTaskListProject";
            this.mmenuQueriesShowTaskListProject.Size = new System.Drawing.Size(239, 22);
            this.mmenuQueriesShowTaskListProject.Text = "Список задач в проекте";
            this.mmenuQueriesShowTaskListProject.Click += new System.EventHandler(this.mmenuQueriesShowTaskListProject_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 24);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.dtGrdV_TablesView);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.rchtxtbx_Logs);
            this.splitContainer1.Size = new System.Drawing.Size(784, 537);
            this.splitContainer1.SplitterDistance = 437;
            this.splitContainer1.TabIndex = 3;
            // 
            // dtGrdV_TablesView
            // 
            this.dtGrdV_TablesView.AllowUserToAddRows = false;
            this.dtGrdV_TablesView.AllowUserToDeleteRows = false;
            this.dtGrdV_TablesView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtGrdV_TablesView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dtGrdV_TablesView.Location = new System.Drawing.Point(0, 0);
            this.dtGrdV_TablesView.Name = "dtGrdV_TablesView";
            this.dtGrdV_TablesView.Size = new System.Drawing.Size(784, 437);
            this.dtGrdV_TablesView.TabIndex = 3;
            // 
            // rchtxtbx_Logs
            // 
            this.rchtxtbx_Logs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rchtxtbx_Logs.Location = new System.Drawing.Point(0, 0);
            this.rchtxtbx_Logs.Name = "rchtxtbx_Logs";
            this.rchtxtbx_Logs.ReadOnly = true;
            this.rchtxtbx_Logs.Size = new System.Drawing.Size(784, 96);
            this.rchtxtbx_Logs.TabIndex = 2;
            this.rchtxtbx_Logs.Text = "";
            // 
            // opnFlDlg_LoadDataSource
            // 
            this.opnFlDlg_LoadDataSource.Filter = "Файлы БД|*.db";
            // 
            // MainWnd
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 561);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainWnd";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Bug Tracking System Test";
            this.Load += new System.EventHandler(this.MainWnd_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dtGrdV_TablesView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem mmenu_File;
        private System.Windows.Forms.ToolStripMenuItem mmenu_FileCreate;
        private System.Windows.Forms.ToolStripMenuItem mmenu_FileLoad;
        private System.Windows.Forms.ToolStripMenuItem mmenu_FileClose;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem mmenu_FileExit;
        private System.Windows.Forms.ToolStripMenuItem mmenuTables;
        private System.Windows.Forms.ToolStripMenuItem mmenuTablesAdd;
        private System.Windows.Forms.ToolStripMenuItem mmenuTablesDelete;
        private System.Windows.Forms.ToolStripMenuItem mmenuQueries;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.DataGridView dtGrdV_TablesView;
        private System.Windows.Forms.RichTextBox rchtxtbx_Logs;
        private System.Windows.Forms.OpenFileDialog opnFlDlg_LoadDataSource;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem mmenuTablesRecEdit;
        private System.Windows.Forms.ToolStripMenuItem mmenuQueriesShowTable;
        private System.Windows.Forms.ToolStripMenuItem mmenuQueriesShowTaskListUser;
        private System.Windows.Forms.ToolStripMenuItem mmenuQueriesShowTaskListProject;
        private System.Windows.Forms.ToolStripMenuItem mmenuTablesRecCanсel;
    }
}


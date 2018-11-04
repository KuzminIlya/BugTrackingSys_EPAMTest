﻿namespace BugTrackingSys_EPAMTest
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
            this.mmenuTablesRefresh = new System.Windows.Forms.ToolStripMenuItem();
            this.mmenuTablesRefreshAll = new System.Windows.Forms.ToolStripMenuItem();
            this.mmenuQueries = new System.Windows.Forms.ToolStripMenuItem();
            this.mmenuQueriesCreate = new System.Windows.Forms.ToolStripMenuItem();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.dtGrdV_TablesView = new System.Windows.Forms.DataGridView();
            this.rchtxtbx_Logs = new System.Windows.Forms.RichTextBox();
            this.mmenuQueriesShow = new System.Windows.Forms.ToolStripMenuItem();
            this.mmenuQueriesShowTable = new System.Windows.Forms.ToolStripMenuItem();
            this.mmenuQueriesShowTaskListProject = new System.Windows.Forms.ToolStripMenuItem();
            this.mmenuQueriesShowTaskListUser = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripSeparator();
            this.mmenu_TablesEdit = new System.Windows.Forms.ToolStripMenuItem();
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
            this.mmenu_FileCreate.Size = new System.Drawing.Size(180, 22);
            this.mmenu_FileCreate.Text = "Создать...";
            this.mmenu_FileCreate.Click += new System.EventHandler(this.mmenu_FileCreate_Click);
            // 
            // mmenu_FileLoad
            // 
            this.mmenu_FileLoad.Name = "mmenu_FileLoad";
            this.mmenu_FileLoad.Size = new System.Drawing.Size(180, 22);
            this.mmenu_FileLoad.Text = "Загрузить...";
            this.mmenu_FileLoad.Click += new System.EventHandler(this.mmenu_FileLoad_Click);
            // 
            // mmenu_FileClose
            // 
            this.mmenu_FileClose.Name = "mmenu_FileClose";
            this.mmenu_FileClose.Size = new System.Drawing.Size(180, 22);
            this.mmenu_FileClose.Text = "Закрыть";
            this.mmenu_FileClose.Click += new System.EventHandler(this.mmenu_FileClose_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(177, 6);
            // 
            // mmenu_FileExit
            // 
            this.mmenu_FileExit.Name = "mmenu_FileExit";
            this.mmenu_FileExit.Size = new System.Drawing.Size(180, 22);
            this.mmenu_FileExit.Text = "Выход";
            this.mmenu_FileExit.Click += new System.EventHandler(this.mmenu_FileExit_Click);
            // 
            // mmenuTables
            // 
            this.mmenuTables.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mmenuTablesAdd,
            this.mmenuTablesDelete,
            this.mmenu_TablesEdit,
            this.toolStripMenuItem2,
            this.mmenuTablesRefresh,
            this.mmenuTablesRefreshAll});
            this.mmenuTables.Name = "mmenuTables";
            this.mmenuTables.Size = new System.Drawing.Size(69, 20);
            this.mmenuTables.Text = "Таблицы";
            // 
            // mmenuTablesAdd
            // 
            this.mmenuTablesAdd.Name = "mmenuTablesAdd";
            this.mmenuTablesAdd.Size = new System.Drawing.Size(180, 22);
            this.mmenuTablesAdd.Text = "Добавить...";
            this.mmenuTablesAdd.Click += new System.EventHandler(this.mmenuTablesAdd_Click);
            // 
            // mmenuTablesDelete
            // 
            this.mmenuTablesDelete.Name = "mmenuTablesDelete";
            this.mmenuTablesDelete.Size = new System.Drawing.Size(180, 22);
            this.mmenuTablesDelete.Text = "Удалить...";
            this.mmenuTablesDelete.Click += new System.EventHandler(this.mmenuTablesDelete_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(177, 6);
            // 
            // mmenuTablesRefresh
            // 
            this.mmenuTablesRefresh.Name = "mmenuTablesRefresh";
            this.mmenuTablesRefresh.Size = new System.Drawing.Size(180, 22);
            this.mmenuTablesRefresh.Text = "Обновить...";
            this.mmenuTablesRefresh.Click += new System.EventHandler(this.mmenuTablesRefresh_Click);
            // 
            // mmenuTablesRefreshAll
            // 
            this.mmenuTablesRefreshAll.Name = "mmenuTablesRefreshAll";
            this.mmenuTablesRefreshAll.Size = new System.Drawing.Size(180, 22);
            this.mmenuTablesRefreshAll.Text = "Обновить все";
            this.mmenuTablesRefreshAll.Click += new System.EventHandler(this.mmenuTablesRefreshAll_Click);
            // 
            // mmenuQueries
            // 
            this.mmenuQueries.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mmenuQueriesShow,
            this.mmenuQueriesCreate});
            this.mmenuQueries.Name = "mmenuQueries";
            this.mmenuQueries.Size = new System.Drawing.Size(68, 20);
            this.mmenuQueries.Text = "Запросы";
            // 
            // mmenuQueriesCreate
            // 
            this.mmenuQueriesCreate.Name = "mmenuQueriesCreate";
            this.mmenuQueriesCreate.Size = new System.Drawing.Size(180, 22);
            this.mmenuQueriesCreate.Text = "Создать запрос...";
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
            // mmenuQueriesShow
            // 
            this.mmenuQueriesShow.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mmenuQueriesShowTable,
            this.toolStripMenuItem3,
            this.mmenuQueriesShowTaskListProject,
            this.mmenuQueriesShowTaskListUser});
            this.mmenuQueriesShow.Name = "mmenuQueriesShow";
            this.mmenuQueriesShow.Size = new System.Drawing.Size(180, 22);
            this.mmenuQueriesShow.Text = "Показать";
            // 
            // mmenuQueriesShowTable
            // 
            this.mmenuQueriesShowTable.Name = "mmenuQueriesShowTable";
            this.mmenuQueriesShowTable.Size = new System.Drawing.Size(239, 22);
            this.mmenuQueriesShowTable.Text = "Всю таблицу...";
            this.mmenuQueriesShowTable.Click += new System.EventHandler(this.mmenuQueriesShowTable_Click);
            // 
            // mmenuQueriesShowTaskListProject
            // 
            this.mmenuQueriesShowTaskListProject.Name = "mmenuQueriesShowTaskListProject";
            this.mmenuQueriesShowTaskListProject.Size = new System.Drawing.Size(239, 22);
            this.mmenuQueriesShowTaskListProject.Text = "Список задач в проекте";
            this.mmenuQueriesShowTaskListProject.Click += new System.EventHandler(this.mmenuQueriesShowTaskListProject_Click);
            // 
            // mmenuQueriesShowTaskListUser
            // 
            this.mmenuQueriesShowTaskListUser.Name = "mmenuQueriesShowTaskListUser";
            this.mmenuQueriesShowTaskListUser.Size = new System.Drawing.Size(239, 22);
            this.mmenuQueriesShowTaskListUser.Text = "Список задач на исполнителе";
            this.mmenuQueriesShowTaskListUser.Click += new System.EventHandler(this.mmenuQueriesShowTaskListUser_Click);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(236, 6);
            // 
            // mmenu_TablesEdit
            // 
            this.mmenu_TablesEdit.Name = "mmenu_TablesEdit";
            this.mmenu_TablesEdit.Size = new System.Drawing.Size(180, 22);
            this.mmenu_TablesEdit.Text = "Редактировать...";
            this.mmenu_TablesEdit.Click += new System.EventHandler(this.mmenu_TablesEdit_Click);
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
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem mmenuTablesRefresh;
        private System.Windows.Forms.ToolStripMenuItem mmenuTablesRefreshAll;
        private System.Windows.Forms.ToolStripMenuItem mmenuQueries;
        private System.Windows.Forms.ToolStripMenuItem mmenuQueriesCreate;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.DataGridView dtGrdV_TablesView;
        private System.Windows.Forms.RichTextBox rchtxtbx_Logs;
        private System.Windows.Forms.ToolStripMenuItem mmenu_TablesEdit;
        private System.Windows.Forms.ToolStripMenuItem mmenuQueriesShow;
        private System.Windows.Forms.ToolStripMenuItem mmenuQueriesShowTable;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem mmenuQueriesShowTaskListProject;
        private System.Windows.Forms.ToolStripMenuItem mmenuQueriesShowTaskListUser;
    }
}

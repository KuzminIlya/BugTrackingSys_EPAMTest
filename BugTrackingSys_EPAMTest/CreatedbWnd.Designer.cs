namespace BugTrackingSys_EPAMTest
{
    partial class CreatedbWnd
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lbDBName = new System.Windows.Forms.Label();
            this.lbDBFilePath = new System.Windows.Forms.Label();
            this.txtBxDBName = new System.Windows.Forms.TextBox();
            this.txtBxDBFilePath = new System.Windows.Forms.TextBox();
            this.chkBxCreateSubCat = new System.Windows.Forms.CheckBox();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnDBFilePathBrowse = new System.Windows.Forms.Button();
            this.fldBrsDlg_CreateFoldDB = new System.Windows.Forms.FolderBrowserDialog();
            this.SuspendLayout();
            // 
            // lbDBName
            // 
            this.lbDBName.AutoSize = true;
            this.lbDBName.Location = new System.Drawing.Point(12, 27);
            this.lbDBName.Name = "lbDBName";
            this.lbDBName.Size = new System.Drawing.Size(64, 13);
            this.lbDBName.TabIndex = 0;
            this.lbDBName.Text = "Имя файла";
            // 
            // lbDBFilePath
            // 
            this.lbDBFilePath.AutoSize = true;
            this.lbDBFilePath.Location = new System.Drawing.Point(12, 56);
            this.lbDBFilePath.Name = "lbDBFilePath";
            this.lbDBFilePath.Size = new System.Drawing.Size(74, 13);
            this.lbDBFilePath.TabIndex = 1;
            this.lbDBFilePath.Text = "Путь к файлу";
            // 
            // txtBxDBName
            // 
            this.txtBxDBName.Location = new System.Drawing.Point(92, 24);
            this.txtBxDBName.Name = "txtBxDBName";
            this.txtBxDBName.Size = new System.Drawing.Size(218, 20);
            this.txtBxDBName.TabIndex = 2;
            // 
            // txtBxDBFilePath
            // 
            this.txtBxDBFilePath.Location = new System.Drawing.Point(92, 53);
            this.txtBxDBFilePath.Name = "txtBxDBFilePath";
            this.txtBxDBFilePath.Size = new System.Drawing.Size(218, 20);
            this.txtBxDBFilePath.TabIndex = 3;
            // 
            // chkBxCreateSubCat
            // 
            this.chkBxCreateSubCat.AutoSize = true;
            this.chkBxCreateSubCat.Location = new System.Drawing.Point(235, 79);
            this.chkBxCreateSubCat.Name = "chkBxCreateSubCat";
            this.chkBxCreateSubCat.Size = new System.Drawing.Size(129, 17);
            this.chkBxCreateSubCat.TabIndex = 4;
            this.chkBxCreateSubCat.Text = "Создать подкаталог";
            this.chkBxCreateSubCat.UseVisualStyleBackColor = true;
            // 
            // btnOK
            // 
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new System.Drawing.Point(207, 102);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 5;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(288, 102);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 6;
            this.btnCancel.Text = "Отмена";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnDBFilePathBrowse
            // 
            this.btnDBFilePathBrowse.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnDBFilePathBrowse.Location = new System.Drawing.Point(314, 53);
            this.btnDBFilePathBrowse.Name = "btnDBFilePathBrowse";
            this.btnDBFilePathBrowse.Size = new System.Drawing.Size(49, 20);
            this.btnDBFilePathBrowse.TabIndex = 7;
            this.btnDBFilePathBrowse.Text = "...\r\n";
            this.btnDBFilePathBrowse.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnDBFilePathBrowse.UseVisualStyleBackColor = true;
            this.btnDBFilePathBrowse.Click += new System.EventHandler(this.btnDBFilePathBrowse_Click);
            // 
            // CreatedbWnd
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(379, 131);
            this.ControlBox = false;
            this.Controls.Add(this.btnDBFilePathBrowse);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.chkBxCreateSubCat);
            this.Controls.Add(this.txtBxDBFilePath);
            this.Controls.Add(this.txtBxDBName);
            this.Controls.Add(this.lbDBFilePath);
            this.Controls.Add(this.lbDBName);
            this.MaximumSize = new System.Drawing.Size(395, 170);
            this.MinimumSize = new System.Drawing.Size(395, 170);
            this.Name = "CreatedbWnd";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Создание таблиц";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbDBName;
        private System.Windows.Forms.Label lbDBFilePath;
        private System.Windows.Forms.TextBox txtBxDBName;
        private System.Windows.Forms.TextBox txtBxDBFilePath;
        private System.Windows.Forms.CheckBox chkBxCreateSubCat;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnDBFilePathBrowse;
        private System.Windows.Forms.FolderBrowserDialog fldBrsDlg_CreateFoldDB;
    }
}
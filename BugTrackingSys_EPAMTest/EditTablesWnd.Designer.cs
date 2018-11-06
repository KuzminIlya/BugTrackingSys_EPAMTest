namespace BugTrackingSys_EPAMTest
{
    partial class EditTablesWnd
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
            this.btn_OK = new System.Windows.Forms.Button();
            this.btn_Cancel = new System.Windows.Forms.Button();
            this.lblTables = new System.Windows.Forms.Label();
            this.lblStrNum = new System.Windows.Forms.Label();
            this.cmbBx_TablesList = new System.Windows.Forms.ComboBox();
            this.txtBx_StrNum = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // btn_OK
            // 
            this.btn_OK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btn_OK.Location = new System.Drawing.Point(129, 89);
            this.btn_OK.Name = "btn_OK";
            this.btn_OK.Size = new System.Drawing.Size(75, 23);
            this.btn_OK.TabIndex = 0;
            this.btn_OK.Text = "OK";
            this.btn_OK.UseVisualStyleBackColor = true;
            this.btn_OK.Click += new System.EventHandler(this.btn_OK_Click);
            // 
            // btn_Cancel
            // 
            this.btn_Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btn_Cancel.Location = new System.Drawing.Point(210, 89);
            this.btn_Cancel.Name = "btn_Cancel";
            this.btn_Cancel.Size = new System.Drawing.Size(75, 23);
            this.btn_Cancel.TabIndex = 1;
            this.btn_Cancel.Text = "Отмена";
            this.btn_Cancel.UseVisualStyleBackColor = true;
            // 
            // lblTables
            // 
            this.lblTables.AutoSize = true;
            this.lblTables.Location = new System.Drawing.Point(12, 23);
            this.lblTables.Name = "lblTables";
            this.lblTables.Size = new System.Drawing.Size(50, 13);
            this.lblTables.TabIndex = 2;
            this.lblTables.Text = "Таблица";
            // 
            // lblStrNum
            // 
            this.lblStrNum.AutoSize = true;
            this.lblStrNum.Location = new System.Drawing.Point(12, 52);
            this.lblStrNum.Name = "lblStrNum";
            this.lblStrNum.Size = new System.Drawing.Size(66, 26);
            this.lblStrNum.TabIndex = 3;
            this.lblStrNum.Text = "Количество\r\nстрок";
            // 
            // cmbBx_TablesList
            // 
            this.cmbBx_TablesList.FormattingEnabled = true;
            this.cmbBx_TablesList.Location = new System.Drawing.Point(84, 20);
            this.cmbBx_TablesList.Name = "cmbBx_TablesList";
            this.cmbBx_TablesList.Size = new System.Drawing.Size(201, 21);
            this.cmbBx_TablesList.TabIndex = 4;
            // 
            // txtBx_StrNum
            // 
            this.txtBx_StrNum.Location = new System.Drawing.Point(84, 52);
            this.txtBx_StrNum.Name = "txtBx_StrNum";
            this.txtBx_StrNum.Size = new System.Drawing.Size(201, 20);
            this.txtBx_StrNum.TabIndex = 5;
            // 
            // EditTablesWnd
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(310, 124);
            this.ControlBox = false;
            this.Controls.Add(this.txtBx_StrNum);
            this.Controls.Add(this.cmbBx_TablesList);
            this.Controls.Add(this.lblStrNum);
            this.Controls.Add(this.lblTables);
            this.Controls.Add(this.btn_Cancel);
            this.Controls.Add(this.btn_OK);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(326, 163);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(326, 163);
            this.Name = "EditTablesWnd";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "__";
            this.Load += new System.EventHandler(this.EditTablesWnd_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_OK;
        private System.Windows.Forms.Button btn_Cancel;
        public System.Windows.Forms.ComboBox cmbBx_TablesList;
        public System.Windows.Forms.TextBox txtBx_StrNum;
        public System.Windows.Forms.Label lblStrNum;
        public System.Windows.Forms.Label lblTables;
    }
}
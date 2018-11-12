using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace BugTrackingSys_EPAMTest
{
    public partial class CreatedbWnd : Form
    {
        public CreatedbWnd()
        {
            InitializeComponent();
        }

        public string File_Path = "";

        public static bool IsFileNameCorrect(string FileName)
        {
            string RegEX = @"[^a-zA-z\d_]";
            return !string.IsNullOrWhiteSpace(FileName) && 
                   !Regex.IsMatch(FileName, RegEX) &&
                   !string.IsNullOrEmpty(FileName);
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            try
            {
                if(!IsFileNameCorrect(txtBxDBName.Text))
                {
                    throw new Exception();
                }

                if (!Directory.Exists(File_Path))
                {
                    throw new DirectoryNotFoundException();
                }

                if (chkBxCreateSubCat.Checked)
                {
                    File_Path += "\\" + txtBxDBName.Text;
                    Directory.CreateDirectory(File_Path);
                }

                File_Path += "\\" + txtBxDBName.Text + ".db";

                if (File.Exists(File_Path))
                {
                    DialogResult = MessageBox.Show("Данный файл уже существует! Заменить?", "Файл существует!", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                }

                (this.Owner as MainWnd).FilePath = File_Path;
            }
            catch (DirectoryNotFoundException)
            {
                MessageBox.Show("Такого пути не существует!", "Ошибка пути к файлу", MessageBoxButtons.OK, MessageBoxIcon.Error);
                DialogResult = DialogResult.None;
            }
            catch (Exception)
            {
                MessageBox.Show("Введите правильное имя файла", "Имя файла не корректно!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                DialogResult = DialogResult.None;
            }
        }

        private void btnDBFilePathBrowse_Click(object sender, EventArgs e)
        {
            if (fldBrsDlg_CreateFoldDB.ShowDialog() == DialogResult.Cancel)
                return;

            File_Path += fldBrsDlg_CreateFoldDB.SelectedPath;
            txtBxDBFilePath.Text = File_Path;
        }
    }
}

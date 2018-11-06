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
    public partial class EditTablesWnd : Form
    {
        public EditTablesWnd()
        {
            InitializeComponent();
        }

        private void EditTablesWnd_Load(object sender, EventArgs e)
        {
            MainWnd mainWnd = Owner as MainWnd;
            cmbBx_TablesList.Items.Clear();
            foreach(string name in mainWnd.TablesList)
            {
                cmbBx_TablesList.Items.Add(name);
            }
        }

        private void btn_OK_Click(object sender, EventArgs e)
        {
            // проверка пустого и неккоректного названия таблицы
        }
    }
}

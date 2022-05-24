using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace XmlDatabaseMerdogan
{
    public partial class FrmPersonList : Form
    {
        #region Variables

        ICrudMethod databaseObj;
        public FrmPersonList()
        {
            InitializeComponent();
            databaseObj = CreateInstanceCrud.Create();
        }
        decimal totalSalary;
        int totalAge;
        List<Person> personList;
        #endregion
        #region Events
        private void btnNew_Click(object sender, EventArgs e)
        {
            FrmPerson frmPerson = new FrmPerson();
            frmPerson.ShowDialog();
            Refresh();
          
        }
        private void kapatToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            DataRefresh();
        }
        private void silToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
                return;

            DialogResult result = MessageBox.Show("Seçilen Kayıdı Silmek İstiyor musunuz?", "Uyarı", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == System.Windows.Forms.DialogResult.Yes)
            {
                if (databaseObj.Delete(Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value)))
                {
                    DataRefresh();
                    MessageBox.Show("Silme Başarılı");
                   
                }
            }
        }
        #endregion
        #region Methods
        void DataRefresh()
        {
            //GetList
           personList =databaseObj.GetList().Where(e => e.Name.ToUpperInvariant().Contains(txtName.Text.ToUpperInvariant()) && e.Surname.ToUpperInvariant().Contains(txtSurname.Text.ToUpperInvariant())).ToList();

            //Sort
           if (rbName.Checked)
           {
               dataGridView1.DataSource = personList.OrderBy(e => e.Name).ToList();
           }
           else if(rbSurName.Checked)
           {
               dataGridView1.DataSource = personList.OrderBy(e => e.Surname).ToList();

           }
          //Calc Data
            if (personList.Count != 0)
            {
                totalSalary = Convert.ToDecimal(personList.Sum(e => e.Salary));
                totalAge = personList.Sum(e => e.Age);


                txttotalCount.Text = personList.Count.ToString();
                txtTotalSalary.Text = totalSalary.ToString();
                txtAvagareAge.Text = (totalAge / personList.Count).ToString();
                txtAvagereSalary.Text = (totalSalary / personList.Count).ToString();
            }
        }
        #endregion
    }
}

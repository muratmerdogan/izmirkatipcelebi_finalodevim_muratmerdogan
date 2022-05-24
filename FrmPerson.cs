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
    public partial class FrmPerson : Form
    {
        #region Variables
        ICrudMethod databaseObj;
        #endregion
        #region Form
        public FrmPerson()
        {
            InitializeComponent();
            databaseObj = CreateInstanceCrud.Create();
        }
        #endregion
        #region Events
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            decimal salary=0;
            if(string.IsNullOrEmpty(txtName.Text))
            {
                MessageBox.Show("Personel Adı Boş Bırakılamaz");
                txtName.Focus();
                return;
            }
            else if(string.IsNullOrEmpty(txtSurname.Text))
            {
                MessageBox.Show("Personel Soyadı Boş Bırakılamaz");
                txtSurname.Focus();
                return;
            }
            if (txtAge.Text == "0" || string.IsNullOrEmpty(txtAge.Text))
            {
                MessageBox.Show("Personel Yaşı Giriniz");
                txtAge.Focus();
                return;
            }
            else if (!decimal.TryParse(txtSalary.Text, out salary))
            {
                MessageBox.Show("Maaş Bilgisi Yanlış Formatta");
                txtSalary.Focus();
                return;
            }


            Person person = new Person();
            person.Name = txtName.Text;
            person.Surname = txtSurname.Text;
            person.Age = Convert.ToInt32(txtAge.Text);
            person.WorkStartDate = dPWorkStartDate.Value;
            person.Salary = salary;

            if (databaseObj.Add(person))
            {
                MessageBox.Show("Kayıt Başarılı");
                txtName.Text = string.Empty;
                txtSurname.Text = string.Empty;
                txtSalary.Text = string.Empty;
                txtAge.Text = "0";
                txtName.Focus();


            }
            else
            {
                MessageBox.Show("Bir Sorun Oluştu");
            }



        }
        #endregion
    }
   
}

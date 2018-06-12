using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace PersonalData
{
    public partial class PersonalData : Form
    {
        //bool isadd = false;
        public PersonalData()
        {
            InitializeComponent();
        }

        private void PersonalData_Load(object sender, EventArgs e)
        {

        }

        private void btn_create_Click(object sender, EventArgs e)
        {
            //add new record
            DBtest prep = new DBtest(txt_fname.Text,txt_mname.Text,txt_lname.Text);
            prep.Add();
            MessageBox.Show("Insert Successfully");
            display();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txt_id.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            txt_fname.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            txt_mname.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            txt_lname.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
        }

        private void btn_refresh_Click(object sender, EventArgs e)
        {
            txt_id.Text = "";
            txt_fname.Text = "";
            txt_mname.Text = "";
            txt_lname.Text = "";
            display();

        }

        public void display()
        {

            try
            {
                BindingSource bindingsource = new BindingSource();

                List<DBtest> rec = DBtest.GetData();

                foreach (DBtest data in rec)
                    bindingsource.Add(data);

                this.dataGridView1.Refresh();
                this.dataGridView1.DataSource = bindingsource;

            }
            catch (Exception ex)
            {
                ex.ToString();
            }
        }

        private void btn_update_Click(object sender, EventArgs e)
        {
            
            //edit record
           int id = Int32.Parse(dataGridView1.CurrentRow.Cells[0].Value.ToString());

            DBtest rec = DBtest.GetDataID(id);

            rec.Firstname = txt_fname.Text;
            rec.Middlename = txt_lname.Text;
            rec.Lastname = txt_mname.Text;

            rec.Update();
            MessageBox.Show("Updated Successfully");

            display(); 
           

        }

        private void btn_delete_Click(object sender, EventArgs e)
        {
            int id = Int32.Parse(dataGridView1.CurrentRow.Cells[0].Value.ToString());

            DBtest del = DBtest.GetDataID(id);

            del.Delete();

            display();
        }
    }//end
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient; 

namespace Studentreg
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            Load();
        }

        SqlConnection con = new SqlConnection("Data Source=AXN-LAP-132; Initial Catalog= gcbt; User ID=sa; Password=P@ssw0rd1 ");
        SqlCommand cmd;
        SqlDataReader read;
        //SqlDataAdapter drr;
        string id;
        bool mode = true;
        string sql;

        public void Load()
        {
            try
            {
                sql = "select * from student";
                cmd = new SqlCommand(sql,con);
                con.Open();

                read = cmd.ExecuteReader();

                //drr = new SqlDataAdapter(sql, con);
                dataGridView1.Rows.Clear();

                while (read.Read())
                {
                    dataGridView1.Rows.Add(read[0], read[1], read[2], read[3]);
                }

                con.Close();


            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string name = txtName.Text;
            string course = txtCourse.Text;
            string fee = txtFee.Text;


            if(mode==true)
            {
                sql = "insert into student (stname,course,fee) values (@stname,@course,@fee)";
                con.Open();
                cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@stname", name);
                cmd.Parameters.AddWithValue("@course", course);
                cmd.Parameters.AddWithValue("@fee", fee);
                MessageBox.Show("Record added.");
                cmd.ExecuteNonQuery();

                txtName.Clear();
                txtCourse.Clear();
                txtFee.Clear();
                txtName.Focus();
                
            }

            else
            {

                id = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                sql = "update student set values stname=@stname,course=@course,fee=@fee where id=@id";
                con.Open();
                cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.Parameters.AddWithValue("@stname", name);
                cmd.Parameters.AddWithValue("@course", course);
                cmd.Parameters.AddWithValue("@fee", fee);
                MessageBox.Show("Record updated...");
                cmd.ExecuteNonQuery();

                txtName.Clear();
                txtCourse.Clear();
                txtFee.Clear();
                txtName.Focus();
                button1.Text = "Save";
                mode = true;


            }
            con.Close();

        }

        public void getID(string id)
        {
            sql = "select * from student where id='" + id + "'";
            cmd = new SqlCommand(sql, con);
            con.Open();
            read = cmd.ExecuteReader();


            while(read.Read())
            {
                txtName.Text = read[1].ToString();
                txtCourse.Text = read[2].ToString();
                txtFee.Text = read[3].ToString();

            }
            con.Close();

        
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.ColumnIndex ==dataGridView1.Columns["Edit"].Index && e.RowIndex >=0)
            {
                mode = false;
                id = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                getID(id);
                button1.Text = "Edit";
                
            }
            else if (e.ColumnIndex == dataGridView1.Columns["Delete"].Index && e.RowIndex >= 0)

            {
                mode = false;
                id = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                sql = "delete from student where id='" + id + "'";
                con.Open();
                cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Record deleted...");
                con.Close();
                
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            txtName.Clear();
            txtCourse.Clear();
            txtFee.Clear();
            txtName.Focus();
            button1.Text = "Save";
            mode = true;
        }

        private void txtName_TextChanged(object sender, EventArgs e)
        {

        }
    }
}

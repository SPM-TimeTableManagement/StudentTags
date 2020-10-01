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

namespace StudentTagsSprint1
{
    public partial class StudentList : Form
    {
        SqlConnection sqlCon = new SqlConnection(@"Data Source=LAPTOP-3T5FJ761;Initial Catalog=test1;Integrated Security=True");

        public StudentList()
        {
            InitializeComponent();
        }

        private void fillCombo()
        {

            if (sqlCon.State == ConnectionState.Closed)
            {
                sqlCon.Open();
            }
            String Query = "Select programmeCode from programme";
            SqlDataAdapter sqladpt = new SqlDataAdapter(Query, sqlCon);


            DataTable dataTable = new DataTable();
            sqladpt.Fill(dataTable);



            foreach (DataRow item in dataTable.Rows)
            {
                comboBox1.Items.Add(item["programmeCode"]).ToString();
            }

            sqlCon.Close();
        }

        private void textBoxSearch_TextChanged(object sender, EventArgs e)
        {
            

            if (sqlCon.State == ConnectionState.Closed)
            {
                sqlCon.Open();
            }
                String Query = "Select year, semester, programmeCode, groupNo, GroupId from studentBatch where (groupNo like '%" + textBoxSearch.Text + "%')";
                SqlCommand sqlCmd = new SqlCommand(Query, sqlCon);



                SqlDataAdapter sqladpt = new SqlDataAdapter(Query, sqlCon);



                DataTable dataTable = new DataTable();
                sqladpt.Fill(dataTable);


                dataGridView1.Rows.Clear();
                foreach (DataRow item in dataTable.Rows)
                {
                    int n = dataGridView1.Rows.Add();
                    dataGridView1.Rows[n].Cells[0].Value = item[0].ToString();
                    dataGridView1.Rows[n].Cells[1].Value = item[1].ToString();
                    dataGridView1.Rows[n].Cells[2].Value = item[2].ToString();
                    dataGridView1.Rows[n].Cells[3].Value = item[3].ToString();
                    dataGridView1.Rows[n].Cells[4].Value = item[4].ToString();
                }

                sqlCon.Close();
            
        }

        private void StudentList_Load(object sender, EventArgs e)
        {
            GridFill();
            fillCombo();
        }

        private void GridFill()
        {
            if (sqlCon.State == ConnectionState.Closed)
            {
                sqlCon.Open();
            }

            string Query = "select year, semester, programmeCode, groupNo, GroupId from studentbatch";
            string Query1 = "select GroupId, StudentCount, SubGroupCount, SubGroupId from studentSubBatches";

            SqlDataAdapter adpt = new SqlDataAdapter(Query, sqlCon);
            DataTable dataTable = new DataTable();
            adpt.Fill(dataTable);

            SqlDataAdapter adpt1 = new SqlDataAdapter(Query1, sqlCon);
            DataTable dataTable1 = new DataTable();
            adpt1.Fill(dataTable1);

            dataGridView1.Rows.Clear();
            foreach (DataRow item in dataTable.Rows)
            {
                int n = dataGridView1.Rows.Add();
                dataGridView1.Rows[n].Cells[0].Value = item[0].ToString();
                dataGridView1.Rows[n].Cells[1].Value = item[1].ToString();
                dataGridView1.Rows[n].Cells[2].Value = item[2].ToString();
                dataGridView1.Rows[n].Cells[3].Value = item[3].ToString();
                dataGridView1.Rows[n].Cells[4].Value = item[4].ToString();
            }

            dataGridView2.Rows.Clear();
            foreach (DataRow item in dataTable1.Rows)
            {
                int n = dataGridView2.Rows.Add();
                dataGridView2.Rows[n].Cells[0].Value = item[0].ToString();
                dataGridView2.Rows[n].Cells[1].Value = item[1].ToString();
                dataGridView2.Rows[n].Cells[2].Value = item[2].ToString();
                dataGridView2.Rows[n].Cells[3].Value = item[3].ToString();
            }


            sqlCon.Close();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (sqlCon.State == ConnectionState.Closed)
            {
                sqlCon.Open();
            }

            if (comboYear.Text.Length == 0 || comboSem.Text.Length == 0 || comboBox1.Text.Length == 0 )
            {
                MessageBox.Show("Please choose all the filter options!");
            }
            else
            {
                String Query = "Select year, semester, programmeCode, groupNo, GroupId from studentBatch where (Year like '%" + comboYear.SelectedItem.ToString()+ "%' and Semester like '%" +comboSem.SelectedItem.ToString()+ "%' and ProgrammeCode like '%" +comboBox1.SelectedItem.ToString()+ "%')";
                SqlCommand sqlCmd = new SqlCommand(Query, sqlCon);

                SqlDataAdapter sqladpt = new SqlDataAdapter(Query, sqlCon);



                DataTable dataTable = new DataTable();
                sqladpt.Fill(dataTable);


                dataGridView1.Rows.Clear();
                foreach (DataRow item in dataTable.Rows)
                {
                    int n = dataGridView1.Rows.Add();
                    dataGridView1.Rows[n].Cells[0].Value = item[0].ToString();
                    dataGridView1.Rows[n].Cells[1].Value = item[1].ToString();
                    dataGridView1.Rows[n].Cells[2].Value = item[2].ToString();
                    dataGridView1.Rows[n].Cells[3].Value = item[3].ToString();
                    dataGridView1.Rows[n].Cells[4].Value = item[4].ToString();
                }

                sqlCon.Close();
            }
        }

        private void textBoxSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void dataGridView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            textBoxSearch1.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
        }

        private void textBoxSearch1_TextChanged(object sender, EventArgs e)
        {
            if (sqlCon.State == ConnectionState.Closed)
            {
                sqlCon.Open();
            }
            String Query = "Select GroupId, StudentCount, SubGroupCount, SubGroupId from studentSubBatches where (GroupId like '%" + textBoxSearch1.Text + "%')";
            SqlCommand sqlCmd = new SqlCommand(Query, sqlCon);



            SqlDataAdapter sqladpt = new SqlDataAdapter(Query, sqlCon);



            DataTable dataTable = new DataTable();
            sqladpt.Fill(dataTable);


            dataGridView2.Rows.Clear();
            foreach (DataRow item in dataTable.Rows)
            {
                int n = dataGridView2.Rows.Add();
                dataGridView2.Rows[n].Cells[0].Value = item[0].ToString();
                dataGridView2.Rows[n].Cells[1].Value = item[1].ToString();
                dataGridView2.Rows[n].Cells[2].Value = item[2].ToString();
                dataGridView2.Rows[n].Cells[3].Value = item[3].ToString();
            }

            sqlCon.Close();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            this.comboBox1.SelectedIndex = -1;
            this.comboYear.SelectedIndex = -1;
            this.comboSem.SelectedIndex = -1;

            textBoxSearch.Text = "";
            textBoxSearch1.Text = "";

            GridFill();
        }
    }
}

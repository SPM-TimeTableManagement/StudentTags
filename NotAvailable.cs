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
    public partial class NotAvailable : Form
    {

        SqlConnection sqlCon = new SqlConnection(@"Data Source=LAPTOP-3T5FJ761;Initial Catalog=test1;Integrated Security=True");

        public NotAvailable()
        {
            InitializeComponent();
        }

        void GridFill()
        {
            if (sqlCon.State == ConnectionState.Closed)
            {
                sqlCon.Open();
            }

            string Query = "select Code, Category, CONVERT(varchar(20), StartTime, 100), CONVERT(varchar(20), EndTime, 100), CONVERT(varchar(20), Date, 103), CategorySelection from NotAvailable";
            //string Query = "select CONVERT(varchar(20), Date, 100) from Table_1";

            SqlDataAdapter adpt = new SqlDataAdapter(Query, sqlCon);
            DataTable dataTable = new DataTable();
            adpt.Fill(dataTable);


            dataGridView1.Rows.Clear();
            foreach (DataRow item in dataTable.Rows)
            {
                int n = dataGridView1.Rows.Add();
                dataGridView1.Rows[n].Cells[0].Value = item[0].ToString();
                dataGridView1.Rows[n].Cells[1].Value = item[1].ToString();
                dataGridView1.Rows[n].Cells[2].Value = item[2].ToString();
                dataGridView1.Rows[n].Cells[3].Value = item[3].ToString();
                dataGridView1.Rows[n].Cells[4].Value = item[4].ToString();
                dataGridView1.Rows[n].Cells[5].Value = item[5].ToString();
            }


            sqlCon.Close();

        }

        private void NotAvailable_Load(object sender, EventArgs e)
        {
            GridFill();
            setTimeFormat();
        }

        void setTimeFormat()
        {
            dateTimePickerStart.ShowUpDown = true;

            dateTimePickerEnd.ShowUpDown = true;
        }

        private void buttonEnter_Click(object sender, EventArgs e)
        {
            if (sqlCon.State == ConnectionState.Closed)
            {
                sqlCon.Open();
            }


            if (comboCategory.Text.Length == 0)
            {
                MessageBox.Show("Please Select A Category!!!");
            }
            else if (comboCategory.SelectedItem.Equals("Lecturer"))
            {
                labelCat.Text = "Lecturer";

                comboCategorySelection.Items.Clear();

                String Query = "Select LecturerName from table_lecturer";
                SqlDataAdapter sqladpt = new SqlDataAdapter(Query, sqlCon);


                DataTable dataTable = new DataTable();
                sqladpt.Fill(dataTable);



                foreach (DataRow item in dataTable.Rows)
                {
                    comboCategorySelection.Items.Add(item["LecturerName"]).ToString();
                }

            }
            else if (comboCategory.SelectedItem.Equals("Session"))
            {
                labelCat.Text = "Session";

                comboCategorySelection.Items.Clear();

                String Query = "Select SessionID from table_sessions";
                SqlDataAdapter sqladpt = new SqlDataAdapter(Query, sqlCon);


                DataTable dataTable = new DataTable();
                sqladpt.Fill(dataTable);



                foreach (DataRow item in dataTable.Rows)
                {
                    comboCategorySelection.Items.Add(item["SessionID"]).ToString();
                }

            }
            else if (comboCategory.SelectedItem.Equals("Group"))
            {
                labelCat.Text = "Group";

                comboCategorySelection.Items.Clear();

                String Query = "Select GroupId from studentBatch";
                SqlDataAdapter sqladpt = new SqlDataAdapter(Query, sqlCon);


                DataTable dataTable = new DataTable();
                sqladpt.Fill(dataTable);



                foreach (DataRow item in dataTable.Rows)
                {
                    comboCategorySelection.Items.Add(item["GroupId"]).ToString();
                }

            }
            else if (comboCategory.SelectedItem.Equals("Sub-Group"))
            {
                labelCat.Text = "Sub-Group";

                comboCategorySelection.Items.Clear();

                String Query = "Select SubGroupId from studentSubBatches";
                SqlDataAdapter sqladpt = new SqlDataAdapter(Query, sqlCon);


                DataTable dataTable = new DataTable();
                sqladpt.Fill(dataTable);



                foreach (DataRow item in dataTable.Rows)
                {
                    comboCategorySelection.Items.Add(item["SubGroupId"]).ToString();
                }

            }

            sqlCon.Close();
        }


    }
}

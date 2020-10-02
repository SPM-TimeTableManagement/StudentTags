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

        private void button1_Click(object sender, EventArgs e)
        {
            if (sqlCon.State == ConnectionState.Closed)
            {
                sqlCon.Open();
            }

            if (comboCategory.Text.Length == 0 || comboCategorySelection.Text.Length == 0 || textBoxCode.Text.Length == 0)
            {
                MessageBox.Show("Please Fill All!!!");
            }
            else
            {

                string dt = dateTimePickerStart.Value.TimeOfDay.ToString();
                string et = dateTimePickerEnd.Value.TimeOfDay.ToString();
                string mt = datePicker.Value.Date.ToString();
                String codes = "NA" + textBoxCode.Text;

                SqlDataAdapter sqladpt = new SqlDataAdapter("Select * from NotAvailable where Code LIKE '%" + codes + "%'", sqlCon);
                DataTable dataTable = new DataTable();
                sqladpt.Fill(dataTable);

                /*SqlDataAdapter sqladpt1 = new SqlDataAdapter("Select * from NotAvailable where StartTime LIKE '%" + Int32.Parse(dt) + "%' AND CategorySelection LIKE '%" + comboCategorySelection.SelectedItem.ToString() + "%' AND Date LIKE '%" + mt + "%'", sqlCon);
                DataTable dataTable1 = new DataTable();
                sqladpt1.Fill(dataTable1);*/


                if (dataTable.Rows.Count != 0)
                {
                    MessageBox.Show("Code already Assigned!!!");
                }
                else
                {
                    String Query = "Insert into NotAvailable (Code, Category, StartTime, EndTime, Date, CategorySelection) values (@code, @cate, @st, @edt, @dat, @cats)";
                    //string Query = "Insert into table_1 (Date) values (@st)";
                    SqlCommand sqlCmd = new SqlCommand(Query, sqlCon);

                    sqlCmd.Parameters.AddWithValue("@code", codes);
                    sqlCmd.Parameters.AddWithValue("@cate", comboCategory.SelectedItem.ToString());
                    sqlCmd.Parameters.AddWithValue("@st", dt);
                    sqlCmd.Parameters.AddWithValue("@edt", et);
                    sqlCmd.Parameters.AddWithValue("@dat", mt);
                    sqlCmd.Parameters.AddWithValue("@cats", comboCategorySelection.SelectedItem.ToString());

                    int rows = sqlCmd.ExecuteNonQuery();

                    if (rows >= 1)
                    {
                        MessageBox.Show("Successfully Added!");
                    }
                    else
                    {
                        MessageBox.Show("Error!");
                    }

                    sqlCon.Close();
                    GridFill();
                    setTimeFormat();

                    comboCategory.SelectedIndex = -1;
                    comboCategorySelection.SelectedIndex = -1;
                    textBoxCode.Text = "";

                    labelCat.Text = "";
                }
            }
        }

        private void dataGridView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            labelCat.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            textBoxCode.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            comboCategory.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();

            buttonEnter.Enabled = false;
            button1.Enabled = false;
        }

        private void buttonClear_Click(object sender, EventArgs e)
        {
            GridFill();
            setTimeFormat();

            comboCategory.SelectedIndex = -1;
            comboCategorySelection.SelectedIndex = -1;
            textBoxCode.Text = "";
            labelCat.Text = "";

            buttonEnter.Enabled = true;
            button1.Enabled = true;
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            if (comboCategory.Text.Length == 0 || textBoxCode.Text.Length == 0)
            {
                MessageBox.Show("Nothing to delete!!");
            }
            else if (MessageBox.Show("Are you sure?", "Delete", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                try
                {
                    if (sqlCon.State == ConnectionState.Closed)
                    {
                        sqlCon.Open();
                    }
                    String Query = "Delete from NotAvailable where Code = @code";

                    SqlCommand sqlCmd = new SqlCommand(Query, sqlCon);

                    sqlCmd.Parameters.AddWithValue("@code", textBoxCode.Text);

                    int rows = sqlCmd.ExecuteNonQuery();

                    if (rows >= 1)
                    {
                        MessageBox.Show("Successfully Deleted!");
                    }
                    else
                    {
                        MessageBox.Show("Error!");
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error!");
                }
                finally
                {
                    sqlCon.Close();
                }

                GridFill();
                setTimeFormat();

                comboCategory.SelectedIndex = -1;
                comboCategorySelection.SelectedIndex = -1;
                textBoxCode.Text = "";

                buttonEnter.Enabled = true;
                button1.Enabled = true;
                labelCat.Text = "";


            }
            else
            {
                GridFill();
                setTimeFormat();

                comboCategory.SelectedIndex = -1;
                comboCategorySelection.SelectedIndex = -1;
                textBoxCode.Text = "";

                buttonEnter.Enabled = true;
                button1.Enabled = true;
                labelCat.Text = "";
            }
        }
    }
}

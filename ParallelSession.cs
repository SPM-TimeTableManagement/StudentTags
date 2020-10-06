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
    public partial class ParallelSession : Form
    {
        SqlConnection sqlCon = new SqlConnection(@"Data Source=LAPTOP-3T5FJ761;Initial Catalog=test1;Integrated Security=True");

        DataTable dt = new DataTable();
        DataRow dr;

        public ParallelSession()
        {
            InitializeComponent();

            dt.Columns.Add("Code");
            dt.Columns.Add("Subject");
            dt.Columns.Add("Tag");
            dt.Columns.Add("SessionID");
        }

        private void ParallelSession_Load(object sender, EventArgs e)
        {
            GridFill();

            fillCombo();

            setTimeFormat();

            textBoxCode.Visible = true;
            buttonDlt.Enabled = false;
            label9.Visible = false;
        }

        void setTimeFormat()
        {
            dateTimePickerStart.ShowUpDown = true;

            dateTimePickerEnd.ShowUpDown = true;
        }

        void GridFill()
        {
            if (sqlCon.State == ConnectionState.Closed)
            {
                sqlCon.Open();
            }

            string Query = "select Code, CONVERT(varchar(20), StartTime, 100), CONVERT(varchar(20), EndTime, 100), CONVERT(varchar(20), Date, 103) from MainParallelTable";

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
            }


            sqlCon.Close();

        }

        private void fillCombo()
        {

            if (sqlCon.State == ConnectionState.Closed)
            {
                sqlCon.Open();
            }

            String Query = "Select SubjectName from table_subject";
            SqlDataAdapter sqladpt = new SqlDataAdapter(Query, sqlCon);


            DataTable dataTable = new DataTable();
            sqladpt.Fill(dataTable);



            foreach (DataRow item in dataTable.Rows)
            {
                comboCategory.Items.Add(item["SubjectName"]).ToString();
            }

            sqlCon.Close();
        }

        private void buttonEnter_Click(object sender, EventArgs e)
        {
            if (sqlCon.State == ConnectionState.Closed)
            {
                sqlCon.Open();
            }

            if (comboCategory.Text.Length == 0)
            {
                MessageBox.Show("Please Select A Subject!!!");
            }
            else
            {

                comboBoxSession.Items.Clear();
                comboBoxTag.Items.Clear();

                String Query = "Select SessionID, SubjectName, Tag from table_sessions where SubjectName = '" + comboCategory.SelectedItem.ToString() + "'";

                SqlDataAdapter sqladpt = new SqlDataAdapter(Query, sqlCon);

                DataTable dataTable = new DataTable();

                sqladpt.Fill(dataTable);

                if (dataTable.Rows.Count == 0)
                {
                    MessageBox.Show("No Sessions Have Been Created for " + comboCategory.SelectedItem.ToString() + "!!!");
                    labelSub.Text = "";
                    comboBoxTag.SelectedIndex = -1;
                    comboBoxSession.Text = "";
                    comboBoxTag.Text = "";
                    comboBoxSession.SelectedIndex = -1;
                }
                else
                {
                    labelSub.Text = comboCategory.SelectedItem.ToString();

                    foreach (DataRow item in dataTable.Rows)
                    {
                        comboBoxTag.Items.Add(item["Tag"]).ToString();
                    }
                }
            }


            sqlCon.Close();
        }

        private void buttonTag_Click(object sender, EventArgs e)
        {
            if (sqlCon.State == ConnectionState.Closed)
            {
                sqlCon.Open();
            }

            if (comboBoxTag.Text.Length == 0)
            {
                MessageBox.Show("Please Select A Tag!!!");
            }
            else
            {

                comboBoxSession.Items.Clear();

                String Query = "Select SessionID, Tag from table_sessions where SubjectName = '" + labelSub.Text + "' And Tag = '" + comboBoxTag.SelectedItem.ToString() + "'";

                SqlDataAdapter sqladpt = new SqlDataAdapter(Query, sqlCon);

                DataTable dataTable = new DataTable();

                sqladpt.Fill(dataTable);

                labelSub.Text = comboCategory.SelectedItem.ToString();

                foreach (DataRow item in dataTable.Rows)
                {
                    comboBoxSession.Items.Add(item["SessionID"]).ToString();
                }

            }


            sqlCon.Close();
        }

        private void buttonAddToGrid_Click(object sender, EventArgs e)
        {
            if (comboBoxTag.Text.Length == 0 || comboBoxSession.Text.Length == 0 || labelSub.Text == "" || textBoxCode.Text == "")
            {
                MessageBox.Show("Nothing To Add!!!");
            }
            else
            {
                dr = dt.NewRow();

                dr["Code"] = "PS" + textBoxCode.Text;
                dr["Subject"] = labelSub.Text;
                dr["Tag"] = comboBoxTag.SelectedItem.ToString();
                dr["SessionID"] = comboBoxSession.SelectedItem.ToString();
                dt.Rows.Add(dr);
                dataGridView2.DataSource = dt;

                comboCategory.SelectedIndex = -1;
                comboBoxTag.SelectedIndex = -1;
                comboBoxSession.SelectedIndex = -1;
                labelSub.Text = "";
                textBoxCode.Visible = false;
            }
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (sqlCon.State == ConnectionState.Closed)
            {
                sqlCon.Open();
            }

            if (dt.Rows.Count <= 1)
            {
                MessageBox.Show("Nothing to Add and List should be More Than One!!!");
            }
            else
            {
                string st = dateTimePickerStart.Value.TimeOfDay.ToString();
                string et = dateTimePickerEnd.Value.TimeOfDay.ToString();
                string day = datePicker.Value.Date.ToString();

                String Query1 = "Insert into MainParallelTable values (@Code, @start, @end, @date)";
                SqlCommand sqlCmd1 = new SqlCommand(Query1, sqlCon);

                sqlCmd1.Parameters.AddWithValue("@Code", "PS" + textBoxCode.Text);
                sqlCmd1.Parameters.AddWithValue("@start", st);
                sqlCmd1.Parameters.AddWithValue("@end", et);
                sqlCmd1.Parameters.AddWithValue("@date", day);

                int rows1 = sqlCmd1.ExecuteNonQuery();

                foreach (DataGridViewRow dr in dataGridView2.Rows)
                {
                    String Query = "Insert into SubParallelTable values (@Code, @subject, @TagName, @sesID)";
                    SqlCommand sqlCmd = new SqlCommand(Query, sqlCon);

                    if (dr.IsNewRow) continue;
                    {

                        sqlCmd.Parameters.AddWithValue("@Code", dr.Cells["Code"].Value);
                        sqlCmd.Parameters.AddWithValue("@subject", dr.Cells["Subject"].Value);
                        sqlCmd.Parameters.AddWithValue("@TagName", dr.Cells["Tag"].Value);
                        sqlCmd.Parameters.AddWithValue("@sesID", dr.Cells["SessionID"].Value);

                        int rows = sqlCmd.ExecuteNonQuery();


                        if (rows >= 1 || rows1 >= 1)
                        {
                            MessageBox.Show("Successfully Added!");
                        }
                        else
                        {
                            MessageBox.Show("Error!");
                        }

                    }
                }

                GridFill();

                dt.Rows.Clear();

                comboBoxTag.SelectedIndex = -1;
                comboBoxSession.SelectedIndex = -1;
                comboCategory.SelectedIndex = -1;
                labelSub.Text = "";
                textBoxCode.Text = "";
                textBoxCode.Visible = true;
                label9.Visible = false;
            }
        }

        private void buttonClear_Click(object sender, EventArgs e)
        {
            GridFill();

            dt.Rows.Clear();


            this.comboBoxTag.SelectedIndex = -1;
            comboBoxSession.SelectedIndex = -1;
            comboCategory.SelectedIndex = -1;
            labelSub.Text = "";
            textBoxCode.Text = "";

            textBoxCode.Visible = true;
            buttonDlt.Enabled = false;
            label9.Visible = false;
        }

        private void dataGridView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            textBoxCode.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            buttonDlt.Enabled = true;
            label9.Visible = true;
        }

        private void buttonDlt_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure?", "Delete", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                try
                {
                    if (sqlCon.State == ConnectionState.Closed)
                    {
                        sqlCon.Open();
                    }
                    String Query = "Delete from MainParallelTable where Code = @code";
                    String Query1 = "Delete from SubParallelTable where Code = @code";

                    SqlCommand sqlCmd = new SqlCommand(Query, sqlCon);
                    SqlCommand sqlCmd1 = new SqlCommand(Query1, sqlCon);

                    sqlCmd.Parameters.AddWithValue("@code", textBoxCode.Text.Trim());
                    sqlCmd1.Parameters.AddWithValue("@code", textBoxCode.Text.Trim());

                    int rows = sqlCmd.ExecuteNonQuery();
                    int rows1 = sqlCmd1.ExecuteNonQuery();

                    if (rows >= 1 || rows1 >= 1)
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
            }

            GridFill();

            dt.Rows.Clear();

            comboBoxTag.SelectedIndex = -1;
            comboBoxSession.SelectedIndex = -1;
            comboCategory.SelectedIndex = -1;
            labelSub.Text = "";
            textBoxCode.Text = "";

            textBoxCode.Visible = true;
            buttonDlt.Enabled = false;
            label9.Visible = false;
        }
    }
}

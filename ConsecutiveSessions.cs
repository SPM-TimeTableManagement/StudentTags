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
    public partial class ConsecutiveSessions : Form
    {
        SqlConnection sqlCon = new SqlConnection(@"Data Source=LAPTOP-3T5FJ761;Initial Catalog=test1;Integrated Security=True");

        DataTable dt = new DataTable();
        DataRow dr;

        public ConsecutiveSessions()
        {
            InitializeComponent();

            dt.Columns.Add("Code");
            dt.Columns.Add("Tag");
            dt.Columns.Add("SessionID");
        }

        private void ConsecutiveSessions_Load(object sender, EventArgs e)
        {
            GridFill();
            fillCombo();
            textBoxCode.Visible = true;
            buttonDlt.Enabled = false;
            textBoxCode.Enabled = true;
            buttonSave.Enabled = true;
        }

        void GridFill()
        {
            if (sqlCon.State == ConnectionState.Closed)
            {
                sqlCon.Open();
            }

            string Query = "select Code, SubjectName from MainConsecutiveTable";

            SqlDataAdapter adpt = new SqlDataAdapter(Query, sqlCon);
            DataTable dataTable = new DataTable();
            adpt.Fill(dataTable);


            dataGridView1.Rows.Clear();
            foreach (DataRow item in dataTable.Rows)
            {
                int n = dataGridView1.Rows.Add();
                dataGridView1.Rows[n].Cells[0].Value = item[0].ToString();
                dataGridView1.Rows[n].Cells[1].Value = item[1].ToString();
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
                MessageBox.Show("Please Select A Category!!!");
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
            GridFill();
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

                dr["Code"] = "CS" + textBoxCode.Text;
                dr["Tag"] = comboBoxTag.SelectedItem.ToString();
                dr["SessionID"] = comboBoxSession.SelectedItem.ToString();
                dt.Rows.Add(dr);
                dataGridView2.DataSource = dt;

                comboBoxTag.SelectedIndex = -1;
                comboBoxSession.SelectedIndex = -1;
                textBoxCode.Visible = false;
                textBoxCode.Enabled = true;
            }
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (sqlCon.State == ConnectionState.Closed)
            {
                sqlCon.Open();
            }

            if (labelSub.Text == "")
            {
                MessageBox.Show("Nothing to Add!!!");
            }
            else
            {
                String Query1 = "Insert into MainConsecutiveTable values (@Code, @SubjectName)";
                SqlCommand sqlCmd1 = new SqlCommand(Query1, sqlCon);

                sqlCmd1.Parameters.AddWithValue("@Code", "CS" + textBoxCode.Text);
                sqlCmd1.Parameters.AddWithValue("@SubjectName", labelSub.Text);

                int rows1 = sqlCmd1.ExecuteNonQuery();

                foreach (DataGridViewRow dr in dataGridView2.Rows)
                {
                    String Query = "Insert into SubConsecutiveTables values (@Code, @TagName, @sesID)";
                    SqlCommand sqlCmd = new SqlCommand(Query, sqlCon);

                    if (dr.IsNewRow) continue;
                    {

                        sqlCmd.Parameters.AddWithValue("@Code", dr.Cells["Code"].Value);
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

                        dataGridView1.Rows.Clear();
                        dataGridView1.Refresh();
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
                textBoxCode.Enabled = true;
            }
        }

        private void buttonClear_Click(object sender, EventArgs e)
        {
            GridFill();

            dt.Rows.Clear();
            comboBoxTag.SelectedIndex = -1;
            comboBoxSession.SelectedIndex = -1;
            comboCategory.SelectedIndex = -1;
            labelSub.Text = "";
            textBoxCode.Text = "";
            textBoxCode.Visible = true;
            buttonDlt.Enabled = false;
            textBoxCode.Enabled = true;
            buttonSave.Enabled = true;
        }

        private void dataGridView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            textBoxCode.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            labelSub.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            buttonDlt.Enabled = true;
            textBoxCode.Enabled = false;
            buttonSave.Enabled = false;
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
                    String Query = "Delete from MainConsecutiveTable where Code = @code";
                    String Query1 = "Delete from SubConsecutiveTables where Code = @code";

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
            textBoxCode.Enabled = true;
            buttonSave.Enabled = true;
        }

        private void textBoxCode_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }
    }
}

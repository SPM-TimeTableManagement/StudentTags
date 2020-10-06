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
    public partial class NotOverlapping : Form
    {

        SqlConnection sqlCon = new SqlConnection(@"Data Source=LAPTOP-3T5FJ761;Initial Catalog=test1;Integrated Security=True");

        DataTable dt = new DataTable();
        DataRow dr;

        public NotOverlapping()
        {
            InitializeComponent();

            dt.Columns.Add("Code");
            dt.Columns.Add("Subject");
            dt.Columns.Add("Tag");
            dt.Columns.Add("SessionID");
        }

        private void NotOverlapping_Load(object sender, EventArgs e)
        {
            GridFill();

            fillCombo();

            textBoxCode.Visible = true;
            buttonDlt.Enabled = false;
        }

        void GridFill()
        {
            if (sqlCon.State == ConnectionState.Closed)
            {
                sqlCon.Open();
            }

            string Query = "select * from MainOverlapTable";

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

                dr["Code"] = "NO" + textBoxCode.Text;
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
                MessageBox.Show("Nothing to Add and list should be More Than One!!!");
            }
            else
            {
                String Query1 = "Insert into MainOverlapTable values (@Code)";
                SqlCommand sqlCmd1 = new SqlCommand(Query1, sqlCon);

                sqlCmd1.Parameters.AddWithValue("@Code", "NO" + textBoxCode.Text);

                int rows1 = sqlCmd1.ExecuteNonQuery();

                foreach (DataGridViewRow dr in dataGridView2.Rows)
                {
                    String Query = "Insert into SubOverlapTable values (@Code, @subject, @TagName, @sesID)";
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
        }

        private void dataGridView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            textBoxCode.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            buttonDlt.Enabled = true;
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
                    String Query = "Delete from MainOverlapTable where Code = @code";
                    String Query1 = "Delete from SubOverlapTable where Code = @code";

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
            comboBoxTag.Items.Clear();
            comboBoxSession.SelectedIndex = -1;
            comboCategory.SelectedIndex = -1;
            labelSub.Text = "";
            textBoxCode.Text = "";

            textBoxCode.Visible = true;
            buttonDlt.Enabled = false;
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

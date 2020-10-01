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
    public partial class Programme : Form
    {

        SqlConnection sqlCon = new SqlConnection(@"Data Source=LAPTOP-3T5FJ761;Initial Catalog=test1;Integrated Security=True");

        public Programme()
        {
            InitializeComponent();
        }

        private void buttonSaveUpdate_Click(object sender, EventArgs e)
        {
            if (buttonSaveUpdate.Text == "Enter")
            {
                if (textBoxProgName.Text.Length == 0 || textBoxProgCode.Text.Length == 0)
                {
                    MessageBox.Show("Fill All Fields!");
                }
                else
                {
                    try
                    {
                        if (sqlCon.State == ConnectionState.Closed)
                        {
                            sqlCon.Open();
                        }
                        String Query = "Insert into programme (programmename, programmecode) values (@name,@code)";
                        SqlCommand sqlCmd = new SqlCommand(Query, sqlCon);

                        sqlCmd.Parameters.AddWithValue("@name", textBoxProgName.Text.Trim());
                        sqlCmd.Parameters.AddWithValue("@code", textBoxProgCode.Text.Trim());

                        int rows = sqlCmd.ExecuteNonQuery();

                        if (rows >= 1)
                        {
                            MessageBox.Show("Successfully Added!");
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

                    this.textBoxProgName.Text = "";
                    this.textBoxProgCode.Text = "";
                }
            }
            else
            {
                if (textBoxProgName.Text.Length == 0 || textBoxProgCode.Text.Length == 0)
                {
                    MessageBox.Show("Fill All Fields!");
                }
                else
                {
                    try
                    {
                        if (sqlCon.State == ConnectionState.Closed)
                        {
                            sqlCon.Open();
                        }
                        String Query = "Update programme set programmename = @name, programmecode = @code where programmecode = @code";
                        SqlCommand sqlCmd = new SqlCommand(Query, sqlCon);

                        sqlCmd.Parameters.AddWithValue("@name", textBoxProgName.Text.Trim());
                        sqlCmd.Parameters.AddWithValue("@code", textBoxProgCode.Text.Trim());

                        int rows = sqlCmd.ExecuteNonQuery();

                        if (rows >= 1)
                        {
                            MessageBox.Show("Successfully Updated!");
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

                    this.textBoxProgName.Text = "";
                    this.textBoxProgCode.Text = "";
                    buttonSaveUpdate.Text = "Enter";
                }
            }
        }

        private void textBoxProgName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar) && !char.IsWhiteSpace(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        void GridFill()
        {
            if (sqlCon.State == ConnectionState.Closed)
            {
                sqlCon.Open();
            }

            string Query = "select * from programme";

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

        private void Programme_Load(object sender, EventArgs e)
        {
            GridFill();
        }

        private void dataGridView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            textBoxProgName.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            textBoxProgCode.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            buttonSaveUpdate.Text = "Update";
        }

        private void btnDlt_Click(object sender, EventArgs e)
        {
            if (textBoxProgName.Text.Length == 0 || textBoxProgCode.Text.Length == 0)
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
                    String Query = "Delete from programme where programmecode = @code";
                    SqlCommand sqlCmd = new SqlCommand(Query, sqlCon);

                    sqlCmd.Parameters.AddWithValue("@code", textBoxProgCode.Text.Trim());

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

                this.textBoxProgName.Text = "";
                this.textBoxProgCode.Text = "";
                buttonSaveUpdate.Text = "Enter";

            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            this.textBoxProgName.Text = "";
            this.textBoxProgCode.Text = "";
            this.textBoxSearch.Text = "";
            buttonSaveUpdate.Text = "Enter";
        }

        private void textBoxSearch_TextChanged(object sender, EventArgs e)
        {
            if (sqlCon.State == ConnectionState.Closed)
            {
                sqlCon.Open();
            }
            String Query = "Select * from programme where (programmename like '%" + textBoxSearch.Text + "%' or programmecode like '%" + textBoxSearch.Text + "')";
            SqlDataAdapter sqladpt = new SqlDataAdapter(Query, sqlCon);

            //sqlCmd.Parameters.AddWithValue("@name", textBoxProgName.Text.Trim());
            //sqlCmd.Parameters.AddWithValue("@code", textBoxProgCode.Text.Trim());

            DataTable dataTable = new DataTable();
            sqladpt.Fill(dataTable);


            dataGridView1.Rows.Clear();
            foreach (DataRow item in dataTable.Rows)
            {
                int n = dataGridView1.Rows.Add();
                dataGridView1.Rows[n].Cells[0].Value = item[0].ToString();
                dataGridView1.Rows[n].Cells[1].Value = item[1].ToString();
            }

            sqlCon.Close();

        }
    }
}

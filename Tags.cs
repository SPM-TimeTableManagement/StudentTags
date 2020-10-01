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
    public partial class Tags : Form
    {
        SqlConnection sqlCon = new SqlConnection(@"Data Source=LAPTOP-3T5FJ761;Initial Catalog=test1;Integrated Security=True");

        String id;


        public Tags()
        {
            InitializeComponent();
        }

        #region Button Submit
        private void buttonSaveUpdate_Click(object sender, EventArgs e)
        {
            if (textBoxTagName.Text.Length == 0)
            {
                MessageBox.Show("Fill The Field!");
            }
            else
            {
                try
                {
                    if (sqlCon.State == ConnectionState.Closed)
                    {
                        sqlCon.Open();
                    }
                    String Query = "Insert into tags (TagName) values (@name)";
                    SqlCommand sqlCmd = new SqlCommand(Query, sqlCon);

                    sqlCmd.Parameters.AddWithValue("@name", textBoxTagName.Text.Trim());

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

                this.textBoxTagName.Text = "";
            }

        }
        #endregion

        #region GridFill
        void GridFill()
        {
            if (sqlCon.State == ConnectionState.Closed)
            {
                sqlCon.Open();
            }

            string Query = "select * from tags";

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
        #endregion

        private void Tags_Load(object sender, EventArgs e)
        {
            GridFill();
        }

        #region textBox TagName KeyPress
        private void textBoxTagName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar) && !char.IsWhiteSpace(e.KeyChar))
            {
                e.Handled = true;
            }
        }
        #endregion

        #region textBox Search TextChanged
        private void textBoxSearch_TextChanged(object sender, EventArgs e)
        {
            if (sqlCon.State == ConnectionState.Closed)
            {
                sqlCon.Open();
            }
            String Query = "Select * from tags where (TagName like '%" + textBoxSearch.Text + "%')";
            SqlDataAdapter sqladpt = new SqlDataAdapter(Query, sqlCon);


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
        #endregion

        #region GridView Double Click
        private void dataGridView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            textBoxTagName.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            id = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            buttonSaveUpdate.Enabled = false;
        }
        #endregion

        #region Button Clear
        private void btnClear_Click(object sender, EventArgs e)
        {
            this.textBoxTagName.Text = "";
            this.textBoxSearch.Text = "";
            buttonSaveUpdate.Enabled = true;
        }
        #endregion

        #region Button Delete
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (textBoxTagName.Text.Length == 0)
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
                    String Query = "Delete from tags where TagName = @name";
                    SqlCommand sqlCmd = new SqlCommand(Query, sqlCon);

                    sqlCmd.Parameters.AddWithValue("@name", textBoxTagName.Text.Trim());

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

                this.textBoxTagName.Text = "";
                buttonSaveUpdate.Enabled = true;

            }
            this.textBoxTagName.Text = "";
        }
        #endregion

        #region Button Edit
        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (textBoxTagName.Text.Length == 0)
            {
                MessageBox.Show("Fill The Field!");
            }
            else
            {
                try
                {
                    if (sqlCon.State == ConnectionState.Closed)
                    {
                        sqlCon.Open();
                    }

                    String Query = "Update tags set TagName = @name where Id = @id";
                    SqlCommand sqlCmd = new SqlCommand(Query, sqlCon);

                    sqlCmd.Parameters.AddWithValue("@name", textBoxTagName.Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@id", id);


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

                this.textBoxTagName.Text = "";
                buttonSaveUpdate.Enabled = true;

            }
        }

        #endregion
    }

}

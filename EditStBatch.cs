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
    public partial class EditStBatch : Form
    {

        SqlConnection sqlCon = new SqlConnection(@"Data Source=LAPTOP-3T5FJ761;Initial Catalog=test1;Integrated Security=True");


        public static String SetValueForGroupID = "";


        public EditStBatch()
        {
            InitializeComponent();
        }

        private void EditStBatch_Load(object sender, EventArgs e)
        {
            fillCombo();
            GridFill();
        }

        #region Fill Programme Combobox
        private void fillCombo()
        {
            comboProg.Items.Clear();

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
                comboProg.Items.Add(item["programmeCode"]).ToString();
            }

            sqlCon.Close();
        }
        #endregion

        #region Gridfill
        private void GridFill()
        {
            if (sqlCon.State == ConnectionState.Closed)
            {
                sqlCon.Open();
            }

            string Query = "select year, semester, programmeCode, groupNo, GroupId from studentbatch";

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
            }


            sqlCon.Close();
        }
        #endregion

        #region TextBoxGroupNoKeyPress
        private void textBoxGrpNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }
        #endregion

        #region SubmitButton
        private void buttonSaveUpdate_Click(object sender, EventArgs e)
        {
            if (comboProg.Text.Length == 0 || comboYear.Text.Length == 0 || comboSem.Text.Length == 0 || textBoxGrpNo.Text.Length == 0)
            {
                MessageBox.Show("Fields are Empty!");
            }
            else
            {
                try
                {
                    if (sqlCon.State == ConnectionState.Closed)
                    {
                        sqlCon.Open();
                    }

                    String year = comboYear.SelectedItem.ToString();
                    String semester = comboSem.SelectedItem.ToString();
                    String programme = comboProg.SelectedItem.ToString();


                    int x = Int32.Parse(textBoxGrpNo.Text);

                    String groupID = year + "." + semester + "." + programme + "." + x;

                    SqlDataAdapter sqladpt = new SqlDataAdapter("Select * from studentBatch where groupId LIKE '%" + groupID + "%'", sqlCon);
                    DataTable dataTable = new DataTable();
                    sqladpt.Fill(dataTable);


                    if (dataTable.Rows.Count != 0)
                    {
                        MessageBox.Show("GroupID already Assigned!!!");
                    }
                    else
                    {
                        String Query = "Insert into studentbatch values (@year, @semester, @programme, @x, @groupID)";
                        SqlCommand sqlCmd = new SqlCommand(Query, sqlCon);

                        sqlCmd.Parameters.AddWithValue("@year", year);
                        sqlCmd.Parameters.AddWithValue("@semester", semester);
                        sqlCmd.Parameters.AddWithValue("@programme", programme);
                        sqlCmd.Parameters.AddWithValue("@x", x);
                        sqlCmd.Parameters.AddWithValue("@groupID", groupID);

                        int rows = sqlCmd.ExecuteNonQuery();


                        if (rows >= 1)
                        {
                            MessageBox.Show("Successfully Added " + groupID + "!");
                        }
                        else
                        {
                            MessageBox.Show("Error!");
                        }
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



                this.textBoxGrpNo.Text = "";
                this.comboProg.SelectedIndex = -1;
                this.comboYear.SelectedIndex = -1;
                this.comboSem.SelectedIndex = -1;

                GridFill();
            }

        }
        #endregion

        #region ClearButton
        private void btnClear_Click(object sender, EventArgs e)
        {
            this.textBoxGrpNo.Text = "";
            this.comboProg.SelectedIndex = -1;
            this.comboYear.SelectedIndex = -1;
            this.comboSem.SelectedIndex = -1;
            this.label7.Text = "";
            buttonSaveUpdate.Enabled = true;
        }
        #endregion

        #region GridViewDoubleClick
        private void dataGridView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            label7.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
            textBoxGrpNo.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            comboProg.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            comboSem.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString(); ;
            comboYear.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            buttonSaveUpdate.Enabled = false;

        }
        #endregion

        #region SubGroupAddButton
        private void btnDlt_Click(object sender, EventArgs e)
        {


            SetValueForGroupID = label7.Text;

            EditStSubBatch editStSubBatch = new EditStSubBatch();
            editStSubBatch.TopLevel = false;
            editStSubBatch.Dock = DockStyle.Fill;
            panel1.Controls.Clear();
            panel1.Controls.Add(editStSubBatch);
            editStSubBatch.BringToFront();
            editStSubBatch.Show();
        }
        #endregion

        #region DeleteButton
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (comboProg.Text.Length == 0 || comboSem.Text.Length == 0 || comboYear.Text.Length == 0 || textBoxGrpNo.Text.Length == 0)
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
                    String Query = "Delete from studentSubBatches where GroupId = @code";
                    String Query1 = "Delete from studentBatch where GroupId = @code";

                    SqlCommand sqlCmd = new SqlCommand(Query, sqlCon);
                    SqlCommand sqlCmd1 = new SqlCommand(Query1, sqlCon);

                    sqlCmd.Parameters.AddWithValue("@code", label7.Text);
                    sqlCmd1.Parameters.AddWithValue("@code", label7.Text);

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

                GridFill();
                this.textBoxGrpNo.Text = "";
                this.comboProg.SelectedIndex = -1;
                this.comboYear.SelectedIndex = -1;
                this.comboSem.SelectedIndex = -1;
                this.label7.Text = "";
                buttonSaveUpdate.Enabled = true;


            }
            else
            {
                GridFill();

                this.textBoxGrpNo.Text = "";
                this.comboProg.SelectedIndex = -1;
                this.comboYear.SelectedIndex = -1;
                this.comboSem.SelectedIndex = -1;
                this.label7.Text = "";
                buttonSaveUpdate.Enabled = true;
            }
        }
        #endregion


    }
}

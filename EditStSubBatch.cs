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
    public partial class EditStSubBatch : Form
    {
        SqlConnection sqlCon = new SqlConnection(@"Data Source=LAPTOP-3T5FJ761;Initial Catalog=test1;Integrated Security=True");

        public EditStSubBatch()
        {
            InitializeComponent();
        }

        private void EditStSubBatch_Load(object sender, EventArgs e)
        {
            GridFill();
            labelGroupId.Text = EditStBatch.SetValueForGroupID;
            label1.Visible = false;
        }

        #region GridFill
        private void GridFill()
        {
            if (sqlCon.State == ConnectionState.Closed)
            {
                sqlCon.Open();
            }

            string Query = "select GroupId, StudentCount, SubGroupCount, SubGroupId from studentSubBatches";

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
        #endregion

        #region textBoxStudentCountKeyPress
        private void textBoxStuCount_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }

        }
        #endregion

        #region textBoxSub StudentCount KeyPress
        private void textBoxSubStuCount_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }
        #endregion

        #region Submit and Edit button
        private void buttonSaveUpdate_Click(object sender, EventArgs e)
        {
            if (buttonSaveUpdate.Text == "Submit")
            {

                if (textBoxStuCount.Text.Length == 0 || textBoxSubStuCount.Text.Length == 0)
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

                        String groupID = labelGroupId.Text.Trim();
                        int tot = Int32.Parse(textBoxStuCount.Text);
                        int div = Int32.Parse(textBoxSubStuCount.Text);

                        int sub_count = tot / div;

                        SqlDataAdapter sqladpt = new SqlDataAdapter("Select * from studentSubBatches where groupId LIKE '%" + groupID + "%'", sqlCon);
                        DataTable dataTable = new DataTable();
                        sqladpt.Fill(dataTable);

                        if (dataTable.Rows.Count != 0)
                        {
                            MessageBox.Show("Sub Group IDs have already been assigned to this Group!!!");
                        }
                        else
                        {
                            for (int y = 1; y <= sub_count; y++)
                            {
                                String subgroupID = groupID + "." + y;

                                String Query = "Insert into studentSubBatches values (@groupID, @tot, @div, @subgroupID)";

                                SqlCommand sqlCmd = new SqlCommand(Query, sqlCon);


                                sqlCmd.Parameters.AddWithValue("@groupID", groupID);
                                sqlCmd.Parameters.AddWithValue("@tot", tot);
                                sqlCmd.Parameters.AddWithValue("@div", div);
                                sqlCmd.Parameters.AddWithValue("@subgroupID", subgroupID);

                                int rows = sqlCmd.ExecuteNonQuery();

                                if (rows >= 1)
                                {
                                    MessageBox.Show("Successfully Added " + subgroupID + "!");
                                }
                                else
                                {
                                    MessageBox.Show("Error!");
                                }
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

                    this.textBoxStuCount.Text = "";
                    this.textBoxSubStuCount.Text = "";
                    this.labelGroupId.Text = "";
                    buttonSaveUpdate.Text = "Submit";

                    GridFill();
                }
            }
            else
            {
                if (textBoxStuCount.Text.Length == 0 || textBoxSubStuCount.Text.Length == 0)
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

                        String groupID = labelGroupId.Text.Trim();
                        int tot = Int32.Parse(textBoxStuCount.Text);
                        int div = Int32.Parse(textBoxSubStuCount.Text);

                        int sub_count = tot / div;

                        for (int y = 1; y <= sub_count; y++)
                        {
                            String subgroupID = groupID + "." + y;

                            String Query = "Update studentSubBatches set StudentCount = @tot, SubGroupCount = @div, SubGroupId = @subgroupID where GroupId = @groupID";

                            SqlCommand sqlCmd = new SqlCommand(Query, sqlCon);


                            sqlCmd.Parameters.AddWithValue("@groupID", groupID);
                            sqlCmd.Parameters.AddWithValue("@tot", tot);
                            sqlCmd.Parameters.AddWithValue("@div", div);
                            sqlCmd.Parameters.AddWithValue("@subgroupID", subgroupID);

                            int rows = sqlCmd.ExecuteNonQuery();

                            if (rows >= 1)
                            {
                                MessageBox.Show("Successfully Updated " + subgroupID + "!");
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

                    this.textBoxStuCount.Text = "";
                    this.textBoxSubStuCount.Text = "";
                    this.labelGroupId.Text = "";
                    buttonSaveUpdate.Text = "Submit";

                    GridFill();

                    label1.Visible = false;
                }
            }
        }

        private void buttonClear_Click(object sender, EventArgs e)
        {
            this.labelGroupId.Text = "";
            this.textBoxStuCount.Text = "";
            this.textBoxSubStuCount.Text = "";
            buttonSaveUpdate.Text = "Submit";

            label1.Visible = false;

            GridFill();
        }
        #endregion

        #region GridView Double Click
        private void dataGridView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            buttonSaveUpdate.Text = "Update";
            label1.Visible = true;
            labelGroupId.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            textBoxStuCount.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            textBoxSubStuCount.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
        }
        #endregion

        #region Delete Button
        private void buttonDelete_Click(object sender, EventArgs e)
        {
            if (textBoxStuCount.Text.Length == 0 || textBoxSubStuCount.Text.Length == 0 || labelGroupId.Text.Length == 0)
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
                    SqlCommand sqlCmd = new SqlCommand(Query, sqlCon);

                    sqlCmd.Parameters.AddWithValue("@code", labelGroupId.Text);

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

                this.labelGroupId.Text = "";
                this.textBoxStuCount.Text = "";
                this.textBoxSubStuCount.Text = "";
                buttonSaveUpdate.Text = "Submit";
                label1.Visible = false;

            }
            else
            {
                GridFill();

                this.labelGroupId.Text = "";
                this.textBoxStuCount.Text = "";
                this.textBoxSubStuCount.Text = "";
                buttonSaveUpdate.Text = "Submit";
                label1.Visible = false;
            }
        }
        #endregion

    }
}

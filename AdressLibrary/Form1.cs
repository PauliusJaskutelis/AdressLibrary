using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AdressLibrary
{
    public partial class Form1 : Form
    {
        private SqlConnection sqlConnection;
        private DataTable dataTable;
        private AddEditForm addForm;
        private AddEditForm editForm;
        private ErrorDialog errorDialog;
        public Form1()
        {
            InitializeComponent();
            sqlConnection = new SqlConnection("Data Source=.\\sqlexpress;Initial Catalog=addressDatabase;Integrated Security=True");
            this.dataTable = new DataTable();
            errorDialog = new ErrorDialog();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            dataGrid_Load();
            formatDataGridView();
        }

        private void addContact_Click(object sender, EventArgs e)
        {
            addForm = new AddEditForm();
            addForm.ShowDialog();

            if(addForm.DialogResult == DialogResult.OK)
            {
                if (addForm.getNumberText() != "")
                {
                    addContact();
                    dataGrid_Load();
                }
                else
                {
                    errorDialog.setLabels("No Number", "Enter a number, \n who are you going to call?");
                    errorDialog.ShowDialog();
                }
            }        
        }

        private void editContact_Click(object sender, EventArgs e)
        {
            editForm = new AddEditForm();

            if (contactDataGridView.SelectedRows != null && !isDataGridEmpty())
            {
                editForm.setNameText(contactDataGridView.SelectedRows[0].Cells[1].Value.ToString());
                editForm.setNumberText(contactDataGridView.SelectedRows[0].Cells[2].Value.ToString());
                editForm.setBirthDate(contactDataGridView.SelectedRows[0].Cells[3].Value.ToString());
                editForm.ShowDialog();

                if(editForm.DialogResult == DialogResult.OK)
                {
                    if (editForm.getNumberText() != "")
                    {
                        updateContact();
                        dataGrid_Load();
                    }
                    else
                    {
                        errorDialog.setLabels("No Number", "Enter a number, \n who you going to call?");
                        errorDialog.ShowDialog();
                    }
                }
            }
            else
            {
                errorDialog.setLabels("Selection error", "No contact was selected for editing, \n please select a contact to edit.");
                errorDialog.ShowDialog();
            }
        }

        private void deleteContact_Click(object sender, EventArgs e)
        {
            if (contactDataGridView.SelectedRows != null && !isDataGridEmpty())
            {
                deleteContact();
                dataGrid_Load();

            }
            else
            {
                errorDialog.setLabels("Selection error", "No contact was selected. \n Please select a contact to delete it.");
                errorDialog.ShowDialog();
            }
        }

        public void dataGrid_Load()
        {
            dataTable.Clear();

            SqlCommand sqlCmd = new SqlCommand("GetAddressList", sqlConnection);
            sqlCmd.CommandType = CommandType.StoredProcedure;

            sqlConnection.Open();
            dataTable.Load(sqlCmd.ExecuteReader());
            sqlConnection.Close();

            contactDataGridView.DataSource = dataTable;
            contactDataGridView.Columns["id"].Visible = false;
            contactDataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            contactDataGridView.ReadOnly = true;
        }
        public void addContact()
        {
            SqlCommand sqlCmd = new SqlCommand("AddContact", sqlConnection);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@name", SqlDbType.VarChar).Value = addForm.getNameText();
            sqlCmd.Parameters.AddWithValue("@number", SqlDbType.VarChar).Value = addForm.getNumberText();
            sqlCmd.Parameters.AddWithValue("@birthDate", SqlDbType.Date).Value = addForm.getBirthDate();

            sqlConnection.Open();
            sqlCmd.ExecuteNonQuery();
            sqlConnection.Close();
        }
        public void updateContact()
        {
            SqlCommand sqlCmd = new SqlCommand("UpdateContact", sqlConnection);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@id", SqlDbType.Int).Value = contactDataGridView.SelectedRows[0].Cells[0].Value;
            sqlCmd.Parameters.AddWithValue("@name", SqlDbType.VarChar).Value = editForm.getNameText();
            sqlCmd.Parameters.AddWithValue("@number", SqlDbType.VarChar).Value = editForm.getNumberText();
            sqlCmd.Parameters.AddWithValue("@birthDate", SqlDbType.Date).Value = editForm.getBirthDate();

            sqlConnection.Open();
            sqlCmd.ExecuteNonQuery();
            sqlConnection.Close();
        }
        public void deleteContact()
        {
            SqlCommand sqlCmd = new SqlCommand("DeleteContact", sqlConnection);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@id", SqlDbType.Int).Value = contactDataGridView.SelectedRows[0].Cells[0].Value.ToString();

            sqlConnection.Open();
            sqlCmd.ExecuteNonQuery();
            sqlConnection.Close();
        }
        public void formatDataGridView()
        {
            contactDataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            var totalRowHeight = contactDataGridView.ColumnHeadersHeight;
            foreach (DataGridViewRow row in contactDataGridView.Rows)
                totalRowHeight += row.Height;

            contactDataGridView.Columns[1].HeaderText = "Full Name";
            contactDataGridView.Columns[2].HeaderText = "Phone Number";
            contactDataGridView.Columns[3].HeaderText = "Date of Birth";
            contactDataGridView.BorderStyle = BorderStyle.None;
            contactDataGridView.AutoResizeColumnHeadersHeight();
            contactDataGridView.ColumnHeadersDefaultCellStyle.BackColor = Color.DarkGray;
            contactDataGridView.RowsDefaultCellStyle.BackColor = Color.LightGray;
            contactDataGridView.GridColor = Color.Black;

            contactDataGridView.Columns[1].Width = 170;
            contactDataGridView.Columns[2].Width = 80;
        }
        public bool isDataGridEmpty()
        {
            return contactDataGridView.Rows.Count == 0;
        }
    }
}

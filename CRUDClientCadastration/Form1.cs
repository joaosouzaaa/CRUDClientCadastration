using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CRUDClientCadastration
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        OleDbConnection con = null;
        OleDbDataAdapter da = null;
        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (txtSearch.Text == "")
            {
                MessageBox.Show("Fill the search textbox", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
                //if a field was not filled, a message will popup.
            }
            else
            {
                int id = Convert.ToInt32(txtSearch.Text);
                DataSet ds = Clients.Search(id);
                //Uses the select function to search a correspondent id that was informed in the textbox.
                if (ds.Tables[0].Rows.Count > 0)
                {
                    dgvShow.DataSource = ds.Tables["DGV1"];
                    dgvShow.Refresh();
                    dgvShow.Update();
                    // Show the data from the corresponding id.
                }
                else
                {
                    MessageBox.Show("Can't find that data", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ShowData();
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                con = new OleDbConnection(Connection.Local());
                da = new OleDbDataAdapter("select * from Clients", con);
                DataSet ds = new DataSet();
                da.Fill(ds, "Clients");
                BindingSource bs = new BindingSource();
                bs.DataSource = ds.Tables["Clients"];
                txtID.DataBindings.Add("text", bs, "ID");
                txtName.DataBindings.Add("text", bs, "Name");
                txtEmail.DataBindings.Add("text", bs, "Email");
                txtCPF.DataBindings.Add("text", bs, "CPF");
                txtID.Enabled = false;
                //Transform all the data that was inserted in your form in a type that can be interpreted, and insert in the table.
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message);
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Exit the application?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (dr == DialogResult.Yes)
            {
                Application.Exit();
            }
            //If you click on yes it will exit the application.
        }

        private void btnShow_Click(object sender, EventArgs e)
        {
            ShowData();
        }
        private void ShowData()
        {
            try
            {
                Clients c = new Clients();
                DataSet ds = c.Att();
                dgvShow.DataSource = ds.Tables[0];
                // Using the select function from the database the DataGridView gets updated.
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtName.Text == "" || txtEmail.Text == "" || txtCPF.Text == "")
                {
                    MessageBox.Show("Fields not filled", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                    //if a field was not filled, a message will popup.
                }
                Clients client = new Clients();
                client.Name = txtName.Text;
                client.Email = txtEmail.Text;
                client.CPF = txtCPF.Text;
                client.AddNew();
                MessageBox.Show("Client included in database.");
                // Fill the fields with the data from the textboxs and insert on the table.
                txtName.Clear();
                txtEmail.Clear();
                txtCPF.Clear();
                txtName.Focus();
                // Clean the data and focus on the name.
                Clients c = new Clients();
                DataSet ds = c.Att();
                dgvShow.DataSource = ds.Tables[0];
                ShowData();
                //Show the Data on DataGridView.
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult dr = MessageBox.Show("Do you want to delete this client?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (dr == DialogResult.Yes)
                {
                    Clients client = new Clients();
                    int id = Convert.ToInt32(txtID.Text);
                    client.Name = txtName.Text;
                    client.Email = txtEmail.Text;
                    client.CPF = txtCPF.Text;
                    client.Delete(id);
                    // Delete from your database the client wich the id corresponds to the selected one on the DataGridView.
                    MessageBox.Show("Deleted Client!");
                    ShowData();
                }
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message);
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (txtName.Text == "" || txtEmail.Text == "" || txtCPF.Text == "")
            {
                MessageBox.Show("Fields not filled", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
                //if a field was not filled, a message will popup.
            }
            Clients client = new Clients();
            int id = Convert.ToInt32(txtID.Text);
            client.Name = txtName.Text;
            client.Email = txtEmail.Text;
            client.CPF = txtCPF.Text;
            client.Update(id);
            // Update the data from the strings that you change on the textboxes.
            MessageBox.Show("Updated Data");
            ShowData();
        }

        private void dgvShow_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.dgvShow.Rows[e.RowIndex];
                txtID.Text = row.Cells[0].Value.ToString();
                txtName.Text = row.Cells[1].Value.ToString();
                txtEmail.Text = row.Cells[2].Value.ToString();
                txtCPF.Text = row.Cells[3].Value.ToString();
                // Show on your DataGridView the data from the rows corresponding to the numbers.
            }
        }

        private void txtName_TextChanged(object sender, EventArgs e)
        {
            if (!System.Text.RegularExpressions.Regex.IsMatch(txtName.Text, @"^[a-zA-Z\u00C0-\u00FF ]*$"))
            {
                txtName.Clear();
            }
            // If the text it's diferent from the accepted chars the textbox will be cleaned.
        }

        private void txtEmail_TextChanged(object sender, EventArgs e)
        {
            if (!System.Text.RegularExpressions.Regex.IsMatch(txtEmail.Text, @"^[a-zA-Z0-9@.]*$"))
            {
                txtEmail.Clear();
            }
            // If the text it's diferent from the accepted chars the textbox will be cleaned.
        }

        private void dgvShow_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar.CompareTo((char)Keys.Return)) == 0)
            {
                e.Handled = true;
                SendKeys.Send("TAB");
            }
            // If tab is pressed or shift tab it will take to the next field.
        }

        private void txtCPF_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {
            if (!System.Text.RegularExpressions.Regex.IsMatch(txtCPF.Text, @"^[0-9]*$"))
            {
                txtCPF.Clear();
            }
            // If the text it's diferent from the accepted chars the textbox will be cleaned
        }
    }
}

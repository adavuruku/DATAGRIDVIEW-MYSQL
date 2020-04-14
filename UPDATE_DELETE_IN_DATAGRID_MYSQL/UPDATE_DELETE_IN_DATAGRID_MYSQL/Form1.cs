using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace UPDATE_DELETE_IN_DATAGRID_MYSQL
{
    public partial class Form1 : Form
    {
        /*declare all the connection and mysqladapter , mysql command 
        mysqldatapter here...so that they will remain public ToolBar the whole
        form so that at any time you can reload the datadapter from what you have in the        database*/
        MySqlConnection con = new MySql.Data.MySqlClient.MySqlConnection();
        MySqlCommand sCommand;
        MySqlDataAdapter sAdapter;
        MySqlCommandBuilder sBuilder;
        DataSet sDs;
        DataTable sTable;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            con = new MySql.Data.MySqlClient.MySqlConnection(@"server=localhost;username=root;password=;database=filemanagement;port=3306");
            reload_form();
            
        }
        private void button1_Click(object sender, EventArgs e)
        {
            reload_form();
        }
        public void reload_form()
        {
            string sql = "SELECT * FROM csharptest";
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
            con.Open();
            sCommand = new MySqlCommand(sql, con);
            sAdapter = new MySqlDataAdapter(sCommand);
            sBuilder = new MySqlCommandBuilder(sAdapter);
            sDs = new DataSet();
            sAdapter.Fill(sDs, "csharptest");
            sTable = sDs.Tables["csharptest"];
            con.Close();
            dataGridView1.DataSource = sDs.Tables["csharptest"];
            dataGridView1.ReadOnly = true;

            save_btn.Enabled = false;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }

        private void delete_btn_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Do you want to delete this row ?", "Delete", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                dataGridView1.Rows.RemoveAt(dataGridView1.SelectedRows[0].Index);
                sAdapter.Update(sTable);
            }
        }

        private void new_btn_Click(object sender, EventArgs e)
        {
            dataGridView1.ReadOnly = false;
            save_btn.Enabled = true;
            new_btn.Enabled = false;
            delete_btn.Enabled = false;
        }

        private void save_btn_Click(object sender, EventArgs e)
        {
            sAdapter.Update(sTable);
            dataGridView1.ReadOnly = true;
            save_btn.Enabled = false;
            new_btn.Enabled = true;
            delete_btn.Enabled = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            dataGridView2.ColumnCount = 3;

            dataGridView2.Columns[0].Name = "Product ID";

            dataGridView2.Columns[1].Name = "Product Name";

            dataGridView2.Columns[2].Name = "Product Price";



            string[] row = new string[] { "1", "Product 1", "1000" };

            dataGridView2.Rows.Add(row);

            row = new string[] { "2", "Product 2", "2000" };

            dataGridView2.Rows.Add(row);

            row = new string[] { "3", "Product 3", "3000" };

            dataGridView2.Rows.Add(row);

            row = new string[] { "4", "Product 4", "4000" };

            dataGridView2.Rows.Add(row);



            DataGridViewCheckBoxColumn chk = new DataGridViewCheckBoxColumn();

            dataGridView2.Columns.Add(chk);

            chk.HeaderText = "Check Data";

            chk.Name = "chk";

            dataGridView2.Rows[2].Cells[3].Value = true;
        }
    }
}

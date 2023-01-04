using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Mssqlconnection
{
    public partial class Form1 : Form
    {
        public string fname, lname, phone;
        int id;
        DataTable table = new DataTable();
        SqlConnection conn = new SqlConnection(@"Data Source=DESKTOP-9BL9P4H\MSSQLSERVER2022;
                Initial Catalog=TestDb;
                Integrated Security=True;");


        
        public Form1()
        {
            InitializeComponent();
            GetTable();
        }
        //Fill datagrid by taking all information from DB
        public void GetTable()
        {
            SqlDataAdapter adapter = new SqlDataAdapter("SELECT *FROM dbo.guest3",conn);
            table = new DataTable();
            adapter.Fill(table);
            dataGridView1.DataSource = table;
        }
        //insert function
        private void button1_Click(object sender, EventArgs e)
        {

            string sqlQuery = "INSERT INTO dbo.guest3 VALUES" +
                "('" + textBox1.Text+ "','" + textBox2.Text + "','" + textBox3.Text + "')";

            conn.Open();
            SqlCommand cmd = new SqlCommand(sqlQuery, conn);

            try
            {
                cmd.ExecuteNonQuery();
                MessageBox.Show("insert Successful", "Success");
                conn.Close();
                GetTable();
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        //Clear Button
        private void clearbtn_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
        }


        //Contnet click In dataGridViewer
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //if (e.RowIndex == 0)
            //    dataGridView1.CurrentRow.Cells[0].Value.ToString();
            //textBox1.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            //textBox2.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            //textBox3.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            id = int.Parse(dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString());
            textBox1.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
            textBox2.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
            textBox3.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
        }

        //Edit in MSSQL DATABASE
        private void updatebtn_Click(object sender, EventArgs e)
        {
            string editQuery = 
                "update dbo.guest3 set FirstName='"+textBox1.Text+"',LastName='"+textBox2.Text+"', Phone='"+textBox3.Text+"' where GuestID='"+id+"'";


            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(editQuery, conn);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Update Successful", "Success");
                conn.Close();
                GetTable();
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        //Delete btn
        private void deletebtn_Click(object sender, EventArgs e)
        {
            string deleteQuery =
                "delete from dbo.guest3 where GuestID='" + id + "'";
            try
            {
                conn.Open();
                SqlCommand cmd= new SqlCommand(deleteQuery, conn);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Data Deleted Successfully", "Success");
                conn.Close();
                GetTable();
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
       
    }
}

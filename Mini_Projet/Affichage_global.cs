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

namespace Mini_Projet
{
    public partial class Affichage_global : Form
    {
        public Affichage_global()
        {
            InitializeComponent();
        }

        private void Affichage_global_Load(object sender, EventArgs e)
        {
           // String ConnectionString = "Database=Terre; Data Source=.\\SQLSERVER2016;" + "User Id=sa;Password=sa";
            String ConnectionString = "Data Source=(local)\\SQLEXPRESS;Initial Catalog=Terre;" + "Integrated Security=true";
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {

                con.Open();
                try
                {
                    using (SqlDataAdapter a= new SqlDataAdapter(
                    "SELECT *FROM Pays", con))
                    {
                        DataTable t = new DataTable();
                        a.Fill(t);
                        dataGridView1.DataSource = t;
                    }
                }
                catch (Exception ex) { label1.Text = label1.Text + " add " + ex.Message; }
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}

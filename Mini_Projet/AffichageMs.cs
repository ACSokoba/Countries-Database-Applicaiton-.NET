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
    public partial class AffichageMs : Form
    {
        public AffichageMs()
        {
            InitializeComponent();
        }

        private void AffichageMs_Load(object sender, EventArgs e)
        {
            this.Text = "AffichegeMS";
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            label4.Text = "";
            String result = textBox1.Text;
            if (result.Length != 2 || Int32.TryParse(result,out int valeur))
                label4.Text = "Entrez bien le Code Alpha 2";
            else
            {


                String ConnectionString = "Data Source=(local)\\SQLEXPRESS;Initial Catalog=Terre;" + "Integrated Security=true";
                using (SqlConnection con = new SqlConnection(ConnectionString))
                {

                    con.Open();
                    try
                    {
                        using (SqlDataAdapter a = new SqlDataAdapter(
                        "SELECT *FROM Pays Where Alpha2='" + result + "'", con))
                        {
                            DataTable t = new DataTable();
                            a.Fill(t);
                            if (t.Rows.Count == 0)
                                label4.Text = "Aucun pays ne correspond a ce code";
                            else
                                 dataGridView1.DataSource = t;
                        }
                    }
                    catch (Exception ex) { label3.Text = label3.Text + " add " + ex.Message; }
                }
            }
        }
    }
}

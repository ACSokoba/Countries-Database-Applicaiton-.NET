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
    public partial class AjoutCapitaleMS : Form
    {
        public AjoutCapitaleMS()
        {
            InitializeComponent();
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            String result1 = textBox1.Text;
            String result2 = textBox2.Text;
            String result3 = textBox3.Text;
            label6.Text = "";

            if (result1.Length != 2 || Int32.TryParse(result1, out int value1))
            {
                label6.Text = "Entrez bien le code Alpha2";
            }
            else if (Int32.TryParse(result2, out int value2))
                label6.Text = "Entrez bien la capitale en FR";
            else if (Int32.TryParse(result3, out int value3))
                label6.Text = "Entrez bien la capitale en EN";
            else
            {
                String requete = "UPDATE Pays Set CapitaleFR='" + result2 + "',CapitaleEN='" + result3 + "' Where Alpha2='" + result1 + "'";

                String ConnectionString = "Data Source=(local)\\SQLEXPRESS;Initial Catalog=Terre;" + "Integrated Security=true";

                SqlConnection con = new SqlConnection(ConnectionString);

                SqlCommand cmd = new SqlCommand(requete, con);
                try
                {
                    con.Open();
                    cmd.ExecuteNonQuery();

                }
                catch (Exception ex)
                {
                    label5.Text = label5.Text + ex.ToString();
                }
                finally
                {
                    con.Close();
                }

                using (SqlConnection con1 = new SqlConnection(ConnectionString))
                {

                    con.Open();
                    try
                    {
                        using (SqlDataAdapter a = new SqlDataAdapter(
                        "SELECT *FROM Pays Where Alpha2='" + result1 + "'", con1))
                        {
                            DataTable t = new DataTable();
                            a.Fill(t);
    
                            if (t.Rows.Count == 0)
                                label6.Text = "Aucun Pays ne correspond à ce code Alpha 2";
                            else
                            dataGridView1.DataSource = t;
                        }
                    }
                    catch (Exception ex) { label5.Text = label5.Text + " add " + ex.Message; }
                }

            }


        }

        private void AjoutCapitaleMS_Load(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}

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
    public partial class RechercheMS : Form
    {
        public RechercheMS()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.Enabled = true;
            textBox2.Enabled = false;
            textBox3.Enabled = false;

            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            label3.Text = "";

        }

        private void button3_Click(object sender, EventArgs e)
        {
            textBox2.Enabled = true;
            textBox1.Enabled = false;
            textBox3.Enabled = false;
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            label3.Text = "";
        }

        private void button4_Click(object sender, EventArgs e)
        {
            textBox3.Enabled = true;
            textBox2.Enabled = false;
            textBox1.Enabled = false;
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            label3.Text = "";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBox1.Enabled = false;
            textBox2.Enabled = false;
            textBox3.Enabled = false;
            label3.Text = "";
            String ConnectionString = "Data Source=(local)\\SQLEXPRESS;Initial Catalog=Terre;" + "Integrated Security=true";
            if (!(textBox1.Text.Length==0))
            {
                
                String result = textBox1.Text;
                if (result.Length != 2 || Int32.TryParse(result, out int value1))
                    label3.Text = "Entrez Bien le Code Alpha 2";
                else
                {
                    using (SqlConnection con = new SqlConnection(ConnectionString))
                    {

                        con.Open();
                        try
                        {
                            using (SqlDataAdapter a = new SqlDataAdapter(
                            "SELECT CodeNum,NomFR,NomEN,CapitaleFR,CapitaleEN FROM Pays Where Alpha2='" + result + "'", con))
                            {
                                DataTable t = new DataTable();
                                a.Fill(t);
                                if (t.Rows.Count == 0)
                                    label3.Text = "Aucun Pays ne correspond à ce code Alpha 2";
                                else
                                     dataGridView1.DataSource = t;
                            }
                        }
                        catch (Exception ex) { label2.Text = label2.Text + " add " + ex.Message; }
                    }
                }
            }

            if (!(textBox2.Text.Length == 0))
            {
                String result = textBox2.Text;
                if (Int32.TryParse(result, out int value2))
                    label3.Text = "Entrez le nom du Pays en FR";
                else
                {
                    using (SqlConnection con = new SqlConnection(ConnectionString))
                    {

                        con.Open();
                        try
                        {
                            using (SqlDataAdapter a = new SqlDataAdapter(
                            "SELECT NomEN FROM Pays Where NomFR='" + result + "'", con))
                            {
                                DataTable t = new DataTable();
                                a.Fill(t);
                                if (t.Rows.Count == 0)
                                    label3.Text = "Aucun Pays ne correspond a ce nom en FR";
                                else
                                     dataGridView1.DataSource = t;
                            }
                        }
                        catch (Exception ex) { label2.Text = label2.Text + " add " + ex.Message; }
                    }
                }
            }

            if (!(textBox3.Text.Length == 0))
            {
                String result = textBox3.Text;
                if (Int32.TryParse(result, out int value3))
                    label3.Text = "Entrez bien la capitale du Pays en FR";

                using (SqlConnection con = new SqlConnection(ConnectionString))
                {

                    con.Open();
                    try
                    {
                        using (SqlDataAdapter a = new SqlDataAdapter(
                        "SELECT CapitaleEN FROM Pays Where CapitaleFR='" + result + "'", con))
                        {
                            DataTable t = new DataTable();
                            a.Fill(t);
                            if (t.Rows.Count == 0)
                                label3.Text = "Aucun pays ne correspond a cette Capitale";
                            else
                                dataGridView1.DataSource = t;
                        }
                    }
                    catch (Exception ex) { label2.Text = label2.Text + " add " + ex.Message; }
                }
            }


        }

        private void RechercheMS_Load(object sender, EventArgs e)
        {

        }
    }
}

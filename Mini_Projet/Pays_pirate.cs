using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mini_Projet
{
    public partial class Pays_pirate : Form
    {
        String ConnectionString = "Data Source=(local)\\SQLEXPRESS;Initial Catalog=Terre;" + "Integrated Security=true";
        public Pays_pirate()
        {
            InitializeComponent();
        }

        public byte[] GetPhoto(string filePath)
        {
            FileStream stream = new FileStream(
                filePath, FileMode.Open, FileAccess.Read);
            BinaryReader reader = new BinaryReader(stream);

            byte[] photo = reader.ReadBytes((int)stream.Length);

            reader.Close();
            stream.Close();

            return photo;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                try
                {
                    con.Open();

                    

                    byte[] photo = GetPhoto(@"drapeaux\pirate.jpg");

                    using (SqlCommand command = new SqlCommand(
                        "INSERT INTO Pays VALUES(@CodeNum, @Alpha2, @Alpha3, @NomFR, @NomEN,NULL,NULL,@drapeau)", con))
                    {

                        command.Parameters.Add(new SqlParameter("CodeNum", textBox1.Text));
                        command.Parameters.Add(new SqlParameter("Alpha2", textBox2.Text));
                        command.Parameters.Add(new SqlParameter("Alpha3", textBox3.Text));
                        command.Parameters.Add(new SqlParameter("NomFR", textBox4.Text));
                        command.Parameters.Add(new SqlParameter("NomEN", textBox5.Text));
                        command.Parameters.Add(new SqlParameter("drapeau", photo));
                        command.ExecuteNonQuery();
                    }

   
                }
                catch(Exception ex)
                {
                    label1.Text = ex.Message;
                }
            }

            execute();
        }


        private void execute()
        {


            using (SqlConnection con = new SqlConnection(ConnectionString))
            {

                con.Open();
                try
                {
                    using (SqlDataAdapter a = new SqlDataAdapter(
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

    }
}


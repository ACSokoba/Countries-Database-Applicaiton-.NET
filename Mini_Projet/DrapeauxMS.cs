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
    public partial class DrapeauxMS : Form
    {
        List<String> chaine = new List<String>();
        String ConnectionString = "Data Source=(local)\\SQLEXPRESS;Initial Catalog=Terre;" + "Integrated Security=true";

        public DrapeauxMS()
        {
            InitializeComponent();
        }

        private void DrapeauxMS_Load(object sender, EventArgs e)
        {

            Modifie_table();
            chargement_photo();
            


           
        }

        public void addDrapeaux(String photoFilePath)
        {
            //String path = ch + ".png";
            // addDrapeaux(@"drapeaux\" + path);
            byte[] photo = GetPhoto(@"drapeaux\"+photoFilePath+".png");

            String ConnectionString = "Data Source=(local)\\SQLEXPRESS;Initial Catalog=Terre;" + "Integrated Security=true";
            //String ConnectionString = "Database=Terre; Data Source=.\\SQLSERVER2016;" + "User Id=sa;Password=sa";
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {

                con.Open();
                try
                {
                    using (SqlCommand com = new SqlCommand("UPDATE Pays SET Drapeau=@drapeau where Alpha2 = '" + photoFilePath.ToUpper() + "'", con))
                    {
                        com.Parameters.Add(new SqlParameter("drapeau", photo));
                        com.ExecuteNonQuery();
                    }
                }
                catch (Exception ex) { label2.Text = " add    " + ex.Message; }
            }
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

        public void Modifie_table()
        {
            String requete = "ALTER TABLE Pays ADD Drapeau Image";

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
                this.Text = ex.ToString();
            }
            finally
            {
                con.Close();
            }
        }

        public void chargement_photo()
        {
            SqlConnection con = new SqlConnection(ConnectionString);

            /**************Recuperer la liste des alphas 2***************/
            using (SqlConnection con1 = new SqlConnection(ConnectionString))
            {

                con.Open();
                try
                {
                    using (SqlDataAdapter a = new SqlDataAdapter(
                    "SELECT Alpha2 FROM Pays", con1))
                    {

                        DataSet ds = new DataSet();
                        a.Fill(ds);
                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {

                            chaine.Add(ds.Tables[0].Rows[i][0].ToString().ToLower());

                        }
                        
                    }
                }
                catch (Exception ex) { label2.Text = " convert   " + ex.Message; }
                finally
                {
                    con.Close();
                }
            }

            /*******************Ajout des drapeaux**********************/
            foreach (String ch in chaine)
            {
                addDrapeaux(ch);
            }

            /*********************Affichage***********************************/
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {

                con.Open();
                try
                {
                    using (SqlDataAdapter a = new SqlDataAdapter(
                    "SELECT *from Pays", conn))
                    {
                        DataTable t = new DataTable();
                        a.Fill(t);

                        dataGridView1.DataSource = t;
                    }
                }
                catch (Exception ex) { label3.Text = label3.Text + " add " + ex.Message; }
                finally
                {
                    con.Close();
                }
            }

            
        }

    }
}

   


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
    public partial class Chargement : Form
    {
        public Chargement()
        {
            InitializeComponent();
        }


        static List<Pays> Allpays = new List<Pays>();
        public static String ConnectionString = "Data Source=(local)\\SQLEXPRESS;Initial Catalog=master;" + "Integrated Security=true";
       // public static String ConnectionString = "Database=master; Data Source=.\\SQLSERVER2016;" + "User Id=sa;Password=sa";
        public void TryCreateDatabase()
        {

            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                con.Open();
                try
                {
                    using (SqlCommand command = new SqlCommand(
                     "DROP DATABASE IF EXISTS Terre",
                     con))
                    {


                        command.ExecuteNonQuery();

                    }
                    using (SqlCommand command = new SqlCommand(
                    "CREATE DATABASE  Terre",
                    con))
                    {
                       

                        command.ExecuteNonQuery();

                    }
                }
                catch (Exception ex){ label2.Text = label2.Text + "base non creee." +ex.Message; }
            }
        }


        public void TryCreateTable()
        {
             String ConnectionString = "Data Source=(local)\\SQLEXPRESS;Initial Catalog=Terre;" + "Integrated Security=true";
            //String ConnectionString = "Database=Terre; Data Source=.\\SQLSERVER2016;" + "User Id=sa;Password=sa";
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                con.Open();
                try
                {
                    using (SqlCommand command = new SqlCommand(
                     "DROP TABLE IF EXISTS Pays",
                     con))
                    {
                        command.ExecuteNonQuery();

                    }



                    using (SqlCommand command = new SqlCommand(
                    "CREATE TABLE Pays (CodeNum TEXT, Alpha2 CHAR(2), Alpha3 TEXT,NomFR CHAR(60),NomEn CHAR(60),CapitaleFR CHAR(60) Default NULL,CapitaleEN CHAR(60) NULL ,PRIMARY KEY(Alpha2))",
                    con))
                    {
                        command.ExecuteNonQuery();
                 
                    }
                }
                catch (Exception ex) { label2.Text = label2.Text +"create table " +ex.Message; }
            }
        }



        public void Addpays(String IsoNum, String IsoAlpha2, String IsoAlpha3, String CountryFR, String CountryEN, String Capitale)
        {

            String ConnectionString = "Data Source=(local)\\SQLEXPRESS;Initial Catalog=Terre;" + "Integrated Security=true";
            //String ConnectionString = "Database=Terre; Data Source=.\\SQLSERVER2016;" + "User Id=sa;Password=sa";
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {

                con.Open();
                try
                {
                    using (SqlCommand command = new SqlCommand(
                    "INSERT INTO Pays VALUES(@CodeNum, @Alpha2, @Alpha3,@NomFR,@NomEN,NULL,NULL)", con))
                    {
                        command.Parameters.Add(new SqlParameter("CodeNum", IsoNum));
                        command.Parameters.Add(new SqlParameter("Alpha2", IsoAlpha2));
                        command.Parameters.Add(new SqlParameter("Alpha3", IsoAlpha3));
                        command.Parameters.Add(new SqlParameter("NomFR", CountryFR));
                        command.Parameters.Add(new SqlParameter("NomEN", CountryEN));
                       // command.Parameters.Add(new SqlParameter("CapitaleFR", Capitale));
                       // command.Parameters.Add(new SqlParameter("CapitaleEN", Capitale));
                        command.ExecuteNonQuery();
                        //Console.WriteLine("Ajput!!!!!!");
                    }
                }
                catch (Exception ex) { label2.Text = label2.Text + " add " + ex.Message; }
            }
        }

        public void importerCSV3(string fichierCSV)
        {
            StreamReader csv = new StreamReader(File.OpenRead(@fichierCSV));
            while (!csv.EndOfStream)
            {
                string ligne = csv.ReadLine();
                string[] champs = ligne.Split(',');
                // délimiteur csv
                Pays s = new Pays(champs);
                Console.WriteLine(s.ToString());
                Allpays.Add(s);
            }
        }








        private void Chargement_Load(object sender, EventArgs e)
        {
            label1.Text = "Chargement en cours!!!!";
            TryCreateDatabase();
            TryCreateTable();
            importerCSV3("pays.csv");

            //dataGridView1.DataSource = Allpays;
            foreach (Pays p in Allpays)
                Addpays(p.IsoNum, p.IsoAlpha1, p.IsoAlpha2, p.CountryFR, p.CountryEN, "");
            label1.Text = "Chargement termine!!!!";
        }

        
    }


    class Pays
    {
        public string IsoAlpha1 { get; set; }
        public string CountryFR { get; set; }
        public string CountryEN { get; set; }
        public string IsoAlpha2 { get; set; }
        public string IsoNum { get; set; }
        public String Capitale { get; set; }
        public Pays(string[] s)
        {
            IsoNum = s[1];
            IsoAlpha1 = s[2];
            IsoAlpha2 = s[3];
            CountryFR = s[4];
            CountryEN = s[5];
            Capitale = null;
        }
        public override String ToString()
        {
            return "[" + IsoAlpha1 + "] [" + CountryFR + "] [" + CountryEN + "]";
        }
    }
}

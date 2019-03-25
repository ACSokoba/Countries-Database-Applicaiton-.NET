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
    public partial class Form1 : Form
    {

        
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
           

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Chargement f2 = new Chargement();
            f2.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Affichage_global f3 = new Affichage_global();
            f3.ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            AffichageMs f4 = new AffichageMs();
            f4.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            AjoutCapitaleMS f5 = new AjoutCapitaleMS();
            f5.ShowDialog();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            RechercheMS f6 = new RechercheMS();
            f6.ShowDialog();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            DrapeauxMS f7 = new DrapeauxMS();
            f7.ShowDialog();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Pays_pirate f8 = new Pays_pirate();
            f8.ShowDialog();
        }
    }






   
}

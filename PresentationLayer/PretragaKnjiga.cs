using BusinessLayer;
using DataLayer;
using SharedFolder.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

namespace PresentationLayer
{
    public partial class PretragaKnjiga : Form
    {
        private readonly KnjigaBusiness knjigaBusiness = new KnjigaBusiness();
        private readonly IznajmljivanjeRepository iznajmljivanjeRepo = new IznajmljivanjeRepository();
        public PretragaKnjiga()
        {
            InitializeComponent();
        }

        private void PretragaPoNaslovuIAutoru()
        {
            string searchTerm = textBox1.Text.Trim();

            if (string.IsNullOrEmpty(searchTerm))
            {
                listBox1.DataSource = null;
                MessageBox.Show("Morate uneti pojam za pretragu");
                return;
            }

            listBox1.DataSource = null;

            List<string> searchResults = new List<string>();
            if (knjigaBusiness.PretragaPoAutoru(searchTerm) == null)
            {
                if (knjigaBusiness.PretragaPoNaslovu(searchTerm) == null)
                {
                    listBox1.Items.Add($"Trenutno nemamo knjiga za pretraženi pojam");
                    return;
                }
                else
                {
                    searchResults.AddRange(knjigaBusiness.PretragaPoNaslovu(searchTerm));
                }
            }
            else
            {
                searchResults.AddRange(knjigaBusiness.PretragaPoAutoru(searchTerm));
            }

            listBox1.DataSource = searchResults;

        }
        // pretrazivanje knjige prvo po autoru onda po naslovu
        private void button1_Click_1(object sender, EventArgs e)
        {
            PretragaPoNaslovuIAutoru();
        }

        //iznajmljivanje knjige koja je izabrana u listboxu
        private void button2_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem != null)
            {
                Iznajmljivanje pozajmica = knjigaBusiness.FormirajPozajmicu(
                    listBox1.SelectedItem.ToString(), Member.TrenutniClanID);

                if (iznajmljivanjeRepo.InsertIznajmljivanje(pozajmica))
                {
                    MessageBox.Show($"Pozajmica je izvrsena.");
                }
                else
                {
                    MessageBox.Show("Tu knjigu ste vec iznajmili, molimo Vas izaberite neku drugu!");
                }

            }
            else
            {
                MessageBox.Show("Morate izabrati knjigu koju želite da iznajmite");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            PrikazKnjiga prikazKnjiga = new PrikazKnjiga();
            prikazKnjiga.Show();
            this.Hide();
        }

        private void PretragaKnjiga_Load(object sender, EventArgs e)
        {

        }
    }

}

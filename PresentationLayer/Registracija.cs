using BusinessLayer;
using SharedFolder.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PresentationLayer
{
    public partial class Registracija : Form
    {
        private readonly ClanBusiness clanBusiness = new ClanBusiness();
        public Registracija()
        {
            InitializeComponent();
        }
     private void button1_Click(object sender, EventArgs e)
        {
            if (textBoxIme.Text != "" && textBoxPrezime.Text != "" && textBoxKorisnickoIme.Text != "" && textBoxLozinka.Text != "" && textBoxAdresa.Text != "" && textBoxKontaktInfo.Text != ""  && Godišnja.Text != "")
            {
                

                Clan c = new Clan();
                c.Ime = textBoxIme.Text;
                c.Prezime = textBoxPrezime.Text;
                c.Adresa = textBoxAdresa.Text;
                c.KontaktInformacije = textBoxKontaktInfo.Text;
                c.Clanarina = Godišnja.Text;
                c.Lozinka = textBoxLozinka.Text;
                c.KorisnickoIme = textBoxKorisnickoIme.Text;

                if(!IsValidPassword(c.Lozinka))
                {
                    MessageBox.Show("Lozinka mora sadržati barem jedan broj i jedan specifičan znak.");
                    return;
                }


                if (this.clanBusiness.InsertMember(c))
                {
                    MessageBox.Show("Registracija je uspesna!");
                }
                else
                {
                    MessageBox.Show("Registracija nije uspesna!");
                }
                textBoxIme.Text = "";
                textBoxPrezime.Text = "";
                textBoxAdresa.Text = "";
                textBoxKontaktInfo.Text = "";
                Godišnja.Text = "";
                textBoxKorisnickoIme.Text = "";
                textBoxLozinka.Text = "";
                

                Prijava prijava = new Prijava();
                this.Hide();
                prijava.Show();

            }

            else
            {
                MessageBox.Show("Sva polja moraju biti popunjena!");
            }

        }

        private bool IsValidPassword(string Lozinka)
        {

            return Lozinka.Any(char.IsDigit) && Lozinka.Any(ch => !char.IsLetterOrDigit(ch));
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Prijava prijava = new Prijava();
            this.Hide();
            prijava.Show();

        }

        private void Registracija_Load(object sender, EventArgs e)
        {

        }
    }
}

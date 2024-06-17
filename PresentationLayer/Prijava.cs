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
    public partial class Prijava : Form
    {
        private readonly ClanBusiness clanBusiness = new ClanBusiness();
        public Prijava()
        {
            InitializeComponent();
        }

        
        private void button1_Click(object sender, EventArgs e)
        {
            string KorisnickoIme = textBox1.Text;
            string Lozinka = textBox2.Text;


            if (KorisnickoIme != "" && Lozinka != "")
            {
                if (IsValidPassword(Lozinka))
                {
                    //mora da se kreira promenljiva i da se inicijalizuje da bi se prosledio u funkciji
                    //kao ref parametar
                    int clanID = 0;
                    //Poziva se funkcija za prijavu gde poredi korisnicko ime i lozinku
                    //i ukoliko pronadje korisnika (logovanje je uspelo) onda prosledjuje i
                    //id clana koji se ulogovao i korisnicko ime i clan se smetaju u staticku
                    //klasu member iz koje se moze pristupiti bilo gde iz koda kada god zatrebaju
                    //podaci o trenutno ulogovanom clanu
                    if (this.clanBusiness.Prijava(KorisnickoIme, Lozinka, ref clanID))
                    {
                        //Postavlja ime i ID clana u klasi Member 
                        Member.PostaviTrenutnogClana(KorisnickoIme);
                        Member.PostaviTrenutniClanId(clanID);
                        PrikazKnjiga prikaz_knjiga = new PrikazKnjiga();
                        this.Hide();
                        prikaz_knjiga.Show();


                    }
                    else
                    {
                        MessageBox.Show("Neispravna lozinka ili korisničko ime");
                    }
                }
                else
                {
                    MessageBox.Show("Lozinka mora sadržati barem jedan broj i jedan specifičan znak.");
                }
            }
            else
            {
                MessageBox.Show("Popunite oba polja za prijavu");
            }


        }

        private bool IsValidPassword(string Lozinka)
        {

            return Lozinka.Any(char.IsDigit) && Lozinka.Any(ch => !char.IsLetterOrDigit(ch));
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Registracija registracija = new Registracija();
            registracija.Show();
            this.Hide();
        }

        private void Prijava_Load(object sender, EventArgs e)
        {

        }
    }
    }



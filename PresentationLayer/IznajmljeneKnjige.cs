using BusinessLayer;
using SharedFolder;
using SharedFolder.Models;
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

namespace PresentationLayer
{
    public partial class IznajmljeneKnjige : Form
    {
        IznajmljivanjeBusiness iznajmljivanjeBusiness = new IznajmljivanjeBusiness();
        List<Iznajmljivanje> lista = new List<Iznajmljivanje>();
        public IznajmljeneKnjige()
        {
            InitializeComponent();
        }

        //Funkcija za vracanje knjige po datom ISBN-u koja je selektovana
        private void button1_Click(object sender, EventArgs e)
        {
            //Ukoliko korisnik nije izabrao knjigu izbacite upozorenje i zavrsiti funkciju
            if(listBox1.SelectedItem == null)
            {
                MessageBox.Show("Izaberite knjigu koju zelite da vratite!");
                return;
            }
            //Uzima isbn knjige zato sto preko isbn-a se u listi poredi sa drugim pozajmicama kako bi znali
            //koji je ID iznajmljivanja trenutne knjige koja treba da se vrati.
            int isbn = iznajmljivanjeBusiness.UzmiISBN(listBox1.SelectedItem);

            foreach(Iznajmljivanje pozajmica in lista)
            {
                if(isbn == pozajmica.Knjiga.ISBN)
                {
                    iznajmljivanjeBusiness.DeleteRental(pozajmica.IdIznajmljivanja);
                    MessageBox.Show("Knjiga je vracena!");
                    break;
                }
            }
            //Svaki put kada se izbrise red u tabeli, mora i lista i listBox da se isprazne
            //kako bi se azurirali podaci i pravilno prikazivali samo redovi koji postoje
            //u tabeli
            lista.Clear();
            textBox1.Clear();
            lista = iznajmljivanjeBusiness.GetIznajmljeneKnjige(Member.TrenutniClanID);
            UnesiListu();
        }

        //Prilikom ucitavanja forme, u listi se smestaju podaci o svim pozajmicama i unose se u listbox
        private void IznajmljeneKnjige_Load(object sender, EventArgs e)
        {
            lista = iznajmljivanjeBusiness.GetIznajmljeneKnjige(Member.TrenutniClanID);

            UnesiListu();
        }
        //Funkcija koja u listu iznajmljivanja smesta podatke o svim pozajmicama koje je napravio trenutno ulogovan
        //korisnik
        private void UnesiListu()
        {
            listBox1.Items.Clear();
            foreach (Iznajmljivanje pozajmica in lista)
            {
                listBox1.Items.Add($"ISBN: {pozajmica.Knjiga.ISBN} Naslov: {pozajmica.Knjiga.Naslov}, " +
                    $"Datum iznajmljivanja: {pozajmica.DatumIznajmljivanja}, " +
                    $"Datum vracanja: {pozajmica.DatumVracanja}");
            }
        }

        //U textbox ispisuje knjigu koja je trenutno selektovana iz listbox-a
        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBox1.Text = listBox1.SelectedItem.ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            PrikazKnjiga prikazKnjiga = new PrikazKnjiga(); 
            prikazKnjiga.Show();
            this.Hide();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}

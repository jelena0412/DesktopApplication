using DataLayer;
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
    public partial class PrikazKnjiga : Form
    {
        private KnjigaRepository knjigaRepository = new KnjigaRepository();
        public PrikazKnjiga()
        {
            InitializeComponent();
            PrikaziPodatke();
            dataGridView1.ReadOnly = true;
        }
        private void PrikaziPodatke()
        {
            knjigaRepository.PrikaziKnjigeUDatagridView(dataGridView1);
        }
       

        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
           

            if (e.RowIndex >= 0)
            {
                if (e.RowIndex % 2 == 0)
                {
                   
                    e.CellStyle.BackColor = Color.FromArgb(189,154,122);
                    e.CellStyle.ForeColor = Color.Black; // Opciono, postavite boju teksta
                }
                else
                {
                    
                    e.CellStyle.BackColor = Color.FromArgb(88, 57, 39);
                    e.CellStyle.ForeColor = Color.White; // Opciono, postavite boju teksta
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            PretragaKnjiga pretraga = new PretragaKnjiga();
            pretraga.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            IznajmljeneKnjige iznajmljeneKnjige = new IznajmljeneKnjige();
            iznajmljeneKnjige.Show();
            this.Hide();
        }

        private void PrikazKnjiga_Load(object sender, EventArgs e)
        {

        }
    }
}

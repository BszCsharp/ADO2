using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplicationDb1
{
    public partial class FormUpdate : Form
    {
        private Artikel selArtikel;
        private DialogResult result = DialogResult.OK;

        public DialogResult Result
        {
            get
            {
                return result;
            }

            private set
            {
                result = value;
            }
        }

        public Artikel SelArtikel
        {
            get
            {
                return selArtikel;
            }

            set
            {
                selArtikel = value;
            }
        }

        public FormUpdate()
        {
            InitializeComponent();
        }
        public FormUpdate(Artikel artikel):this()
        {
            SelArtikel = artikel;
            InitializeControls();

        }

        private void InitializeControls()
        {
            this.textBoxArtikelOID.Text = SelArtikel.ArtikelOid.ToString();
            this.textBoxArtikelnummer.Text = SelArtikel.ArtikelNr;
            this.textBoxARtikelgruppe.Text = SelArtikel.ArtikelGruppe.ToString();
            this.textBoxARtikelbezeichnung.Text = SelArtikel.Bezeichnung;
            this.textBoxBestand.Text = SelArtikel.Bestand.ToString();
            this.textBoxMeldebestand.Text = SelArtikel.Meldebestand.ToString();
            this.textBoxVerpackung.Text = SelArtikel.Verpackung.ToString();
            this.textBoxVkPreis.Text = SelArtikel.VkPreis.ToString();
            this.textBoxltzteEntnahme.Text = SelArtikel.LetzteEntnahme.ToShortDateString();
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            SelArtikel.ArtikelNr = textBoxArtikelnummer.Text;
            SelArtikel.Bezeichnung = textBoxARtikelbezeichnung.Text;
            SelArtikel.Bestand = Convert.ToInt16(textBoxBestand.Text);
            SelArtikel.Meldebestand = Convert.ToInt16(textBoxMeldebestand.Text);
            SelArtikel.VkPreis = Convert.ToDecimal(textBoxVkPreis.Text);
            SelArtikel.LetzteEntnahme = Convert.ToDateTime(textBoxltzteEntnahme.Text);
            this.Close();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Result = DialogResult.Cancel;
            this.Close();
        }

        private void buttonTextOpen_Click(object sender, EventArgs e)
        {
            textBoxARtikelgruppe.Enabled = true;

        }
    }
}

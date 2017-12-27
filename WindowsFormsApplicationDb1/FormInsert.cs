using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;

namespace WindowsFormsApplicationDb1
{
    public partial class FormInsert : Form
    {
        OleDbConnection con;
        Artikel a;
        public FormInsert()
        {
            InitializeComponent();
        }
        public FormInsert(OleDbConnection con, Artikel artikel):this()
        {
            this.con = con;
            this.a = artikel;
            InitializeControls();
        }

        private void InitializeControls()
        {
            if (con.State != ConnectionState.Open)
            {
                con.Open();
            }
            OleDbCommand cmd = con.CreateCommand();
            cmd.CommandText = "Select * from tVerpackung";
            OleDbDataReader reader = cmd.ExecuteReader();
            while(reader.Read())
            {
                Verpackung v = new Verpackung();
                v.VerpackungId = reader.GetInt32(0);
                v.Bezeichnung = reader.GetString(1);
                comboBoxVerpackung.Items.Add(v);
            }
            reader.Close();
            cmd.CommandText = "Select * from tArtGruppe";
            reader = cmd.ExecuteReader();
            while(reader.Read())
            {
                Artikelgruppe ag = new Artikelgruppe();
                ag.ArtGruppenId = reader.GetInt32(0);
                ag.Gruppenbez = reader.GetString(1);
                comboBoxArtikelGruppe.Items.Add(ag);
            }
            reader.Close();


        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            a.ArtikelNr = textBoxArtikelnummer.Text;
            a.ArtikelGruppe = ((Artikelgruppe)comboBoxArtikelGruppe.SelectedItem).ArtGruppenId;
            a.Bezeichnung = textBoxARtikelbezeichnung.Text;
            a.Bestand = Convert.ToInt16(textBoxBestand.Text);
            a.Meldebestand = Convert.ToInt16(textBoxMeldebestand.Text);
            a.Verpackung = ((Verpackung)comboBoxVerpackung.SelectedItem).VerpackungId;
            a.VkPreis = Convert.ToDecimal(textBoxVkPreis.Text);
            a.LetzteEntnahme = dateTimePicker1.Value;

            this.Close();
        }
    }
}

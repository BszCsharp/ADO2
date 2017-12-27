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
using System.Globalization;

namespace WindowsFormsApplicationDb1
{
    public partial class Form1 : Form
    {
        OleDbConnection con = null;
        OleDbCommand cmd = null;
        OleDbDataReader reader = null;
        List<Artikel> artikelList = null;
        public Form1()
        {
            InitializeComponent();
            artikelList = new List<Artikel>();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Provider=Microsoft.ACE.OLEDB.12.0;Data Source=Bestellung.accdb
            con = new OleDbConnection();
            //OleDbConnectionStringBuilder builder = new OleDbConnectionStringBuilder();
            //builder.Provider = "Microsoft.ACE.OLEDB.12.0";
            //builder.DataSource = "Bestellung.accdb";
            con.ConnectionString = Properties.Settings.Default.DbCon;

            try
            {
                con.Open();
                toolStripStatusLabel1.Text = "Verbindung erfolgreich";
            }
            catch (InvalidOperationException Inv)
            {
                MessageBox.Show("Verbindung bereits geöffnet");
            }
            catch (OleDbException ole)
            {
                MessageBox.Show(ole.Message);
            }

        }

        private void buttonCommand_Click(object sender, EventArgs e)
        {
            cmd = con.CreateCommand();
            cmd.CommandText = "Select * from tArtikel";
            try
            {
                reader = cmd.ExecuteReader();
            }
            catch (Exception)
            {

                MessageBox.Show("Zugriff auf tArtikel nicht möglich");
            }
            
        }

        private void buttonReader_Click(object sender, EventArgs e)
        {
            while(reader.Read() == true)
            {
                //String bez = reader.GetString(3);
                Artikel a = mkArtikelObject(reader);
                //listBoxAusgabe.Items.Add(a);
                artikelList.Add(a);
            }
            listBoxAusgabe.DataSource = artikelList;
            reader.Close();
        }

        private Artikel mkArtikelObject(OleDbDataReader reader)
        {
            Artikel a = new Artikel();
            int i = 0;
            a.ArtikelOid = reader.GetInt32(i++);
            //i++;

            //if (reader[i] != DBNull.Value) a.ArtikelNr = reader.GetString(i++);
            //else i++;
            a.ArtikelNr = Convert.ToString(CheckDBNull(reader[i++]));
            a.ArtikelGruppe = Convert.ToInt32(CheckDBNull(reader[i++])); //reader.GetInt32(i++);
            a.Bezeichnung = reader.GetString(i++);
            //if (reader[i] != DBNull.Value)
            a.Bestand = Convert.ToInt16(CheckDBNull( reader[i++]));
            //else i++;
            if (reader[i] != DBNull.Value) a.Meldebestand = reader.GetInt16(i++);
            a.Verpackung =Convert.ToInt16( reader[i++]);
            a.VkPreis = reader.GetDecimal(i++);
            if (reader[i] !=  DBNull.Value)
            {
                a.LetzteEntnahme = reader.GetDateTime(i++);
            }


            // Attribute aus DataReader gebildet werden
            return a;
        }
        private Object CheckDBNull(Object o)
        {
            if (o == DBNull.Value) return null;
            else return o;
        }

        private void buttonUpdate_Click(object sender, EventArgs e)
        {
            if (listBoxAusgabe.SelectedItem != null)
            {
                Artikel a =  (Artikel) listBoxAusgabe.SelectedItem;
                FormUpdate frmUpdate = new FormUpdate(a);
                frmUpdate.ShowDialog(); // modales Fenster
                if(frmUpdate.Result == DialogResult.OK)
                {

                    
                    updateArtikel(a);
                    listBoxAusgabe.DataSource = null;
                    listBoxAusgabe.DataSource = artikelList;

                }
                else
                {
                    toolStripStatusLabel1.Text = "Änderung wurde abgebrochen";

                }

            }


           
            
        }

        private void updateArtikel(Artikel a )
        {
            //TODO: Command-Objekt
            OleDbCommand cmd = con.CreateCommand();
            //TODO: Parameter generieren
            cmd.Parameters.AddWithValue("ANR", a.ArtikelNr);
            cmd.Parameters.AddWithValue("BEZ", a.Bezeichnung);
            cmd.Parameters.AddWithValue("BEST", a.Bestand);
            cmd.Parameters.AddWithValue("MBEST", a.Meldebestand);
            cmd.Parameters.AddWithValue("VKPR", a.VkPreis);
            cmd.Parameters.AddWithValue("ENT", a.LetzteEntnahme);
            //TODO: Commandtext: SQL
            String sql = "UPDATE tArtikel SET ArtikelNR = ANR, Bezeichnung = BEZ, Bestand = BEST, Meldebestand = MBEST, VkPreis = VKPR, letzteEntnahme = ENT ";
            sql += "WHERE ArtikelOID = " + a.ArtikelOid.ToString();
            cmd.CommandText = sql;
            //TODO: Conn open
            //con.Open();
            //TODO: Command ausführen
            try
            {
                cmd.ExecuteNonQuery();
                toolStripStatusLabel1.Text = "Update erfolgreich";
            }
            catch (Exception exc)
            {

                MessageBox.Show("Fehler beim Update");
                toolStripStatusLabel1.Text = exc.Message;
            }
        }

        private void buttonNeuerDatensatz_Click(object sender, EventArgs e)
        {
            Artikel a = new Artikel();
            FormInsert frmInsert = new FormInsert(con, a);
            frmInsert.ShowDialog();
            InsertData(a);

        }
        private void InsertData(Artikel a)
        {
            OleDbCommand cmd = con.CreateCommand();
            cmd.Parameters.AddWithValue("ANR", a.ArtikelNr);
            cmd.Parameters.AddWithValue("AGR", a.ArtikelGruppe);
            cmd.Parameters.AddWithValue("BEZ", a.Bezeichnung);
            cmd.Parameters.AddWithValue("BST", a.Bestand);
            cmd.Parameters.AddWithValue("MBST", a.Meldebestand);
            cmd.Parameters.AddWithValue("VPA", a.Verpackung);
            String preis = a.VkPreis.ToString(new CultureInfo("de-DE"));
            cmd.Parameters.AddWithValue("VKP", a.VkPreis.ToString(new CultureInfo("de-DE")));
            cmd.Parameters.AddWithValue("LENT", a.LetzteEntnahme.ToString());
            String sql = "Insert INTO tArtikel(ArtikelNr,ArtikelGruppe,Bezeichnung,Bestand,Meldebestand,Verpackung,VkPreis,letzteEntnahme) ";
            sql += "Values(ANR,AGR,BEZ,BST,MBST,VPA,VKP,LENT)";
            cmd.CommandText = sql;
            try
            {
                cmd.ExecuteNonQuery();
                toolStripStatusLabel1.Text = "Insert erfolgreich";

            }
            catch (Exception exc)
            {

                MessageBox.Show("DB-Fehler");
                toolStripStatusLabel1.Text = exc.Message;

            }
        }
    }
}

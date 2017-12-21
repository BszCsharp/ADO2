using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplicationDb1
{
    public class Verpackung
    {
        Int32 verpackungId;
        String verpackung;

        public int VerpackungId
        {
            get
            {
                return verpackungId;
            }

            set
            {
                verpackungId = value;
            }
        }

        public string Bezeichnung
        {
            get
            {
                return verpackung;
            }

            set
            {
                verpackung = value;
            }
        }
        public override string ToString()
        {
            return this.Bezeichnung;
        }
    }
}

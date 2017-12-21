using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplicationDb1
{
    public class Artikelgruppe
    {
        Int32 artGruppenId;
        String gruppenbez;

        public int ArtGruppenId
        {
            get
            {
                return artGruppenId;
            }

            set
            {
                artGruppenId = value;
            }
        }

        public string Gruppenbez
        {
            get
            {
                return gruppenbez;
            }

            set
            {
                gruppenbez = value;
            }
        }
        public override string ToString()
        {
            return this.Gruppenbez;
        }
    }
}

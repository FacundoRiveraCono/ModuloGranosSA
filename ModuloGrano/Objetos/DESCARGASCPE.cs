using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModuloGrano.Objetos
{
    public class DESCARGASCPE
    {
        public class Root
        {
 
            public List<Value> value { get; set; }
        }

        public class Value
        {
            public int DocEntryOv { get; set; }
            public string ShipToCode { get; set; }
            public string CTG { get; set; }
            public double Cantidad { get; set; }
            public int id__ { get; set; }
        }
    }
}

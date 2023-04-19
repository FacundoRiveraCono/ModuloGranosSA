using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModuloGrano.Objetos
{
    internal class ORDENESFLETE
    {
        // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
        public class Root
        {
           
            public List<Value> value { get; set; }

          
        }

        public class Value
        {
            public string CardCode { get; set; }
            public string ItemCode { get; set; }
            public string Norma1 { get; set; }
            public string Norma2 { get; set; }
            public string Norma3 { get; set; }
            public string Norma5 { get; set; }
            public string Norma4 { get; set; }
            public string CTG { get; set; }
            public double? Precio_Bolsa { get; set; }
            public double Origen { get; set; }
            public double Descarga { get; set; }
            public double Diferenciatot { get; set; }
            public double Tolerancia { get; set; }
            public double? ImporteNeto { get; set; }
            public double? Iva { get; set; }
            public double Diferencia { get; set; }
            public int id__ { get; set; }
        }


    }
}

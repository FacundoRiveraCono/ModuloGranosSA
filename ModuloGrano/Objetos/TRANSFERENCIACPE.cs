using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModuloGrano.Objetos
{
     public class TRANSFERENCIACPE
    {
        public class Root
        {
            public List<Value> value { get; set; }
        }

        public class Value
        {
            public int DocEntry { get; set; }
            public string GranoSAP { get; set; }
            public string AlmacenOrigen { get; set; }
            public string AlmacenOrigen2 { get; set; }
            public string AlmacenOrigen3 { get; set; }
            public string AlmacenTercero { get; set; }
            public string CardCode { get; set; }
            public string Address { get; set; }
            public string Planta { get; set; }
            public string Ubicacion { get; set; }
            public string Ubicacion2 { get; set; }
            public string Ubicacion3 { get; set; }
            public double Cantidad1 { get; set; }
            public double Cantidad2 { get; set; }
            public double Cantidad3 { get; set; }
            public string Norma1 { get; set; }
            public string Norma2 { get; set; }
            public string Norma5 { get; set; }
            public string Norma3 { get; set; }
            public string Norma4 { get; set; }         
            public string CTG { get; set; }
            public int Folio { get; set; }
            public int AbsEntry { get; set; } = 0;
            public int AbsEntry2 { get; set; } = 0;
            public int AbsEntry3 { get; set; } = 0;
            public int id__ { get; set; }
        }
    }
}

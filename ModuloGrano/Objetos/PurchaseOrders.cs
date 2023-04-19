using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModuloGrano.Objetos
{
     class PurchaseOrders 
    {
        public DateTime DocDate { get; set; }
        public DateTime DocDueDate { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public string CardCode { get; set; }
        public string U_CTG { get; set; }
        public string DocObjectCode { get; set; } = "oPurchaseOrders";
        public List<DocumentLines> DocumentLines { get; set; }


    }
}

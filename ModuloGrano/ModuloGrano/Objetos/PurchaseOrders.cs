using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModuloGrano.Objetos
{

    //class root
    //{
    //    public List <PurchaseOrders> PurchaseOrders { get; set; }

    //}
     class PurchaseOrders 
    {
        public DateTime DocDate { get; set; }
        public DateTime DocDueDate { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public string CardCode { get; set; }
        public string U_CTG { get; set; }
        public string DocObjectCode { get; set; } = "oPurchaseOrders";
        public List<PurchaseOrdersLines> DocumentLines { get; set; }
        public string PTICode { get; set; }


    }

     class PurchaseOrdersLines
    {
        public string ItemCode { get; set; }
        public string CostingCode { get; set; }
        public int LineNum { get; set; }
        public string CostingCode2 { get; set; }
        public string CostingCode3 { get; set; }
        public string CostingCode4 { get; set; }
        public string CostingCode5 { get; set; }
        public double Price { get; set; }
        public double Quantity { get; set; }

    }
}

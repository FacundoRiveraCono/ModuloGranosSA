using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModuloGrano.Objetos
{
    //Se utiliza herencia ya que muchos datos son iguales
     class DeliveryNotes
    {
        public DateTime DocDate { get; set; } = DateTime.Now;
        public DateTime TaxDate { get; set; } = DateTime.Now;
        public DateTime DocDueDate { get; set; } = DateTime.Now;
        public double DocTotal { get; set; }
        public string DocObjectCode { get; set; } = "oDeliveryNotes";
        public DateTime CreationDate { get; set; } = DateTime.Now;
        public DateTime UpdateDate { get; set; } = DateTime.Now;

        public string PointOfIssueCode { get; set; } = "00005";
        public string ShipToCode { get; set; }
        public string U_CTG { get; set; }
        public string NroContrato2 { get; set; }    

        public List<DocumentLines> DocumentLines { get; set; }
    }

   internal class DocumentLines
    {
        public string ItemCode { get; set; }
        public double BaseEntry { get; set; }
        public int BaseType { get; set; }
        public double Quantity { get; set; }
        public double Price { get; set; }
        public int BaseLine { get; set; }
        public string CostingCode { get; set; }
        public string CostingCode2 { get; set; }
        public string CostingCode3 { get; set; }
        public string CostingCode4 { get; set; }
        public string CostingCode5 { get; set; }
        public double DiscountPercent { get; set; }

       
    }
}

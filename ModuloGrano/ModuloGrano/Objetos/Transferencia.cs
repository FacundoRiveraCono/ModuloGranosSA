using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SAPbobsCOM;
using SAPbouiCOM;

namespace ModuloGrano.Objetos
{
   
    public class Transfer
    {

        public int DocEntry { get; set; }
        public int Series { get; set; }
        public string Printed { get; set; }
        public DateTime DocDate { get; set; }
        public DateTime DueDate { get; set; }
        public string CardCode { get; set; }
        public string CardName { get; set; }
        public string Address { get; set; }
        public string Reference1 { get; set; }
        public string Reference2 { get; set; }
        public string Comments { get; set; }
        public string JournalMemo { get; set; }
        public int PriceList { get; set; }
        public int SalesPersonCode { get; set; }
        public string FromWarehouse { get; set; }
        public string ToWarehouse { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public int FinancialPeriod { get; set; }
        public int TransNum { get; set; }
        public int DocNum { get; set; }
        public DateTime TaxDate { get; set; }
        public int ContactPerson { get; set; }
        public string FolioPrefixString { get; set; }
        public string FolioNumber { get; set; }
        public string DocObjectCode { get; set; }
        public int FolioNumberFrom { get; set; }
        public int FolioNumberTo { get; set; }
        public string AttachmentEntry { get; set; }
        public string DocumentStatus { get; set; }
        public string ShipToCode { get; set; }
        public string U_TipoComprobAFIP { get; set; }
        public string U_NroContrato { get; set; }
        public double U_PesoNeto { get; set; }
        public string U_NumFolio { get; set; }
        public string U_CTG { get; set; }
        public List<StockTransferLines> StockTransferLines { get; set; }


    }

    public class StockTransferLines
    {
        public string ItemCode { get; set; }
        public double Quantity { get; set; }
        public string WarehouseCode { get; set;}
        public string FromWarehouseCode { get; set; }
        public string DistributionRule { get;set; }
        public string DistributionRule2 { get; set; }
        public string DistributionRule3 { get; set; }
        public string DistributionRule4 { get; set; }
        public string DistributionRule5 { get; set; }


        public List<StockTransferLinesBinAllocations> StockTransferLinesBinAllocations { get; set; }

    }

    public class StockTransferLinesBinAllocations
    {
        public int BinAbsEntry { get; set; }
        public double Quantity { get; set; }
        public string AllowNegativeQuantity { get; set; }
        public int SerialAndBatchNumbersBaseLine { get; set; }

        //public string UbicaciónSAP { get; set; }
        public string BinActionType { get; set; }
        //public int BaseLineNumber { get; set; }
    }
}

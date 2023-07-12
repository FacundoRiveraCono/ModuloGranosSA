using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModuloGrano.Objetos
{
    public class CartaPorte
    {
        // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
        public class Root
        {
            
            public string odatacontext { get; set; }
            public List<Value> value { get; set; }
        }

        public class Value
        {
            public string CUITSolicitante { get; set; }
            public string nroOrden { get; set; }
            public string ProvinciaOrigen { get; set; }
            public string LocalidadOrigen { get; set; }
            public string CUITRemitenteComercialVentaPrimaria { get; set; }
            public string CUITRemitenteComercialVentaSecundaria { get; set; }
            public string CUITRemitenteComercialVentaSecundaria2 { get; set; }
            public string CUITMercadoInterno { get; set; }
            public string CUITCorredorVentaPrimaria { get; set; }
            public string CUITCorredorVentaSecundaria { get; set; }
            public string CUITEntregador { get; set; }
            public string Cosecha { get; set; }
            public string Km { get; set; }
            public string CUITTransportista { get; set; }
            public string CUITChofer { get; set; }
            public string Dominio1 { get; set; }
            public string Dominio2 { get; set; }
            public string ProvinciaDestino { get; set; }
            public string LocalidadDestino { get; set; }
            public string CUITDestino { get; set; }
            public string CUITDesinatario { get; set; }
            public string FechaPartida { get; set; }
            public string Planta { get; set; }
            public string CUITPagadorFlete { get; set; }
            public string Observaciones { get; set; }
            public int Codigo { get; set; }
            public int TipoCPE { get; set; }
            public int Sucursal { get; set; }
            public int CodigoGrano { get; set; }
            public int PesoBruto { get; set; }
            public int PesoTara { get; set; }
            public double Tarifa { get; set; }
            public int id__ { get; set; }
        }


        public class UpdateSAP
        {
            public string U_MensajeAFIP { get; set; }
            public string U_CTG { get; set; }
            public string U_Adjunto { get; set; }
            public Int32 U_NroOrden { get; set; }
        }


    }
}

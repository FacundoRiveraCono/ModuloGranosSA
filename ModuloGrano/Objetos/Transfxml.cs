using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ModuloGrano.Objetos
{

    // using System.Xml.Serialization;
    // XmlSerializer serializer = new XmlSerializer(typeof(ConsultarCPEAutomotorResponse));
    // using (StringReader reader = new StringReader(xml))
    // {
    //    var test = (ConsultarCPEAutomotorResponse)serializer.Deserialize(reader);
    // }

    [XmlRoot(ElementName = "cabecera")]
    public class Cabecera
    {

        [XmlElement(ElementName = "tipoCartaPorte")]
        public int TipoCartaPorte { get; set; }

        [XmlElement(ElementName = "sucursal")]
        public int Sucursal { get; set; }

        [XmlElement(ElementName = "nroOrden")]
        public int NroOrden { get; set; }

        [XmlElement(ElementName = "nroCTG")]
        public double NroCTG { get; set; }

        [XmlElement(ElementName = "fechaEmision")]
        public DateTime FechaEmision { get; set; }

        [XmlElement(ElementName = "estado")]
        public string Estado { get; set; }

        [XmlElement(ElementName = "fechaInicioEstado")]
        public DateTime FechaInicioEstado { get; set; }

        [XmlElement(ElementName = "fechaVencimiento")]
        public DateTime FechaVencimiento { get; set; }

        [XmlElement(ElementName = "observaciones")]
        public string Observaciones { get; set; }
    }

    [XmlRoot(ElementName = "origen")]
    public class Origen
    {

        [XmlElement(ElementName = "cuit")]
        public double Cuit { get; set; }

        [XmlElement(ElementName = "codProvincia")]
        public int CodProvincia { get; set; }

        [XmlElement(ElementName = "codLocalidad")]
        public int CodLocalidad { get; set; }
    }

    [XmlRoot(ElementName = "intervinientes")]
    public class Intervinientes
    {

        [XmlElement(ElementName = "cuitRemitenteComercialVentaPrimaria")]
        public double CuitRemitenteComercialVentaPrimaria { get; set; }

        [XmlElement(ElementName = "cuitRemitenteComercialVentaSecundaria")]
        public double CuitRemitenteComercialVentaSecundaria { get; set; }


        [XmlElement(ElementName = "cuitRemitenteComercialVentaSecundaria2")]
        public double CuitRemitenteComercialVentaSecundaria2 { get; set; }

        [XmlElement(ElementName = "cuitMercadoATermino")]
        public double cuitMercadoATermino { get; set; }

        [XmlElement(ElementName = "cuitCorredorVentaPrimaria")]
        public double CuitCorredorVentaPrimaria { get; set; }

        [XmlElement(ElementName = "cuitCorredorVentaSecundaria")]
        public double CuitCorredorVentaSecundaria { get; set; }

        [XmlElement(ElementName = "cuitRepresentanteEntregador")]
        public double cuitRepresentanteEntregador { get; set; }

        [XmlElement(ElementName = "cuitRepresentanteRecibidor")]
        public double cuitRepresentanteRecibidor { get; set; }

    }

    [XmlRoot(ElementName = "datosCarga")]
    public class DatosCarga
    {

        [XmlElement(ElementName = "codGrano")]
        public int CodGrano { get; set; }

        [XmlElement(ElementName = "pesoBruto")]
        public int PesoBruto { get; set; }

        [XmlElement(ElementName = "pesoTara")]
        public int PesoTara { get; set; }

        [XmlElement(ElementName = "cosecha")]
        public int Cosecha { get; set; }
    }

    [XmlRoot(ElementName = "destino")]
    public class Destino
    {

        [XmlElement(ElementName = "cuit")]
        public double Cuit { get; set; }

        [XmlElement(ElementName = "codProvincia")]
        public int CodProvincia { get; set; }

        [XmlElement(ElementName = "codLocalidad")]
        public int CodLocalidad { get; set; }

        [XmlElement(ElementName = "planta")]
        public int Planta { get; set; }
    }

    [XmlRoot(ElementName = "destinatario")]
    public class Destinatario
    {

        [XmlElement(ElementName = "cuit")]
        public double Cuit { get; set; }
    }

    [XmlRoot(ElementName = "transporte")]
    public class Transporte
    {

        [XmlElement(ElementName = "cuitTransportista")]
        public double CuitTransportista { get; set; }

        [XmlElement(ElementName = "dominio")]
        public List<string> Dominio { get; set; }

        [XmlElement(ElementName = "fechaHoraPartida")]
        public DateTime FechaHoraPartida { get; set; }

        [XmlElement(ElementName = "kmRecorrer")]
        public int KmRecorrer { get; set; }

        [XmlElement(ElementName = "cuitChofer")]
        public double CuitChofer { get; set; }

        [XmlElement(ElementName = "tarifaReferencia")]
        public double TarifaReferencia { get; set; }

        [XmlElement(ElementName = "tarifa")]
        public int Tarifa { get; set; }

        [XmlElement(ElementName = "cuitPagadorFlete")]
        public double CuitPagadorFlete { get; set; }

        [XmlElement(ElementName = "mercaderiaFumigada")]
        public bool MercaderiaFumigada { get; set; }
    }

    [XmlRoot(ElementName = "metadata")]
    public class Metadata
    {

        [XmlElement(ElementName = "servidor")]
        public string Servidor { get; set; }

        [XmlElement(ElementName = "fechaHora")]
        public DateTime FechaHora { get; set; }
    }

    [XmlRoot(ElementName = "respuesta")]
    public class Respuesta
    {

        [XmlElement(ElementName = "cabecera")]
        public Cabecera Cabecera { get; set; }

        [XmlElement(ElementName = "origen")]
        public Origen Origen { get; set; }

        [XmlElement(ElementName = "correspondeRetiroProductor")]
        public bool CorrespondeRetiroProductor { get; set; }

        [XmlElement(ElementName = "retiroProductor")]
        public object RetiroProductor { get; set; }

        [XmlElement(ElementName = "intervinientes")]
        public Intervinientes Intervinientes { get; set; }

        [XmlElement(ElementName = "datosCarga")]
        public DatosCarga DatosCarga { get; set; }

        [XmlElement(ElementName = "destino")]
        public Destino Destino { get; set; }

        [XmlElement(ElementName = "destinatario")]
        public Destinatario Destinatario { get; set; }

        [XmlElement(ElementName = "transporte")]
        public Transporte Transporte { get; set; }

        [XmlElement(ElementName = "pdf")]
        public string Pdf { get; set; }

        [XmlElement(ElementName = "errores")]
        public object Errores { get; set; }

        [XmlElement(ElementName = "metadata")]
        public Metadata Metadata { get; set; }
    }

    [XmlRoot(ElementName = "consultarCPEAutomotorResponse")]
    public class ConsultarCPEAutomotorResponse
    {

        [XmlElement(ElementName = "respuesta")]
        public Respuesta Respuesta { get; set; }

        [XmlAttribute(AttributeName = "xsi")]
        public string Xsi { get; set; }

        [XmlAttribute(AttributeName = "xsd")]
        public string Xsd { get; set; }

        [XmlText]
        public string Text { get; set; }
    }
}



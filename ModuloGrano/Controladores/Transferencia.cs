using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using WSAfip;
using System.Xml.Serialization;
using System.Xml;
using ModuloGrano.Objetos;
using System.IO;
using SAPbobsCOM;

namespace ModuloGrano.Controladores
{
    class Transferencia
    {

        //public async void AddTransferencia(Objetos.Transfer oTransferencia)
        //{
        //    string SessionID = "";
        //    //Conexión.ConexiónSAPB1 oCon = new Conexión.ConexiónSAPB1();
        //    Datos.DataBase oDb = new Datos.DataBase();

        //    try
        //    {
        //        //Busco el SessionID de la base de datos
        //        //SessionID = oCon.GetSessionID();
        //        SessionID = oDb.GetSessionID();
        //        var json = JsonSerializer.Serialize(oTransferencia);
        //        var data = new StringContent(json, Encoding.UTF8, "application/json");
        //        var baseAddress = new Uri("http://br01-srv-db02:50002/b1s/v1/StockTransfers");

        //        String Cookie = String.Format("B1SESSION={0}", SessionID);
        //        using (var handler = new HttpClientHandler { UseCookies = false })
        //        using (var client = new HttpClient(handler) { BaseAddress = baseAddress })
        //        {
        //            var message = new HttpRequestMessage(HttpMethod.Post, baseAddress);
        //            message.Content = data;
        //            message.Headers.Add("Cookie", Cookie);
        //            var result = await client.SendAsync(message);
        //            if (result.StatusCode == HttpStatusCode.Unauthorized)
        //            {
        //                //Conexión.ConexiónSAPB1 Sl = new Conexión.ConexiónSAPB1();
        //                //Sl.ConectarSL();
        //                this.AddTransferencia(oTransferencia);
        //            }
        //            else
        //            {
        //                var content = await result.Content.ReadAsStringAsync();
        //                result.EnsureSuccessStatusCode();
        //            }

        //        }
        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }
        //}

        //public async void GetLoteCantidad(decimal Cantidad)
        //{
        //    try
        //    {

        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }
        //}


        //public WSAfip.autorizarCPEAutomotorResponse PostCartaPorte(Objetos.Transfer oTransferencia)
        //{
        //    string fechatest = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss");
        //    DateTime Fecha = Convert.ToDateTime(fechatest);
        //    //string ruta = ;
        //    bool Productor = false;
        //    WSAfip.AutorizarAutomotorSolicitud reqSol = new WSAfip.AutorizarAutomotorSolicitud();
        //    WSAfip.autorizarCPEAutomotorResponse oResponse = new WSAfip.autorizarCPEAutomotorResponse();
        //    //WSAfip.autorizarCPEAutomotorRequest reqSol = new WSAfip.autorizarCPEAutomotorRequest();

        //    //WSAfip.automoro
        //    //WSAfip.
        //    Objetos.SesionAFIP oSesion = new Objetos.SesionAFIP();
        //    try
        //    {
        //        #region Definicion de Objetos
        //        WSAfip.Auth oauth = new WSAfip.Auth();
        //        //Defino el resto de objetos correspondiente a los tag del xml 
        //        WSAfip.DetalleAutomotorRespuesta responseCpe = new WSAfip.DetalleAutomotorRespuesta();
        //        //Cabecera
        //        WSAfip.CabeceraAutomotorSolicitud cabecera = new WSAfip.CabeceraAutomotorSolicitud();
        //        WSAfip.OrigenAutomotorSolicitud CPEOrigen = new WSAfip.OrigenAutomotorSolicitud();
        //        //WSAfip.OrigenOperadorAutomotorSolicitud CPEOrigenOp = new WSAfip.OrigenOperadorAutomotorSolicitud();
        //        WSAfip.OrigenProductorAutomotorSolicitud CPEOrigenPr = new WSAfip.OrigenProductorAutomotorSolicitud();
        //        //DatosCarga
        //        WSAfip.DatosCargaAutomotorSolicitud CPECarga = new WSAfip.DatosCargaAutomotorSolicitud();
        //        //Datos de transporte
        //        WSAfip.TransporteAutomotorSolicitud Transporte = new WSAfip.TransporteAutomotorSolicitud();
        //        //Destino
        //        WSAfip.DestinoSolicitud CPEDestino = new WSAfip.DestinoSolicitud();
        //        //Destino Destinatario
        //        WSAfip.DestinatarioSolicitud CPEDestinatario = new WSAfip.DestinatarioSolicitud();
        //        //Intervinientes
        //        WSAfip.IntervinientesSolicitud CPEInter = new WSAfip.IntervinientesSolicitud();
        //        WSAfip.CpePortTypeClient cpe = new WSAfip.CpePortTypeClient();
        //        //WSAfip.autorizarCPEAutomotorResponse oResponse = new WSAfip.autorizarCPEAutomotorResponse();

        //        #endregion

        //        #region Completo Objeto

        //        //auatorización
        //        oauth = oSesion.CompletarAuth();
        //        //Fin Autorización
        //        cabecera.tipoCP = oTransferencia.U_TipoCPE;
        //        cabecera.cuitSolicitante = 
        //        cabecera.sucursal = 5;
        //        //Metodo para obtener el ultimo Nro de Orden
        //        cabecera.nroOrden = oTransferencia.U_NroOrden;
        //        reqSol.cabecera = cabecera;


        //        CPEOrigenPr.codLocalidad = oTransferencia.U_LocOrigen;
        //        CPEOrigenPr.codProvincia = oTransferencia.U_ProvOrigen;
        //        CPEOrigen.productor = CPEOrigenPr;

        //        //reqSol.retiroProductor.
        //        reqSol.origen = CPEOrigen;


        //        if (!String.IsNullOrEmpty(oTransferencia.U_CUITRteVtaPrim))
        //        {

        //            CPEInter.cuitRemitenteComercialVentaPrimariaSpecified = true;
        //            CPEInter.cuitRemitenteComercialVentaPrimaria = Convert.ToInt64(oTransferencia.U_CUITRteVtaPrim);
        //        }
        //        else { CPEInter.cuitRemitenteComercialVentaPrimariaSpecified = false; }
        //        if (!String.IsNullOrEmpty(oTransferencia.U_CUITRteVtaSec))
        //        {
        //            CPEInter.cuitRemitenteComercialVentaSecundariaSpecified = true;
        //            CPEInter.cuitRemitenteComercialVentaSecundaria = Convert.ToInt64(oTransferencia.U_CUITRteVtaSec);
        //        }
        //        else { CPEInter.cuitRemitenteComercialVentaSecundaria2Specified = false; }
        //        if (!String.IsNullOrEmpty(oTransferencia.U_CUITRteVtaSec2))
        //        {
        //            CPEInter.cuitRemitenteComercialVentaSecundaria2Specified = true;
        //            CPEInter.cuitRemitenteComercialVentaSecundaria2 = Convert.ToInt64(oTransferencia.U_CUITRteVtaSec2);
        //        }
        //        else { CPEInter.cuitRemitenteComercialVentaSecundaria2Specified = false; }
        //        if (!String.IsNullOrEmpty(oTransferencia.U_CUITMAT))
        //        {
        //            CPEInter.cuitMercadoATerminoSpecified = true;
        //            CPEInter.cuitMercadoATermino = Convert.ToInt64(oTransferencia.U_CUITMAT);
        //        }
        //        else { CPEInter.cuitMercadoATerminoSpecified = false; }
        //        //if (!String.IsNullOrEmpty(oTransferencia.U_CUITRteComerProd))
        //        //{
        //        //    CPEInter.com= true;
        //        //    CPEInter.cuitRemitenteComercialVentaPrimaria = Convert.ToInt64(oTransferencia.U_CUITRteComerProd);
        //        //}
        //        //else { CPEInter.cuitRemitenteComercialVentaPrimariaSpecified = false; }
        //        //if (!String.IsNullOrEmpty(oTransferencia.U_CUITRteComerProd))
        //        //{
        //        //    CPEInter.CUIT
        //        //}
        //        //CPEInter. 
        //        if (!String.IsNullOrEmpty(oTransferencia.U_CUITCorrVtaPrim))
        //        {
        //            CPEInter.cuitCorredorVentaPrimariaSpecified = true;
        //            CPEInter.cuitCorredorVentaPrimaria = Convert.ToInt64(oTransferencia.U_CUITCorrVtaPrim);
        //        }
        //        else { CPEInter.cuitCorredorVentaPrimariaSpecified = false; }
        //        if (!String.IsNullOrEmpty(oTransferencia.U_CUITCorrVtaSec))
        //        {
        //            CPEInter.cuitCorredorVentaSecundariaSpecified = true;
        //            CPEInter.cuitCorredorVentaSecundaria = Convert.ToInt64(oTransferencia.U_CUITCorrVtaSec);
        //        }
        //        else
        //        {
        //            CPEInter.cuitCorredorVentaSecundariaSpecified = false;
        //        }

        //        //Falta agregar remitente comercial venta secundaria
        //        if (!String.IsNullOrEmpty(oTransferencia.U_CUITEntregador))
        //        {
        //            CPEInter.cuitRepresentanteEntregadorSpecified = true;
        //            CPEInter.cuitRepresentanteEntregador = Convert.ToInt64(oTransferencia.U_CUITEntregador);
        //        }
        //        else { CPEInter.cuitRepresentanteEntregadorSpecified = false; }
        //        if (!String.IsNullOrEmpty(oTransferencia.U_CUITRecibidor))
        //        {
        //            CPEInter.cuitRepresentanteRecibidorSpecified = true;
        //            CPEInter.cuitRepresentanteRecibidor = Convert.ToInt64(oTransferencia.U_CUITRecibidor);

        //        }
        //        else { CPEInter.cuitRepresentanteRecibidorSpecified = false; }
        //        reqSol.intervinientes = CPEInter;


        //        if (oTransferencia.U_EsProd == "Y") { reqSol.esSolicitanteCampo = true; } else { reqSol.esSolicitanteCampo = false; }
        //        if (oTransferencia.U_Retiro == "Y") { reqSol.correspondeRetiroProductor = true; } else { reqSol.correspondeRetiroProductor = false; }
        //        //if (oTransferencia.U_EsCampo == "Y") { reqSol.}
        //        //if (oTransferencia.U_EsCampo == "Y") { reqSol.esSolicitanteCampo = true; } else { reqSol.esSolicitanteCampo = false; }

        //        //Datos de carga
        //        //Codigo de Grano
        //        CPECarga.codGrano = Convert.ToInt16(oTransferencia.U_Grano);
        //        CPECarga.cosecha = Convert.ToInt16(oTransferencia.U_Cosecha);
        //        CPECarga.pesoBruto = Convert.ToInt32(oTransferencia.U_PBruto);
        //        CPECarga.pesoTara = Convert.ToInt32(oTransferencia.U_PTara);

        //        Transporte.fechaHoraPartida = Fecha.AddMinutes(15);
        //        Transporte.kmRecorrer = Convert.ToInt16(oTransferencia.U_KmRecorrer);
        //        if (oTransferencia.U_Tarifa > 0)
        //        {
        //            Transporte.tarifaSpecified = true;
        //            Transporte.tarifa = Convert.ToDecimal(oTransferencia.U_Tarifa);
        //        }
        //        else
        //        {
        //            Transporte.tarifaSpecified = false;
        //        }

        //        if (!String.IsNullOrEmpty(oTransferencia.U_CUITPagadorFlete))
        //        {
        //            Transporte.cuitPagadorFleteSpecified = true;
        //            Transporte.cuitPagadorFlete = Convert.ToInt64(oTransferencia.U_CUITPagadorFlete);
        //        }
        //        else
        //        {
        //            Transporte.cuitPagadorFleteSpecified = false;
        //        }


        //        Transporte.cuitTransportista = Convert.ToInt64(oTransferencia.U_CUITTransportista);
        //        Transporte.cuitChofer = Convert.ToInt64(oTransferencia.U_CUITChofer);
        //        Transporte.dominio = new string[] { oTransferencia.U_PatCamion, oTransferencia.U_PatAcoplado };
        //        reqSol.transporte = Transporte;
        //        //reqSol.datosCarga = CPECarga;
        //        reqSol.datosCarga = CPECarga;
        //        //Fin datos de Carga
        //        //Datos Destino               
        //        CPEDestino.esDestinoCampo = false;
        //        CPEDestino.codLocalidad = oTransferencia.U_LocDestino;//Crear Campo en SAP
        //        CPEDestino.codProvincia = oTransferencia.U_ProvDestino;
        //        CPEDestino.plantaSpecified = true;
        //        CPEDestino.planta = oTransferencia.U_PlaDest;
        //        CPEDestino.cuit = Convert.ToInt64(oTransferencia.U_CUITDestino);
        //        //reqSol.destino = CPEDestino;
        //        reqSol.destino = CPEDestino;
        //        //Fin Datos Destino
        //        //Inicio datos Destinatario
        //        CPEDestinatario.cuit = Convert.ToInt64(oTransferencia.U_CUITDestinatario);
        //        //reqSol.destinatario = CPEDestinatario;
        //        reqSol.destinatario = CPEDestinatario;
        //        reqSol.observaciones = oTransferencia.Comments;

        //        //Fin Datos Destinatario
        //        GuardarxmlEnviadoEmisionEgreso(reqSol);

        //        oResponse = cpe.autorizarCPEAutomotorAsync(oauth, reqSol).Result;
        //        //Escribir pdf
        //        //if (oResponse.respuesta.pdf != null)
        //        //{
        //        //    byte[] pdf = oResponse.respuesta.pdf;
        //        //    File.WriteAllBytes(@"C:\WSCPESAP\PDF\prueba.pdf", pdf);

        //        //}


        //        GuardarxmlRespuestaEmisionEgreso(oResponse);
        //        //Crear Campo Codigo Provincia Destino
        //        //if (oTransferencia.campo)              
        //        #endregion

        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }

        //    return oResponse;

        //}

        public Objetos.ConsultarCPEAutomotorResponse GetCartaPorte(Int64 CTG)
        {
            Objetos.ConsultarCPEAutomotorResponse oxml = new Objetos.ConsultarCPEAutomotorResponse();
            try
            {
                WSAfip.consultarCPEAutomotorRequest oReuquest = new WSAfip.consultarCPEAutomotorRequest();
                WSAfip.consultarCPEAutomotorResponse oResp = new WSAfip.consultarCPEAutomotorResponse();
                WSAfip.ConsultarAutomotorSolicitud oSolicitud = new ConsultarAutomotorSolicitud();
                string oError = "";
                WSAfip.dummyResponse oDummy = new dummyResponse();
                WSAfip.CpePortTypeClient oClient = new CpePortTypeClient();
                WSAfip.CartaPorte oCpe = new CartaPorte();
                WSAfip.Auth oAuth = new WSAfip.Auth();
                Objetos.SesionAFIP oSesion = new Objetos.SesionAFIP();
                oAuth = oSesion.CompletarAuth();
                oSolicitud.nroCTGSpecified = true;
                oSolicitud.nroCTG = CTG;

                oResp = oClient.consultarCPEAutomotorAsync(oAuth, oSolicitud).Result;
                GuardarRespuestaCartaPorte(oResp);

                XmlSerializer ser = new XmlSerializer(typeof(Objetos.ConsultarCPEAutomotorResponse));
                using (StreamReader reader = new StreamReader($"C:/WSCPESAP/Respuesta/CPE.xml"))
                {
                    oxml = (Objetos.ConsultarCPEAutomotorResponse)ser.Deserialize(reader);
                }

                if (oResp.respuesta.errores.Length > 0)
                {
                    //error
                    
                }
                else
                {
                    ByteArrayToFile($"//br01-srv-db02/B1_SHF/Adjuntos/CartasPorte/" + oResp.respuesta.cabecera.nroCTG + ".pdf", oResp.respuesta.pdf);
                }

            }
            catch (Exception)
            {

                throw;
            }
            return oxml;
        }

        public bool ByteArrayToFile(string fileName, byte[] byteArray)
        {
            try
            {
                using (var fs = new FileStream(fileName, FileMode.Create, FileAccess.Write))
                {
                    fs.Write(byteArray, 0, byteArray.Length);
                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception caught in process: {0}", ex);
                return false;
            }
        }

        //public WSAfip.desvioCPEAutomotorResponse InformarDesvio(Objetos.Transfer oTransferencia)
        //{

        //    WSAfip.Auth oAuth = new WSAfip.Auth();
        //    WSAfip.CartaPorte oCpe = new WSAfip.CartaPorte();
        //    WSAfip.TransporteAutomotorModificaSolicitud oTransporte = new WSAfip.TransporteAutomotorModificaSolicitud();
        //    WSAfip.DesvioDestinoAutomotorSolicitud oDestino = new WSAfip.DesvioDestinoAutomotorSolicitud();
        //    WSAfip.DesvioAutomotorSolicitud oSol = new WSAfip.DesvioAutomotorSolicitud();

        //    WSAfip.desvioCPEAutomotorRequest oReqSol = new WSAfip.desvioCPEAutomotorRequest();
        //    WSAfip.desvioCPEAutomotorResponse oResp = new WSAfip.desvioCPEAutomotorResponse();
        //    WSAfip.CpePortTypeClient cpe = new WSAfip.CpePortTypeClient();
        //    Objetos.SesionAFIP oSes = new Objetos.SesionAFIP();
        //    try
        //    {

        //        //Auth
        //        oAuth = oSes.CompletarAuth();
        //        //Fin Auth
        //        //Solicitud

        //        oTransporte.fechaHoraPartida = DateTime.Now;
        //        oTransporte.kmRecorrer = Convert.ToInt16(oTransferencia.U_KmRecorrer);
        //        //Inicio CartaPorte
        //        oCpe.nroOrden = oTransferencia.U_NroOrden;
        //        oCpe.sucursal = oTransferencia.U_Sucursal;
        //        oCpe.tipoCPE = 74;
        //        //Fin Transporte
        //        //Inicio Destino
        //        oDestino.planta = oTransferencia.U_PlaDest;
        //        oDestino.codProvincia = oTransferencia.U_ProvDestino;
        //        oDestino.codLocalidad = oTransferencia.U_LocDestino;
        //        oDestino.cuit = Convert.ToInt64(oTransferencia.U_CUITDestino);
        //        //Fin Destino
        //        //Inicio Solicitud
        //        oSol.destino = oDestino;
        //        oSol.cartaPorte = oCpe;
        //        oSol.transporte = oTransporte;

        //        oSol.cuitSolicitante = 30604956281;
        //        //Fin Solicitud
        //        //Solicitud

        //        GuardarxmlEnviadoEmisionDesvio(oSol);

        //        oResp = cpe.desvioCPEAutomotorAsync(oAuth, oSol).Result;

        //        GuardarxmlRespuestaEmisionDesvio(oResp);


        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }

        //    return oResp;
        //}

        //public WSAfip.anularCPEResponse AnularCPE(Objetos.Transfer oTransfer)
        //{
        //    WSAfip.anularCPEResponse anularCPEResponse = new WSAfip.anularCPEResponse();
        //    WSAfip.AnularCPESolicitud oSolicitud = new WSAfip.AnularCPESolicitud();
        //    WSAfip.anularCPERequest oRequest = new WSAfip.anularCPERequest();
        //    WSAfip.CartaPorte oCpe = new WSAfip.CartaPorte();
        //    Objetos.SesionAFIP oSesion = new Objetos.SesionAFIP();
        //    WSAfip.Auth oauth = new WSAfip.Auth();
        //    WSAfip.CpePortTypeClient cpe = new WSAfip.CpePortTypeClient();
        //    try
        //    {

        //        oCpe.nroOrden = oTransfer.U_NroOrden;
        //        oCpe.sucursal = oTransfer.U_Sucursal;
        //        oCpe.tipoCPE = 74;


        //        oauth = oSesion.CompletarAuth();

        //        oSolicitud.cartaPorte = oCpe;
        //        oSolicitud.anulacionMotivoSpecified = false;
        //        oSolicitud.anulacionMotivoSpecified = false;

        //        anularCPEResponse = cpe.anularCPEAsync(oauth, oSolicitud).Result;


        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }

        //    return anularCPEResponse;
        //}

        //public WSAfip.nuevoDestinoDestinatarioCPEAutomotorResponse InformarCambio(Objetos.Transfer oTransferencia)
        //{
        //    WSAfip.CartaPorte oCpe = new WSAfip.CartaPorte();
        //    WSAfip.nuevoDestinoDestinatarioCPEAutomotorResponse oResp = new WSAfip.nuevoDestinoDestinatarioCPEAutomotorResponse();
        //    WSAfip.nuevoDestinoDestinatarioCPEAutomotorRequest oReq = new WSAfip.nuevoDestinoDestinatarioCPEAutomotorRequest();
        //    try
        //    {
        //        WSAfip.Auth oAuth = new WSAfip.Auth();
        //        WSAfip.DestinatarioSolicitud oDestinatario = new WSAfip.DestinatarioSolicitud();
        //        WSAfip.NuevoDestinoDestinatarioAutomotorSolicitud oSol = new WSAfip.NuevoDestinoDestinatarioAutomotorSolicitud();
        //        WSAfip.DestinoSolicitud oDestino = new WSAfip.DestinoSolicitud();
        //        Objetos.SesionAFIP oSes = new Objetos.SesionAFIP();
        //        WSAfip.TransporteAutomotorModificaSolicitud oTransporte = new WSAfip.TransporteAutomotorModificaSolicitud();
        //        WSAfip.CpePortTypeClient cpe = new WSAfip.CpePortTypeClient();
        //        oAuth = oSes.CompletarAuth();

        //        //Inicio CartaPorte
        //        oCpe.sucursal = oTransferencia.U_Sucursal;
        //        oCpe.nroOrden = oTransferencia.U_NroOrden;
        //        oCpe.tipoCPE = 74;
        //        //Fin CartaPorte

        //        //Inicio Destino
        //        oDestino.esDestinoCampo = false;
        //        oDestino.planta = oTransferencia.U_Planta;
        //        oDestino.codProvincia = oTransferencia.U_ProvDestino;
        //        oDestino.codLocalidad = oTransferencia.U_LocDestino;
        //        oDestino.cuit = Convert.ToInt64(oTransferencia.U_CUITDestino);
        //        //Fin Destino
        //        //Inicio Destinatario
        //        oDestinatario.cuit = Convert.ToInt64(oTransferencia.U_CUITDestinatario);
        //        //Fin Destinatario
        //        //Inicio Transporte
        //        oTransporte.fechaHoraPartida = DateTime.Now;
        //        oTransporte.kmRecorrer = Convert.ToInt16(oTransferencia.U_KmRecorrer);

        //        oSol.destinatario = oDestinatario;
        //        oSol.cartaPorte = oCpe;
        //        oSol.destino = oDestino;
        //        oSol.transporte = oTransporte;

        //        GuardarxmlEnviadoEmisionCambio(oSol);

        //        oResp = cpe.nuevoDestinoDestinatarioCPEAutomotorAsync(oAuth, oSol).Result;

        //        GuardarxmlRespuestaEmisionCambio(oResp);


        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }

        //    return oResp;
        //}

        public void GuardarxmlEnviadoEmisionDesvio(WSAfip.DesvioAutomotorSolicitud EmisionCPE)
        {
            try
            {
                System.Xml.Serialization.XmlSerializer x = new System.Xml.Serialization.XmlSerializer(EmisionCPE.GetType());
                x.Serialize(Console.Out, EmisionCPE);

                using (StreamWriter writer = new StreamWriter($"C:/WSCPESAP/Envio-Desvio/CPE.xml"))
                {
                    x.Serialize(writer, EmisionCPE);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void GuardarRespuestaCartaPorte(WSAfip.consultarCPEAutomotorResponse EmisionCPE)
        {
            try
            {
                System.Xml.Serialization.XmlSerializer x = new System.Xml.Serialization.XmlSerializer(EmisionCPE.GetType());
                x.Serialize(Console.Out, EmisionCPE);

                using (StreamWriter writer = new StreamWriter($"C:/WSCPESAP/Respuesta/CPE.xml"))
                {
                    x.Serialize(writer, EmisionCPE);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public string  AddTransferencia(Objetos.Transfer oTrans, int DocEntry)
        {
            string SessionID = "";
            string error = "";
            Int64 NroOrden = 0;
            bool Exito = false;
            Objetos.Transfer oTransfer = new Transfer();
            Objetos.Root msgSL = new Root();
            //Controladores.Sesion oSesion = new Sesion();
            string ruta = @"C:\WSCPESAP\SessionSAP\Session.txt";
            //string ruta2 = Application.StartupPath + "Session.txt";
            try
            {
               
                    //StreamReader str = new StreamReader(@"C:\WSCPESAP\SessionSAP\Session.txt");
                
                oTransfer = ArmarEquivalencias(oTrans, DocEntry);
                oTransfer.CreationDate = DateTime.Now;
                oTransfer.DocDate = DateTime.Now;
                oTransfer.DueDate = DateTime.Now;
                oTransfer.TaxDate = DateTime.Now;
                using (StreamReader str = new StreamReader(ruta))
                {
                    SessionID = str.ReadLine();
                    //File.SetAttributes(ruta, FileAttributes.Hidden);
                }
                var json = JsonSerializer.Serialize(oTransfer);
                var data = new StringContent(json, Encoding.UTF8, "application/json");
                var baseAddress = new Uri("http://br01-srv-db02:50002/b1s/v1/StockTransfers");
               
                String Cookie = String.Format("B1SESSION={0}", SessionID);
                using (var handler = new HttpClientHandler { UseCookies = false })
                using (var client = new HttpClient(handler) { BaseAddress = baseAddress })
                {
                    var message = new HttpRequestMessage(HttpMethod.Post, baseAddress);
                    message.Content = data;
                    message.Headers.Add("Cookie", Cookie);
                    var result = client.SendAsync(message).Result;
                    if (result.StatusCode == HttpStatusCode.Unauthorized)
                    {
                        Comunes.ConexiónSL oCon = new Comunes.ConexiónSL();
                        SessionID = oCon.ConectarSL();
                    }
                    else
                    {
                        var content = result.Content.ReadAsStringAsync();
                        var info = content.Result;
                        if (result.StatusCode != HttpStatusCode.Created)
                        {
                            msgSL = JsonSerializer.Deserialize<Objetos.Root>(info);
                            error = msgSL.error.message.value;
                            //oDb.UpdateNovedad(NroOrden, error);
                        }
                        else
                        {
                            error = "Ok";
                            //Exito = true;
                        }
                        //result.EnsureSuccessStatusCode();
                        //oDb.UpdateNovedad(NroOrden, error);
                    }

                }
            }
            catch (Exception)
            {

                throw;
            }

            return error;
            //return Exito;
        }

        //Revisar si el token es invalido
        public Objetos.Transfer ArmarEquivalencias(Objetos.Transfer oTransferencia, int DocEntry)
        {
            var baseAddress2 = new Uri("http://br01-srv-db02:50002/b1s/v1/sml.svc/CV_TRANSFERENCIACPE?$select=*&$filter=DocEntry" + " " +  "eq" + " " + DocEntry);
            string SessionID = "";
            string error = "";
            Int64 NroOrden = 0;
            bool Exito = false;
            //Controladores.Sesion oSesion = new Sesion();
           
            Objetos.Root msgSL = new Objetos.Root();
            Objetos.TRANSFERENCIACPE.Root oEquivalencia = new TRANSFERENCIACPE.Root();
            Objetos.StockTransferLines oLines = null;
            Objetos.StockTransferLinesBinAllocations oUbicacion = null;
            Objetos.StockTransferLinesBinAllocations oUbicacionB = null;
            try
            {

                oLines = new Objetos.StockTransferLines();
                oUbicacion = new Objetos.StockTransferLinesBinAllocations();
                oUbicacionB = new Objetos.StockTransferLinesBinAllocations();
                List<Objetos.StockTransferLinesBinAllocations> oListadoUbicacion = new List<Objetos.StockTransferLinesBinAllocations>();
                List<Objetos.StockTransferLines> oListadoLinea = new List<Objetos.StockTransferLines>();
                //string uta = Application.StartupPath + "Session.txt";
                string ruta = @"C:\WSCPESAP\SessionSAP\Session.txt";
                StreamReader str = new StreamReader(ruta);
               
                SessionID = str.ReadLine();
                str.Close();
                str.Dispose();


                String Cookie = String.Format("B1SESSION={0}", SessionID);
                using (var handler = new HttpClientHandler { UseCookies = false })
                using (var client = new HttpClient(handler) { BaseAddress = baseAddress2 })
                {
                    var message = new HttpRequestMessage(HttpMethod.Get, baseAddress2);
                    //message.Content = data;
                    message.Headers.Add("Cookie", Cookie);
                    var result = client.SendAsync(message).Result;
                    if (result.StatusCode == HttpStatusCode.Unauthorized)
                    {
                        Comunes.ConexiónSL oCon = new Comunes.ConexiónSL();
                        SessionID = oCon.ConectarSL();
                        result = HeaderEnvio(SessionID,DocEntry);
                        //Pruebo reenvio con nuevo Sesion
                        oTransferencia =  ReenvioSL(result, oTransferencia);
                        //Hago otro envio
                        
                    }
                    else
                    {
                        result = HeaderEnvio(SessionID,DocEntry);
                       oTransferencia = ReenvioSL(result, oTransferencia);
                      
                    }

                }
            }
            catch (Exception)
            {

                throw;
            }

            return oTransferencia;
        }

        private Objetos.Transfer ReenvioSL(HttpResponseMessage result,Objetos.Transfer oTransferencia)
        {
            Objetos.TRANSFERENCIACPE.Root oEquivalencia = new TRANSFERENCIACPE.Root();
            Objetos.StockTransferLines oLines = null;
            Objetos.StockTransferLinesBinAllocations oUbicacion = null;
            Objetos.StockTransferLinesBinAllocations oUbicacionB = null;
            Objetos.StockTransferLinesBinAllocations oUbicacionC = null;
            try
            {
               
                oUbicacion = new Objetos.StockTransferLinesBinAllocations();
                oUbicacionB = new Objetos.StockTransferLinesBinAllocations();
                oUbicacionC = new Objetos.StockTransferLinesBinAllocations();
                List<Objetos.StockTransferLinesBinAllocations> oListadoUbicacion = null;
                List<Objetos.StockTransferLines> oListadoLinea = new List<Objetos.StockTransferLines>();
                if (result.StatusCode == HttpStatusCode.OK)
                {
                    var content = result.Content.ReadAsStringAsync();
                    var info = content.Result;
                    oEquivalencia = JsonSerializer.Deserialize<Objetos.TRANSFERENCIACPE.Root>(info);
                    
                    foreach (var item in oEquivalencia.value)
                    {
                        if (String.IsNullOrEmpty(item.Ubicacion2) && String.IsNullOrEmpty(item.Ubicacion3))
                        {
                            //Defino Linea
                            oLines = new Objetos.StockTransferLines();
                            oListadoUbicacion = new List<Objetos.StockTransferLinesBinAllocations>();
                            oUbicacion.BinActionType = "batFromWarehouse";
                            oUbicacion.Quantity = oTransferencia.U_PesoNeto;
                            oUbicacion.BinAbsEntry = item.AbsEntry;
                            oUbicacion.AllowNegativeQuantity = "tYES";
                            oListadoUbicacion.Add(oUbicacion);
                            oLines.ItemCode = item.GranoSAP;
                            oLines.FromWarehouseCode = item.AlmacenOrigen;
                            oLines.WarehouseCode = item.AlmacenTercero;
                            oLines.Quantity = oTransferencia.U_PesoNeto;
                            oLines.DistributionRule = item.Norma1;
                            oLines.DistributionRule2 = item.Norma2;
                            oLines.DistributionRule3 = item.Norma3;
                            oLines.DistributionRule4 = item.Norma4;
                            oLines.DistributionRule5 = item.Norma5;
                            oLines.StockTransferLinesBinAllocations = oListadoUbicacion;
                            oListadoLinea.Add(oLines);
                        }
                        else
                        {
                            //Defino Primer Linea
                            oLines = new Objetos.StockTransferLines();
                            oListadoUbicacion = new List<Objetos.StockTransferLinesBinAllocations>();
                            //Ubicacion 1
                            oUbicacion.BinActionType = "batFromWarehouse";
                            oUbicacion.Quantity = item.Cantidad1;
                            oUbicacion.BinAbsEntry = item.AbsEntry;
                            oUbicacion.AllowNegativeQuantity = "tYES";
                            oListadoUbicacion.Add(oUbicacion);
                            oLines.ItemCode = item.GranoSAP;
                            oLines.FromWarehouseCode = item.AlmacenOrigen;
                            oLines.WarehouseCode = item.AlmacenTercero;
                            oLines.Quantity = item.Cantidad1;
                            oLines.DistributionRule = item.Norma1;
                            oLines.DistributionRule2 = item.Norma2;
                            oLines.DistributionRule3 = item.Norma3;
                            oLines.DistributionRule4 = item.Norma4;
                            oLines.DistributionRule5 = item.Norma5;
                            oLines.StockTransferLinesBinAllocations = oListadoUbicacion;
                            oListadoLinea.Add(oLines);
                            //Ubicacion2
                            //Defino segunda Linea
                            oLines = new Objetos.StockTransferLines();
                            oListadoUbicacion = new List<Objetos.StockTransferLinesBinAllocations>();
                            oUbicacionB.BinActionType = "batFromWarehouse";
                            oUbicacionB.Quantity = item.Cantidad2;
                            oUbicacionB.BinAbsEntry = item.AbsEntry2;
                            oUbicacionB.AllowNegativeQuantity = "tYES";
                            oListadoUbicacion.Add(oUbicacionB);
                            oLines.ItemCode = item.GranoSAP;
                            oLines.FromWarehouseCode = item.AlmacenOrigen2;
                            oLines.WarehouseCode = item.AlmacenTercero;
                            oLines.Quantity = item.Cantidad2;
                            oLines.DistributionRule = item.Norma1;
                            oLines.DistributionRule2 = item.Norma2;
                            oLines.DistributionRule3 = item.Norma3;
                            oLines.DistributionRule4 = item.Norma4;
                            oLines.DistributionRule5 = item.Norma5;
                            oLines.StockTransferLinesBinAllocations = oListadoUbicacion;
                            oListadoLinea.Add(oLines);
                            //Ubicacion 3 si no esta vacia
                            if (!String.IsNullOrEmpty(item.Ubicacion3))
                            {

                                //Defino Tercer Linea
                                oLines = new Objetos.StockTransferLines();
                                oListadoUbicacion = new List<Objetos.StockTransferLinesBinAllocations>();
                                //Ubicacion 1
                                oUbicacionC.BinActionType = "batFromWarehouse";
                                oUbicacionC.Quantity = item.Cantidad3;
                                oUbicacionC.BinAbsEntry = item.AbsEntry3;
                                oUbicacionC.AllowNegativeQuantity = "tYES";
                                oListadoUbicacion.Add(oUbicacionC);
                                oLines.ItemCode = item.GranoSAP;
                                oLines.FromWarehouseCode = item.AlmacenOrigen3;
                                oLines.WarehouseCode = item.AlmacenTercero;
                                oLines.Quantity = item.Cantidad3;
                                oLines.DistributionRule = item.Norma1;
                                oLines.DistributionRule2 = item.Norma2;
                                oLines.DistributionRule3 = item.Norma3;
                                oLines.DistributionRule4 = item.Norma4;
                                oLines.DistributionRule5 = item.Norma5;
                              
                                oLines.StockTransferLinesBinAllocations = oListadoUbicacion;
                                oListadoLinea.Add(oLines);
                               
                            }
                        }
                            //oLines.ItemCode = item.GranoSAP;
                            //oLines.FromWarehouseCode = item.AlmacenOrigen;
                            //oLines.WarehouseCode = item.AlmacenTercero;
                            //oLines.Quantity = oTransferencia.U_PesoNeto;
                            //oLines.DistributionRule = item.Norma1;
                            //oLines.DistributionRule2 = item.Norma2;
                            //oLines.DistributionRule3 = item.Norma3;
                            //oLines.DistributionRule4 = item.Norma4;
                            //oLines.DistributionRule5 = item.Norma5;
                            //oLines.StockTransferLinesBinAllocations = oListadoUbicacion;
                            //oListadoLinea.Add(oLines);
                            oTransferencia.StockTransferLines = oListadoLinea;
                            oTransferencia.CardCode = item.CardCode;
                            oTransferencia.ShipToCode = item.Address;
                            oTransferencia.U_NumFolio = item.Folio.ToString();
                            oTransferencia.U_CTG = item.CTG;
                            oTransferencia.DocObjectCode = "67";
                        
                        //oTransferencia.U_CTG = item.CTG;

                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
            return oTransferencia;

        }

        private HttpResponseMessage HeaderEnvio(string SessionID,int DocEntry)
        {
            HttpResponseMessage Resultado = null;
            try
            {
                
                String Cookie = String.Format("B1SESSION={0}", SessionID);
                var baseAddress2 = new Uri("http://br01-srv-db02:50002/b1s/v1/sml.svc/CV_TRANSFERENCIACPE?$select=*&$filter=DocEntry" + " " + "eq" + " " + DocEntry);
                using (var handler = new HttpClientHandler { UseCookies = false })
                using (var client = new HttpClient(handler) { BaseAddress = baseAddress2 })
                {
                    var message = new HttpRequestMessage(HttpMethod.Get, baseAddress2);
                    //message.Content = data;
                    message.Headers.Add("Cookie", Cookie);
                    var result = client.SendAsync(message).Result;
                    Resultado = result;
                }
            }
            catch (Exception)
            {

                throw;
            }


            //return result
            return Resultado;
        }

        

    }
}

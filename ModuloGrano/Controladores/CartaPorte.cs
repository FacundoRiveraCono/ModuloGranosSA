using ModuloGrano.Objetos;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.DirectoryServices.ActiveDirectory;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Reflection;
using System.Security.Policy;
using System.ServiceModel.Channels;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using WSAfip;

namespace ModuloGrano.Controladores
{
    class CartaPorte
    {


        public string ArmarEquivalencias(string Codigo,string Metodo)
        {
            var baseAddress2 = new Uri("http://br01-srv-db02:50002/b1s/v1/sml.svc/CV_CARTAPORTEAFIP?$select=*&$filter=Codigo" + " " + "eq" + " " + String.Format("{0}", Codigo));
            string SessionID = "";
            string error = "";
            try
            {

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
                    message.Headers.Add("Cookie", Cookie);
                    var result = client.SendAsync(message).Result;
                    if (result.StatusCode == HttpStatusCode.Unauthorized)
                    {
                        Comunes.ConexiónSL oCon = new Comunes.ConexiónSL();
                        SessionID = oCon.ConectarSL();
                        result = HeaderEnvio(SessionID, Codigo);
                        //Pruebo reenvio con nuevo Sesion
                        error = ReenvioSL(result, Codigo,Metodo);
                        //Hago otro envio
                    }
                    else
                    {
                        error = ReenvioSL(result, Codigo,Metodo);

                    }

                }
            }
            catch (Exception)
            {

                throw;
            }

            return error;
        }
        private string ReenvioSL(HttpResponseMessage result, string Codigo,string Metodo)
        {
            Objetos.CartaPorte.Root oEquivalencia = new Objetos.CartaPorte.Root();

            string error = "";
            try
            {

                if (result.StatusCode == HttpStatusCode.OK)
                {
                    var content = result.Content.ReadAsStringAsync();
                    var info = content.Result;
                    oEquivalencia = JsonSerializer.Deserialize<Objetos.CartaPorte.Root>(info);
                    if (oEquivalencia.value.Count > 0)
                    {

                        foreach (var item in oEquivalencia.value)
                        {
                            if (item.id__ == 0 || item == null)
                            {
                                error = "Error en carta de porte SAP";
                                break;


                            }
                            else
                            {
                                if (Metodo == "Add")
                                {
                                    PostCartaPorte(item, Codigo);
                                }
                                if (Metodo == "Update")
                                {
                                    PostEditCartaPorte(item, Codigo);
                                }
                            }
                            break;

                        }
                    }
                    else
                    {
                        error = "Valor Incompleto en petición SAP";
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
            return error;

        }

        private HttpResponseMessage HeaderEnvio(string SessionID, string CTG)
        {
            HttpResponseMessage Resultado = null;
            try
            {

                String Cookie = String.Format("B1SESSION={0}", SessionID);
                var baseAddress2 = new Uri("http://br01-srv-db02:50002/b1s/v1/sml.svc/CV_CARTAPORTEAFIP?$select=*&$filter=Codigo" + " " + "eq" + " " + String.Format("{0}", CTG));
                using (var handler = new HttpClientHandler { UseCookies = false })
                using (var client = new HttpClient(handler) { BaseAddress = baseAddress2 })
                {
                    var message = new HttpRequestMessage(HttpMethod.Get, baseAddress2);
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




        public WSAfip.autorizarCPEAutomotorResponse PostCartaPorte(Objetos.CartaPorte.Value oCartaPorte, string Codigo)
        {
            string fechatest = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss");
            DateTime Fecha = Convert.ToDateTime(fechatest);
            bool Productor = false;
            WSAfip.AutorizarAutomotorSolicitud reqSol = new WSAfip.AutorizarAutomotorSolicitud();
            WSAfip.autorizarCPEAutomotorResponse oResponse = new WSAfip.autorizarCPEAutomotorResponse();

            Objetos.SesionAFIP oSesion = new Objetos.SesionAFIP();
            try
            {
                #region Definicion de Objetos
                WSAfip.Auth oauth = new WSAfip.Auth();
                //Defino el resto de objetos correspondiente a los tag del xml 
                WSAfip.DetalleAutomotorRespuesta responseCpe = new WSAfip.DetalleAutomotorRespuesta();
                //Cabecera
                WSAfip.CabeceraAutomotorSolicitud cabecera = new WSAfip.CabeceraAutomotorSolicitud();
                WSAfip.OrigenAutomotorSolicitud CPEOrigen = new WSAfip.OrigenAutomotorSolicitud();              
                WSAfip.OrigenProductorAutomotorSolicitud CPEOrigenPr = new WSAfip.OrigenProductorAutomotorSolicitud();
                //DatosCarga
                WSAfip.DatosCargaAutomotorSolicitud CPECarga = new WSAfip.DatosCargaAutomotorSolicitud();
                //Datos de transporte
                WSAfip.TransporteAutomotorSolicitud Transporte = new WSAfip.TransporteAutomotorSolicitud();
                //Destino
                WSAfip.DestinoSolicitud CPEDestino = new WSAfip.DestinoSolicitud();
                //Destino Destinatario
                WSAfip.DestinatarioSolicitud CPEDestinatario = new WSAfip.DestinatarioSolicitud();
                //Intervinientes
                WSAfip.IntervinientesSolicitud CPEInter = new WSAfip.IntervinientesSolicitud();
                WSAfip.CpePortTypeClient cpe = new WSAfip.CpePortTypeClient();
             
                #endregion

                #region Completo Objeto
                //auatorización
                oauth = oSesion.CompletarAuth();
                //Fin Autorización
                cabecera.tipoCP = (short)oCartaPorte.TipoCPE;
                cabecera.cuitSolicitante = Convert.ToInt64(oCartaPorte.CUITSolicitante);
                cabecera.sucursal = 5;
                
                //Metodo para obtener el ultimo Nro de Orden
                cabecera.nroOrden = UltimoNroOrden();
                reqSol.cabecera = cabecera;
                CPEOrigenPr.codLocalidad = Convert.ToInt16(oCartaPorte.LocalidadOrigen);
                CPEOrigenPr.codProvincia = Convert.ToInt16(oCartaPorte.ProvinciaOrigen);
                CPEOrigen.productor = CPEOrigenPr;
                
                reqSol.origen = CPEOrigen;

                if (!String.IsNullOrEmpty(oCartaPorte.CUITRemitenteComercialVentaPrimaria))
                {

                    CPEInter.cuitRemitenteComercialVentaPrimariaSpecified = true;
                    CPEInter.cuitRemitenteComercialVentaPrimaria = Convert.ToInt64(oCartaPorte.CUITRemitenteComercialVentaPrimaria);
                }
                else { CPEInter.cuitRemitenteComercialVentaPrimariaSpecified = false; }
                if (!String.IsNullOrEmpty(oCartaPorte.CUITRemitenteComercialVentaSecundaria))
                {
                    CPEInter.cuitRemitenteComercialVentaSecundariaSpecified = true;
                    CPEInter.cuitRemitenteComercialVentaSecundaria = Convert.ToInt64(oCartaPorte.CUITRemitenteComercialVentaSecundaria);
                }
                else { CPEInter.cuitRemitenteComercialVentaSecundariaSpecified = false; }
                if (!String.IsNullOrEmpty(oCartaPorte.CUITRemitenteComercialVentaSecundaria2))
                {
                    CPEInter.cuitRemitenteComercialVentaSecundaria2Specified = true;
                    CPEInter.cuitRemitenteComercialVentaSecundaria2 = Convert.ToInt64(oCartaPorte.CUITRemitenteComercialVentaSecundaria2);
                }
                else { CPEInter.cuitRemitenteComercialVentaSecundaria2Specified = false; }
                if (!String.IsNullOrEmpty(oCartaPorte.CUITMercadoInterno))
                {
                    CPEInter.cuitMercadoATerminoSpecified = true;
                    CPEInter.cuitMercadoATermino = Convert.ToInt64(oCartaPorte.CUITMercadoInterno);
                }
                else { CPEInter.cuitMercadoATerminoSpecified = false; }

                if (!String.IsNullOrEmpty(oCartaPorte.CUITCorredorVentaPrimaria))
                {
                    CPEInter.cuitCorredorVentaPrimariaSpecified = true;
                    CPEInter.cuitCorredorVentaPrimaria = Convert.ToInt64(oCartaPorte.CUITCorredorVentaPrimaria);
                }
                else { CPEInter.cuitCorredorVentaPrimariaSpecified = false; }
                if (!String.IsNullOrEmpty(oCartaPorte.CUITCorredorVentaSecundaria))
                {
                    CPEInter.cuitCorredorVentaSecundariaSpecified = true;
                    CPEInter.cuitCorredorVentaSecundaria = Convert.ToInt64(oCartaPorte.CUITCorredorVentaSecundaria);
                }
                else
                {
                    CPEInter.cuitCorredorVentaSecundariaSpecified = false;
                }

                //Falta agregar remitente comercial venta secundaria
                if (!String.IsNullOrEmpty(oCartaPorte.CUITEntregador))
                {
                    CPEInter.cuitRepresentanteEntregadorSpecified = true;
                    CPEInter.cuitRepresentanteEntregador = Convert.ToInt64(oCartaPorte.CUITEntregador);
                }
                else { CPEInter.cuitRepresentanteEntregadorSpecified = false; }
                reqSol.intervinientes = CPEInter;
                reqSol.esSolicitanteCampo = true;
                reqSol.correspondeRetiroProductor = false;

                //Datos de carga
                //Codigo de Grano
                CPECarga.codGrano = (short)oCartaPorte.CodigoGrano;
                CPECarga.cosecha = Convert.ToInt16(oCartaPorte.Cosecha);
                CPECarga.pesoBruto = Convert.ToInt32(oCartaPorte.PesoBruto);
                CPECarga.pesoTara = Convert.ToInt32(oCartaPorte.PesoTara);
                

                Transporte.fechaHoraPartida = Fecha.AddMinutes(15);
                if (oCartaPorte.ProvinciaDestino == "12")
                {
                    Transporte.codigoTurno = oCartaPorte.Turno;
                }
                
                Transporte.kmRecorrer = Convert.ToInt16(oCartaPorte.Km);
                if (oCartaPorte.Tarifa > 0)
                {
                    Transporte.tarifaSpecified = true;
                    Transporte.tarifa = Convert.ToDecimal(oCartaPorte.Tarifa);
                    
                }
                else
                {
                    Transporte.tarifaSpecified = false;
                }

                if (!String.IsNullOrEmpty(oCartaPorte.CUITPagadorFlete))
                {
                    Transporte.cuitPagadorFleteSpecified = true;
                    Transporte.cuitPagadorFlete = Convert.ToInt64(oCartaPorte.CUITPagadorFlete);
                }
                else
                {
                    Transporte.cuitPagadorFleteSpecified = false;
                }


                Transporte.cuitTransportista = Convert.ToInt64(oCartaPorte.CUITTransportista);
                Transporte.cuitChofer = Convert.ToInt64(oCartaPorte.CUITChofer);
                Transporte.dominio = new string[] { oCartaPorte.Dominio1, oCartaPorte.Dominio2 };
                Transporte.mercaderiaFumigada = true;
                reqSol.transporte = Transporte;
                reqSol.datosCarga = CPECarga;
                //Fin datos de Carga
                //Datos Destino               
                CPEDestino.esDestinoCampo = false;
                CPEDestino.codLocalidad = Convert.ToInt16(oCartaPorte.LocalidadDestino);//Crear Campo en SAP
                CPEDestino.codProvincia = Convert.ToInt16(oCartaPorte.ProvinciaDestino);
                CPEDestino.plantaSpecified = true;
                CPEDestino.planta = Convert.ToInt32(oCartaPorte.Planta);
                CPEDestino.cuit = Convert.ToInt64(oCartaPorte.CUITDestino);
                reqSol.destino = CPEDestino;
                //Fin Datos Destino
                //Inicio datos Destinatario
                CPEDestinatario.cuit = Convert.ToInt64(oCartaPorte.CUITDesinatario);
                reqSol.destinatario = CPEDestinatario;
                reqSol.observaciones = oCartaPorte.Observaciones;

                //Fin Datos Destinatario
                GuardarxmlEnviadoEmisionEgreso(reqSol);

                oResponse = cpe.autorizarCPEAutomotorAsync(oauth, reqSol).Result;

                GuardarxmlRespuestaEmisionEgreso(oResponse);

                if (oResponse.respuesta.errores.Length == 0)
                {
                    ByteArrayToFile($"//br01-srv-db02/B1_SHF/Adjuntos/CartasPorte/" + oResponse.respuesta.cabecera.nroCTG + ".pdf", oResponse.respuesta.pdf);

                }
                UpgradeCPESAP(oResponse, Codigo);           
                #endregion

            }
            catch (Exception)
            {

                throw;
            }

            return oResponse;

        }



        public WSAfip.editarCPEAutomotorResponse PostEditCartaPorte(Objetos.CartaPorte.Value oCartaPorte, string Codigo)
        {
            WSAfip.editarCPEConfirmadaAutomotorRequest reqsol = new editarCPEConfirmadaAutomotorRequest();
            WSAfip.EditarActivaAutomotorSolicitud sol = new EditarActivaAutomotorSolicitud();
            WSAfip.editarCPEAutomotorRequest oreq = new editarCPEAutomotorRequest();
            WSAfip.DestinoSolicitud CPEDestino = new WSAfip.DestinoSolicitud();
            WSAfip.editarCPEAutomotorResponse oResponse = new editarCPEAutomotorResponse();
            //WSAfip.AutorizarAutomotorSolicitud reqSol = new WSAfip.AutorizarAutomotorSolicitud();
            Objetos.SesionAFIP oSesion = new Objetos.SesionAFIP();
            try
            {
                #region Definicion de Objetos
                WSAfip.Auth oauth = new WSAfip.Auth();
                WSAfip.CpePortTypeClient cpe = new WSAfip.CpePortTypeClient();

                #endregion

                #region Completo Objeto
                //auatorización
                oauth = oSesion.CompletarAuth();
                //Fin Autorización

                sol.nroCTG = 10110919587;  //Convert.ToInt64(oCartaPorte.nroCTG);
                sol.pesoBruto = oCartaPorte.PesoBruto;
                sol.cosechaSpecified = true;
                sol.cuitTransportistaSpecified = true;
                sol.cuitTransportista = Convert.ToInt64(oCartaPorte.CUITTransportista);
                sol.cosecha = Convert.ToInt16(oCartaPorte.Cosecha);
                sol.codGrano = Convert.ToInt16(oCartaPorte.CodigoGrano);
                sol.dominio = new string[] { oCartaPorte.Dominio1, oCartaPorte.Dominio2 };
                sol.cuitDestinatarioSpecified = true;
                sol.cuitDestinatario = Convert.ToInt64(oCartaPorte.CUITDesinatario);
                sol.cuitChoferSpecified = true;
                sol.cuitChofer = Convert.ToInt64(oCartaPorte.CUITChofer);
                //Intervinientes
                if (!String.IsNullOrEmpty(oCartaPorte.CUITRemitenteComercialVentaPrimaria))
                {

                    sol.cuitRemitenteComercialVentaPrimariaSpecified = true;
                    sol.cuitRemitenteComercialVentaPrimaria = Convert.ToInt64(oCartaPorte.CUITRemitenteComercialVentaPrimaria);
                }
                else { sol.cuitRemitenteComercialVentaPrimariaSpecified = false; }
                if (!String.IsNullOrEmpty(oCartaPorte.CUITRemitenteComercialVentaSecundaria))
                {
                    sol.cuitRemitenteComercialVentaSecundariaSpecified = true;
                    sol.cuitRemitenteComercialVentaSecundaria = Convert.ToInt64(oCartaPorte.CUITRemitenteComercialVentaSecundaria);
                }
                else { sol.cuitRemitenteComercialVentaSecundariaSpecified = false; }
                if (!String.IsNullOrEmpty(oCartaPorte.CUITRemitenteComercialVentaSecundaria2))
                {
                    sol.cuitRemitenteComercialVentaSecundaria2Specified = true;
                    sol.cuitRemitenteComercialVentaSecundaria2 = Convert.ToInt64(oCartaPorte.CUITRemitenteComercialVentaSecundaria2);
                }
                else { sol.cuitRemitenteComercialVentaSecundaria2Specified = false; }
               
              

                if (!String.IsNullOrEmpty(oCartaPorte.CUITCorredorVentaPrimaria))
                {
                    sol.cuitCorredorVentaPrimariaSpecified = true;
                    sol.cuitCorredorVentaPrimaria = Convert.ToInt64(oCartaPorte.CUITCorredorVentaPrimaria);
                }
                else { sol.cuitCorredorVentaPrimariaSpecified = false; }
                if (!String.IsNullOrEmpty(oCartaPorte.CUITCorredorVentaSecundaria))
                {
                    sol.cuitCorredorVentaSecundariaSpecified = true;
                    sol.cuitCorredorVentaSecundaria = Convert.ToInt64(oCartaPorte.CUITCorredorVentaSecundaria);
                }
                else
                {
                    sol.cuitCorredorVentaSecundariaSpecified = false;
                }
                
               

                //Fin Intervinientes
                CPEDestino.esDestinoCampo = false;
                CPEDestino.planta =Convert.ToInt16(oCartaPorte.Planta);
                CPEDestino.cuit = Convert.ToInt64(oCartaPorte.CUITDestino);
                CPEDestino.plantaSpecified = true;
                CPEDestino.codLocalidad = Convert.ToInt16(oCartaPorte.LocalidadDestino);
                CPEDestino.codProvincia = Convert.ToInt16(oCartaPorte.ProvinciaDestino);
                sol.destino = CPEDestino;
                oreq.auth = oauth;
                

                oreq.solicitud = sol; 
                oResponse = cpe.editarCPEAutomotorAsync(oauth, oreq.solicitud).Result;
                if (oResponse.respuesta.errores.Length == 0)
                {
                    ByteArrayToFile($"//br01-srv-db02/B1_SHF/Adjuntos/CartasPorte/" + oResponse.respuesta.cabecera.nroCTG + ".pdf", oResponse.respuesta.pdf);

                }
                UpgradeEditCPESAP(oResponse, Codigo);
                #endregion

            }
            catch (Exception)
            {

                throw;
            }

            return oResponse;

        }
        public void GuardarxmlRespuestaEmisionEgreso(WSAfip.autorizarCPEAutomotorResponse EmisionCPE)
        {
            try
            {
                System.Xml.Serialization.XmlSerializer x = new System.Xml.Serialization.XmlSerializer(EmisionCPE.GetType());
                x.Serialize(Console.Out, EmisionCPE);

                using (StreamWriter writer = new StreamWriter($"C:/WSCPESAP/Envio/CPE.xml"))
                {
                    x.Serialize(writer, EmisionCPE);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void GuardarxmlEnviadoEmisionEgreso(WSAfip.AutorizarAutomotorSolicitud EmisionCPE)
        {
            try
            {
                System.Xml.Serialization.XmlSerializer x = new System.Xml.Serialization.XmlSerializer(EmisionCPE.GetType());
                x.Serialize(Console.Out, EmisionCPE);

                using (StreamWriter writer = new StreamWriter($"C:/WSCPESAP/Envio/CPE.xml"))
                {
                    x.Serialize(writer, EmisionCPE);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public int UltimoNroOrden()
        {
            long NroOrden = 0;
            try
            {
                Objetos.SesionAFIP oSesion = new Objetos.SesionAFIP();
                WSAfip.consultarUltNroOrdenRequest oRequest = new WSAfip.consultarUltNroOrdenRequest();
                WSAfip.ConsultarUltNroOrdenSolicitud oSolicitud = new WSAfip.ConsultarUltNroOrdenSolicitud();
                WSAfip.consultarUltNroOrdenResponse oResponse = new WSAfip.consultarUltNroOrdenResponse();
                WSAfip.ConsultarUltNroOrdenRespuesta oRespuesta = new WSAfip.ConsultarUltNroOrdenRespuesta();
                WSAfip.CpePortTypeClient oCpe = new WSAfip.CpePortTypeClient();
                WSAfip.Auth oAuth = new WSAfip.Auth();


                oSolicitud.tipoCPE = 74;
                oSolicitud.sucursal = 5;
                oRequest.solicitud = oSolicitud;
                oRequest.auth = oSesion.CompletarAuth();
                oAuth = oRequest.auth;

                oResponse = oCpe.consultarUltNroOrdenAsync(oAuth, oSolicitud).Result;
                oRespuesta.nroOrden = oResponse.respuesta.nroOrden;
                //Se le suma uno al ultimo para tener el correcto
                NroOrden = oRespuesta.nroOrden + 1;


            }
            catch (Exception)
            {

                throw;
            }
            return Convert.ToInt16(NroOrden);

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

        private void UpgradeCPESAP(WSAfip.autorizarCPEAutomotorResponse respuesta, string Codigo)
        {
            var baseAddress2 = new Uri("http://br01-srv-db02:50002/b1s/v1/CARTAPORTE" + " " + "eq" + " " + String.Format("({0})", Codigo));
            string SessionID = "";
            string error = "";
            WSAfip.CodigoDescripcion[] codigoDescripcion = respuesta.respuesta.errores;

            Objetos.CartaPorte.UpdateSAP oCpe = new Objetos.CartaPorte.UpdateSAP();
            try
            {

                string ruta = @"C:\WSCPESAP\SessionSAP\Session.txt";
                StreamReader str = new StreamReader(ruta);

                SessionID = str.ReadLine();
                str.Close();
                str.Dispose();

                if (codigoDescripcion.Length > 0)
                {
                    oCpe.U_MensajeAFIP = codigoDescripcion[0].descripcion;
                    oCpe.U_NroOrden = 0;
                    oCpe.U_Adjunto = "";
                    oCpe.U_CTG = "";
                }
                else
                {
                    oCpe.U_MensajeAFIP = "Emitida";
                    oCpe.U_NroOrden = (int)respuesta.respuesta.cabecera.nroOrden;
                    oCpe.U_CTG = respuesta.respuesta.cabecera.nroCTG.ToString();
                    oCpe.U_Adjunto = @"\\br01-srv-db02\B1_SHF\Adjuntos\CartasPorte\" + respuesta.respuesta.cabecera.nroCTG + ".pdf";
                }


                var json = JsonSerializer.Serialize(oCpe);
                var data = new StringContent(json, Encoding.UTF8);
                var baseAddress = new Uri("http://br01-srv-db02:50002/b1s/v1/CARTAPORTE" + string.Format("({0})", Convert.ToInt32(Codigo)));
                string uri = baseAddress.ToString();
                String Cookie = String.Format("B1SESSION={0}", SessionID);
                using (var handler = new HttpClientHandler { UseCookies = false })
                using (var client = new HttpClient(handler) { BaseAddress = baseAddress })
                {
                    var message = new HttpRequestMessage(HttpMethod.Patch, baseAddress);
                    message.Content = new StringContent(json, Encoding.UTF8);
                    message.Content.Headers.Add("Cookie", Cookie);
                    message.Headers.Add("Cookie", Cookie);
                    message.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json-patch+json");

                    var result = client.SendAsync(message).Result;
                    if (result.StatusCode == HttpStatusCode.Unauthorized)
                    {
                        Comunes.ConexiónSL oCon = new Comunes.ConexiónSL();
                        SessionID = oCon.ConectarSL();
                        UpdateReenvio(SessionID, Codigo, data);
                    }
                    else if (result.StatusCode == HttpStatusCode.NoContent && respuesta.respuesta.errores.Length == 0)
                    {
                        var p = new Process();
                        p.StartInfo = new ProcessStartInfo(@"\\br01-srv-db02\B1_SHF\Adjuntos\CartasPorte\" + respuesta.respuesta.cabecera.nroCTG + ".pdf")
                        {
                            UseShellExecute = true
                        };
                        p.Start();
                    }

                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        private void UpgradeEditCPESAP(WSAfip.editarCPEAutomotorResponse respuesta, string Codigo)
        {
            var baseAddress2 = new Uri("http://br01-srv-db02:50002/b1s/v1/CARTAPORTE" + " " + "eq" + " " + String.Format("({0})", Codigo));
            string SessionID = "";
            string error = "";
            WSAfip.CodigoDescripcion[] codigoDescripcion = respuesta.respuesta.errores;

            Objetos.CartaPorte.UpdateSAP oCpe = new Objetos.CartaPorte.UpdateSAP();
            try
            {

                string ruta = @"C:\WSCPESAP\SessionSAP\Session.txt";
                StreamReader str = new StreamReader(ruta);

                SessionID = str.ReadLine();
                str.Close();
                str.Dispose();

                if (codigoDescripcion.Length > 0)
                {
                    oCpe.U_MensajeAFIP = codigoDescripcion[0].descripcion;
                    oCpe.U_NroOrden = 0;
                    oCpe.U_Adjunto = "";
                    oCpe.U_CTG = "";
                }
                else
                {
                    oCpe.U_MensajeAFIP = "Emitida";
                    oCpe.U_NroOrden = (int)respuesta.respuesta.cabecera.nroOrden;
                    oCpe.U_CTG = respuesta.respuesta.cabecera.nroCTG.ToString();
                    oCpe.U_Adjunto = @"\\br01-srv-db02\B1_SHF\Adjuntos\CartasPorte\" + respuesta.respuesta.cabecera.nroCTG + ".pdf";
                }


                var json = JsonSerializer.Serialize(oCpe);
                var data = new StringContent(json, Encoding.UTF8);
                var baseAddress = new Uri("http://br01-srv-db02:50002/b1s/v1/CARTAPORTE" + string.Format("({0})", Convert.ToInt32(Codigo)));
                string uri = baseAddress.ToString();
                String Cookie = String.Format("B1SESSION={0}", SessionID);
                using (var handler = new HttpClientHandler { UseCookies = false })
                using (var client = new HttpClient(handler) { BaseAddress = baseAddress })
                {
                    var message = new HttpRequestMessage(HttpMethod.Patch, baseAddress);
                    message.Content = new StringContent(json, Encoding.UTF8);
                    message.Content.Headers.Add("Cookie", Cookie);
                    message.Headers.Add("Cookie", Cookie);
                    message.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json-patch+json");

                    var result = client.SendAsync(message).Result;
                    if (result.StatusCode == HttpStatusCode.Unauthorized)
                    {
                        Comunes.ConexiónSL oCon = new Comunes.ConexiónSL();
                        SessionID = oCon.ConectarSL();
                        UpdateReenvio(SessionID, Codigo, data);
                    }
                    else if (result.StatusCode == HttpStatusCode.NoContent && respuesta.respuesta.errores.Length == 0)
                    {
                        var p = new Process();
                        p.StartInfo = new ProcessStartInfo(@"\\br01-srv-db02\B1_SHF\Adjuntos\CartasPorte\" + respuesta.respuesta.cabecera.nroCTG + ".pdf")
                        {
                            UseShellExecute = true
                        };
                        p.Start();
                    }

                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        private HttpResponseMessage UpdateReenvio(string SessionID, string Codigo, StringContent Mensaje)
        {
            HttpResponseMessage Resultado = null;
            try
            {

                String Cookie = String.Format("B1SESSION={0}", SessionID);
                var baseAddress2 = new Uri("http://br01-srv-db02:50002/b1s/v1/CARTAPORTE" + " " + "eq" + " " + String.Format("({0})", Convert.ToInt32(Codigo)));
                string uri = baseAddress2.ToString();
                using (var handler = new HttpClientHandler { UseCookies = false })
                using (var client = new HttpClient(handler) { BaseAddress = baseAddress2 })
                {
                    var message = new HttpRequestMessage(HttpMethod.Patch, baseAddress2);
                    message.Content = Mensaje;
                    message.Content.Headers.Add("Cookie", Cookie);
                    //message.Headers.Add("Cookie", Cookie);
                    var result = client.PatchAsync(uri, message.Content).Result;
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



        #region Metodos Localidades y Provincias
        public List<Objetos.Localidad.localidad> ObtenerLocalidades(int CodProvincia)
        {
            List<Objetos.Localidad.localidad> ListLocalidades = new List<Objetos.Localidad.localidad>();
            try
            {
                Objetos.SesionAFIP oLogin = new Objetos.SesionAFIP();
                WSAfip.consultarLocalidadesPorProvinciaRequest oLocalidades = new WSAfip.consultarLocalidadesPorProvinciaRequest();
                WSAfip.ConsultarLocalidadesPorProvinciaSolicitud OSolicitud = new WSAfip.ConsultarLocalidadesPorProvinciaSolicitud();
                WSAfip.consultarLocalidadesPorProvinciaResponse oResp = new WSAfip.consultarLocalidadesPorProvinciaResponse();
                WSAfip.ConsultarLocalidadesPorProvinciaRespuesta oRespuesta = new WSAfip.ConsultarLocalidadesPorProvinciaRespuesta();
                WSAfip.CpePortTypeClient cpe = new WSAfip.CpePortTypeClient();
                //cpe.consultar
                WSAfip.Auth Autho = new WSAfip.Auth();
                Autho = oLogin.CompletarAuth();
                oLocalidades.auth = Autho;
                OSolicitud.codProvincia = CodProvincia;
                oLocalidades.solicitud = OSolicitud;

                oResp = cpe.consultarLocalidadesPorProvinciaAsync(Autho, OSolicitud).Result;
                oRespuesta = oResp.respuesta;

                foreach (var item in oRespuesta.localidad)
                {
                    Objetos.Localidad.localidad oLocalidad = new Objetos.Localidad.localidad();
                    oLocalidad.codigo = item.codigo;
                    oLocalidad.descripcion = item.descripcion;
                    ListLocalidades.Add(oLocalidad);
                }

            }
            catch (Exception)
            {

                throw;
            }

            return ListLocalidades;
        }




        public List<Objetos.Planta> GetPlantas(long Cuit)
        {
            List<Objetos.Planta> Listado = new List<Objetos.Planta>();
            try
            {
                Objetos.SesionAFIP Sesion = new Objetos.SesionAFIP();
                WSAfip.consultarPlantasRequest pRequest = new WSAfip.consultarPlantasRequest();
                WSAfip.Auth oauth = new WSAfip.Auth();
                WSAfip.consultarPlantasResponse oResponse = new WSAfip.consultarPlantasResponse();
                WSAfip.ConsultarPlantasRespuesta oRespuesta = new WSAfip.ConsultarPlantasRespuesta();
                WSAfip.ConsultarPlantasSolicitud oSolicitud = new WSAfip.ConsultarPlantasSolicitud();
                WSAfip.CpePortTypeClient ocpe = new WSAfip.CpePortTypeClient();
                oauth = Sesion.CompletarAuth();
                oSolicitud.cuit = Cuit;
                oResponse = ocpe.consultarPlantasAsync(oauth, oSolicitud).Result;

                oRespuesta = oResponse.respuesta;

                foreach (var item in oRespuesta.planta)
                {
                    //if (item.codLocalidad == Localidad && item.codProvincia == Provincia)
                    //{
                    Objetos.Planta oPlanta = new Objetos.Planta();
                    oPlanta.Codigo = item.nroPlanta.ToString();
                    oPlanta.Localidad = item.codLocalidad;
                    oPlanta.Provincia = item.codProvincia;

                    Listado.Add(oPlanta);
                    //}
                }
            }
            catch (Exception)
            {

                throw;
            }
            return Listado;
        }

        public List<Objetos.Planta> GetUbiPlantas(long Cuit, string planta)
        {
            List<Objetos.Planta> Listado = new List<Objetos.Planta>();
            try
            {
                Objetos.SesionAFIP Sesion = new Objetos.SesionAFIP();
                WSAfip.consultarPlantasRequest pRequest = new WSAfip.consultarPlantasRequest();
                WSAfip.Auth oauth = new WSAfip.Auth();
                WSAfip.consultarPlantasResponse oResponse = new WSAfip.consultarPlantasResponse();
                WSAfip.ConsultarPlantasRespuesta oRespuesta = new WSAfip.ConsultarPlantasRespuesta();
                WSAfip.ConsultarPlantasSolicitud oSolicitud = new WSAfip.ConsultarPlantasSolicitud();
                WSAfip.CpePortTypeClient ocpe = new WSAfip.CpePortTypeClient();
                oauth = Sesion.CompletarAuth();
                oSolicitud.cuit = Cuit;
                oResponse = ocpe.consultarPlantasAsync(oauth, oSolicitud).Result;

                oRespuesta = oResponse.respuesta;

                foreach (var item in oRespuesta.planta)
                {
                    if (item.nroPlanta.ToString() == planta.Trim())
                    {
                        Objetos.Planta oPlanta = new Objetos.Planta();

                        oPlanta.Localidad = item.codLocalidad;
                        oPlanta.Provincia = item.codProvincia;

                        Listado.Add(oPlanta);
                        break;
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
            return Listado;
        }



        #endregion

    }
}

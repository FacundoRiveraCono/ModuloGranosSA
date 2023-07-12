using ModuloGrano.Objetos;
using SAPbobsCOM;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ModuloGrano.Controladores
{
     class Entrega
    {

        public string AddEntrega(string TipoEntrega,Objetos.DeliveryNotes oDelivery)
        {
            string SessionID = "";
            string error = "";
            Int64 NroOrden = 0;
            bool Exito = false;
            Objetos.DeliveryNotes oEntrega = new DeliveryNotes();
            Objetos.Root msgSL = new Root();
            string ruta = @"C:\WSCPESAP\SessionSAP\Session.txt";
            try
            {
                oEntrega = ArmarEquivalencias(oDelivery);
                if (oEntrega.NroContrato2 != "Error Cantidad")
                {
                    using (StreamReader str = new StreamReader(ruta))
                    {
                        SessionID = str.ReadLine();
                        //File.SetAttributes(ruta, FileAttributes.Hidden);
                    }
                    var json = JsonSerializer.Serialize(oEntrega);
                    var data = new StringContent(json, Encoding.UTF8, "application/json");

                    var baseAddress = new Uri("http://br01-srv-db02:50002/b1s/v1/" + String.Format("{0}", TipoEntrega));

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
                            }
                            else
                            {
                                error = "Ok";
                            }
                        }

                    }
                }
                else
                {
                    error = "Debe aumentar linea de Contrato";
                }
            }
            catch (Exception)
            {

                throw;
            }

            return error;
        }

        public Objetos.DeliveryNotes ArmarEquivalencias(Objetos.DeliveryNotes oDelivery)
        {
            var baseAddress2 = new Uri("http://br01-srv-db02:50002/b1s/v1/sml.svc/CV_DESCARGASCPE?$select=*&$filter=CTG" + " " + "eq" + " " + String.Format("'{0}'", oDelivery.U_CTG) + "&$orderby=LineNum");
            string SessionID = "";
            Objetos.DocumentLines oLines = null;
           
            try
            {

                oLines = new Objetos.DocumentLines();
               
                List<Objetos.DocumentLines> oListadoLinea = new List<Objetos.DocumentLines>();
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
                        result = HeaderEnvio(SessionID, oDelivery.U_CTG);
                        //Pruebo reenvio con nuevo Sesion
                        oDelivery = ReenvioSL(result, oDelivery);
                        //Hago otro envio

                    }
                    else
                    {
                        result = HeaderEnvio(SessionID, oDelivery.U_CTG);
                        oDelivery = ReenvioSL(result, oDelivery);

                    }

                }
            }
            catch (Exception)
            {

                throw;
            }

            return oDelivery;
        }
        private Objetos.DeliveryNotes ReenvioSL(HttpResponseMessage result, Objetos.DeliveryNotes oDelivery)
        {
            Objetos.DESCARGASCPE.Root oEquivalencia = new DESCARGASCPE.Root();
            Objetos.DocumentLines oLines = null;
             int LineNum = 0;
            double Acumulador = 0;
            double CanLinea = 0;
            bool LineaPartida = false;
            bool difContrato = false;
            double cantidadtotal = 0;
            double CanContrato = 0;
          
            try
            {
                //oLines = new Objetos.DocumentLines();
               
               
                List<Objetos.DocumentLines> oListadoLinea = new List<Objetos.DocumentLines>();
                if (result.StatusCode == HttpStatusCode.OK)
                {
                    var content = result.Content.ReadAsStringAsync();
                    var info = content.Result;
                    oEquivalencia = JsonSerializer.Deserialize<Objetos.DESCARGASCPE.Root>(info);
                    foreach (var item in oEquivalencia.value)
                    {
                        if (String.IsNullOrEmpty(oDelivery.NroContrato2))
                        {
                            oLines = new Objetos.DocumentLines();
                            CanContrato = CanContrato + item.Cantidad;
                            if (item.ComparteLinea == "NO")
                            {
                                
                                if (LineaPartida)
                                {
                                    
                                    oLines.Quantity = item.Cantidad - Acumulador ;
                                }
                                else
                                {
                                    oLines.Quantity = item.Cantidad;
                                }


                                oLines.LineNum = LineNum;
                                oLines.BaseLine = item.LineNum;
                                oLines.BaseEntry = item.DocEntryOv;
                                oLines.DiscountPercent = 100.0;
                                oLines.BaseType = 17;
                                LineaPartida = false;
                                //oListadoLinea.Add(oLines);
                            }
                            else
                            {
                                oLines.LineNum = LineNum;
                                oLines.BaseLine = item.LineNum;
                                oLines.BaseEntry = item.DocEntryOv;
                                oLines.DiscountPercent = 100.0;
                                oLines.BaseType = 17;
                                //LineaPartida = false;
                                CanLinea = (item.Pendiente - item.Cantidad) * -1;
                                oLines.Quantity = item.Cantidad - CanLinea;
                               

                                LineaPartida = true;
                            }
                            //cantidadtotal = cantidadtotal + oLines.Quantity;
                          
                            Acumulador = Acumulador + oLines.Quantity;
                            oListadoLinea.Add(oLines);

                        }
                        else
                        {
                            //cantidadtotal = cantidadtotal + oLines.Quantity;
                            if (item.Contrato != oDelivery.NroContrato2)
                            {
                                CanContrato = CanContrato + item.Cantidad;
                                //CountContrto++;
                                difContrato = true;
                                oLines = new Objetos.DocumentLines();
                                if (item.ComparteLinea == "NO")
                                {
                                    if (LineaPartida)
                                    {
                                        oLines.Quantity = item.Cantidad - Acumulador;
                                    }
                                    else
                                    {
                                        oLines.Quantity = item.Cantidad;
                                    }


                                    oLines.LineNum = LineNum;
                                    oLines.BaseLine = item.LineNum;
                                    oLines.BaseEntry = item.DocEntryOv;
                                    oLines.DiscountPercent = 100.0;
                                    oLines.BaseType = 17;
                                    LineaPartida = false;
                                    //oListadoLinea.Add(oLines);
                                }
                                else
                                {
                                    oLines.LineNum = LineNum;
                                    oLines.BaseLine = item.LineNum;
                                    oLines.BaseEntry = item.DocEntryOv;
                                    oLines.DiscountPercent = 100.0;
                                    oLines.BaseType = 17;
                                    //LineaPartida = false;
                                    CanLinea = (item.Pendiente - item.Cantidad) * -1;
                                    oLines.Quantity = item.Cantidad - CanLinea;
                                    //Acumulador = Acumulador + oLines.Quantity;

                                    LineaPartida = true;
                                }
                               
                                oListadoLinea.Add(oLines);
                                Acumulador = Acumulador + oLines.Quantity;
                            }
                            else
                            {
                                CanContrato = CanContrato + item.Cantidad;
                                //CountContrto++;
                                oLines = new Objetos.DocumentLines();
                                if (item.ComparteLinea == "NO")
                                {
                                    if (LineaPartida && !difContrato)
                                    {
                                        oLines.Quantity = item.Cantidad - Acumulador;
                                    }
                                    //if (LineaPartida && difContrato)
                                    else
                                    {
                                        oLines.Quantity = item.Cantidad;
                                    }


                                    oLines.LineNum = LineNum;
                                    oLines.BaseLine = item.LineNum;
                                    oLines.BaseEntry = item.DocEntryOv;
                                    oLines.DiscountPercent = 100.0;
                                    oLines.BaseType = 17;
                                    LineaPartida = false;
                                    difContrato = false;
                                    //oListadoLinea.Add(oLines);
                                }
                                else
                                {
                                    oLines.LineNum = LineNum;
                                    oLines.BaseLine = item.LineNum;
                                    oLines.BaseEntry = item.DocEntryOv;
                                    oLines.DiscountPercent = 100.0;
                                    oLines.BaseType = 17;
                                    //LineaPartida = false;
                                    CanLinea = (item.Pendiente - item.Cantidad) * -1;
                                    oLines.Quantity = item.Cantidad - CanLinea;
                                    //Acumulador = Acumulador + oLines.Quantity;

                                    LineaPartida = true;
                                    difContrato = false;
                                }
                               
                                oListadoLinea.Add(oLines);
                                Acumulador = Acumulador + oLines.Quantity;

                            }
                        }

                
                        oDelivery.DocumentLines = oListadoLinea;
                        oDelivery.DocDate = item.Fecha;
                        oDelivery.TaxDate = item.Fecha;
                        oDelivery.DocDueDate = item.Fecha;
                        oDelivery.ShipToCode = item.ShipToCode;
                        oDelivery.U_CTG = item.CTG;
                        oDelivery.DocObjectCode = "15";
                        oDelivery.CardCode = item.CardCode;
                        
                        LineNum = LineNum + 1;
                        if (!LineaPartida && String.IsNullOrEmpty(oDelivery.NroContrato2))
                        {
                            break;
                        }

                    }
                    if (Acumulador != CanContrato)
                    {
                        oDelivery.NroContrato2 = "Error Cantidad";
                    }
                    
                }
            }
            catch (Exception)
            {

                throw;
            }
            return oDelivery;

        }

        private HttpResponseMessage HeaderEnvio(string SessionID, string CTG)
        {
            HttpResponseMessage Resultado = null;
            try
            {

                String Cookie = String.Format("B1SESSION={0}", SessionID);
                var baseAddress2 = new Uri("http://br01-srv-db02:50002/b1s/v1/sml.svc/CV_DESCARGASCPE?$select=*&$filter=CTG" + " " + "eq" + " " + String.Format("'{0}'",CTG) + "&$orderby=LineNum");
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

       

    }
}

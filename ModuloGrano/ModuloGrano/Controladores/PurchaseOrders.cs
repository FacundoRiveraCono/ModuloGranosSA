using ModuloGrano.Objetos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ModuloGrano.Controladores
{
     class PurchaseOrders
    {

        public string AddOrden(string TipoEntrega, Objetos.PurchaseOrders oPurchase)
        {
            string SessionID = "";
            string error = "";
            Int64 NroOrden = 0;
            bool Exito = false;
            Objetos.PurchaseOrders oOc = new Objetos.PurchaseOrders();
            Objetos.Root msgSL = new Root();
            string ruta = @"C:\WSCPESAP\SessionSAP\Session.txt";
            try
            {
                oOc = ArmarEquivalencias(oPurchase);
                using (StreamReader str = new StreamReader(ruta))
                {
                    SessionID = str.ReadLine();
                    //File.SetAttributes(ruta, FileAttributes.Hidden);
                }
                var json = JsonSerializer.Serialize(oPurchase);
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
            catch (Exception)
            {

                throw;
            }

            return error;
        }

        public Objetos.PurchaseOrders ArmarEquivalencias(Objetos.PurchaseOrders oPurchase)
        {
            var baseAddress2 = new Uri("http://br01-srv-db02:50002/b1s/v1/sml.svc/CV_ORDENESFLETE?$select=*&$filter=CTG" + " " + "eq" + " " + String.Format("'{0}'", oPurchase.U_CTG));
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
                        result = HeaderEnvio(SessionID, oPurchase.U_CTG);
                        //Pruebo reenvio con nuevo Sesion
                        oPurchase = ReenvioSL(result, oPurchase);
                        //Hago otro envio

                    }
                    else
                    {
                        result = HeaderEnvio(SessionID, oPurchase.U_CTG);
                        oPurchase = ReenvioSL(result, oPurchase);

                    }

                }
            }
            catch (Exception)
            {

                throw;
            }

            return oPurchase;
        }
        private Objetos.PurchaseOrders ReenvioSL(HttpResponseMessage result, Objetos.PurchaseOrders oPurchase)
        {
            Objetos.ORDENESFLETE.Root oEquivalencia = new ORDENESFLETE.Root();
            Objetos.DocumentLines oLines = null;
            Objetos.PurchaseOrdersLines oPurchaseLines = null;

            try
            {
              

                
                List<Objetos.PurchaseOrdersLines> oListadoLinea = new List<Objetos.PurchaseOrdersLines>();
                if (result.StatusCode == HttpStatusCode.OK)
                {
                    var content = result.Content.ReadAsStringAsync();
                    var info = content.Result;
                    oEquivalencia = JsonSerializer.Deserialize<Objetos.ORDENESFLETE.Root>(info);
                    foreach (var item in oEquivalencia.value)
                    {
                        oPurchaseLines = new Objetos.PurchaseOrdersLines();
                        if (item.ItemCode == "SER-000123")
                        {
                            oPurchaseLines.ItemCode = item.ItemCode;
                            oPurchaseLines.CostingCode = item.Norma1;
                            oPurchaseLines.CostingCode2 = item.Norma2;
                            oPurchaseLines.CostingCode3 = item.Norma3;
                            oPurchaseLines.CostingCode4 = item.Norma4;
                            oPurchaseLines.CostingCode5 = item.Norma5;
                            oPurchaseLines.LineNum = 0;                          
                            oPurchaseLines.Quantity = item.Quantity * -1;
                            oPurchaseLines.Price = (double)item.Price;
                            oListadoLinea.Add(oPurchaseLines);

                        }
                        if (item.ItemCode == "SER-000029")
                        {
                            oPurchaseLines.ItemCode = item.ItemCode;
                            oPurchaseLines.CostingCode = item.Norma1;
                            oPurchaseLines.CostingCode2 = item.Norma2;
                            oPurchaseLines.CostingCode3 = item.Norma3;
                            oPurchaseLines.CostingCode4 = item.Norma4;
                            oPurchaseLines.CostingCode5 = item.Norma5;
                            oPurchaseLines.LineNum = 1;                          
                            oPurchaseLines.Quantity = item.Quantity;
                            oPurchaseLines.Price = (double)item.Price;
                            oListadoLinea.Add(oPurchaseLines);
                        }
                       

                        oPurchase.UpdateDate = DateTime.Now;
                        oPurchase.DocDueDate = DateTime.Now;
                        oPurchase.DocDate = DateTime.Now;
                        oPurchase.CreationDate = DateTime.Now;
                        oPurchase.CardCode = item.CardCode;
                        oPurchase.PTICode = item.PTICode;
                        //oPurchase.DocObjectCode = "";
                        
                        oPurchase.DocumentLines = oListadoLinea;
                        


                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
            return oPurchase;

        }

        private HttpResponseMessage HeaderEnvio(string SessionID, string CTG)
        {
            HttpResponseMessage Resultado = null;
            try
            {

                String Cookie = String.Format("B1SESSION={0}", SessionID);
                var baseAddress2 = new Uri("http://br01-srv-db02:50002/b1s/v1/sml.svc/CV_ORDENESFLETE?$select=*&$filter=CTG" + " " + "eq" + " " + String.Format("'{0}'", CTG));
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

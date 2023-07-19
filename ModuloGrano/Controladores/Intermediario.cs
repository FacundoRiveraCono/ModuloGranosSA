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
    public class Intermediario
    {
        public string ArmarEquivalencias(string CUIT)
        {
            var baseAddress2 = new Uri("http://br01-srv-db02:50002/b1s/v1/sml.svc/CV_INTERMEDIARIOS?$select=CardName&$filter=CUIT" + " " + "eq" + " " + String.Format("'{0}'", CUIT));
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
                        result = HeaderEnvio(SessionID, CUIT);
                        //Pruebo reenvio con nuevo Sesion
                        error = ReenvioSL(result);
                        //Hago otro envio

                    }
                    else
                    {
                        //result = HeaderEnvio(SessionID, Codigo);
                        error = ReenvioSL(result);

                    }

                }
            }
            catch (Exception)
            {

                throw;
            }

            return error;
        }
        private string ReenvioSL(HttpResponseMessage result)
        {
            //Objetos.CartaPorte.Root oEquivalencia = new Objetos.CartaPorte.Root();
            Objetos.INTERMEDIARIO.Root oEquivalencia = new INTERMEDIARIO.Root();

            //string error = "";
            string Nombre = "";
            try
            {

                if (result.StatusCode == HttpStatusCode.OK)
                {
                    var content = result.Content.ReadAsStringAsync();
                    var info = content.Result;
                    oEquivalencia = JsonSerializer.Deserialize<Objetos.INTERMEDIARIO.Root>(info);
                    if (oEquivalencia.value.Count > 0)
                    {

                        foreach (var item in oEquivalencia.value)
                        {
                            Nombre = item.CardName;
                            break;

                        }
                    }
                   
                }
            }
            catch (Exception)
            {

                throw;
            }
            return Nombre;

        }

        private HttpResponseMessage HeaderEnvio(string SessionID, string CUIT)
        {
            HttpResponseMessage Resultado = null;
            try
            {

                String Cookie = String.Format("B1SESSION={0}", SessionID);
                var baseAddress2 = new Uri("http://br01-srv-db02:50002/b1s/v1/sml.svc/CV_INTERMEDIARIOS?$select=CardName&$filter=CUIT" + " " + "eq" + " " + String.Format("'{0}'", CUIT));
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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ModuloGrano.Comunes
{
    public class ConexiónSL
    {

        public ConexiónSL()
        {
            //ConectarSL();
        }
        public string ConectarSL()
        {
            Objetos.Sesion oSes = new Objetos.Sesion();
            Objetos.Login oLogin = new Objetos.Login();
            string sesion = "";
            try
            {
                var baseAddress = new Uri("http://br01-srv-db02:50002/b1s/v1/Login");
                oLogin.CompanyDB = "CONO_PROD";
                oLogin.Password = "Admin1234";
                oLogin.UserName = "manager";

                var json = JsonSerializer.Serialize(oLogin);
                var data = new StringContent(json, Encoding.UTF8, "application/json");
                using (var handler = new HttpClientHandler { UseCookies = false })
                using (var client = new HttpClient(handler) { BaseAddress = baseAddress })
                {
                    var message = new HttpRequestMessage(HttpMethod.Post, baseAddress);
                    message.Content = data;
                    var result = client.SendAsync(message).Result;

                    var content = result.Content.ReadAsStringAsync();
                    var info = content.Result;
                    oSes = JsonSerializer.Deserialize<Objetos.Sesion>(info);
                }
                sesion = oSes.SessionId;
                string ruta = @"C:\WSCPESAP\SessionSAP\Session.txt";

                using (StreamWriter str = new StreamWriter(ruta))
                {
                    str.WriteLine(sesion);


                }

            }
            catch (Exception e)
            {
                throw;
            }
            return sesion;
        }
    }
}

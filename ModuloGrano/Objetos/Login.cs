using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace ModuloGrano.Objetos
{
    public class Login
    {

        public string CompanyDB { get; set; }
        public string Password { get; set; }
        public string UserName { get; set; }

        //public string Host { get; set; }

    }

    public class Sesion
    {
        public string SessionId { get; set; }
        public string Version { get; set; }
        public int SessionTimeout { get; set; }
    }

    public class SesionAFIP
    {
        public WSAfip.Auth CompletarAuth()
        {
            XmlDocument xmlSignToken = null;
            string Sign, Token;
            long Cuit = 30604956281;
            WSAfip.Auth Autorizacion = new WSAfip.Auth();
            try
            {

                //Desarmo el login ticket response con el token y el sign para luego enviarlo en las peticiones
                //El tiempo de duración de esto es de 12 hs
                xmlSignToken = new XmlDocument();
                xmlSignToken.Load("C:/TokenCP/XmlLoginTicketResponseCP.xml");
                Sign = xmlSignToken.SelectSingleNode("//sign").InnerText;
                Token = xmlSignToken.SelectSingleNode("//token").InnerText;

                Autorizacion.cuitRepresentada = Cuit;
                Autorizacion.sign = Sign;
                Autorizacion.token = Token;


            }
            catch (Exception)
            {

                throw;
            }

            return Autorizacion;
        }
    }
}

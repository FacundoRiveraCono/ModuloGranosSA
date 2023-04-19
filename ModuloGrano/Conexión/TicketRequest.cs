using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Cryptography.Pkcs;
using System.Security.Cryptography.X509Certificates;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace ModuloGrano.Conexión
{
    public class TicketRequest
    {
        public UInt32 UniqueId; // Entero de 32 bits sin signo que identifica el requerimiento
        public DateTime GenerationTime; // Momento en que fue generado el requerimiento
        public DateTime ExpirationTime; // Momento en el que expira la solicitud
        public string Service; // Identificacion del WSN para el cual se solicita el TA
        public string Sign; // Firma de seguridad recibida en la respuesta
        public string Token; // Token de seguridad recibido en la respuesta
        public XmlDocument XmlLoginTicketRequest = null;
        public XmlDocument XmlLoginTicketResponse = null;
        XmlDocument xmlExpTime = null;
        DateTime ExpTime;
        public string RutaDelCertificadoFirmante;
        public string XmlStrLoginTicketRequestTemplate = "<loginTicketRequest><header><uniqueId></uniqueId><generationTime></generationTime><expirationTime></expirationTime></header><service></service></loginTicketRequest>";
        private bool _verboseMode = true;
        private static UInt32 _globalUniqueID = 0; // OJO! NO ES THREAD-SAFE


        public async void ObtenerLoginTicketResponse(string argServicio, string argUrlWsaa, string argRutaCertx5900, SecureString argPassword, bool argverboose)
        {

            string RutaDelCertificadoFirmante = argRutaCertx5900;
            bool verboseMode = argverboose;
            string cmsFirmadoBase64;
            string LoginTicketResponse = "";
            XmlNode xmlNodeUnidequeId = default(XmlNode);
            XmlNode xmlNodeGenerationTime = default(XmlNode);
            XmlNode xmlNodeExipirationTime = default(XmlNode);
            XmlNode xmlNodeService = default(XmlNode);

            //Pasos 1 generamos el login ticker response

            try
            {
                _globalUniqueID += 1;

                XmlLoginTicketRequest = new XmlDocument();
                XmlLoginTicketRequest.LoadXml(XmlStrLoginTicketRequestTemplate);

                xmlNodeUnidequeId = XmlLoginTicketRequest.SelectSingleNode("//uniqueId");
                xmlNodeGenerationTime = XmlLoginTicketRequest.SelectSingleNode("//generationTime");
                xmlNodeExipirationTime = XmlLoginTicketRequest.SelectSingleNode("//expirationTime");
                xmlNodeService = XmlLoginTicketRequest.SelectSingleNode("//service");
                xmlNodeGenerationTime.InnerText = DateTime.Now.AddMinutes(-10).ToString("s");
                xmlNodeExipirationTime.InnerText = DateTime.Now.AddMinutes(+10).ToString("s");
                xmlNodeUnidequeId.InnerText = Convert.ToString(_globalUniqueID);
                xmlNodeService.InnerText = argServicio;
                this.Service = argServicio;
                //Console.WriteLine("XML", XmlLoginTicketRequest.OuterXml);

            }
            catch (Exception excepcionalGenerarLoginTicketRequest)
            {

                throw new Exception("** ERROR generando LoginTicketRequest" + excepcionalGenerarLoginTicketRequest);
            }

            //Paso 2 Firmo el login ticket Request
            try
            {
                SecureString Password = new NetworkCredential("", "").SecurePassword;
                X509Certificate2 certFirmante = ObtenerCertificado(RutaDelCertificadoFirmante, Password);

                //Se convierte el loginticket en bytes para firmar
                Encoding Encodemsg = Encoding.UTF8;
                byte[] msBytes = Encodemsg.GetBytes(XmlLoginTicketRequest.OuterXml);

                //Se firma y pasa a base64
                byte[] encodeSignedCms = FirmaByteMensaje(msBytes, certFirmante);
                cmsFirmadoBase64 = Convert.ToBase64String(encodeSignedCms);
            }
            catch (Exception exepcionalFirmar)
            {

                throw new Exception("**ERROR firmando el LoginTicketRequest : " + exepcionalFirmar.Message);
            }

            //
            //Invoco al WSAA para obtener el Login Ticket Response
            try
            {

                //AfipLogin.ServiceReference1.LoginCMSService serviciowsa = new ServiceReference1.LoginCMSService();
                //LoginAfip.

                //serviciowsa.Url = argUrlWsaa;
                LoginAfip.LoginCMSClient serviciowsa = new LoginAfip.LoginCMSClient();
                LoginAfip.loginCmsResponse oResp = new LoginAfip.loginCmsResponse();

                oResp = await serviciowsa.loginCmsAsync(cmsFirmadoBase64);
                LoginTicketResponse = oResp.loginCmsReturn;

            }
            catch (Exception errorInvocandoWsaa)
            {

                throw new Exception("ERROR Invocando el servicio WSAA: " + errorInvocandoWsaa.Message);
            }

            //Analizo el Login Ticket Response recibido del WSAA
            try
            {

                XmlLoginTicketResponse = new XmlDocument();

                //XmlLoginTicketResponse.Save("C:/TokenCP/XmlLoginTicketResponseCP.xml");
                ////Se borra el xml previamente creado
                //string ubicacion = "C:/Users/frivera/OneDrive - Cono Group/Escritorio/TokenCP";
                //string nombre = "XmlLoginTicketResponseCP.xml";
                //if (File.Exists(Path.Combine(ubicacion, nombre)))
                //{
                //    File.Delete(Path.Combine(ubicacion, nombre));
                //    Console.WriteLine("Archivo Borrado");
                //}

                XmlLoginTicketResponse.LoadXml(LoginTicketResponse);

                this.UniqueId = UInt32.Parse(XmlLoginTicketResponse.SelectSingleNode("//uniqueId").InnerText);
                this.GenerationTime = DateTime.Parse(XmlLoginTicketResponse.SelectSingleNode("//generationTime").InnerText);
                this.ExpirationTime = DateTime.Parse(XmlLoginTicketResponse.SelectSingleNode("//expirationTime").InnerText);
                this.Sign = XmlLoginTicketResponse.SelectSingleNode("//sign").InnerText;
                this.Token = XmlLoginTicketResponse.SelectSingleNode("//token").InnerText;

                //Guardo el xml con la información en una ruta en el servidor
                //XmlLoginTicketResponse.
                XmlLoginTicketResponse.Save("C:/TokenCP/XmlLoginTicketResponseCP.xml");
                //Si pasa esta parte es porque no se genero ningun error en AFIP y el loginticket devolvió respuesta
                // EnvioWS WSAfip = new EnvioWS();
                // WSAfip.EnviarCartaPorte();

            }
            catch (Exception excepcionAlAnalizarLoginTicketResponse)
            {

                throw new Exception("***Error ANALIZANDO el LoginTicketResponse : " + excepcionAlAnalizarLoginTicketResponse.Message);
            }
            //return LoginTicketResponse;
        }
        public static X509Certificate2 ObtenerCertificado(string pfxpath, SecureString password)
        {

            X509Certificate2 objCert = new X509Certificate2();
            try
            {
                objCert = new System.Security.Cryptography.X509Certificates.X509Certificate2(pfxpath, password, X509KeyStorageFlags.PersistKeySet);
                return objCert;
            }
            catch (Exception exceptionImportCert)
            {

                throw new Exception("pfxpath =" + pfxpath + "exepcion=" + exceptionImportCert.Message + " " + exceptionImportCert.StackTrace);
            }

        }
        private byte[] FirmaByteMensaje(byte[] argBytemsg, X509Certificate2 argCertFirmante)
        {
            try
            {
                //Se pasa el mensaje en un objeto contentinfo (Requerido para construir el obj SignedCms
                ContentInfo infoContenido = new ContentInfo(argBytemsg);
                SignedCms cmsFirmado = new SignedCms(infoContenido);

                CmsSigner cmsFirmante = new CmsSigner(argCertFirmante);
                cmsFirmante.IncludeOption = X509IncludeOption.EndCertOnly;

                //Firmo el mensaje pcks7
                cmsFirmado.ComputeSignature(cmsFirmante);

                return cmsFirmado.Encode();

            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool TokenControl()
        {
            DateTime FechaActual = DateTime.Now;
            double DifHorario = 0;
            TimeSpan diferencia;
            bool Valido = false;
            try
            {
                //Primero valido que el archivo exista, sino se crea de nuevo.

                xmlExpTime = new XmlDocument();
                string ubicacion = "C:/TokenCP/";
                string nombre = "XmlLoginTicketResponseCP.xml";
                if (File.Exists(Path.Combine(ubicacion, nombre)))
                {
                    xmlExpTime.Load("C:/TokenCP/XmlLoginTicketResponseCP.xml");
                    ExpTime = DateTime.Parse(xmlExpTime.SelectSingleNode("//expirationTime").InnerText);
                    //File.Delete(Path.Combine(ubicacion, nombre));
                    diferencia = ExpTime - FechaActual;
                    DifHorario = diferencia.TotalHours;

                    //Si la diferencia es menor que cero es porque sigue viguente
                    if (DifHorario >= 0)
                    {
                        Valido = true;

                    }
                    else
                    {

                        Valido = false;
                    }

                }
                else
                {
                    //Sino existe devuelvo falso para que se cree de nuevo.
                    Valido = false;
                }
            }
            catch (Exception)
            {

                throw;
            }
            return Valido;

        }

    }
}

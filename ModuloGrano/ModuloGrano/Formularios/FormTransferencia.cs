using ModuloGrano.Objetos;
using SAPbobsCOM;
using SAPbouiCOM;
using SAPbouiCOM.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Net.Http.Headers;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace ModuloGrano.Formularios
{
    class FormTransferencia
    {
        #region Atributos
        public const int TipoFormularioint = 2000060004;
        public const string TipoFormulariostr = "UDO_FT_CARTAPORTE";
        public const string Formulario = "CartaPorte";
        private static SAPbobsCOM.Company oCompany = null;
        private static SAPbouiCOM.Form oForm = null;
        private static SAPbouiCOM.EditText oEditText = null;
        private static SAPbouiCOM.Item oItem = null;
        #endregion

        #region Constructor

        public FormTransferencia()
        {

        }

        #endregion

        #region Eventos
        public void OSAPB1appl_ItemEvent(string FormUID, ref SAPbouiCOM.ItemEvent pVal, out bool BubbleEvent)
        {
            BubbleEvent = true;
            string error = "";
            SAPbouiCOM.Form oform = null;
            Conexión.TicketRequest oLogin = new Conexión.TicketRequest();
            #region Controladores
            Controladores.Transferencia oTransf = new Controladores.Transferencia();
            Controladores.Entrega ctrlEntrega = new Controladores.Entrega();
            Controladores.PurchaseOrders ctrolPurchase = new Controladores.PurchaseOrders();
            #endregion
            #region Objetos
            Objetos.ConsultarCPEAutomotorResponse oConsultar = new Objetos.ConsultarCPEAutomotorResponse();
            Objetos.DeliveryNotes oDelivery = null;
            Objetos.PurchaseOrders oPurchase = null;
            #endregion
            try
            {
                oform = Comunes.ConexiónSAPB1.oSAPB1appl.Forms.Item(FormUID);
                if (pVal.BeforeAction)
                {
                    switch (pVal.EventType)
                    {
                        case SAPbouiCOM.BoEventTypes.et_ITEM_PRESSED:
                            if (pVal.ItemUID == "btnDefi")
                            {
                                if (!String.IsNullOrEmpty(((SAPbouiCOM.EditText)oform.Items.Item("Item_125").Specific).Value.ToString()) && ((SAPbouiCOM.EditText)oform.Items.Item("Item_125").Specific).Value.ToString() != "0.0")
                                {
                                    oDelivery = new DeliveryNotes();
                                    oDelivery.U_CTG = ((SAPbouiCOM.EditText)oform.Items.Item("Item_83").Specific).Value.ToString();
                                    oDelivery.NroContrato2 = ((SAPbouiCOM.EditText)oform.Items.Item("Item_144").Specific).Value.ToString();
                                    error = ctrlEntrega.AddEntrega("DeliveryNotes", oDelivery);
                                    Comunes.ConexiónSAPB1.oSAPB1appl.MessageBox(error.ToString());
                                }
                                else
                                {
                                    Comunes.ConexiónSAPB1.oSAPB1appl.MessageBox("Debe completar Peso Neto (Kg)");
                                }
                            }
                            if (pVal.ItemUID == "btnPreli")
                            {
                                if (!String.IsNullOrEmpty(((SAPbouiCOM.EditText)oform.Items.Item("Item_125").Specific).Value.ToString()) && ((SAPbouiCOM.EditText)oform.Items.Item("Item_125").Specific).Value.ToString() != "0.0")
                                {
                                    oDelivery = new DeliveryNotes();
                                    oDelivery.U_CTG = ((SAPbouiCOM.EditText)oform.Items.Item("Item_83").Specific).Value.ToString();
                                    oDelivery.NroContrato2 = ((SAPbouiCOM.EditText)oform.Items.Item("Item_144").Specific).Value.ToString();
                                    error = ctrlEntrega.AddEntrega("Drafts", oDelivery);
                                    Comunes.ConexiónSAPB1.oSAPB1appl.MessageBox(error.ToString());
                                }
                                else
                                {
                                    Comunes.ConexiónSAPB1.oSAPB1appl.MessageBox("Debe completar Peso Neto (Kg)");
                                }
                            }
                            if (pVal.ItemUID == "Item_151")
                            {
                                if (((SAPbouiCOM.CheckBox)oform.Items.Item(pVal.ItemUID).Specific).Checked)
                                {
                                    ((SAPbouiCOM.EditText)oform.Items.Item("Item_142").Specific).Item.Enabled = true;
                                    ((SAPbouiCOM.EditText)oform.Items.Item("Item_146").Specific).Item.Enabled = true;
                                    ((SAPbouiCOM.EditText)oform.Items.Item("Item_144").Specific).Item.Enabled = true;
                                    ((SAPbouiCOM.EditText)oform.Items.Item("Item_147").Specific).Item.Enabled = true;
                                }
                                else
                                {
                                    ((SAPbouiCOM.EditText)oform.Items.Item("Item_142").Specific).Item.Enabled = false;
                                    ((SAPbouiCOM.EditText)oform.Items.Item("Item_146").Specific).Item.Enabled = false;
                                    ((SAPbouiCOM.EditText)oform.Items.Item("Item_144").Specific).Item.Enabled = false;
                                    ((SAPbouiCOM.EditText)oform.Items.Item("Item_147").Specific).Item.Enabled = false;
                                }
                            }
                            if (pVal.ItemUID == "Item_165")
                            {
                                if (((SAPbouiCOM.CheckBox)oform.Items.Item(pVal.ItemUID).Specific).Checked)
                                {
                                    ((SAPbouiCOM.EditText)oform.Items.Item("Item_28").Specific).Item.Enabled = true;
                                    ((SAPbouiCOM.EditText)oform.Items.Item("Item_152").Specific).Item.Enabled = true;
                                    ((SAPbouiCOM.EditText)oform.Items.Item("Item_154").Specific).Item.Enabled = true;
                                    ((SAPbouiCOM.EditText)oform.Items.Item("Item_157").Specific).Item.Enabled = true;
                                    ((SAPbouiCOM.EditText)oform.Items.Item("Item_159").Specific).Item.Enabled = true;
                                    ((SAPbouiCOM.EditText)oform.Items.Item("Item_161").Specific).Item.Enabled = true;
                                }
                                else
                                {
                                    ((SAPbouiCOM.EditText)oform.Items.Item("Item_28").Specific).Item.Enabled = false;
                                    ((SAPbouiCOM.EditText)oform.Items.Item("Item_152").Specific).Item.Enabled = false;
                                    ((SAPbouiCOM.EditText)oform.Items.Item("Item_154").Specific).Item.Enabled = false;
                                    ((SAPbouiCOM.EditText)oform.Items.Item("Item_157").Specific).Item.Enabled = false;
                                    ((SAPbouiCOM.EditText)oform.Items.Item("Item_159").Specific).Item.Enabled = false;
                                    ((SAPbouiCOM.EditText)oform.Items.Item("Item_161").Specific).Item.Enabled = false;
                                }
                            }

                            if (pVal.ItemUID == "Item_119")
                            {
                                XmlDocument xmlExpTime = null;
                                XmlDocument xmlResp = null;
                                DateTime ExpTime;
                                //DataHandling oDataHand = null;
                                //Atributos para la creacion del login ticket
                                string strUrlWsaaWsdl = "https://wsaa.afip.gov.ar/ws/services/LoginCms";
                                string strIdServicioNegocio = "wscpe";
                                string strRutaCertSigner = "C:\\WSCPESAP\\alias.p12";
                                SecureString strPasswordSecureString = new SecureString();
                                bool blnVerboseMode = true;

                                if (!oLogin.TokenControl())
                                {
                                    oLogin.ObtenerLoginTicketResponse(strIdServicioNegocio, strUrlWsaaWsdl, strRutaCertSigner, strPasswordSecureString, blnVerboseMode);
                                }


                                string CTG = ((SAPbouiCOM.EditText)oform.Items.Item("Item_83").Specific).Value;
                                if (!String.IsNullOrEmpty(CTG))
                                {
                                    oConsultar = oTransf.GetCartaPorte(Convert.ToInt64(CTG));

                                    CompletarForm(FormUID, oConsultar);

                                }
                                else
                                {
                                    Comunes.ConexiónSAPB1.oSAPB1appl.StatusBar.SetText("Ingrese CTG", SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Error);
                                }


                            }

                            if (pVal.ItemUID == "btnRepro")
                            {
                                try
                                {
                                    oform = Comunes.ConexiónSAPB1.oSAPB1appl.Forms.Item(FormUID);
                                    error = AltaTransferenciaSL(oform);
                                    if (error != "Ok")
                                    {

                                        Comunes.ConexiónSAPB1.oSAPB1appl.MessageBox("No se puedo crear transferencia, debe reprocesar");
                                        break;
                                    }
                                    else
                                    {
                                        Comunes.ConexiónSAPB1.oSAPB1appl.MessageBox("Transferencia creada con exito");

                                    }
                                }
                                catch (Exception ex)
                                {

                                    Comunes.ConexiónSAPB1.oSAPB1appl.StatusBar.SetText(ex.ToString());
                                }
                            }
                            break;
                        
                    }
                }
            }
            catch (Exception ex)
            {

                Comunes.ConexiónSAPB1.oSAPB1appl.StatusBar.SetText("Error en Transferencia > ChooseFromList" + ex.Message);
            }
        }



        public void OSAPB1appl_FormDataEvent(ref SAPbouiCOM.BusinessObjectInfo BusinessObjectInfo, out bool BubbleEvent)
        {
            BubbleEvent = true;
            string error = "";
            SAPbouiCOM.Form oform = null;
            try
            {
                if (BusinessObjectInfo.ActionSuccess)
                {
                    switch (BusinessObjectInfo.EventType)
                    {
                        case SAPbouiCOM.BoEventTypes.et_FORM_DATA_ADD:
                            oform = Comunes.ConexiónSAPB1.oSAPB1appl.Forms.Item(BusinessObjectInfo.FormUID);
                            error = AltaTransferenciaSL(oform);
                            if (error != "Ok")
                            {

                                Comunes.ConexiónSAPB1.oSAPB1appl.MessageBox("No se puedo crear transferencia, debe reprocesar");
                                break;
                            }
                            else
                            {
                                Comunes.ConexiónSAPB1.oSAPB1appl.MessageBox("Transferencia creada con exito");

                            }
                            break;
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        #endregion

        #region Metodos
        public void CompletarForm(string FormUID, Objetos.ConsultarCPEAutomotorResponse Objeto)
        {
            SAPbouiCOM.Form oform = null;
            WSAfip.consultarCPEAutomotorResponse resp = new WSAfip.consultarCPEAutomotorResponse();
            string[] ContratoUbi = null;
            try
            {

                oform = Comunes.ConexiónSAPB1.oSAPB1appl.Forms.Item(FormUID);

                if (Objeto.Respuesta.Cabecera != null)
                {
                    //Inicio Primer Pestaña
                    ((SAPbouiCOM.EditText)oform.Items.Item("Item_24").Specific).Value = Objeto.Respuesta.Cabecera.Sucursal.ToString();
                    ((SAPbouiCOM.EditText)oform.Items.Item("Item_25").Specific).Value = Objeto.Respuesta.Cabecera.NroOrden.ToString();
                    ((SAPbouiCOM.EditText)oform.Items.Item("Item_83").Specific).Value = Objeto.Respuesta.Cabecera.NroCTG.ToString();
                    ((SAPbouiCOM.EditText)oform.Items.Item("Item_21").Specific).Value = Objeto.Respuesta.Origen.CodProvincia.ToString();
                    ((SAPbouiCOM.EditText)oform.Items.Item("Item_20").Specific).Value = Objeto.Respuesta.Origen.CodLocalidad.ToString();
                    if (Objeto.Respuesta.Intervinientes.CuitRemitenteComercialVentaPrimaria != 0)
                        ((SAPbouiCOM.EditText)oform.Items.Item("Item_167").Specific).Value = Objeto.Respuesta.Intervinientes.CuitRemitenteComercialVentaPrimaria.ToString();
                    if (Objeto.Respuesta.Intervinientes.CuitRemitenteComercialVentaSecundaria != 0)
                        ((SAPbouiCOM.EditText)oform.Items.Item("Item_170").Specific).Value = Objeto.Respuesta.Intervinientes.CuitRemitenteComercialVentaSecundaria.ToString();
                    if (Objeto.Respuesta.Intervinientes.CuitRemitenteComercialVentaSecundaria2 != 0)
                        ((SAPbouiCOM.EditText)oform.Items.Item("Item_171").Specific).Value = Objeto.Respuesta.Intervinientes.CuitRemitenteComercialVentaSecundaria2.ToString();
                    if (Objeto.Respuesta.Intervinientes.cuitMercadoATermino != 0)
                        ((SAPbouiCOM.EditText)oform.Items.Item("Item_172").Specific).Value = Objeto.Respuesta.Intervinientes.cuitMercadoATermino.ToString();
                    if (Objeto.Respuesta.Intervinientes.CuitCorredorVentaPrimaria != 0)
                        ((SAPbouiCOM.EditText)oform.Items.Item("Item_173").Specific).Value = Objeto.Respuesta.Intervinientes.CuitCorredorVentaPrimaria.ToString();
                    if (Objeto.Respuesta.Intervinientes.CuitCorredorVentaSecundaria != 0)
                        ((SAPbouiCOM.EditText)oform.Items.Item("Item_174").Specific).Value = Objeto.Respuesta.Intervinientes.CuitCorredorVentaSecundaria.ToString();
                    if (Objeto.Respuesta.Intervinientes.cuitRepresentanteEntregador != 0)
                        ((SAPbouiCOM.EditText)oform.Items.Item("Item_176").Specific).Value = Objeto.Respuesta.Intervinientes.cuitRepresentanteEntregador.ToString();
                    if (Objeto.Respuesta.Intervinientes.cuitRepresentanteRecibidor != 0)
                        ((SAPbouiCOM.EditText)oform.Items.Item("Item_177").Specific).Value = Objeto.Respuesta.Intervinientes.cuitRepresentanteRecibidor.ToString();
                    ContratoUbi = ContratoUbicacion(Objeto.Respuesta.Cabecera.Observaciones);
                    ((SAPbouiCOM.EditText)oform.Items.Item("Item_164").Specific).Value = ContratoUbi[1].ToUpper();
                    ((SAPbouiCOM.EditText)oform.Items.Item("Item_112").Specific).Value = ContratoUbi[0].ToUpper();
                    //Fin de primer pestaña

                    ((SAPbouiCOM.Folder)oform.Items.Item("Item_56").Specific).Select();


                    //Comienzo Segunda Pestaña
                    ((SAPbouiCOM.EditText)oform.Items.Item("Item_57").Specific).Value = Objeto.Respuesta.Transporte.CuitChofer.ToString();
                    ((SAPbouiCOM.EditText)oform.Items.Item("Item_182").Specific).Value = Objeto.Respuesta.Transporte.CuitTransportista.ToString();
                    ((SAPbouiCOM.EditText)oform.Items.Item("Item_62").Specific).Value = Objeto.Respuesta.Transporte.Dominio[0].ToString();
                    ((SAPbouiCOM.EditText)oform.Items.Item("Item_64").Specific).Value = Objeto.Respuesta.Transporte.Dominio[1].ToString();
                    ((SAPbouiCOM.EditText)oform.Items.Item("Item_72").Specific).Value = Objeto.Respuesta.DatosCarga.CodGrano.ToString();
                    ((SAPbouiCOM.EditText)oform.Items.Item("Item_74").Specific).Value = Objeto.Respuesta.DatosCarga.Cosecha.ToString();
                    ((SAPbouiCOM.EditText)oform.Items.Item("Item_76").Specific).Value = Objeto.Respuesta.DatosCarga.PesoBruto.ToString();
                    ((SAPbouiCOM.EditText)oform.Items.Item("Item_78").Specific).Value = Objeto.Respuesta.DatosCarga.PesoTara.ToString();
                    ((SAPbouiCOM.EditText)oform.Items.Item("Item_80").Specific).Value = Convert.ToString((Objeto.Respuesta.DatosCarga.PesoBruto - Objeto.Respuesta.DatosCarga.PesoTara));
                    //Fin de Segunda Pestaña

                    //Inicio Tercera Pestaña
                    ((SAPbouiCOM.Folder)oform.Items.Item("Item_81").Specific).Select();
                    ((SAPbouiCOM.EditText)oform.Items.Item("Item_180").Specific).Value = Objeto.Respuesta.Destino.Cuit.ToString();
                    ((SAPbouiCOM.EditText)oform.Items.Item("Item_179").Specific).Value = Objeto.Respuesta.Destinatario.Cuit.ToString();
                    //((SAPbouiCOM.EditText)oform.Items.Item("CUITDesa").Specific).Value = Objeto.Respuesta.Destinatario.Cuit.ToString();
                    ((SAPbouiCOM.EditText)oform.Items.Item("Item_94").Specific).Value = Objeto.Respuesta.Destino.CodLocalidad.ToString();
                    ((SAPbouiCOM.EditText)oform.Items.Item("Item_183").Specific).Value = Objeto.Respuesta.Destino.CodProvincia.ToString();
                    string fecha = Objeto.Respuesta.Transporte.FechaHoraPartida.ToString("dd/MM/yyyy");
                    ((SAPbouiCOM.EditText)oform.Items.Item("Item_106").Specific).String = fecha;
                    ((SAPbouiCOM.EditText)oform.Items.Item("Item_96").Specific).Value = Objeto.Respuesta.Destino.Planta.ToString();
                    ((SAPbouiCOM.EditText)oform.Items.Item("Item_108").Specific).Value = Objeto.Respuesta.Transporte.KmRecorrer.ToString();
                    ((SAPbouiCOM.EditText)oform.Items.Item("Item_110").Specific).Value = Objeto.Respuesta.Transporte.TarifaReferencia.ToString();
                    ((SAPbouiCOM.EditText)oform.Items.Item("Item_114").Specific).Value = Objeto.Respuesta.Transporte.Tarifa.ToString();
                    if(Objeto.Respuesta.Transporte.CuitPagadorFlete!=0)
                    ((SAPbouiCOM.EditText)oform.Items.Item("Item_181").Specific).Value = Objeto.Respuesta.Transporte.CuitPagadorFlete.ToString();
                    //Fin Tercera Pestaña
                    //inico Cuarta Pestaña
                    ((SAPbouiCOM.Folder)oform.Items.Item("Item_16").Specific).Select();
                    ((SAPbouiCOM.EditText)oform.Items.Item("Item_18").Specific).Value = @"\\br01-srv-db02\B1_SHF\Adjuntos\CartasPorte\" + Objeto.Respuesta.Cabecera.NroCTG + ".pdf";
                    ((SAPbouiCOM.EditText)oform.Items.Item("Item_37").Specific).Value = Objeto.Respuesta.Cabecera.Observaciones.ToString();
                    //Fin Cuarta Pestaña

                    
                }
                else
                {
                    Comunes.ConexiónSAPB1.oSAPB1appl.MessageBox("Error devuelto por AFIP");
                }
            }
            //}
            catch (Exception)
            {

                throw;
            }
            finally
            {

            }

        }

        public string[] ContratoUbicacion(string Observaciones)
        {
            int IndexUbicacion, IndexContrato, Largo;
            string TipoUbi = "";
            string PreUbicacion = "";
            string Ubicacion = "";
            string[] ContratoUbi = new string[2];
            try
            {
                IndexContrato = Observaciones.ToLower().IndexOf("cto:");
                IndexUbicacion = Observaciones.ToLower().IndexOf("ubi:");
                Largo = Observaciones.Length;
                //Pregunto para saber si es un silo o una ubicación y dividir en base a esta variable
                if (IndexUbicacion == 0 || IndexUbicacion == -1)
                {
                    IndexUbicacion = Observaciones.ToLower().IndexOf("silo:");
                    TipoUbi = "silo:";
                }
                else
                {
                    TipoUbi = "ubi:";
                }
                //Numero de Contrato
                if (IndexContrato == -1)
                {
                    ContratoUbi[0] = "";
                }
                else
                {
                    string[] palabra = Observaciones.ToLower().Split(TipoUbi);
                    ContratoUbi[0] = palabra[0].Replace("cto: ", string.Empty).Trim();
                }

                if (IndexUbicacion != 0 && IndexUbicacion != -1)
                {
                    string[] palabra = Observaciones.ToLower().Split(TipoUbi);
                    string[] ubicacion = palabra[1].Split(" ");
                    if (String.IsNullOrEmpty(ubicacion[0]))
                    {
                        PreUbicacion = ubicacion[1].Replace(" ", string.Empty);
                        Ubicacion = PreUbicacion.Split(" ")[0];
                        ContratoUbi[1] = Ubicacion;
                    }
                    else
                    {
                        PreUbicacion = ubicacion[0].Replace(" ", string.Empty);
                        Ubicacion = PreUbicacion.Split(" ")[0];
                        ContratoUbi[1] = Ubicacion;

                    }
                }
                if (IndexUbicacion == -1)
                {
                    ContratoUbi[1] = "";
                }
                //Fin Numero de Contrato
                return ContratoUbi;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public string AltaTransferenciaSL(SAPbouiCOM.Form oForm)
        {
            string Error = "";
            Objetos.Transfer oTransfer = null;

            Controladores.Transferencia ctrlTransf = new Controladores.Transferencia();
            try
            {
                if (!string.IsNullOrEmpty(((SAPbouiCOM.EditText)oForm.Items.Item("Item_112").Specific).Value.ToString()) && !string.IsNullOrEmpty(((SAPbouiCOM.EditText)oForm.Items.Item("Item_28").Specific).Value.ToString()))
                {
                    oTransfer = new Objetos.Transfer();

                    List<Objetos.StockTransferLinesBinAllocations> oListadoUbicacion = new List<Objetos.StockTransferLinesBinAllocations>();
                    List<Objetos.StockTransferLines> oListadoLinea = new List<Objetos.StockTransferLines>();

                    int offset = oForm.DataSources.DBDataSources.Item(0).Offset;
                    string DocEntry = oForm.DataSources.DBDataSources.Item(0).GetValue("DocEntry", offset);
                    int PesoNeto = Convert.ToInt32(((SAPbouiCOM.EditText)oForm.Items.Item("Item_80").Specific).Value);
                    oTransfer.U_PesoNeto = PesoNeto;

                    Error = ctrlTransf.AddTransferencia(oTransfer, Convert.ToInt16(DocEntry));
                    if (Error != "Ok")
                    {
                        Comunes.ConexiónSAPB1.oSAPB1appl.MessageBox(Error.ToString());
                    }
                    //else
                    //{
                    //    Comunes.ConexiónSAPB1.oSAPB1appl.MessageBox("Transferencia Creada");
                    //}
                }
                else
                {
                    Comunes.ConexiónSAPB1.oSAPB1appl.MessageBox("debe completar NroContrato/Ubicación");
                }

            }
            catch (Exception)
            {

                throw;
            }
            return Error;
        }
        #endregion
    }
}

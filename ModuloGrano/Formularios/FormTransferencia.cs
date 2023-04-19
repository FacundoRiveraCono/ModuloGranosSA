using ModuloGrano.Objetos;
using SAPbobsCOM;
using SAPbouiCOM.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http.Headers;
using System.Security;
using System.Text;
using System.Threading.Tasks;
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
            SAPbouiCOM.Form  oform= null;
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

                            if (pVal.ItemUID == "btnPreli")
                            {
                                if (!String.IsNullOrEmpty(((SAPbouiCOM.EditText)oform.Items.Item("Item_125").Specific).Value.ToString()) && ((SAPbouiCOM.EditText)oform.Items.Item("Item_125").Specific).Value.ToString() != "0.0")
                                {
                                    //oDelivery = new DeliveryNotes();
                                    oPurchase = new PurchaseOrders();
                                    oPurchase.U_CTG = ((SAPbouiCOM.EditText)oform.Items.Item("Item_83").Specific).Value.ToString();
                                    //oDelivery.U_CTG = ((SAPbouiCOM.EditText)oform.Items.Item("Item_83").Specific).Value.ToString();
                                    //oDelivery.NroContrato2 = ((SAPbouiCOM.EditText)oform.Items.Item("Item_144").Specific).Value.ToString();
                                    //error = ctrlEntrega.AddEntrega("Drafts", oDelivery);
                                    error = ctrolPurchase.AddOrden("Drafts", oPurchase);
                                    Comunes.ConexiónSAPB1.oSAPB1appl.MessageBox(error.ToString());
                                }
                                else
                                {
                                    Comunes.ConexiónSAPB1.oSAPB1appl.MessageBox("Debe completar Peso Neto (Kg)");
                                }
                            }
                            if (pVal.ItemUID == "btnDefi")
                            {
                                if (!String.IsNullOrEmpty(((SAPbouiCOM.EditText)oform.Items.Item("Item_125").Specific).Value.ToString()) && ((SAPbouiCOM.EditText)oform.Items.Item("Item_125").Specific).Value.ToString()!= "0.0")
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
                            if (pVal.ItemUID == "Item_151")
                            {
                               if( ((SAPbouiCOM.CheckBox)oform.Items.Item(pVal.ItemUID).Specific).Checked)
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
                                string strRutaCertSigner = "C:\\alias.p12";
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
                           
                            break;
                        case SAPbouiCOM.BoEventTypes.et_FORM_LOAD:
                            CargarCFLA(FormUID);
                            CargarCFLB(FormUID);
                            CargarCFLC(FormUID);
                            CargarCFLD(FormUID);
                            CargarCFLE(FormUID);
                            CargarCFLF(FormUID);
                            CargarCFLG(FormUID);
                            CargarCFLH(FormUID);
                            CargarCFLI(FormUID);
                            CargarCFLJ(FormUID);
                            CargarCFLK(FormUID);
                            CargarCFLL(FormUID);
                            EditarFormulario(FormUID);
                            break;
                    }
                }
                else
                {
                    switch(pVal.EventType)
                    {
                        case SAPbouiCOM.BoEventTypes.et_CHOOSE_FROM_LIST:
                            SAPbouiCOM.IChooseFromListEvent oCho = null;
                            oform = Comunes.ConexiónSAPB1.oSAPB1appl.Forms.Item(FormUID);
                            oCho = (SAPbouiCOM.IChooseFromListEvent)pVal;
                            SAPbouiCOM.DataTable oDt = null;
                            if (oCho.ChooseFromListUID=="AB_1" && pVal.ItemUID =="CUITPrim")
                            {
                                oDt = oCho.SelectedObjects;
                                SAPbouiCOM.EditText oEdit = null;
                                string val = oDt.GetValue("CardName", 0).ToString();
                                oEdit = (SAPbouiCOM.EditText)oForm.Items.Item("Item_30").Specific;
                                oEdit.Value = val;
                            }
                            if (oCho.ChooseFromListUID == "AB_2" && pVal.ItemUID == "CUITSec")
                            {
                                oDt = oCho.SelectedObjects;
                                SAPbouiCOM.EditText oEdit = null;
                                string val = oDt.GetValue("CardName", 0).ToString();
                                oEdit = (SAPbouiCOM.EditText)oForm.Items.Item("Item_34").Specific;
                                oEdit.Value = val;
                            }
                            if (oCho.ChooseFromListUID == "AB_3" && pVal.ItemUID == "CUITSec2")
                            {
                                oDt = oCho.SelectedObjects;
                                SAPbouiCOM.EditText oEdit = null;
                                string val = oDt.GetValue("CardName", 0).ToString();
                                oEdit = (SAPbouiCOM.EditText)oForm.Items.Item("Item_39").Specific;
                                oEdit.Value = val;
                            }
                            if (oCho.ChooseFromListUID == "AB_4" && pVal.ItemUID == "CUITMit")
                            {
                                oDt = oCho.SelectedObjects;
                                SAPbouiCOM.EditText oEdit = null;
                                string val = oDt.GetValue("CardName", 0).ToString();
                                oEdit = (SAPbouiCOM.EditText)oForm.Items.Item("Item_42").Specific;
                                oEdit.Value = val;
                            }
                            if (oCho.ChooseFromListUID == "AB_5" && pVal.ItemUID == "CUITVPr")
                            {
                                oDt = oCho.SelectedObjects;
                                SAPbouiCOM.EditText oEdit = null;
                                string val = oDt.GetValue("CardName", 0).ToString();
                                oEdit = (SAPbouiCOM.EditText)oForm.Items.Item("Item_46").Specific;
                                oEdit.Value = val;
                            }
                            if (oCho.ChooseFromListUID == "AB_6" && pVal.ItemUID == "CUITVSe")
                            {
                                oDt = oCho.SelectedObjects;
                                SAPbouiCOM.EditText oEdit = null;
                                string val = oDt.GetValue("CardName", 0).ToString();
                                oEdit = (SAPbouiCOM.EditText)oForm.Items.Item("Item_51").Specific;
                                oEdit.Value = val;
                            }
                            if (oCho.ChooseFromListUID == "AB_7" && pVal.ItemUID == "CUITRen")
                            {
                                oDt = oCho.SelectedObjects;
                                SAPbouiCOM.EditText oEdit = null;
                                string val = oDt.GetValue("CardName", 0).ToString();
                                oEdit = (SAPbouiCOM.EditText)oForm.Items.Item("Item_54").Specific;
                                oEdit.Value = val;
                            }
                            if (oCho.ChooseFromListUID == "AB_8" && pVal.ItemUID == "CUITRre")
                            {
                                oDt = oCho.SelectedObjects;
                                SAPbouiCOM.EditText oEdit = null;
                                string val = oDt.GetValue("CardName", 0).ToString();
                                oEdit = (SAPbouiCOM.EditText)oForm.Items.Item("Item_122").Specific;
                                oEdit.Value = val;
                            }
                            if (oCho.ChooseFromListUID == "AB_9" && pVal.ItemUID == "CUITTran")
                            {
                                oDt = oCho.SelectedObjects;
                                SAPbouiCOM.EditText oEdit = null;
                                string val = oDt.GetValue("CardName", 0).ToString();
                                oEdit = (SAPbouiCOM.EditText)oForm.Items.Item("Item_23").Specific;
                                oEdit.Value = val;
                            }
                            if (oCho.ChooseFromListUID == "AB_10" && pVal.ItemUID == "CUITDes")
                            {
                                oDt = oCho.SelectedObjects;
                                SAPbouiCOM.EditText oEdit = null;
                                string val = oDt.GetValue("CardName", 0).ToString();
                                oEdit = (SAPbouiCOM.EditText)oForm.Items.Item("Item_86").Specific;
                                oEdit.Value = val;
                            }
                            if (oCho.ChooseFromListUID == "AB_11" && pVal.ItemUID == "CUITDestina")
                            {
                                oDt = oCho.SelectedObjects;
                                SAPbouiCOM.EditText oEdit = null;
                                string val = oDt.GetValue("CardName", 0).ToString();
                                oEdit = (SAPbouiCOM.EditText)oForm.Items.Item("Item_103").Specific;
                                oEdit.Value = val;
                            }
                            if (oCho.ChooseFromListUID == "AB_12" && pVal.ItemUID == "CUITPFlete")
                            {
                                
                                oDt = oCho.SelectedObjects;
                                SAPbouiCOM.EditText oEdit = null;
                                string val = oDt.GetValue("CardName", 0).ToString();
                                oEdit = (SAPbouiCOM.EditText)oForm.Items.Item("Item_118").Specific;
                                oEdit.Value = val;
                            }
                            break;
                        case SAPbouiCOM.BoEventTypes.et_FORM_DATA_ADD:
                          
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
            string Ubicacion = "";
            string PreUbicacion = "";
            string Contrato = "";
            string Observaciones = "";



            try
            {

                oform = Comunes.ConexiónSAPB1.oSAPB1appl.Forms.Item(FormUID);

                if (Objeto.Respuesta.Cabecera != null)
                {

                    int IndexContrato = Objeto.Respuesta.Cabecera.Observaciones.ToLower().IndexOf("cto:");
                    int IndexUbicacion = Objeto.Respuesta.Cabecera.Observaciones.ToLower().IndexOf("ubi:");
                    int largo = Objeto.Respuesta.Cabecera.Observaciones.Length;
                    if (IndexContrato <=0)
                    {
                        Contrato = "";
                    }
                    else
                    {
                        string[] palabra = Objeto.Respuesta.Cabecera.Observaciones.ToLower().Split("ubi:");
                        Contrato = palabra[0].Replace(" ", string.Empty).Remove(IndexContrato, 4);
                    }
                    if (IndexUbicacion <=0)
                    {
                        Ubicacion = "";
                    }
                    else
                    {
                        string[] palabra = Objeto.Respuesta.Cabecera.Observaciones.ToLower().Split("ubi:");
                        string[] ubicacion = palabra[1].Split(" ");
                        if (String.IsNullOrEmpty(ubicacion[0]))
                        {
                            PreUbicacion = ubicacion[1].Replace(" ", string.Empty);
                            Ubicacion = PreUbicacion.Split(" ")[0];
                        }
                        else
                        {
                            PreUbicacion = ubicacion[0].Replace(" ", string.Empty);
                            Ubicacion = PreUbicacion.Split(" ")[0];

                        }
                    }
                    ((SAPbouiCOM.EditText)oform.Items.Item("Item_18").Specific).Value = @"\\br01-srv-db02\B1_SHF\Adjuntos\CartasPorte\" + Objeto.Respuesta.Cabecera.NroCTG + ".pdf";
                    ((SAPbouiCOM.EditText)oform.Items.Item("Item_37").Specific).Value = Objeto.Respuesta.Cabecera.Observaciones.ToString();
                    ((SAPbouiCOM.EditText)oform.Items.Item("Item_24").Specific).Value = Objeto.Respuesta.Cabecera.Sucursal.ToString();
                    ((SAPbouiCOM.EditText)oform.Items.Item("Item_25").Specific).Value = Objeto.Respuesta.Cabecera.NroOrden.ToString();

                    ((SAPbouiCOM.EditText)oform.Items.Item("Item_28").Specific).Value = Ubicacion.ToUpper();
                    ((SAPbouiCOM.EditText)oform.Items.Item("Item_112").Specific).Value = Contrato.ToUpper();
                    ((SAPbouiCOM.EditText)oform.Items.Item("Item_62").Specific).Value = Objeto.Respuesta.Transporte.Dominio[0].ToString();
                    ((SAPbouiCOM.EditText)oform.Items.Item("Item_64").Specific).Value = Objeto.Respuesta.Transporte.Dominio[1].ToString();
                    ((SAPbouiCOM.EditText)oform.Items.Item("Item_57").Specific).Value = Objeto.Respuesta.Transporte.CuitChofer.ToString();
                    ((SAPbouiCOM.EditText)oform.Items.Item("CUITTran").Specific).Value = Objeto.Respuesta.Transporte.CuitTransportista.ToString();
                    ((SAPbouiCOM.EditText)oform.Items.Item("Item_74").Specific).Value = Objeto.Respuesta.DatosCarga.Cosecha.ToString();
                    ((SAPbouiCOM.EditText)oform.Items.Item("Item_76").Specific).Value = Objeto.Respuesta.DatosCarga.PesoBruto.ToString();
                    ((SAPbouiCOM.EditText)oform.Items.Item("Item_78").Specific).Value = Objeto.Respuesta.DatosCarga.PesoTara.ToString();
                    ((SAPbouiCOM.EditText)oform.Items.Item("Item_72").Specific).Value = Objeto.Respuesta.DatosCarga.CodGrano.ToString();
                    ((SAPbouiCOM.EditText)oform.Items.Item("Item_80").Specific).Value = Convert.ToString((Objeto.Respuesta.DatosCarga.PesoBruto - Objeto.Respuesta.DatosCarga.PesoTara));

                    //Destino
                    ((SAPbouiCOM.EditText)oform.Items.Item("CUITDes").Specific).Value = Objeto.Respuesta.Destino.Cuit.ToString();
                    ((SAPbouiCOM.EditText)oform.Items.Item("CUITDesa").Specific).Value = Objeto.Respuesta.Destinatario.Cuit.ToString();
                    //((SAPbouiCOM.EditText)oform.Items.Item("CUITDesa").Specific).Value = Objeto.Respuesta.Destinatario.Cuit.ToString();
                    /*((SAPbouiCOM.EditText)oform.Items.Item("Item_106").Specific).Value = Convert.ToDateTime(Objeto.Respuesta.Transporte.FechaHoraPartida).ToString();*/
                    ;
                    ((SAPbouiCOM.EditText)oform.Items.Item("Item_108").Specific).Value = Objeto.Respuesta.Transporte.KmRecorrer.ToString();
                    ((SAPbouiCOM.EditText)oform.Items.Item("Item_110").Specific).Value = Objeto.Respuesta.Transporte.TarifaReferencia.ToString();
                    ((SAPbouiCOM.EditText)oform.Items.Item("Item_114").Specific).Value = Objeto.Respuesta.Transporte.Tarifa.ToString();
                    ((SAPbouiCOM.EditText)oform.Items.Item("Item_96").Specific).Value = Objeto.Respuesta.Destino.Planta.ToString();
                    //((SAPbouiCOM.EditText)oform.Items.Item("Item_92").Specific).Value = Objeto.Respuesta.Destino.CodProvincia.ToString();
                    //((SAPbouiCOM.ComboBox)oform.Items.Item("Item_92").Specific).Select(Objeto.Respuesta.Destino.CodProvincia, SAPbouiCOM.BoSearchKey.psk_ByValue);
                    ((SAPbouiCOM.EditText)oform.Items.Item("Item_94").Specific).Value = Objeto.Respuesta.Destino.CodLocalidad.ToString();

                    //Origen
                    ((SAPbouiCOM.EditText)oform.Items.Item("Item_83").Specific).Value = Objeto.Respuesta.Cabecera.NroCTG.ToString();
                    ((SAPbouiCOM.EditText)oform.Items.Item("Item_21").Specific).Value = Objeto.Respuesta.Origen.CodProvincia.ToString();
                    ((SAPbouiCOM.EditText)oform.Items.Item("Item_20").Specific).Value = Objeto.Respuesta.Origen.CodLocalidad.ToString();
                    if (Objeto.Respuesta.Intervinientes.CuitRemitenteComercialVentaPrimaria != 0)
                        ((SAPbouiCOM.EditText)oform.Items.Item("CUITPrim").Specific).Value = Objeto.Respuesta.Intervinientes.CuitRemitenteComercialVentaPrimaria.ToString();
                    if (Objeto.Respuesta.Intervinientes.CuitRemitenteComercialVentaSecundaria != 0)
                        ((SAPbouiCOM.EditText)oform.Items.Item("CUITSec").Specific).Value = Objeto.Respuesta.Intervinientes.CuitRemitenteComercialVentaSecundaria.ToString();
                    if (Objeto.Respuesta.Intervinientes.CuitRemitenteComercialVentaSecundaria2 != 0)
                        ((SAPbouiCOM.EditText)oform.Items.Item("CUITSec2").Specific).Value = Objeto.Respuesta.Intervinientes.CuitRemitenteComercialVentaSecundaria2.ToString();
                    if (Objeto.Respuesta.Intervinientes.cuitMercadoATermino != 0)
                        ((SAPbouiCOM.EditText)oform.Items.Item("CUITMit").Specific).Value = Objeto.Respuesta.Intervinientes.cuitMercadoATermino.ToString();
                    if (Objeto.Respuesta.Intervinientes.CuitCorredorVentaPrimaria != 0)
                        ((SAPbouiCOM.EditText)oform.Items.Item("CUITVPr").Specific).Value = Objeto.Respuesta.Intervinientes.CuitCorredorVentaPrimaria.ToString();
                    if (Objeto.Respuesta.Intervinientes.CuitCorredorVentaSecundaria != 0)
                        ((SAPbouiCOM.EditText)oform.Items.Item("CUITVSe").Specific).Value = Objeto.Respuesta.Intervinientes.CuitCorredorVentaSecundaria.ToString();
                    if (Objeto.Respuesta.Intervinientes.cuitRepresentanteEntregador != 0)
                        ((SAPbouiCOM.EditText)oform.Items.Item("CUITRen").Specific).Value = Objeto.Respuesta.Intervinientes.cuitRepresentanteEntregador.ToString();
                    if (Objeto.Respuesta.Intervinientes.cuitRepresentanteRecibidor != 0)
                        ((SAPbouiCOM.EditText)oform.Items.Item("CUITRre").Specific).Value = Objeto.Respuesta.Intervinientes.cuitRepresentanteRecibidor.ToString();
                      
                    //System.Diagnostics.Process.Start("//br01-srv-db02/B1_SHF/Adjuntos/CartasPorte/" + Objeto.Respuesta.Cabecera.NroCTG + ".pdf");
                    //System.Diagnostics.Process.Start(@"D:\Users\frivera\source\repos\ModuloGrano\ModuloGrano\bin\x64\Debug\net6.0 - windows\" + Objeto.Respuesta.Cabecera.NroCTG + ".pdf");
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

        }

        private void CargarCFLA(string FormUID)
        {
            SAPbouiCOM.ChooseFromListCollection oCFLs = null;
            SAPbouiCOM.Conditions oCons = null;
            SAPbouiCOM.Condition oCon = null;
            SAPbouiCOM.Condition oCon2 = null;
            SAPbouiCOM.ChooseFromList oCfl = null;
            SAPbouiCOM.ChooseFromListCreationParams oCflParams = null;
            try
            {
                oForm = Comunes.ConexiónSAPB1.oSAPB1appl.Forms.Item(FormUID);
                oCFLs = oForm.ChooseFromLists;
                oCflParams = (SAPbouiCOM.ChooseFromListCreationParams)Comunes.ConexiónSAPB1.oSAPB1appl.CreateObject(SAPbouiCOM.BoCreatableObjectType.cot_ChooseFromListCreationParams);
                oCflParams.MultiSelection = false;
                oCflParams.ObjectType = "2";
                oCflParams.UniqueID = "AB_1";
                oCfl = oCFLs.Add(oCflParams);


                oCons = oCfl.GetConditions();


                oCon = oCons.Add();
                oCon.BracketOpenNum = 2;
                oCon.Alias = "U_TCAcopiador";
                oCon.Operation = SAPbouiCOM.BoConditionOperation.co_EQUAL;
                oCon.CondVal = "1";
                oCon.BracketCloseNum = 1;
                oCon.Relationship = SAPbouiCOM.BoConditionRelationship.cr_AND;

                oCon = oCons.Add();
                oCon.BracketOpenNum = 1;
                oCon.Alias = "CardType";
                oCon.Operation = SAPbouiCOM.BoConditionOperation.co_EQUAL;
                oCon.CondVal = "C";
                oCon.BracketCloseNum = 2;


                oCfl.SetConditions(oCons);


            }
            catch (Exception)
            {

                throw;
            }
        }
        private void CargarCFLB(string FormUID)
        {
            SAPbouiCOM.ChooseFromListCollection oCFLs = null;
            SAPbouiCOM.Conditions oCons = null;
            SAPbouiCOM.Condition oCon = null;
            SAPbouiCOM.Condition oCon2 = null;
            SAPbouiCOM.ChooseFromList oCfl = null;
            SAPbouiCOM.ChooseFromListCreationParams oCflParams = null;
            try
            {
                oForm = Comunes.ConexiónSAPB1.oSAPB1appl.Forms.Item(FormUID);
                oCFLs = oForm.ChooseFromLists;
                oCflParams = (SAPbouiCOM.ChooseFromListCreationParams)Comunes.ConexiónSAPB1.oSAPB1appl.CreateObject(SAPbouiCOM.BoCreatableObjectType.cot_ChooseFromListCreationParams);
                oCflParams.MultiSelection = false;
                oCflParams.ObjectType = "2";
                oCflParams.UniqueID = "AB_2";
                oCfl = oCFLs.Add(oCflParams);


                oCons = oCfl.GetConditions();


                oCon = oCons.Add();
                oCon.BracketOpenNum = 2;
                oCon.Alias = "U_TCAcopiador";
                oCon.Operation = SAPbouiCOM.BoConditionOperation.co_EQUAL;
                oCon.CondVal = "1";
                oCon.BracketCloseNum = 1;
                oCon.Relationship = SAPbouiCOM.BoConditionRelationship.cr_AND;

                oCon = oCons.Add();
                oCon.BracketOpenNum = 1;
                oCon.Alias = "CardType";
                oCon.Operation = SAPbouiCOM.BoConditionOperation.co_EQUAL;
                oCon.CondVal = "C";
                oCon.BracketCloseNum = 2;
                oCfl.SetConditions(oCons);


            }
            catch (Exception)
            {

                throw;
            }
        }
        private void CargarCFLC(string FormUID)
        {
            SAPbouiCOM.ChooseFromListCollection oCFLs = null;
            SAPbouiCOM.Conditions oCons = null;
            SAPbouiCOM.Condition oCon = null;
            SAPbouiCOM.Condition oCon2 = null;
            SAPbouiCOM.ChooseFromList oCfl = null;
            SAPbouiCOM.ChooseFromListCreationParams oCflParams = null;
            try
            {
                oForm = Comunes.ConexiónSAPB1.oSAPB1appl.Forms.Item(FormUID);
                oCFLs = oForm.ChooseFromLists;
                oCflParams = (SAPbouiCOM.ChooseFromListCreationParams)Comunes.ConexiónSAPB1.oSAPB1appl.CreateObject(SAPbouiCOM.BoCreatableObjectType.cot_ChooseFromListCreationParams);
                oCflParams.MultiSelection = false;
                oCflParams.ObjectType = "2";
                oCflParams.UniqueID = "AB_3";
                oCfl = oCFLs.Add(oCflParams);


                oCons = oCfl.GetConditions();


                oCon = oCons.Add();
                oCon.BracketOpenNum = 2;
                oCon.Alias = "U_TCAcopiador";
                oCon.Operation = SAPbouiCOM.BoConditionOperation.co_EQUAL;
                oCon.CondVal = "1";
                oCon.BracketCloseNum = 1;
                oCon.Relationship = SAPbouiCOM.BoConditionRelationship.cr_AND;

                oCon = oCons.Add();
                oCon.BracketOpenNum = 1;
                oCon.Alias = "CardType";
                oCon.Operation = SAPbouiCOM.BoConditionOperation.co_EQUAL;
                oCon.CondVal = "C";
                oCon.BracketCloseNum = 2;
                oCfl.SetConditions(oCons);


            }
            catch (Exception)
            {

                throw;
            }
        }
        private void CargarCFLD(string FormUID)
        {
            SAPbouiCOM.ChooseFromListCollection oCFLs = null;
            SAPbouiCOM.Conditions oCons = null;
            SAPbouiCOM.Condition oCon = null;
            SAPbouiCOM.Condition oCon2 = null;
            SAPbouiCOM.ChooseFromList oCfl = null;
            SAPbouiCOM.ChooseFromListCreationParams oCflParams = null;
            try
            {
                oForm = Comunes.ConexiónSAPB1.oSAPB1appl.Forms.Item(FormUID);
                oCFLs = oForm.ChooseFromLists;
                oCflParams = (SAPbouiCOM.ChooseFromListCreationParams)Comunes.ConexiónSAPB1.oSAPB1appl.CreateObject(SAPbouiCOM.BoCreatableObjectType.cot_ChooseFromListCreationParams);
                oCflParams.MultiSelection = false;
                oCflParams.ObjectType = "2";
                oCflParams.UniqueID = "AB_4";
                oCfl = oCFLs.Add(oCflParams);


                oCons = oCfl.GetConditions();


                oCon = oCons.Add();
                oCon.BracketOpenNum = 2;
                oCon.Alias = "U_TCAcopiador";
                oCon.Operation = SAPbouiCOM.BoConditionOperation.co_EQUAL;
                oCon.CondVal = "1";
                oCon.BracketCloseNum = 1;
                oCon.Relationship = SAPbouiCOM.BoConditionRelationship.cr_AND;

                oCon = oCons.Add();
                oCon.BracketOpenNum = 1;
                oCon.Alias = "CardType";
                oCon.Operation = SAPbouiCOM.BoConditionOperation.co_EQUAL;
                oCon.CondVal = "C";
                oCon.BracketCloseNum = 2;

                oCfl.SetConditions(oCons);


            }
            catch (Exception)
            {

                throw;
            }
        }

        private void CargarCFLE(string FormUID)
        {
            SAPbouiCOM.ChooseFromListCollection oCFLs = null;
            SAPbouiCOM.Conditions oCons = null;
            SAPbouiCOM.Condition oCon = null;
            SAPbouiCOM.Condition oCon2 = null;
            SAPbouiCOM.ChooseFromList oCfl = null;
            SAPbouiCOM.ChooseFromListCreationParams oCflParams = null;
            try
            {
                oForm = Comunes.ConexiónSAPB1.oSAPB1appl.Forms.Item(FormUID);
                oCFLs = oForm.ChooseFromLists;
                oCflParams = (SAPbouiCOM.ChooseFromListCreationParams)Comunes.ConexiónSAPB1.oSAPB1appl.CreateObject(SAPbouiCOM.BoCreatableObjectType.cot_ChooseFromListCreationParams);
                oCflParams.MultiSelection = false;
                oCflParams.ObjectType = "2";
                oCflParams.UniqueID = "AB_5";
                oCfl = oCFLs.Add(oCflParams);


                oCons = oCfl.GetConditions();


                oCon = oCons.Add();
                oCon.BracketOpenNum = 2;
                oCon.Alias = "U_TCAcopiador";
                oCon.Operation = SAPbouiCOM.BoConditionOperation.co_EQUAL;
                oCon.CondVal = "1";
                oCon.BracketCloseNum = 1;
                oCon.Relationship = SAPbouiCOM.BoConditionRelationship.cr_AND;

                oCon = oCons.Add();
                oCon.BracketOpenNum = 1;
                oCon.Alias = "CardType";
                oCon.Operation = SAPbouiCOM.BoConditionOperation.co_EQUAL;
                oCon.CondVal = "C";
                oCon.BracketCloseNum = 2;

                oCfl.SetConditions(oCons);


            }
            catch (Exception)
            {

                throw;
            }
        }
        private void CargarCFLF(string FormUID)
        {
            SAPbouiCOM.ChooseFromListCollection oCFLs = null;
            SAPbouiCOM.Conditions oCons = null;
            SAPbouiCOM.Condition oCon = null;
            SAPbouiCOM.Condition oCon2 = null;
            SAPbouiCOM.ChooseFromList oCfl = null;
            SAPbouiCOM.ChooseFromListCreationParams oCflParams = null;
            try
            {
                oForm = Comunes.ConexiónSAPB1.oSAPB1appl.Forms.Item(FormUID);
                oCFLs = oForm.ChooseFromLists;
                oCflParams = (SAPbouiCOM.ChooseFromListCreationParams)Comunes.ConexiónSAPB1.oSAPB1appl.CreateObject(SAPbouiCOM.BoCreatableObjectType.cot_ChooseFromListCreationParams);
                oCflParams.MultiSelection = false;
                oCflParams.ObjectType = "2";
                oCflParams.UniqueID = "AB_6";
                oCfl = oCFLs.Add(oCflParams);


                oCons = oCfl.GetConditions();


                oCon = oCons.Add();
                oCon.BracketOpenNum = 2;
                oCon.Alias = "U_TCAcopiador";
                oCon.Operation = SAPbouiCOM.BoConditionOperation.co_EQUAL;
                oCon.CondVal = "1";
                oCon.BracketCloseNum = 1;
                oCon.Relationship = SAPbouiCOM.BoConditionRelationship.cr_AND;

                oCon = oCons.Add();
                oCon.BracketOpenNum = 1;
                oCon.Alias = "CardType";
                oCon.Operation = SAPbouiCOM.BoConditionOperation.co_EQUAL;
                oCon.CondVal = "C";
                oCon.BracketCloseNum = 2;

                oCfl.SetConditions(oCons);


            }
            catch (Exception)
            {

                throw;
            }
        }
        private void CargarCFLG(string FormUID)
        {
            SAPbouiCOM.ChooseFromListCollection oCFLs = null;
            SAPbouiCOM.Conditions oCons = null;
            SAPbouiCOM.Condition oCon = null;
            SAPbouiCOM.Condition oCon2 = null;
            SAPbouiCOM.ChooseFromList oCfl = null;
            SAPbouiCOM.ChooseFromListCreationParams oCflParams = null;
            try
            {
                oForm = Comunes.ConexiónSAPB1.oSAPB1appl.Forms.Item(FormUID);
                oCFLs = oForm.ChooseFromLists;
                oCflParams = (SAPbouiCOM.ChooseFromListCreationParams)Comunes.ConexiónSAPB1.oSAPB1appl.CreateObject(SAPbouiCOM.BoCreatableObjectType.cot_ChooseFromListCreationParams);
                oCflParams.MultiSelection = false;
                oCflParams.ObjectType = "2";
                oCflParams.UniqueID = "AB_7";
                oCfl = oCFLs.Add(oCflParams);


                oCons = oCfl.GetConditions();


                oCon = oCons.Add();
                oCon.BracketOpenNum = 2;
                oCon.Alias = "U_TCAcopiador";
                oCon.Operation = SAPbouiCOM.BoConditionOperation.co_EQUAL;
                oCon.CondVal = "1";
                oCon.BracketCloseNum = 1;
                oCon.Relationship = SAPbouiCOM.BoConditionRelationship.cr_AND;

                oCon = oCons.Add();
                oCon.BracketOpenNum = 1;
                oCon.Alias = "CardType";
                oCon.Operation = SAPbouiCOM.BoConditionOperation.co_EQUAL;
                oCon.CondVal = "C";
                oCon.BracketCloseNum = 2;

                oCfl.SetConditions(oCons);


            }
            catch (Exception)
            {

                throw;
            }
        }
        private void CargarCFLH(string FormUID)
        {
            SAPbouiCOM.ChooseFromListCollection oCFLs = null;
            SAPbouiCOM.Conditions oCons = null;
            SAPbouiCOM.Condition oCon = null;
            SAPbouiCOM.Condition oCon2 = null;
            SAPbouiCOM.ChooseFromList oCfl = null;
            SAPbouiCOM.ChooseFromListCreationParams oCflParams = null;
            try
            {
                oForm = Comunes.ConexiónSAPB1.oSAPB1appl.Forms.Item(FormUID);
                oCFLs = oForm.ChooseFromLists;
                oCflParams = (SAPbouiCOM.ChooseFromListCreationParams)Comunes.ConexiónSAPB1.oSAPB1appl.CreateObject(SAPbouiCOM.BoCreatableObjectType.cot_ChooseFromListCreationParams);
                oCflParams.MultiSelection = false;
                oCflParams.ObjectType = "2";
                oCflParams.UniqueID = "AB_8";
                oCfl = oCFLs.Add(oCflParams);


                oCons = oCfl.GetConditions();


                oCon = oCons.Add();
                oCon.BracketOpenNum = 2;
                oCon.Alias = "U_TCAcopiador";
                oCon.Operation = SAPbouiCOM.BoConditionOperation.co_EQUAL;
                oCon.CondVal = "1";
                oCon.BracketCloseNum = 1;
                oCon.Relationship = SAPbouiCOM.BoConditionRelationship.cr_AND;

                oCon = oCons.Add();
                oCon.BracketOpenNum = 1;
                oCon.Alias = "CardType";
                oCon.Operation = SAPbouiCOM.BoConditionOperation.co_EQUAL;
                oCon.CondVal = "C";
                oCon.BracketCloseNum = 2;

                oCfl.SetConditions(oCons);


            }
            catch (Exception)
            {

                throw;
            }
        }
        private void CargarCFLI(string FormUID)
        {
            SAPbouiCOM.ChooseFromListCollection oCFLs = null;
            SAPbouiCOM.Conditions oCons = null;
            SAPbouiCOM.Condition oCon = null;
            SAPbouiCOM.Condition oCon2 = null;
            SAPbouiCOM.ChooseFromList oCfl = null;
            SAPbouiCOM.ChooseFromListCreationParams oCflParams = null;
            try
            {
                oForm = Comunes.ConexiónSAPB1.oSAPB1appl.Forms.Item(FormUID);
                oCFLs = oForm.ChooseFromLists;
                oCflParams = (SAPbouiCOM.ChooseFromListCreationParams)Comunes.ConexiónSAPB1.oSAPB1appl.CreateObject(SAPbouiCOM.BoCreatableObjectType.cot_ChooseFromListCreationParams);
                oCflParams.MultiSelection = false;
                oCflParams.ObjectType = "2";
                oCflParams.UniqueID = "AB_9";
                oCfl = oCFLs.Add(oCflParams);


                oCons = oCfl.GetConditions();


                oCon = oCons.Add();
                oCon.BracketOpenNum = 2;
                oCon.Alias = "U_TCFletero";
                oCon.Operation = SAPbouiCOM.BoConditionOperation.co_EQUAL;
                oCon.CondVal = "1";
                oCon.BracketCloseNum = 1;
                oCon.Relationship = SAPbouiCOM.BoConditionRelationship.cr_AND;

                oCon = oCons.Add();
                oCon.BracketOpenNum = 1;
                oCon.Alias = "CardType";
                oCon.Operation = SAPbouiCOM.BoConditionOperation.co_EQUAL;
                oCon.CondVal = "S";
                oCon.BracketCloseNum = 2;


                oCfl.SetConditions(oCons);


            }
            catch (Exception)
            {

                throw;
            }
        }
        private void CargarCFLJ(string FormUID)
        {
            SAPbouiCOM.ChooseFromListCollection oCFLs = null;
            SAPbouiCOM.Conditions oCons = null;
            SAPbouiCOM.Condition oCon = null;
            SAPbouiCOM.Condition oCon2 = null;
            SAPbouiCOM.ChooseFromList oCfl = null;
            SAPbouiCOM.ChooseFromListCreationParams oCflParams = null;
            try
            {
                oForm = Comunes.ConexiónSAPB1.oSAPB1appl.Forms.Item(FormUID);
                oCFLs = oForm.ChooseFromLists;
                oCflParams = (SAPbouiCOM.ChooseFromListCreationParams)Comunes.ConexiónSAPB1.oSAPB1appl.CreateObject(SAPbouiCOM.BoCreatableObjectType.cot_ChooseFromListCreationParams);
                oCflParams.MultiSelection = false;
                oCflParams.ObjectType = "2";
                oCflParams.UniqueID = "AB_10";
                oCfl = oCFLs.Add(oCflParams);


                oCons = oCfl.GetConditions();


                oCon = oCons.Add();
                oCon.BracketOpenNum = 2;
                oCon.Alias = "U_TCAcopiador";
                oCon.Operation = SAPbouiCOM.BoConditionOperation.co_EQUAL;
                oCon.CondVal = "1";
                oCon.BracketCloseNum = 1;
                oCon.Relationship = SAPbouiCOM.BoConditionRelationship.cr_AND;

                oCon = oCons.Add();
                oCon.BracketOpenNum = 1;
                oCon.Alias = "CardType";
                oCon.Operation = SAPbouiCOM.BoConditionOperation.co_EQUAL;
                oCon.CondVal = "C";
                oCon.BracketCloseNum = 2;

                oCfl.SetConditions(oCons);


            }
            catch (Exception)
            {

                throw;
            }
        }
        private void CargarCFLK(string FormUID)
        {
            SAPbouiCOM.ChooseFromListCollection oCFLs = null;
            SAPbouiCOM.Conditions oCons = null;
            SAPbouiCOM.Condition oCon = null;
            SAPbouiCOM.Condition oCon2 = null;
            SAPbouiCOM.ChooseFromList oCfl = null;
            SAPbouiCOM.ChooseFromListCreationParams oCflParams = null;
            try
            {
                oForm = Comunes.ConexiónSAPB1.oSAPB1appl.Forms.Item(FormUID);
                oCFLs = oForm.ChooseFromLists;
                oCflParams = (SAPbouiCOM.ChooseFromListCreationParams)Comunes.ConexiónSAPB1.oSAPB1appl.CreateObject(SAPbouiCOM.BoCreatableObjectType.cot_ChooseFromListCreationParams);
                oCflParams.MultiSelection = false;
                oCflParams.ObjectType = "2";
                oCflParams.UniqueID = "AB_11";
                oCfl = oCFLs.Add(oCflParams);


                oCons = oCfl.GetConditions();


                oCon = oCons.Add();
                oCon.BracketOpenNum = 2;
                oCon.Alias = "U_TCAcopiador";
                oCon.Operation = SAPbouiCOM.BoConditionOperation.co_EQUAL;
                oCon.CondVal = "1";
                oCon.BracketCloseNum = 1;
                oCon.Relationship = SAPbouiCOM.BoConditionRelationship.cr_AND;

                oCon = oCons.Add();
                oCon.BracketOpenNum = 1;
                oCon.Alias = "CardType";
                oCon.Operation = SAPbouiCOM.BoConditionOperation.co_EQUAL;
                oCon.CondVal = "S";
                oCon.BracketCloseNum = 2;

                oCfl.SetConditions(oCons);


            }
            catch (Exception)
            {

                throw;
            }
        }
        private void CargarCFLL(string FormUID)
        {
            SAPbouiCOM.ChooseFromListCollection oCFLs = null;
            SAPbouiCOM.Conditions oCons = null;
            SAPbouiCOM.Condition oCon = null;
            SAPbouiCOM.Condition oCon2 = null;
            SAPbouiCOM.ChooseFromList oCfl = null;
            SAPbouiCOM.ChooseFromListCreationParams oCflParams = null;
            try
            {
                oForm = Comunes.ConexiónSAPB1.oSAPB1appl.Forms.Item(FormUID);
                oCFLs = oForm.ChooseFromLists;
                oCflParams = (SAPbouiCOM.ChooseFromListCreationParams)Comunes.ConexiónSAPB1.oSAPB1appl.CreateObject(SAPbouiCOM.BoCreatableObjectType.cot_ChooseFromListCreationParams);
                oCflParams.MultiSelection = false;
                oCflParams.ObjectType = "2";
                oCflParams.UniqueID = "AB_12";
                oCfl = oCFLs.Add(oCflParams);


                oCons = oCfl.GetConditions();


                oCon = oCons.Add();
                oCon.BracketOpenNum = 2;
                oCon.Alias = "U_TCAcopiador";
                oCon.Operation = SAPbouiCOM.BoConditionOperation.co_EQUAL;
                oCon.CondVal = "1";
                oCon.BracketCloseNum = 1;
                oCon.Relationship = SAPbouiCOM.BoConditionRelationship.cr_AND;

                oCon = oCons.Add();
                oCon.BracketOpenNum = 1;
                oCon.Alias = "CardType";
                oCon.Operation = SAPbouiCOM.BoConditionOperation.co_EQUAL;
                oCon.CondVal = "S";
                oCon.BracketCloseNum = 2;

                oCfl.SetConditions(oCons);


            }
            catch (Exception)
            {

                throw;
            }
        }


        private void EditarFormulario(string FormUID)
        {
            try
            {
                oForm = Comunes.ConexiónSAPB1.oSAPB1appl.Forms.Item(FormUID);
                oForm.Items.Add("CUITPrim", SAPbouiCOM.BoFormItemTypes.it_EDIT);

                oItem = oForm.Items.Item("CUITPrim");

                oItem.Top = 93;
                oItem.Left = 638;
                oItem.Width = 210;
                oItem.Height = 14;
                oItem.FromPane = 1;
                oItem.ToPane = 1;
                oItem.Enabled = false;
                oEditText = (SAPbouiCOM.EditText)oForm.Items.Item("CUITPrim").Specific;
                oEditText.DataBind.SetBound(true, "@CARTAPORTE", "U_CUITRteVtaPrim");
                
                oEditText.ChooseFromListUID = "AB_1";
                oEditText.ChooseFromListAlias = "LicTradNum";
                
                

                //Srgundo edittext
                oForm.Items.Add("CUITSec", SAPbouiCOM.BoFormItemTypes.it_EDIT);
                oItem = oForm.Items.Item("CUITSec");

                oItem.Top = 160;
                oItem.Left = 638;
                oItem.Width = 210;
                oItem.Height = 14;
                oItem.FromPane = 1;
                oItem.ToPane = 1;
                oItem.Enabled = false;
                oEditText = (SAPbouiCOM.EditText)oForm.Items.Item("CUITSec").Specific;
                oEditText.DataBind.SetBound(true, "@CARTAPORTE", "U_CUITRteVtaSec");

                oEditText.ChooseFromListUID = "AB_2";
                oEditText.ChooseFromListAlias = "LicTradNum";

                //Srgundo edittext
                oForm.Items.Add("CUITSec2", SAPbouiCOM.BoFormItemTypes.it_EDIT);
                oItem = oForm.Items.Item("CUITSec2");

                oItem.Top = 228;
                oItem.Left = 638;
                oItem.Width = 210;
                oItem.Height = 14;
                oItem.FromPane = 1;
                oItem.ToPane = 1;
                oItem.Enabled = false;
                oEditText = (SAPbouiCOM.EditText)oForm.Items.Item("CUITSec2").Specific;
                oEditText.DataBind.SetBound(true, "@CARTAPORTE", "U_CUITRteVtaSec2");

                oEditText.ChooseFromListUID = "AB_3";
                oEditText.ChooseFromListAlias = "LicTradNum";

                //Srgundo edittext
                oForm.Items.Add("CUITMit", SAPbouiCOM.BoFormItemTypes.it_EDIT);
                oItem = oForm.Items.Item("CUITMit");

                oItem.Top = 282;
                oItem.Left = 638;
                oItem.Width = 210;
                oItem.Height = 14;
                oItem.FromPane = 1;
                oItem.ToPane = 1;
                oItem.Enabled = false;
                oEditText = (SAPbouiCOM.EditText)oForm.Items.Item("CUITMit").Specific;
                oEditText.DataBind.SetBound(true, "@CARTAPORTE", "U_CUITMAT");

                oEditText.ChooseFromListUID = "AB_4";
                oEditText.ChooseFromListAlias = "LicTradNum";

                //Srgundo edittext
                oForm.Items.Add("CUITVPr", SAPbouiCOM.BoFormItemTypes.it_EDIT);
                oItem = oForm.Items.Item("CUITVPr");

                oItem.Top = 342;
                oItem.Left = 638;
                oItem.Width = 210;
                oItem.Height = 14;
                oItem.FromPane = 1;
                oItem.ToPane = 1;
                oItem.Enabled = false;
                oEditText = (SAPbouiCOM.EditText)oForm.Items.Item("CUITVPr").Specific;
                oEditText.DataBind.SetBound(true, "@CARTAPORTE", "U_CUITCorrVtaPrim");

                oEditText.ChooseFromListUID = "AB_5";
                oEditText.ChooseFromListAlias = "LicTradNum";

                //Srgundo edittext
                oForm.Items.Add("CUITVSe", SAPbouiCOM.BoFormItemTypes.it_EDIT);
                oItem = oForm.Items.Item("CUITVSe");

                oItem.Top = 402;
                oItem.Left = 638;
                oItem.Width = 210;
                oItem.Height = 14;
                oItem.FromPane = 1;
                oItem.ToPane = 1;
                oItem.Enabled = false;
                oEditText = (SAPbouiCOM.EditText)oForm.Items.Item("CUITVSe").Specific;
                oEditText.DataBind.SetBound(true, "@CARTAPORTE", "U_CUITCorrVtaSec");

                oEditText.ChooseFromListUID = "AB_6";
                oEditText.ChooseFromListAlias = "LicTradNum";

                //Srgundo edittext
                oForm.Items.Add("CUITRen", SAPbouiCOM.BoFormItemTypes.it_EDIT);
                oItem = oForm.Items.Item("CUITRen");

                oItem.Top = 468;
                oItem.Left = 638;
                oItem.Width = 210;
                oItem.Height = 14;
                oItem.FromPane = 1;
                oItem.ToPane = 1;
                oItem.Enabled = false;
                oEditText = (SAPbouiCOM.EditText)oForm.Items.Item("CUITRen").Specific;
                oEditText.DataBind.SetBound(true, "@CARTAPORTE", "U_CUITEntregador");

                oEditText.ChooseFromListUID = "AB_7";
                oEditText.ChooseFromListAlias = "LicTradNum";

                //Srgundo edittext
                oForm.Items.Add("CUITRre", SAPbouiCOM.BoFormItemTypes.it_EDIT);
                oItem = oForm.Items.Item("CUITRre");

                oItem.Top = 518;
                oItem.Left = 638;
                oItem.Width = 210;
                oItem.Height = 14;
                oItem.FromPane = 1;
                oItem.ToPane = 1;
                oItem.Enabled = false;
                oEditText = (SAPbouiCOM.EditText)oForm.Items.Item("CUITRre").Specific;
                oEditText.DataBind.SetBound(true, "@CARTAPORTE", "U_CUITRecibidor");

                oEditText.ChooseFromListUID = "AB_8";
                oEditText.ChooseFromListAlias = "LicTradNum";

                //Transporte
                oForm.Items.Add("CUITTran", SAPbouiCOM.BoFormItemTypes.it_EDIT);

                oItem = oForm.Items.Item("CUITTran");

                oItem.Top = 129;
                oItem.Left = 50;
                oItem.Width = 210;
                oItem.Height = 14;
                oItem.FromPane = 2;
                oItem.ToPane = 2;
                oItem.Enabled = false;
                oEditText = (SAPbouiCOM.EditText)oForm.Items.Item("CUITTran").Specific;
                oEditText.DataBind.SetBound(true, "@CARTAPORTE", "U_CUITTransportista");

                oEditText.ChooseFromListUID = "AB_9";
                oEditText.ChooseFromListAlias = "LicTradNum";

                //Destino
                oForm.Items.Add("CUITDes", SAPbouiCOM.BoFormItemTypes.it_EDIT);

                oItem = oForm.Items.Item("CUITDes");

                oItem.Top = 143;
                oItem.Left = 58;
                oItem.Width = 210;
                oItem.Height = 14;
                oItem.FromPane = 3;
                oItem.ToPane = 3;
                oItem.Enabled = false;
                oEditText = (SAPbouiCOM.EditText)oForm.Items.Item("CUITDes").Specific;
                oEditText.DataBind.SetBound(true, "@CARTAPORTE", "U_CUITDestino");

                oEditText.ChooseFromListUID = "AB_10";
                oEditText.ChooseFromListAlias = "LicTradNum";

                //Destinatario
                oForm.Items.Add("CUITDesa", SAPbouiCOM.BoFormItemTypes.it_EDIT);

                oItem = oForm.Items.Item("CUITDesa");

                oItem.Top = 139;
                oItem.Left = 705;
                oItem.Width = 210;
                oItem.Height = 14;
                oItem.FromPane = 3;
                oItem.ToPane = 3;
                oItem.Enabled = false;
                oEditText = (SAPbouiCOM.EditText)oForm.Items.Item("CUITDesa").Specific;
                oEditText.DataBind.SetBound(true, "@CARTAPORTE", "U_CUITDestina");

                oEditText.ChooseFromListUID = "AB_11";
                oEditText.ChooseFromListAlias = "LicTradNum";

                //Pagador Flete
                oForm.Items.Add("CUITPFlete", SAPbouiCOM.BoFormItemTypes.it_EDIT);

                oItem = oForm.Items.Item("CUITPFlete");

                oItem.Top = 475;
                oItem.Left = 705;
                oItem.Width = 210;
                oItem.Height = 14;
                oItem.FromPane = 3;
                oItem.ToPane = 3;
                oItem.Enabled = false;
                oEditText = (SAPbouiCOM.EditText)oForm.Items.Item("CUITPFlete").Specific;
                oEditText.DataBind.SetBound(true, "@CARTAPORTE", "U_CUITPagadorFlete");

                oEditText.ChooseFromListUID = "AB_12";
                oEditText.ChooseFromListAlias = "LicTradNum";
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
                    int PesoNeto =Convert.ToInt32(((SAPbouiCOM.EditText)oForm.Items.Item("Item_80").Specific).Value);
                    oTransfer.U_PesoNeto = PesoNeto;

                    Error = ctrlTransf.AddTransferencia(oTransfer,Convert.ToInt16(DocEntry));
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

        //public void Exportxml(SAPbouiCOM.Form oForm)
        //{
        //    XmlDocument oxml = null;
        //    try
        //    {
        //        oxml = new XmlDocument();
        //        oxml.LoadXml(oForm.GetAsXML());
        //        oxml.Save(@"D:\Users\frivera\source\repos\ModuloGrano\ModuloGrano\bin\x64\Debug\Form.xml");
        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }
        //}
        #endregion
    }
}

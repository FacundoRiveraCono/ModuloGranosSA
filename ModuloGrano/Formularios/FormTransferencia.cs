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
            List<Objetos.Planta> oPlanta = new List<Planta>();
            List<Objetos.Planta> oPlantacmb = new List<Planta>();
            #region Controladores
            Controladores.Transferencia oTransf = new Controladores.Transferencia();
            Controladores.Entrega ctrlEntrega = new Controladores.Entrega();
            Controladores.PurchaseOrders ctrolPurchase = new Controladores.PurchaseOrders();
            #endregion
            #region Objetos
            Objetos.ConsultarCPEAutomotorResponse oConsultar = new Objetos.ConsultarCPEAutomotorResponse();
            Objetos.DeliveryNotes oDelivery = null;
            Controladores.CartaPorte oCartaPorte = new Controladores.CartaPorte();
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
                                    ((SAPbouiCOM.EditText)oform.Items.Item("Item_96").Specific).Item.Enabled = true;
                                    ((SAPbouiCOM.EditText)oform.Items.Item("Item_6").Specific).Item.Enabled = true;
                                    ((SAPbouiCOM.EditText)oform.Items.Item("Item_13").Specific).Item.Enabled = true;
                                    ((SAPbouiCOM.EditText)oform.Items.Item("Item_11").Specific).Item.Enabled = true;
                                }
                                else
                                {
                                    ((SAPbouiCOM.EditText)oform.Items.Item("Item_28").Specific).Item.Enabled = false;
                                    ((SAPbouiCOM.EditText)oform.Items.Item("Item_152").Specific).Item.Enabled = false;
                                    ((SAPbouiCOM.EditText)oform.Items.Item("Item_154").Specific).Item.Enabled = false;
                                    ((SAPbouiCOM.EditText)oform.Items.Item("Item_157").Specific).Item.Enabled = false;
                                    ((SAPbouiCOM.EditText)oform.Items.Item("Item_159").Specific).Item.Enabled = false;
                                    ((SAPbouiCOM.EditText)oform.Items.Item("Item_161").Specific).Item.Enabled = false;
                                    ((SAPbouiCOM.EditText)oform.Items.Item("Item_96").Specific).Item.Enabled = false;
                                    ((SAPbouiCOM.EditText)oform.Items.Item("Item_6").Specific).Item.Enabled = false;
                                    ((SAPbouiCOM.EditText)oform.Items.Item("Item_13").Specific).Item.Enabled = false;
                                    ((SAPbouiCOM.EditText)oform.Items.Item("Item_11").Specific).Item.Enabled = false;
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


                            if (pVal.ItemUID == "Item_94")
                            {
                                string valor = ((SAPbouiCOM.ButtonCombo)oform.Items.Item(pVal.ItemUID).Specific).Caption;

                                if (valor == "AFIP")
                                {
                                    var p = new Process();

                                    if (String.IsNullOrEmpty(((SAPbouiCOM.EditText)oform.Items.Item("Item_83").Specific).Value))
                                    {
                                        int offset = oform.DataSources.DBDataSources.Item(0).Offset;
                                        string DocEntry = oform.DataSources.DBDataSources.Item(0).GetValue("DocEntry", offset);

                                        error = oCartaPorte.ArmarEquivalencias(DocEntry);
                                        if (!String.IsNullOrEmpty(error))
                                            Comunes.ConexiónSAPB1.oSAPB1appl.MessageBox(error);
                                    }
                                    else
                                    {
                                        Comunes.ConexiónSAPB1.oSAPB1appl.MessageBox("CTG Existente, Imposible Reprocesar");
                                    }


                                }
                                if (valor == "SAP")
                                {
                                    try
                                    {
                                        oform = Comunes.ConexiónSAPB1.oSAPB1appl.Forms.Item(FormUID);
                                        //ExportToxml(oform);

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
                            }

                            break;

                    }
                }
                else
                {
                    switch (pVal.EventType)
                    {

                        case BoEventTypes.et_COMBO_SELECT:
                            if (pVal.ItemUID == "cmbPlanta")
                            {
                                //List<Objetos.Planta> oPlanta = new List<Planta>();
                                Controladores.CartaPorte oCpe = new Controladores.CartaPorte();
                                string planta = ((SAPbouiCOM.ComboBox)oform.Items.Item(pVal.ItemUID).Specific).Value;
                                oPlanta = oCpe.GetUbiPlantas(Convert.ToInt64(((SAPbouiCOM.EditText)oform.Items.Item("CUITI").Specific).Value), planta);
                                ((SAPbouiCOM.EditText)oform.Items.Item("Item_167").Specific).Value = oPlanta[0].Provincia.ToString();
                                ((SAPbouiCOM.EditText)oform.Items.Item("Item_170").Specific).Value = oPlanta[0].Localidad.ToString();
                                //Completo los valores de manera automatica


                            }
                            break;
                        case BoEventTypes.et_CHOOSE_FROM_LIST:
                            SAPbouiCOM.IChooseFromListEvent oCho = null;
                            oForm = Comunes.ConexiónSAPB1.oSAPB1appl.Forms.Item(FormUID);
                            oCho = (SAPbouiCOM.IChooseFromListEvent)pVal;
                            SAPbouiCOM.DataTable oDt = null;

                            if (oCho.ChooseFromListUID == "AB_1" && pVal.ItemUID == "CUITA")
                            {
                                try
                                {
                                    oDt = oCho.SelectedObjects;
                                    SAPbouiCOM.EditText oEdit = null;
                                    string val = oDt.GetValue("CardName", 0).ToString();
                                    string val2 = oDt.GetValue("LicTradNum", 0).ToString();
                                    oEdit = (SAPbouiCOM.EditText)oForm.Items.Item("Item_30").Specific;
                                    oEdit.Value = val;
                                    oEdit = (SAPbouiCOM.EditText)oForm.Items.Item("CUITA").Specific;
                                    oEdit.Value = val2;
                                }
                                catch (Exception)
                                {

                                    Comunes.ConexiónSAPB1.oSAPB1appl.StatusBar.SetText("Cliente duplicado para el CUIT", BoMessageTime.bmt_Short, BoStatusBarMessageType.smt_Error);
                                }
                               
                            }
                            if (oCho.ChooseFromListUID == "AB_2" && pVal.ItemUID == "CUITB")
                            {
                                try
                                {
                                    oDt = oCho.SelectedObjects;
                                    SAPbouiCOM.EditText oEdit = null;
                                    string val = oDt.GetValue("CardName", 0).ToString();
                                    string val2 = oDt.GetValue("LicTradNum", 0).ToString();
                                    oEdit = (SAPbouiCOM.EditText)oForm.Items.Item("Item_34").Specific;
                                    oEdit.Value = val;
                                    oEdit = (SAPbouiCOM.EditText)oForm.Items.Item("CUITB").Specific;
                                    oEdit.Value = val2;
                                }
                                catch (Exception)
                                {

                                    Comunes.ConexiónSAPB1.oSAPB1appl.StatusBar.SetText("Cliente duplicado para el CUIT", BoMessageTime.bmt_Short, BoStatusBarMessageType.smt_Error);
                                }
                               
                            }
                            if (oCho.ChooseFromListUID == "AB_3" && pVal.ItemUID == "CUITC")
                            {
                                try
                                {
                                    oDt = oCho.SelectedObjects;
                                    SAPbouiCOM.EditText oEdit = null;
                                    string val = oDt.GetValue("CardName", 0).ToString();
                                    string val2 = oDt.GetValue("LicTradNum", 0).ToString();
                                    oEdit = (SAPbouiCOM.EditText)oForm.Items.Item("Item_39").Specific;
                                    oEdit.Value = val;
                                    oEdit = (SAPbouiCOM.EditText)oForm.Items.Item("CUITC").Specific;
                                    oEdit.Value = val2;

                                }
                                catch (Exception)
                                {

                                    Comunes.ConexiónSAPB1.oSAPB1appl.StatusBar.SetText("Cliente duplicado para el CUIT", BoMessageTime.bmt_Short, BoStatusBarMessageType.smt_Error);
                                }
                               
                            }
                            if (oCho.ChooseFromListUID == "AB_4" && pVal.ItemUID == "CUITD")
                            {
                                try
                                {
                                    oDt = oCho.SelectedObjects;
                                    SAPbouiCOM.EditText oEdit = null;
                                    string val = oDt.GetValue("CardName", 0).ToString();
                                    string val2 = oDt.GetValue("LicTradNum", 0).ToString();
                                    oEdit = (SAPbouiCOM.EditText)oForm.Items.Item("Item_42").Specific;
                                    oEdit.Value = val;
                                    oEdit = (SAPbouiCOM.EditText)oForm.Items.Item("CUITD").Specific;
                                    oEdit.Value = val2;
                                }
                                catch (Exception)
                                {

                                    Comunes.ConexiónSAPB1.oSAPB1appl.StatusBar.SetText("Cliente duplicado para el CUIT", BoMessageTime.bmt_Short, BoStatusBarMessageType.smt_Error);
                                }
                               
                            }
                            if (oCho.ChooseFromListUID == "AB_5" && pVal.ItemUID == "CUITE")
                            {
                                try
                                {
                                    oDt = oCho.SelectedObjects;
                                    SAPbouiCOM.EditText oEdit = null;
                                    string val = oDt.GetValue("CardName", 0).ToString();
                                    string val2 = oDt.GetValue("LicTradNum", 0).ToString();
                                    oEdit = (SAPbouiCOM.EditText)oForm.Items.Item("Item_46").Specific;
                                    oEdit.Value = val;
                                    oEdit = (SAPbouiCOM.EditText)oForm.Items.Item("CUITE").Specific;
                                    oEdit.Value = val2;
                                }
                                catch (Exception)
                                {

                                    Comunes.ConexiónSAPB1.oSAPB1appl.StatusBar.SetText("Cliente duplicado para el CUIT", BoMessageTime.bmt_Short, BoStatusBarMessageType.smt_Error);
                                }
                               
                            }
                            if (oCho.ChooseFromListUID == "AB_6" && pVal.ItemUID == "CUITF")
                            {
                                try
                                {
                                    oDt = oCho.SelectedObjects;
                                    SAPbouiCOM.EditText oEdit = null;
                                    string val = oDt.GetValue("CardName", 0).ToString();
                                    string val2 = oDt.GetValue("LicTradNum", 0).ToString();
                                    oEdit = (SAPbouiCOM.EditText)oForm.Items.Item("Item_51").Specific;
                                    oEdit.Value = val;
                                    oEdit = (SAPbouiCOM.EditText)oForm.Items.Item("CUITF").Specific;
                                    oEdit.Value = val2;
                                }
                                catch (Exception)
                                {

                                    Comunes.ConexiónSAPB1.oSAPB1appl.StatusBar.SetText("Cliente dulpicado para CUIT", BoMessageTime.bmt_Short, BoStatusBarMessageType.smt_Error);
                                }
                               
                            }
                            if (oCho.ChooseFromListUID == "AB_7" && pVal.ItemUID == "CUITG")
                            {
                                try
                                {
                                    oDt = oCho.SelectedObjects;
                                    SAPbouiCOM.EditText oEdit = null;
                                    string val = oDt.GetValue("CardName", 0).ToString();
                                    string val2 = oDt.GetValue("LicTradNum", 0).ToString();
                                    oEdit = (SAPbouiCOM.EditText)oForm.Items.Item("Item_54").Specific;
                                    oEdit.Value = val;
                                    oEdit = (SAPbouiCOM.EditText)oForm.Items.Item("CUITG").Specific;
                                    oEdit.Value = val2;
                                }
                                catch (Exception)
                                {

                                    Comunes.ConexiónSAPB1.oSAPB1appl.StatusBar.SetText("Cliente duplicado para el CUIT", BoMessageTime.bmt_Short, BoStatusBarMessageType.smt_Error);
                                }
                               
                            }
                            if (oCho.ChooseFromListUID == "AB_8" && pVal.ItemUID == "CUITH")
                            {
                                try
                                {
                                    oDt = oCho.SelectedObjects;
                                    SAPbouiCOM.EditText oEdit = null;
                                    string val = oDt.GetValue("CardName", 0).ToString();
                                    string val2 = oDt.GetValue("LicTradNum", 0).ToString();
                                    oEdit = (SAPbouiCOM.EditText)oForm.Items.Item("Item_122").Specific;
                                    oEdit.Value = val;
                                    oEdit = (SAPbouiCOM.EditText)oForm.Items.Item("CUITH").Specific;
                                    oEdit.Value = val2;
                                }
                                catch (Exception)
                                {

                                    Comunes.ConexiónSAPB1.oSAPB1appl.StatusBar.SetText("Cliente duplicado para el CUIT", BoMessageTime.bmt_Short, BoStatusBarMessageType.smt_Error);
                                }
                              
                            }
                            if (oCho.ChooseFromListUID == "AB_9" && pVal.ItemUID == "CUITI")
                            {
                                try
                                {


                                    oDt = oCho.SelectedObjects;
                                    SAPbouiCOM.EditText oEdit = null;
                                    string val = oDt.GetValue("CardName", 0).ToString();
                                    string val2 = oDt.GetValue("LicTradNum", 0).ToString();
                                    oEdit = (SAPbouiCOM.EditText)oForm.Items.Item("Item_86").Specific;
                                    oEdit.Value = val;
                                    oEdit = (SAPbouiCOM.EditText)oForm.Items.Item("CUITI").Specific;
                                    oEdit.Value = val2;
                                    Controladores.CartaPorte oCpe = new Controladores.CartaPorte();
                                    oPlanta = oCpe.GetPlantas(Convert.ToInt64(((SAPbouiCOM.EditText)oform.Items.Item("CUITI").Specific).Value));
                                    foreach (var item in oPlanta)
                                    {
                                        ((SAPbouiCOM.ComboBox)oform.Items.Item("cmbPlanta").Specific).ValidValues.Add(item.Codigo.ToString(), item.Codigo.ToString());
                                        oPlantacmb.Add(item);

                                    }
                                }
                                catch (Exception)
                                {

                                    Comunes.ConexiónSAPB1.oSAPB1appl.StatusBar.SetText("Cliente duplicado para el CUIT", BoMessageTime.bmt_Short, BoStatusBarMessageType.smt_Error);
                                }
                            }
                            if (oCho.ChooseFromListUID == "AB_10" && pVal.ItemUID == "CUITJ")
                            {
                                try
                                {
                                    oDt = oCho.SelectedObjects;
                                    SAPbouiCOM.EditText oEdit = null;
                                    string val = oDt.GetValue("CardName", 0).ToString();
                                    string val2 = oDt.GetValue("LicTradNum", 0).ToString();
                                    oEdit = (SAPbouiCOM.EditText)oForm.Items.Item("Item_103").Specific;
                                    oEdit.Value = val;
                                    oEdit = (SAPbouiCOM.EditText)oForm.Items.Item("CUITJ").Specific;
                                    oEdit.Value = val2;
                                }
                                catch (Exception)
                                {

                                    Comunes.ConexiónSAPB1.oSAPB1appl.StatusBar.SetText("Cliente duplicado para el CUIT", BoMessageTime.bmt_Short, BoStatusBarMessageType.smt_Error);
                                }
                               
                            }
                            if (oCho.ChooseFromListUID == "AB_11" && pVal.ItemUID == "CUITK")
                            {
                                try
                                {
                                    oDt = oCho.SelectedObjects;
                                    SAPbouiCOM.EditText oEdit = null;
                                    string val = oDt.GetValue("CardName", 0).ToString();
                                    string val2 = oDt.GetValue("LicTradNum", 0).ToString();
                                    oEdit = (SAPbouiCOM.EditText)oForm.Items.Item("Item_118").Specific;
                                    oEdit.Value = val;
                                    oEdit = (SAPbouiCOM.EditText)oForm.Items.Item("CUITK").Specific;
                                    oEdit.Value = val2;
                                }
                                catch (Exception)
                                {

                                    Comunes.ConexiónSAPB1.oSAPB1appl.StatusBar.SetText("Cliente duplicado para el CUIT", BoMessageTime.bmt_Short, BoStatusBarMessageType.smt_Error);
                                }
                               
                            }
                            break;



                    }
                    //switch (pVal.EventType)
                    //{
                    //    case BoEventTypes.et_FORM_LOAD:
                    //        //CargarCFLA(FormUID);
                    //        //CargarCFLB(FormUID);
                    //        //CargarCFLC(FormUID);
                    //        //CargarCFLD(FormUID);
                    //        //CargarCFLE(FormUID);
                    //        //CargarCFLF(FormUID);
                    //        //CargarCFLG(FormUID);
                    //        //CargarCFLH(FormUID);
                    //        //CargarCFLI(FormUID);
                    //        //CargarCFLJ(FormUID);
                    //        //CargarCFLK(FormUID);
                    //        //EditarFormulario(FormUID);
                    //        break;
                    //}
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
            string errorAFIP = "";
            Controladores.CartaPorte ctrCpe = new Controladores.CartaPorte();
            SAPbouiCOM.Form oform = null;
            try
            {
               
                if (BusinessObjectInfo.ActionSuccess)
                {
                    oform = Comunes.ConexiónSAPB1.oSAPB1appl.Forms.Item(BusinessObjectInfo.FormUID);
                    switch (BusinessObjectInfo.EventType)
                    {
                        case SAPbouiCOM.BoEventTypes.et_FORM_DATA_ADD:

                            //Si el numero de CTG esta vacio es porque esta emitiendo, sino, trayendo desde AFIP
                            if (!String.IsNullOrEmpty(((SAPbouiCOM.EditText)oform.Items.Item("Item_83").Specific).Value.ToString()))
                              {
                                errorAFIP = ctrCpe.ArmarEquivalencias(BusinessObjectInfo.ObjectKey);

                                if (String.IsNullOrEmpty(errorAFIP))
                                {

                                    // oform = Comunes.ConexiónSAPB1.oSAPB1appl.Forms.Item(BusinessObjectInfo.FormUID);
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
                                else
                                {
                                    MessageBox.Show(errorAFIP);
                                }
                            }
                            else
                            {
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
                            break;
                        case BoEventTypes.et_FORM_DATA_LOAD:
                            oform = Comunes.ConexiónSAPB1.oSAPB1appl.Forms.Item(BusinessObjectInfo.FormUID);
                            //Limpiar los valores una vez que se carga el formulario para que no tire error
                            if (((SAPbouiCOM.ButtonCombo)oform.Items.Item("Item_94").Specific).ValidValues.Count == 0)
                            {
                                ((SAPbouiCOM.ButtonCombo)oform.Items.Item("Item_94").Specific).ValidValues.Add("AFIP", "AFIP");
                                ((SAPbouiCOM.ButtonCombo)oform.Items.Item("Item_94").Specific).ValidValues.Add("SAP", "SAP");
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
                    ((SAPbouiCOM.EditText)oform.Items.Item("Item_92").Specific).Value = Objeto.Respuesta.Origen.CodProvincia.ToString();
                    ((SAPbouiCOM.EditText)oform.Items.Item("Item_90").Specific).Value = Objeto.Respuesta.Origen.CodLocalidad.ToString();
                    if (Objeto.Respuesta.Intervinientes.CuitRemitenteComercialVentaPrimaria != 0)
                        ((SAPbouiCOM.EditText)oform.Items.Item("CUITA").Specific).Value = Objeto.Respuesta.Intervinientes.CuitRemitenteComercialVentaPrimaria.ToString();
                    if (Objeto.Respuesta.Intervinientes.CuitRemitenteComercialVentaSecundaria != 0)
                        ((SAPbouiCOM.EditText)oform.Items.Item("CUITB").Specific).Value = Objeto.Respuesta.Intervinientes.CuitRemitenteComercialVentaSecundaria.ToString();
                    if (Objeto.Respuesta.Intervinientes.CuitRemitenteComercialVentaSecundaria2 != 0)
                        ((SAPbouiCOM.EditText)oform.Items.Item("CUITC").Specific).Value = Objeto.Respuesta.Intervinientes.CuitRemitenteComercialVentaSecundaria2.ToString();
                    if (Objeto.Respuesta.Intervinientes.cuitMercadoATermino != 0)
                        ((SAPbouiCOM.EditText)oform.Items.Item("CUITD").Specific).Value = Objeto.Respuesta.Intervinientes.cuitMercadoATermino.ToString();
                    if (Objeto.Respuesta.Intervinientes.CuitCorredorVentaPrimaria != 0)
                        ((SAPbouiCOM.EditText)oform.Items.Item("CUITE").Specific).Value = Objeto.Respuesta.Intervinientes.CuitCorredorVentaPrimaria.ToString();
                    if (Objeto.Respuesta.Intervinientes.CuitCorredorVentaSecundaria != 0)
                        ((SAPbouiCOM.EditText)oform.Items.Item("CUITF").Specific).Value = Objeto.Respuesta.Intervinientes.CuitCorredorVentaSecundaria.ToString();
                    if (Objeto.Respuesta.Intervinientes.cuitRepresentanteEntregador != 0)
                        ((SAPbouiCOM.EditText)oform.Items.Item("CUITG").Specific).Value = Objeto.Respuesta.Intervinientes.cuitRepresentanteEntregador.ToString();
                    if (Objeto.Respuesta.Intervinientes.cuitRepresentanteRecibidor != 0)
                        ((SAPbouiCOM.EditText)oform.Items.Item("CUITH").Specific).Value = Objeto.Respuesta.Intervinientes.cuitRepresentanteRecibidor.ToString();
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
                    ((SAPbouiCOM.EditText)oform.Items.Item("CUITI").Specific).Value = Objeto.Respuesta.Destino.Cuit.ToString();
                    ((SAPbouiCOM.EditText)oform.Items.Item("CUITJ").Specific).Value = Objeto.Respuesta.Destinatario.Cuit.ToString();
                    //((SAPbouiCOM.EditText)oform.Items.Item("CUITDesa").Specific).Value = Objeto.Respuesta.Destinatario.Cuit.ToString();
                    ((SAPbouiCOM.EditText)oform.Items.Item("Item_170").Specific).Value = Objeto.Respuesta.Destino.CodLocalidad.ToString();
                    ((SAPbouiCOM.EditText)oform.Items.Item("Item_167").Specific).Value = Objeto.Respuesta.Destino.CodProvincia.ToString();
                    string fecha = Objeto.Respuesta.Transporte.FechaHoraPartida.ToString("dd/MM/yyyy");
                    ((SAPbouiCOM.EditText)oform.Items.Item("Item_106").Specific).String = fecha;
                    ((SAPbouiCOM.EditText)oform.Items.Item("Item_96").Specific).Value = Objeto.Respuesta.Destino.Planta.ToString();
                    ((SAPbouiCOM.EditText)oform.Items.Item("Item_108").Specific).Value = Objeto.Respuesta.Transporte.KmRecorrer.ToString();
                    ((SAPbouiCOM.EditText)oform.Items.Item("Item_110").Specific).Value = Objeto.Respuesta.Transporte.TarifaReferencia.ToString();
                    ((SAPbouiCOM.EditText)oform.Items.Item("Item_114").Specific).Value = Objeto.Respuesta.Transporte.Tarifa.ToString();
                    if (Objeto.Respuesta.Transporte.CuitPagadorFlete != 0)
                        ((SAPbouiCOM.EditText)oform.Items.Item("CUITK").Specific).Value = Objeto.Respuesta.Transporte.CuitPagadorFlete.ToString();
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
            double CanContrato1 = 0;
            double CanContrato2 = 0;
            double CanContrato3 = 0;
            double CanContrato4 = 0;
            double CanContrato5 = 0;
            double total = 0;

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

                    if (((SAPbouiCOM.CheckBox)oForm.Items.Item("Item_165").Specific).Checked)
                    {
                        //Sumo los contratos
                        CanContrato1 = Convert.ToDouble(((SAPbouiCOM.EditText)oForm.Items.Item("Item_157").Specific).Value);
                        CanContrato2 = Convert.ToDouble(((SAPbouiCOM.EditText)oForm.Items.Item("Item_159").Specific).Value);
                        CanContrato3 = Convert.ToDouble(((SAPbouiCOM.EditText)oForm.Items.Item("Item_161").Specific).Value);
                        CanContrato4 = Convert.ToDouble(((SAPbouiCOM.EditText)oForm.Items.Item("Item_6").Specific).Value);
                        CanContrato5 = Convert.ToDouble(((SAPbouiCOM.EditText)oForm.Items.Item("Item_13").Specific).Value);
                        total = CanContrato1 + CanContrato2 + CanContrato3 + CanContrato4 + CanContrato5;

                        if (total == PesoNeto)
                        {
                            Error = ctrlTransf.AddTransferencia(oTransfer, Convert.ToInt16(DocEntry));
                        }
                        else
                        {
                            Error = "Las cantidades de los contratos no coinciden con el total";
                        }

                    }
                    else
                    {
                        Error = ctrlTransf.AddTransferencia(oTransfer, Convert.ToInt16(DocEntry));
                    }

                    if (Error != "Ok")
                    {
                        Comunes.ConexiónSAPB1.oSAPB1appl.MessageBox(Error.ToString());
                    }

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

        #region ChooseFromLists
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
                oCon.CondVal = "C";
                oCon.BracketCloseNum = 2;


                oCfl.SetConditions(oCons);


            }
            catch (Exception)
            {

                throw;
            }
        }
        #endregion

        #region EditForm
        public void EditarFormulario(string FormUID)
        {
            try
            {
                //cuit remitente comercial venta primaria
                oForm = Comunes.ConexiónSAPB1.oSAPB1appl.Forms.Item(FormUID);
                oForm.Items.Add("CUITA", SAPbouiCOM.BoFormItemTypes.it_EDIT);

                oItem = oForm.Items.Item("CUITA");

                oItem.Top = 300;
                oItem.Left = 30;
                oItem.Width = 220;
                oItem.Height = 14;
                oItem.FromPane = 1;
                oItem.ToPane = 1;
                oItem.Enabled = true;
                oEditText = (SAPbouiCOM.EditText)oForm.Items.Item("CUITA").Specific;
                oEditText.DataBind.SetBound(true, "@CARTAPORTE", "U_CUITRteVtaPrim");

                oEditText.ChooseFromListUID = "AB_1";
                oEditText.ChooseFromListAlias = "CardName";

                //cuit remitente comercial venta secundaria
                oForm = Comunes.ConexiónSAPB1.oSAPB1appl.Forms.Item(FormUID);
                oForm.Items.Add("CUITB", SAPbouiCOM.BoFormItemTypes.it_EDIT);

                oItem = oForm.Items.Item("CUITB");

                oItem.Top = 360;
                oItem.Left = 30;
                oItem.Width = 220;
                oItem.Height = 14;
                oItem.FromPane = 1;
                oItem.ToPane = 1;
                oItem.Enabled = true;
                oEditText = (SAPbouiCOM.EditText)oForm.Items.Item("CUITB").Specific;
                oEditText.DataBind.SetBound(true, "@CARTAPORTE", "U_CUITRteVtaSec");

                oEditText.ChooseFromListUID = "AB_2";
                oEditText.ChooseFromListAlias = "CardName";

                //cuit remitente comercial venta secundaria 2
                oForm = Comunes.ConexiónSAPB1.oSAPB1appl.Forms.Item(FormUID);
                oForm.Items.Add("CUITC", SAPbouiCOM.BoFormItemTypes.it_EDIT);

                oItem = oForm.Items.Item("CUITC");

                oItem.Top = 420;
                oItem.Left = 30;
                oItem.Width = 220;
                oItem.Height = 14;
                oItem.FromPane = 1;
                oItem.ToPane = 1;
                oItem.Enabled = true;
                oEditText = (SAPbouiCOM.EditText)oForm.Items.Item("CUITC").Specific;
                oEditText.DataBind.SetBound(true, "@CARTAPORTE", "U_CUITRteVtaSec2");

                oEditText.ChooseFromListUID = "AB_3";
                oEditText.ChooseFromListAlias = "CardName";

                //cuit mercado interno
                oForm = Comunes.ConexiónSAPB1.oSAPB1appl.Forms.Item(FormUID);
                oForm.Items.Add("CUITD", SAPbouiCOM.BoFormItemTypes.it_EDIT);

                oItem = oForm.Items.Item("CUITD");

                oItem.Top = 480;
                oItem.Left = 30;
                oItem.Width = 220;
                oItem.Height = 14;
                oItem.FromPane = 1;
                oItem.ToPane = 1;
                oItem.Enabled = true;
                oEditText = (SAPbouiCOM.EditText)oForm.Items.Item("CUITD").Specific;
                oEditText.DataBind.SetBound(true, "@CARTAPORTE", "U_CUITMAT");

                oEditText.ChooseFromListUID = "AB_4";
                oEditText.ChooseFromListAlias = "CardName";

                //cuit corredor venta primaria
                oForm = Comunes.ConexiónSAPB1.oSAPB1appl.Forms.Item(FormUID);
                oForm.Items.Add("CUITE", SAPbouiCOM.BoFormItemTypes.it_EDIT);

                oItem = oForm.Items.Item("CUITE");

                oItem.Top = 120;
                oItem.Left = 600;
                oItem.Width = 220;
                oItem.Height = 14;
                oItem.FromPane = 1;
                oItem.ToPane = 1;
                oItem.Enabled = true;
                oEditText = (SAPbouiCOM.EditText)oForm.Items.Item("CUITE").Specific;
                oEditText.DataBind.SetBound(true, "@CARTAPORTE", "U_CUITCorrVtaPrim");

                oEditText.ChooseFromListUID = "AB_5";
                oEditText.ChooseFromListAlias = "CardName";

                //cuit corredor venta secundaria
                oForm = Comunes.ConexiónSAPB1.oSAPB1appl.Forms.Item(FormUID);
                oForm.Items.Add("CUITF", SAPbouiCOM.BoFormItemTypes.it_EDIT);

                oItem = oForm.Items.Item("CUITF");

                oItem.Top = 180;
                oItem.Left = 600;
                oItem.Width = 220;
                oItem.Height = 14;
                oItem.FromPane = 1;
                oItem.ToPane = 1;
                oItem.Enabled = true;
                oEditText = (SAPbouiCOM.EditText)oForm.Items.Item("CUITF").Specific;
                oEditText.DataBind.SetBound(true, "@CARTAPORTE", "U_CUITCorrVtaSec");

                oEditText.ChooseFromListUID = "AB_6";
                oEditText.ChooseFromListAlias = "CardName";

                //cuit corredor venta secundaria
                oForm = Comunes.ConexiónSAPB1.oSAPB1appl.Forms.Item(FormUID);
                oForm.Items.Add("CUITG", SAPbouiCOM.BoFormItemTypes.it_EDIT);

                oItem = oForm.Items.Item("CUITG");

                oItem.Top = 240;
                oItem.Left = 600;
                oItem.Width = 220;
                oItem.Height = 14;
                oItem.FromPane = 1;
                oItem.ToPane = 1;
                oItem.Enabled = true;
                oEditText = (SAPbouiCOM.EditText)oForm.Items.Item("CUITG").Specific;
                oEditText.DataBind.SetBound(true, "@CARTAPORTE", "U_CUITEntregador");

                oEditText.ChooseFromListUID = "AB_7";
                oEditText.ChooseFromListAlias = "CardName";

                //cuit corredor venta secundaria
                oForm = Comunes.ConexiónSAPB1.oSAPB1appl.Forms.Item(FormUID);
                oForm.Items.Add("CUITH", SAPbouiCOM.BoFormItemTypes.it_EDIT);

                oItem = oForm.Items.Item("CUITH");

                oItem.Top = 300;
                oItem.Left = 600;
                oItem.Width = 220;
                oItem.Height = 14;
                oItem.FromPane = 1;
                oItem.ToPane = 1;
                oItem.Enabled = true;
                oEditText = (SAPbouiCOM.EditText)oForm.Items.Item("CUITH").Specific;
                oEditText.DataBind.SetBound(true, "@CARTAPORTE", "U_CUITRecibidor");

                oEditText.ChooseFromListUID = "AB_8";
                oEditText.ChooseFromListAlias = "CardName";

                //cuit Destino
                oForm = Comunes.ConexiónSAPB1.oSAPB1appl.Forms.Item(FormUID);
                oForm.Items.Add("CUITI", SAPbouiCOM.BoFormItemTypes.it_EDIT);

                oItem = oForm.Items.Item("CUITI");

                oItem.Top = 140;
                oItem.Left = 50;
                oItem.Width = 220;
                oItem.Height = 14;
                oItem.FromPane = 3;
                oItem.ToPane = 3;
                oItem.Enabled = true;
                oEditText = (SAPbouiCOM.EditText)oForm.Items.Item("CUITI").Specific;
                oEditText.DataBind.SetBound(true, "@CARTAPORTE", "U_CUITDestino");

                oEditText.ChooseFromListUID = "AB_9";
                oEditText.ChooseFromListAlias = "CardName";

                //cuit Destino
                oForm = Comunes.ConexiónSAPB1.oSAPB1appl.Forms.Item(FormUID);
                oForm.Items.Add("CUITJ", SAPbouiCOM.BoFormItemTypes.it_EDIT);

                oItem = oForm.Items.Item("CUITJ");

                oItem.Top = 140;
                oItem.Left = 710;
                oItem.Width = 220;
                oItem.Height = 14;
                oItem.FromPane = 3;
                oItem.ToPane = 3;
                oItem.Enabled = true;
                oEditText = (SAPbouiCOM.EditText)oForm.Items.Item("CUITJ").Specific;
                oEditText.DataBind.SetBound(true, "@CARTAPORTE", "U_CUITDestina");

                oEditText.ChooseFromListUID = "AB_10";
                oEditText.ChooseFromListAlias = "CardName";

                //cuit Pagador Flete
                oForm = Comunes.ConexiónSAPB1.oSAPB1appl.Forms.Item(FormUID);
                oForm.Items.Add("CUITK", SAPbouiCOM.BoFormItemTypes.it_EDIT);

                oItem = oForm.Items.Item("CUITK");

                oItem.Top = 478;
                oItem.Left = 710;
                oItem.Width = 220;
                oItem.Height = 14;
                oItem.FromPane = 3;
                oItem.ToPane = 3;
                oItem.Enabled = true;
                oEditText = (SAPbouiCOM.EditText)oForm.Items.Item("CUITK").Specific;
                oEditText.DataBind.SetBound(true, "@CARTAPORTE", "U_CUITPagadorFlete");

                oEditText.ChooseFromListUID = "AB_11";
                oEditText.ChooseFromListAlias = "CardName";

            }
            catch (Exception)
            {

                throw;
            }
        }

        #endregion
        #endregion

        private void ExportToxml(SAPbouiCOM.Form oForm)
        {
            XmlDocument oXml = new XmlDocument();
            try
            {
                oXml.LoadXml(oForm.GetAsXML());
                oXml.Save(@"C:\WSCPESAP\CartaPortev4.xml");
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}

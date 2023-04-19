using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web;
//using CrystalDecisions.CrystalReports.Engine;
//using CrystalDecisions.Shared;


namespace ModuloGrano.Formularios
{
    public class FormCarga
    {
        #region Atributos
        public const string TipoFormulariostr = "UDO_FT_CARGACPE";
        public const string Formulario = "UDO_F_CARGACPE";
        private static SAPbobsCOM.Company oCompany = null;
        private static SAPbouiCOM.Form oForm = null;
        private static SAPbouiCOM.EditText oEditText = null;
        private static SAPbouiCOM.Item oItem = null;
        private static SAPbouiCOM.EventForm oFormEvent = null;
        #endregion

        #region Constructor

        public FormCarga() { }

        #endregion

        #region Eventos
        public void OSAPB1appl_ItemEvent(string FormUID, ref SAPbouiCOM.ItemEvent pVal, out bool BubbleEvent)
        {
            BubbleEvent = true;

            if (!pVal.BeforeAction)
            {
                switch(pVal.EventType)
                {
                    case SAPbouiCOM.BoEventTypes.et_FORM_LOAD:
                        CargarCFLA(FormUID);
                        EditarFormulario(FormUID);
                        CargarLayout();
                        //oForm = Comunes.ConexiónSAPB1.oSAPB1appl.Forms.Item(FormUID);
                        //oFormEvent = Comunes.ConexiónSAPB1.oSAPB1appl.Forms.GetEventForm(oForm.UniqueID);
                        //oFormEvent.LayoutKeyBefore += OFormEvent_LayoutKeyBefore;
                        //Comunes.ConexiónSAPB1.oSAPB1appl.FormDataEvent += OSAPB1appl_FormDataEvent;
                        break;
                    case SAPbouiCOM.BoEventTypes.et_CHOOSE_FROM_LIST:
                        SAPbouiCOM.IChooseFromListEvent oCho = null;
                        oForm = Comunes.ConexiónSAPB1.oSAPB1appl.Forms.Item(FormUID);
                        oCho = (SAPbouiCOM.IChooseFromListEvent)pVal;
                        SAPbouiCOM.DataTable oDt = null;
                        if (oCho.ChooseFromListUID == "AB_1" && pVal.ItemUID == "CUITTrans")
                        {
                            oDt = oCho.SelectedObjects;
                            SAPbouiCOM.EditText oEdit = null;
                            string val = oDt.GetValue("CardName", 0).ToString();
                            oEdit = (SAPbouiCOM.EditText)oForm.Items.Item("CUITTrans").Specific;
                            oEdit.Value = val;
                        }
                        break;

                }
            }
            else
            {
                //switch(pVal.EventType)
                //{
                //    case SAPbouiCOM.BoEventTypes.et_CHOOSE_FROM_LIST:
                //        SAPbouiCOM.IChooseFromListEvent oCho = null;
                //        oForm = Comunes.ConexiónSAPB1.oSAPB1appl.Forms.Item(FormUID);
                //        oCho = (SAPbouiCOM.IChooseFromListEvent)pVal;
                //        SAPbouiCOM.DataTable oDt = null;
                //        if (oCho.ChooseFromListUID == "AB_1" && pVal.ItemUID == "CUITTrans")
                //        {
                //            oDt = oCho.SelectedObjects;
                //            SAPbouiCOM.EditText oEdit = null;
                //            string val = oDt.GetValue("CardName", 0).ToString();
                //            oEdit = (SAPbouiCOM.EditText)oForm.Items.Item("CUITTrans").Specific;
                //            oEdit.Value = val;
                //        }
                //        break;
                //}
            }
        }

        //private void OFormEvent_LayoutKeyBefore(ref SAPbouiCOM.LayoutKeyInfo eventInfo, out bool BubbleEvent)
        //{
        //    BubbleEvent = true;
        //    try
        //    {
        //        eventInfo.LayoutKey = oForm.DataSources.DBDataSources.Item("@CARGACPE").GetValue("DocEntry", 0);
        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }
        //}

        public void OSAPB1appl_FormDataEvent(ref SAPbouiCOM.BusinessObjectInfo BusinessObjectInfo, out bool BubbleEvent)
        {
            BubbleEvent = true;
            try
            {

                //if (BusinessObjectInfo.ActionSuccess)
                //{
                //    switch (BusinessObjectInfo.EventType)
                //    {
                //        case SAPbouiCOM.BoEventTypes.et_FORM_DATA_ADD:
                //            //ImprimirVale();
                //            break;
                        
                //    }

                //}

            }
            catch (Exception)
            {

                throw;
            }

        }

        //public void OSAPB1appl_FormDataEvent(ref SAPbouiCOM.BusinessObjectInfo BusinessObjectInfo, out bool BubbleEvent)
        //{
        //    BubbleEvent = true;
        //    try
        //    {
        //        switch (BusinessObjectInfo.EventType)
        //        {
        //            case SAPbouiCOM.BoEventTypes.et_PRINT_LAYOUT_KEY:
        //                string hola = "Hola";
        //                break;
        //        }
        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }
        //}

        #endregion

        #region Metodos
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

        public void EditarFormulario(string FormUID)
        {
            try
            {
                oForm = Comunes.ConexiónSAPB1.oSAPB1appl.Forms.Item(FormUID);
                oForm.Items.Add("CUITTrans", SAPbouiCOM.BoFormItemTypes.it_EDIT);

                oItem = oForm.Items.Item("CUITTrans");

                oItem.Top = 182;
                oItem.Left = 430;
                oItem.Width = 260;
                oItem.Height = 14;
                oItem.FromPane = 1;
                oItem.ToPane = 1;
                oItem.Enabled = false;
                oEditText = (SAPbouiCOM.EditText)oForm.Items.Item("CUITTrans").Specific;
                oEditText.DataBind.SetBound(true, "@CARGACPE", "U_Transporte");

                oEditText.ChooseFromListUID = "AB_1";
                oEditText.ChooseFromListAlias = "CardName";

               
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void CargarLayout()
        {
            SAPbobsCOM.Recordset oRecordSet = null;
            SAPbobsCOM.CompanyService oCompanyService = null;
            //-------------------------------------------------------//
            SAPbobsCOM.ReportTypesService oReportTypeService = null;
            SAPbobsCOM.ReportType ReportType = null;
            SAPbobsCOM.ReportTypeParams oReportTypeParams = null; // => Variable utilizada al momento de agregar nuestro nuevo tipo de servicio
            //------------------------------------------------------//
            SAPbobsCOM.ReportLayoutsService oReportLayoutService = null;
            SAPbobsCOM.ReportLayout oReportLayout = null;
            SAPbobsCOM.ReportLayoutParams oReportLayoutParams = null;
            //---------------------------------------------------------//
            SAPbobsCOM.Blob oBlob = null;
            SAPbobsCOM.BlobParams oBlobParams = null;
            SAPbobsCOM.BlobTableKeySegment oKey = null;
            //---------------------------------------------------------//



            try
            {
                oCompany = Comunes.ConexiónSAPB1.oCompanyDIAPI;
                oRecordSet = (SAPbobsCOM.Recordset)oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
                oCompanyService = oCompany.GetCompanyService();


                oRecordSet.DoQuery("SELECT CODE FROM RTYP WHERE NAME = 'ValeCarga_layout'");
                if (oRecordSet.RecordCount == 0)
                {
                    // 1. ReportType
                    oReportTypeService = (SAPbobsCOM.ReportTypesService)oCompanyService.GetBusinessService(SAPbobsCOM.ServiceTypes.ReportTypesService);
                    ReportType = (SAPbobsCOM.ReportType)oReportTypeService.GetDataInterface(SAPbobsCOM.ReportTypesServiceDataInterfaces.rtsReportType);
                    ReportType.AddonName = "Modulo de Granos";
                    ReportType.TypeName = "ValeCarga_layout";
                    //Valor de la cabecera del formulario en el codigo
                    ReportType.AddonFormType = "UDO_F_CARGACPE";
                    ReportType.MenuID = "Carga";
                    oReportTypeParams = oReportTypeService.AddReportType(ReportType);
                    //---------------------------------------------------------------------//


                    //2. Report Layout
                    oReportLayoutService = (SAPbobsCOM.ReportLayoutsService)oCompanyService.GetBusinessService(SAPbobsCOM.ServiceTypes.ReportLayoutsService);
                    oReportLayout = (SAPbobsCOM.ReportLayout)oReportLayoutService.GetDataInterface(SAPbobsCOM.ReportLayoutsServiceDataInterfaces.rlsdiReportLayout);
                    oReportLayout.Author = "manager";
                    oReportLayout.Category = SAPbobsCOM.ReportLayoutCategoryEnum.rlcCrystal;
                    oReportLayout.Name = "ValeCarga";
                    oReportLayout.TypeCode = oReportTypeParams.TypeCode;
                    oReportLayoutParams = oReportLayoutService.AddReportLayout(oReportLayout);

                    //3. Vincular Layout con el tipo de reporte
                    ReportType = oReportTypeService.GetReportType(oReportTypeParams);
                    ReportType.DefaultReportLayout = oReportLayoutParams.LayoutCode;

                    //4. Realizar la carga de archivo Crystal Report a SAP.
                    oBlob = (SAPbobsCOM.Blob)oCompanyService.GetDataInterface(SAPbobsCOM.CompanyServiceDataInterfaces.csdiBlob);
                    oBlobParams = (SAPbobsCOM.BlobParams)oCompanyService.GetDataInterface(SAPbobsCOM.CompanyServiceDataInterfaces.csdiBlobParams);
                    oBlobParams.Table = "RDOC";
                    oBlobParams.Field = "Template";
                    oKey = oBlobParams.BlobTableKeySegments.Add();
                    oKey.Name = "DocCode";
                    oKey.Value = oReportLayoutParams.LayoutCode;
                    oBlob.Content = Convert.ToBase64String(Properties.Resources.ValeCarga);

                    oCompanyService.SetBlob(oBlobParams, oBlob);

                    //5.Asingar nuestro reporte al formulario
                    //El tipo de formato de impresion que tiene asignado es el que creamos en los pasos anteriores

                    //this.UIAPIRawForm.ReportType = ReportType.TypeCode;
                    oForm.ReportType = ReportType.TypeCode;


                }
                else
                {

                    //this.UIAPIRawForm.ReportType = oRecordSet.Fields.Item(0).Value.ToString();
                    oForm.ReportType = oRecordSet.Fields.Item(0).Value.ToString();
                }

            }
            catch (Exception)
            {

                throw;
            }
        }

       

        #endregion


    }
}

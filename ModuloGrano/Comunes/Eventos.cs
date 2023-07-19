using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace ModuloGrano.Comunes
{
    class Eventos
    {
        #region Atributos

        #endregion

        #region Constructor

        public Eventos()
        {
            try
            {
                ConexiónSAPB1.oSAPB1appl.AppEvent += OSAPB1appl_AppEvent;
                //Descomentar para grilla logistica, comentar para broker
                ConexiónSAPB1.oSAPB1appl.ItemEvent += OSAPB1appl_ItemEvent;
                //ConexiónSAPB1.oSAPB1appl.
                //ConexiónSAPB1.oSAPB1appl.FormDataEvent += OSAPB1appl_FormDataEvent;
                ConexiónSAPB1.oSAPB1appl.FormDataEvent += OSAPB1appl_FormDataEvent;
                Comunes.ConexiónSAPB1.oSAPB1appl.MenuEvent += OSAPB1appl_MenuEvent;
                Comunes.ConexiónSAPB1.oSAPB1appl.LayoutKeyEvent += OSAPB1appl_LayoutKeyEvent;


                FiltroEventos();
            }
            catch (Exception ex)
            {

                ConexiónSAPB1.oSAPB1appl.StatusBar.SetText("Eventos()" + ex.Message, SAPbouiCOM.BoMessageTime.bmt_Medium, SAPbouiCOM.BoStatusBarMessageType.smt_Error);
            }
        }

        private void OSAPB1appl_LayoutKeyEvent(ref SAPbouiCOM.LayoutKeyInfo eventInfo, out bool BubbleEvent)
        {
            
            BubbleEvent = true;
            try
            {
                if (eventInfo.FormUID == "UDO_F_CARGACPE")
                {
                    SAPbouiCOM.Form oForm = Comunes.ConexiónSAPB1.oSAPB1appl.Forms.Item(eventInfo.FormUID);
                    eventInfo.LayoutKey = oForm.DataSources.DBDataSources.Item("@CARGACPE").GetValue("DocEntry", 0);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void OSAPB1appl_FormDataEvent(ref SAPbouiCOM.BusinessObjectInfo BusinessObjectInfo, out bool BubbleEvent)
        {
            BubbleEvent = true;
            try
            {
                switch (BusinessObjectInfo.FormUID)
                {
                    case Formularios.FormTransferencia.Formulario:
                        Formularios.FormTransferencia oForm = new Formularios.FormTransferencia();
                        oForm.OSAPB1appl_FormDataEvent(ref BusinessObjectInfo, out BubbleEvent);
                        break;
                    case Formularios.FormCarga.Formulario:
                        Formularios.FormCarga oForm2 = new Formularios.FormCarga();
                        oForm2.OSAPB1appl_FormDataEvent(ref BusinessObjectInfo, out BubbleEvent);
                        break;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void OSAPB1appl_MenuEvent(ref SAPbouiCOM.MenuEvent pVal, out bool BubbleEvent)
        {
            BubbleEvent = true;
            try
            {
                switch (pVal.MenuUID)
                {
                    case Formularios.Menu.frmMenu:
                        Formularios.Menu oMenu = new Formularios.Menu();
                        oMenu.OSAPB1appl_MenuEvent(ref pVal, out BubbleEvent);
                        break;
                    case Formularios.Menu.frmCarga:
                        Formularios.Menu oMenu2 = new Formularios.Menu();
                        oMenu2.OSAPB1appl_MenuEvent(ref pVal, out BubbleEvent);
                        break;
                    case Formularios.Menu.frmFijacion:
                        Formularios.Menu oMenu3 = new Formularios.Menu();
                        oMenu3.OSAPB1appl_MenuEvent(ref pVal, out BubbleEvent);
                        break;
                    case Formularios.Menu.frmContrato:
                        Formularios.Menu oMenu4 = new Formularios.Menu();
                        oMenu4.OSAPB1appl_MenuEvent(ref pVal, out BubbleEvent);
                        break;

                }
            }
            catch (Exception ex) { }
        }

        private void OSAPB1appl_ItemEvent(string FormUID, ref SAPbouiCOM.ItemEvent pVal, out bool BubbleEvent)
        {
            BubbleEvent = true;
            try
            {
                switch (pVal.FormTypeEx)
                {

                    case Formularios.FormTransferencia.TipoFormulariostr:
                        Formularios.FormTransferencia oFormTransf = new Formularios.FormTransferencia();
                        oFormTransf.OSAPB1appl_ItemEvent(FormUID, ref pVal, out BubbleEvent);
                        break;
                    case Formularios.FormCarga.TipoFormulariostr:
                        Formularios.FormCarga oFormCarga = new Formularios.FormCarga();
                        oFormCarga.OSAPB1appl_ItemEvent(FormUID, ref pVal, out BubbleEvent);
                        break;
                    case Formularios.FormFijaciones.TipoFormulariostr:
                        Formularios.FormFijaciones oFormFijaciones = new Formularios.FormFijaciones();
                        oFormFijaciones.OSAPB1appl_ItemEvent(FormUID, ref pVal, out BubbleEvent);
                        break;
                    case Formularios.FormContrato.frmContratostr:
                        Formularios.FormContrato oFormContrato = new Formularios.FormContrato();
                        oFormContrato.OSAPB1appl_ItemEvent(FormUID, ref pVal, out BubbleEvent);
                        break;
                }

            }
            catch (Exception ex)
            {

                Comunes.ConexiónSAPB1.oSAPB1appl.StatusBar.SetText(ex.Message, SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Error);
            }
        }

        #endregion

        #region Eventos
        //Descomentar para gastos logisticos, comentar pra broker


        private void OSAPB1appl_AppEvent(SAPbouiCOM.BoAppEventTypes EventType)
        {
            try
            {
                //Switch para los diferentes tipos de eventos.
                //De esta forma terminamos la aplicación en cualquiera de todos estos eventos.
                switch (EventType)
                {
                    //Cuando se cierra la aplicación
                    case SAPbouiCOM.BoAppEventTypes.aet_ShutDown:
                    case SAPbouiCOM.BoAppEventTypes.aet_ServerTerminition:
                    case SAPbouiCOM.BoAppEventTypes.aet_CompanyChanged:
                        Comunes.ConexiónSAPB1.oSAPB1appl.StatusBar.SetText("Finalizando Modulo Granos");
                        Application.Exit();
                        break;
                    //Cuando se cambia la fuente o el lenguaje, nuestro formulario debe cargar de nuevo                    
                    case SAPbouiCOM.BoAppEventTypes.aet_FontChanged:
                    case SAPbouiCOM.BoAppEventTypes.aet_LanguageChanged:
                        Application.Restart();
                        ConexiónSAPB1.oSAPB1appl.StatusBar.SetText("Reiniciando Addon");
                        break;
                }
            }
            catch (Exception ex)
            {

                ConexiónSAPB1.oSAPB1appl.StatusBar.SetText("Eventos()" + ex.Message, SAPbouiCOM.BoMessageTime.bmt_Medium, SAPbouiCOM.BoStatusBarMessageType.smt_Error);
            }
        }


        #endregion

        #region Metodos

        private void FiltroEventos()
        {
            //Revisar documentacion SDK
            SAPbouiCOM.EventFilters oFiltros = null;
            SAPbouiCOM.EventFilter oFiltro = null;

            try
            {
                //De esta forma filtro el evento que quiero escuchar en mi aplicación para que no este
                //escuchando todos los itemsevents.
                oFiltros = new SAPbouiCOM.EventFilters();
                oFiltro = oFiltros.Add(SAPbouiCOM.BoEventTypes.et_ITEM_PRESSED);
                oFiltro = oFiltros.Add(SAPbouiCOM.BoEventTypes.et_MENU_CLICK);
                oFiltro = oFiltros.Add(SAPbouiCOM.BoEventTypes.et_FORM_LOAD);
                oFiltro = oFiltros.Add(SAPbouiCOM.BoEventTypes.et_CHOOSE_FROM_LIST);
                oFiltro = oFiltros.Add(SAPbouiCOM.BoEventTypes.et_GOT_FOCUS);
               
                oFiltro = oFiltros.Add(SAPbouiCOM.BoEventTypes.et_LOST_FOCUS);
                oFiltro = oFiltros.Add(SAPbouiCOM.BoEventTypes.et_FORM_DATA_ADD);
                oFiltro = oFiltros.Add(SAPbouiCOM.BoEventTypes.et_FORM_DATA_LOAD);
                oFiltro = oFiltros.Add(SAPbouiCOM.BoEventTypes.et_COMBO_SELECT);
                
                oFiltro = oFiltros.Add(SAPbouiCOM.BoEventTypes.et_PRINT_LAYOUT_KEY);
                oFiltro.AddEx(Formularios.Menu.frmMenu);

                //oFiltro = oFiltros.Add(SAPbouiCOM.BoEventTypes.et_ITEM_PRESSED);
                //oFiltro = oFiltros.Add(SAPbouiCOM.BoEventTypes.et_MENU_CLICK);
                //oFiltro = oFiltros.Add(SAPbouiCOM.BoEventTypes.et_FORM_LOAD);
                oFiltro.AddEx(Formularios.FormTransferencia.TipoFormulariostr);
                //oFiltro = oFiltros.Add(SAPbouiCOM.BoEventTypes.et_FORM_DATA_UPDATE);
                oFiltro.AddEx(Formularios.FormCarga.TipoFormulariostr);
                oFiltro.AddEx(Formularios.FormFijaciones.TipoFormulariostr);
                oFiltro.AddEx(Formularios.FormContrato.frmContrato);
                //oFiltro = oFiltros.Add(SAPbouiCOM.BoEventTypes.et_ITEM_PRESSED);
                //oFiltro.AddEx(Formularios.FormMonitor.strFormulario);
                //oFiltro.AddEx(Formularios.FormMonitor.strFormulario);
                //Esto funciona en cascada, se define el filtro y los formularios que estan debajo, es para donde aplican esos filtros          
                //De esa forma podemos filtrar en que formulario en especifico queremos que se use dicho filtro
                //De esta forma seteamos los filtros
                ConexiónSAPB1.oSAPB1appl.SetFilter(oFiltros);

            }
            catch (Exception ex)
            {

                ConexiónSAPB1.oSAPB1appl.StatusBar.SetText("Eventos()" + ex.Message, SAPbouiCOM.BoMessageTime.bmt_Medium, SAPbouiCOM.BoStatusBarMessageType.smt_Error);
            }
        }

        #endregion
    }
}

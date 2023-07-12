using SAPbobsCOM;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModuloGrano.Formularios
{
    public class FormContrato
    {

        #region Atributos
        public const string frmContratostr = "139";
        public const string frmContrato = "60004";
        private static SAPbouiCOM.Form oForm = null;
        SAPbouiCOM.DataTable oDt = null;

        #endregion

        #region Constructor
        public FormContrato() { }
        #endregion

        #region Eventos
        public void OSAPB1appl_ItemEvent(string FormUID, ref SAPbouiCOM.ItemEvent pVal, out bool BubbleEvent)
        {
            BubbleEvent = true;

            SAPbouiCOM.Matrix oMatrix = null;
            SAPbouiCOM.Column oColumn = null;
            SAPbouiCOM.EditText oEdit = null;
            SAPbouiCOM.EditText oEditGrano = null;
            string CodigoGrano = "";
            string DocNum = "";
            string Campaña = "";
            string NormaGrano = "";
            string NormaCamp = "";

            oForm = Comunes.ConexiónSAPB1.oSAPB1appl.Forms.Item(pVal.FormUID);
           
            try
            {
                if (!pVal.BeforeAction)
                {
                    oMatrix = (SAPbouiCOM.Matrix)oForm.Items.Item("38").Specific;
                   
                    switch (pVal.EventType)
                    {

                        case SAPbouiCOM.BoEventTypes.et_LOST_FOCUS:
                            if (pVal.ColUID == "1")
                            {
                                //oEdit = (SAPbouiCOM.EditText)oMatrix.Columns.Item("2001").Cells.Item(1).Specific;
                                oEditGrano = (SAPbouiCOM.EditText)oMatrix.Columns.Item("1").Cells.Item(1).Specific;

                                //switch (oEdit.Value)
                                //{
                                //    case "C23-24":
                                //        Campaña = "24";
                                //        break;
                                //    case "C22-23":
                                //        Campaña = "23";
                                //        break;

                                //}
                                switch (oEditGrano.Value)
                                {
                                    case "GRA-000026-CP":
                                        CodigoGrano = "23";
                                        NormaGrano = "SOJA";
                                        NormaCamp = "C22-23";
                                        Campaña = "23";
        
                                        break;
                                    case "GRA-000027-CP":
                                        CodigoGrano = "22";
                                        NormaGrano = "SORGO";
                                        NormaCamp = "C22-23";
                                        Campaña = "23";

                                        break;
                                    case "GRA-000028-CP":
                                        CodigoGrano = "19";
                                        NormaGrano = "MAIZ";
                                        NormaCamp = "C22-23";
                                        Campaña = "23";
                                        break;
                                    case "GRA-000029-CP":
                                        CodigoGrano = "24";
                                        NormaGrano = "GRA-000029-CP";
                                        NormaCamp = "C22-23";
                                        Campaña = "23";
                                        break;
                                    case "GRA-000069-EM":
                                        CodigoGrano = "74";
                                        NormaGrano = "POROMUNG";
                                        NormaCamp = "C22-23";
                                        Campaña = "23";
                                        break;
                                    case "GRA-000070-EM":
                                        CodigoGrano = "34";
                                        NormaGrano = "CRANBERR";
                                        NormaCamp = "C22-23";
                                        Campaña = "23";
                                        break;
                                    case "GRA-000071-EM":
                                        CodigoGrano = "33";
                                        NormaCamp = "C22-23";
                                        NormaGrano = "POROBLAN";
                                        Campaña = "23";
                                        break;
                                    case "GRA-000072-EM":
                                        CodigoGrano = "62";
                                        NormaGrano = "CHIA";
                                        NormaCamp = "C22-23";
                                        Campaña = "23";
                                        break;
                                    case "GRA-000073-EM":
                                        CodigoGrano = "71";
                                        NormaGrano = "POROROJO";
                                        NormaCamp = "C22-23";
                                        Campaña = "23";
                                        break;
                                    case "GRA-000074-EM":
                                        CodigoGrano = "27";
                                        NormaGrano = "MAIZPISI";
                                        NormaCamp = "C22-23";
                                        Campaña = "23";
                                        break;
                                    case "GRA-000031-CP":
                                        CodigoGrano = "23";
                                        NormaGrano = "SOJA";
                                        NormaCamp = "C23-24";
                                        Campaña = "24";
                                        break;


                                }
                                //Completo normas de reparto
                                ((SAPbouiCOM.EditText)oMatrix.Columns.Item("2004").Cells.Item(1).Specific).Value = "GRANOS";
                                ((SAPbouiCOM.EditText)oMatrix.Columns.Item("2003").Cells.Item(1).Specific).Value = "NOAPLICA";
                                ((SAPbouiCOM.EditText)oMatrix.Columns.Item("2002").Cells.Item(1).Specific).Value = NormaGrano;
                                ((SAPbouiCOM.EditText)oMatrix.Columns.Item("2001").Cells.Item(1).Specific).Value = NormaCamp;
                                ((SAPbouiCOM.EditText)oMatrix.Columns.Item("2000").Cells.Item(1).Specific).Value = "ADG";
                                DocNum = ((SAPbouiCOM.EditText)oForm.Items.Item("8").Specific).Value;
                                ((SAPbouiCOM.EditText)oForm.Items.Item("U_NroContrato").Specific).String = "VC" + CodigoGrano + "-" + Campaña + "-" + DocNum;
                            }
                            break;
                       

                    }
                    //Completo numero de contrato
                   

                }
      

            }
            catch (Exception)
            {

                throw;
            }
        }


        #endregion

        #region Metodos

        public void NormaGrano(string FormUID)
        {
            string Grano = "";
            string NormaGrano = "";
            try
            {
                oForm = Comunes.ConexiónSAPB1.oSAPB1appl.Forms.Item(FormUID);
                ((SAPbouiCOM.EditText)oForm.Items.Item("Item_8").Specific).Value = "GRANOS";
                ((SAPbouiCOM.EditText)oForm.Items.Item("Item_21").Specific).Value = "NOAPLICA";
                ((SAPbouiCOM.EditText)oForm.Items.Item("Item_50").Specific).Value = "ADG";
                Grano = ((SAPbouiCOM.EditText)oForm.Items.Item("Item_0").Specific).Value;
                switch (Grano)
                {
                    case "GRA-000026-CP":
                        NormaGrano = "SOJA";
                        ((SAPbouiCOM.EditText)oForm.Items.Item("Item_22").Specific).Value = "SOJA";
                        break;
                    case "GRA-000027-CP":
                        NormaGrano = "SORGO";
                        ((SAPbouiCOM.EditText)oForm.Items.Item("Item_22").Specific).Value = "SORGO";
                        break;
                    case "MAÍZ 22-23":
                        NormaGrano = "MAIZ";
                        ((SAPbouiCOM.EditText)oForm.Items.Item("Item_22").Specific).Value = "MAIZ";
                        break;
                    case "GRA-000029-CP":
                        NormaGrano = "SOJA";
                        ((SAPbouiCOM.EditText)oForm.Items.Item("Item_22").Specific).Value = "SOJA";
                        break;
                    case "GRA-000069-EM":
                        NormaGrano = "POROMUNG";
                        ((SAPbouiCOM.EditText)oForm.Items.Item("Item_22").Specific).Value = "POROMUNG";
                        break;
                    case "GRA-000070-EM":
                        NormaGrano = "POROTOCR";
                        ((SAPbouiCOM.EditText)oForm.Items.Item("Item_22").Specific).Value = "POROTOCR";
                        break;
                    case "GRA-000071-EM":
                        break;
                    case "GRA-000072-EM":
                        NormaGrano = "CHIA";
                        ((SAPbouiCOM.EditText)oForm.Items.Item("Item_22").Specific).Value = "CHIA";
                        break;
                    case "GRA-000073-EM":

                        break;
                    case "GRA-000074-EM":
                        NormaGrano = "MAIZPISI";
                        ((SAPbouiCOM.EditText)oForm.Items.Item("Item_22").Specific).Value = "MAIZPISI";
                        break;
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

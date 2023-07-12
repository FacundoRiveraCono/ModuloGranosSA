using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModuloGrano.Formularios
{
    public class FormFijaciones
    {

        #region Atributos
        public const string Formulario = "Fijaciones";
        public const string TipoFormulariostr = "UDO_FT_FIJACIONES";
        public SAPbouiCOM.Form oForm = null;
        public SAPbouiCOM.Matrix oMatrix = null;

        #endregion

        #region Constructor
        public FormFijaciones() { }
        #endregion

        #region Eventos

        public void OSAPB1appl_ItemEvent(string FormUID, ref SAPbouiCOM.ItemEvent pVal, out bool BubbleEvent)
        {
            BubbleEvent = true;
            try
            {
                if (pVal.BeforeAction)
                {
                    switch (pVal.EventType)
                    {
                        case SAPbouiCOM.BoEventTypes.et_ITEM_PRESSED:
                            if (pVal.ItemUID== "btnAddL")
                            {
                                AgregarLinea(FormUID);
                            }
                            if (pVal.ItemUID == "btnDelL")
                            {

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

        public void AgregarLinea(string FormUID)
        {
            try
            {
                oForm = Comunes.ConexiónSAPB1.oSAPB1appl.Forms.Item(FormUID);
                oMatrix = (SAPbouiCOM.Matrix)oForm.Items.Item("0_U_G").Specific;
                oMatrix.AddRow();
            }
            catch (Exception)
            {

                throw;
            }
        }

            #endregion
        }
}

using ModuloGrano.Comunes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModuloGrano.Formularios
{
    public class Menu
    {

        #region Atributos
        public const string frmMenu = "CartaPorte";
        public const string frmCarga = "Carga";
        public const string frmFijacion = "Fijaciones";
        //public const string ListaPrecio = "ListaPrecio";
        //public const string Autorizaciones = "Autorizaciones";
        #endregion

        #region Constructor 
        public Menu()
        {

           

        }

       
        #endregion

        #region Eventos

        public void OSAPB1appl_MenuEvent(ref SAPbouiCOM.MenuEvent pVal, out bool BubbleEvent)
        {
            BubbleEvent = true;
            //string Nombre = "CartaPorteSA.xml";
            try
            {
                
                if (!pVal.BeforeAction)
                {

                    if (pVal.MenuUID == "CartaPorte")
                    {
                        //frmGastosLogisticos();
                        frmCartaPorte();
                        //LoadFromXML(ref Nombre);
                    }
                    if (pVal.MenuUID == "Carga")
                    {
                        frmCargas();
                    }
                    if (pVal.MenuUID == "Fijaciones")
                    {
                        frmFijaciones();
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


        private void frmCartaPorte()
        {
            try
            {
                //Comunes.ConexiónSAPB1.oSAPB1appl.LoadBatchActions(Properties.Resources.Form);
                Comunes.ConexiónSAPB1.oSAPB1appl.LoadBatchActions(Properties.Resources.CartaPorteV2);
            }
            catch (Exception)
            {

                throw;
            }
        }
        private void frmCargas()
        {
            try
            {
                //Comunes.ConexiónSAPB1.oSAPB1appl.LoadBatchActions(Properties.Resources.Form);
                Comunes.ConexiónSAPB1.oSAPB1appl.LoadBatchActions(Properties.Resources.CargaCPE);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void frmFijaciones()
        {
            try
            {
                Comunes.ConexiónSAPB1.oSAPB1appl.LoadBatchActions(Properties.Resources.FijacionPrecio);
            }
            catch (Exception)
            {

                throw;
            }

        }


        private void LoadFromXML(ref string FileName)
        {

            System.Xml.XmlDocument oXmlDoc = null;

            oXmlDoc = new System.Xml.XmlDocument();

            // load the content of the XML File
            string sPath = null;

            sPath = System.IO.Directory.GetParent(Application.StartupPath).ToString();
            sPath = System.IO.Directory.GetParent(sPath).ToString();

            oXmlDoc.Load(sPath + "\\" + FileName);

            // load the form to the SBO application in one batch
            string sXML = oXmlDoc.InnerXml.ToString();
            //SBO_Application.LoadBatchActions(ref sXML);
            Comunes.ConexiónSAPB1.oSAPB1appl.LoadBatchActions(ref sXML);

        }






        #endregion
    }
}

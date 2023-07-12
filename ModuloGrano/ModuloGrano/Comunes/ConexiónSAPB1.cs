using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ModuloGrano.Comunes
{
    class ConexiónSAPB1
    {

        #region Atributos

        public static SAPbouiCOM.Application oSAPB1appl = null;
        public static SAPbobsCOM.Company oCompanyDIAPI = null;

        #endregion

        #region Constructor

        public ConexiónSAPB1()
        {
            ObtenerAplicacion();
            if (oSAPB1appl != null)
            {
                //Si la variable es diferente de null es porque conectamos a SAP y podemos conectar a DIAPI
                ConectarDIAPI();
            }
        }

        #endregion

        #region Metodos

        public void ObtenerAplicacion()
        {
            // Para poder establecer la conexión a nuestra aplicacion es que tenemos que utulizar un string de argumento
            //Que nos permite establecer esa "relacion de confianza" entre nuestro addon y la interfaz de SAP
            //Documentacion de ayuda => Guia del desarrollador =>UIAPI=> How To=> Conecting to UIAPI
            bool activo = true;
            string strCon = "";
            string[] strArgumentos = new string[4];
            SAPbouiCOM.SboGuiApi oSboGuiApi = null;

            try
            {
                oSboGuiApi = new SAPbouiCOM.SboGuiApi();
                strArgumentos = System.Environment.GetCommandLineArgs();
                //////Siemrpe es bueno realizar algunas validaciones
                if (strArgumentos.Length > 0)
                {
                    //Si es mayor que cero significa que tenemos argumentos y podes realizar la conexión
                    //Si sap esta activo entramos en dos posibles caminos
                    //Camino 1. Nosotros estemos ejecutando nuestra aplicacion en modo debug y tomar los argumentos tomandso en nuestra linea de comandos
                    //Camino 2. Los argumentos pueden venir proporcionados directamente desde sap b1, esto es cuando esta en modo release y lista para se instalada en el cliente
                    #region Modo Debug
                    if (strArgumentos.Length > 1)
                    {
                        //Si es mayor que uno es que estamos ejecutando en modo debug
                        //Si es igual que uno es en modo release

                        if (strArgumentos[0].LastIndexOf("\\") > 0)
                            //El bloque de la pos 1 corresponde a la ejecucion en modo debug
                            //Dodne nuestro posiicon 1 del array corresponde a la cade de la linea de comandos
                            strCon = strArgumentos[1];
                        else
                            //Sino viene proporcionado directamente desde sap b1, en modoe relase
                            strCon = strArgumentos[0];
                    }
                    #endregion
                    #region Modo Release
                    else
                    {

                        if (strArgumentos[0].LastIndexOf("\\") > -1)
                            strCon = strArgumentos[0];
                        else
                        {
                            activo = false;
                            MessageBox.Show("Debe existir una instancia de SAP Business One Activa");
                        }
                    }
                    #endregion

                }
                else
                {
                    activo = false;
                    MessageBox.Show("Debe Existir una instancia de SAP Business One Activa");
                    //Las aplicaciones que utilizan DIAPI deben ejecutarse siempre dentro de un cliente SAP B1

                }

                //Una vez realizada la validación se prosigue con hacer la conexión.
               //strCon = "0030002C0030002C00530041005000420044005F00440061007400650076002C0050004C006F006D0056004900490056";
                oSboGuiApi.AddonIdentifier = "Modulo de Granos";
                oSboGuiApi.Connect(strCon);
                //Se pasa el argumento -1 para que nos traiga    al aplicación que estamos ejecutando
                oSAPB1appl = oSboGuiApi.GetApplication(-1);

            }
            catch (Exception ex)
            {

                //MessageBox.Show(ex.Message);
            }

        }

        public void ConectarDIAPI()
        {
            int ErrCode = 0;
            string strErroMes = "";
            string strCookie = "";

            try
            {
                if (oCompanyDIAPI == null)
                {

                    //Nueva Instancia
                    oCompanyDIAPI = new SAPbobsCOM.Company();
                    oCompanyDIAPI = (SAPbobsCOM.Company)oSAPB1appl.Company.GetDICompany();

                }



            }
            catch (Exception ex)
            {

                oSAPB1appl.StatusBar.SetText(ex.Message, SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Error);
            }
        }

       

        #endregion
    }
}

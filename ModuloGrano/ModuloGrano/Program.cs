namespace ModuloGrano
{
    using ModuloGrano.Objetos;
    using SAPbobsCOM;
    using SAPbouiCOM;
    using System.Security;
    using System.Xml;

    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        //Variable Interfaz (Con esto podemos afectar la UI de SAP)
        public static SAPbouiCOM.Application SBO_Application = null;
        //Variable UIAPI conexión
        public static SAPbobsCOM.Company oCompany = null;
        public static string[] DocEntryRef = new string[2];
        [STAThread]
        static void Main()
        {
            Comunes.ConexiónSAPB1 oConexion = new Comunes.ConexiónSAPB1();
            Comunes.Eventos oEventos = null;
            //Formularios.FormEntrega oForm = null;
            if (Comunes.ConexiónSAPB1.oSAPB1appl != null && Comunes.ConexiónSAPB1.oCompanyDIAPI.Connected)
            {
                CrearMenu();
                Comunes.ConexiónSAPB1.oSAPB1appl.StatusBar.SetText("Modulo de Granos Conectado", BoMessageTime.bmt_Medium, BoStatusBarMessageType.smt_Success);
                oEventos = new Comunes.Eventos();
               
            }
            GC.KeepAlive(oEventos);
            GC.KeepAlive(oConexion);
            System.Windows.Forms.Application.Run();
        }

        private static void CrearMenu()
        {
            SAPbouiCOM.Menus oMenus = null;
            SAPbouiCOM.MenuItem oMenuItem = null;
            try
            {
                SAPbouiCOM.MenuCreationParams oCreationPackage = null;
                oCreationPackage = (SAPbouiCOM.MenuCreationParams)Comunes.ConexiónSAPB1.oSAPB1appl.CreateObject(SAPbouiCOM.BoCreatableObjectType.cot_MenuCreationParams);
                oMenuItem = Comunes.ConexiónSAPB1.oSAPB1appl.Menus.Item("43520");

                oCreationPackage.Type = SAPbouiCOM.BoMenuType.mt_POPUP;
                oCreationPackage.UniqueID = "ModuloGranos";
                oCreationPackage.String = "Modulo Granos";
                oCreationPackage.Enabled = true;
                oCreationPackage.Position = 15;

                oMenus = oMenuItem.SubMenus;

                try
                {
                    if (!oMenus.Exists("ModuloGranos"))
                    oMenus.AddEx(oCreationPackage);
                }
                catch (Exception e)
                {

                   
                }

                try
                {
                    oMenuItem = Comunes.ConexiónSAPB1.oSAPB1appl.Menus.Item("ModuloGranos");
                    oMenus = oMenuItem.SubMenus;

                   
                    //Crear SubMenu
                    oCreationPackage.Type = SAPbouiCOM.BoMenuType.mt_STRING;
                    oCreationPackage.UniqueID = "CartaPorte";
                    oCreationPackage.String = "Carta de Porte";
                    if (!oMenus.Exists("CartaPorte"))
                    oMenus.AddEx(oCreationPackage);
                    //Crear Submenu
                    oCreationPackage.Type = SAPbouiCOM.BoMenuType.mt_STRING;
                    oCreationPackage.UniqueID = "Carga";
                    oCreationPackage.String = "Carga";
                    if (!oMenus.Exists("Carga"))
                    oMenus.AddEx(oCreationPackage);
                    //Crear Submenu
                    oCreationPackage.Type = SAPbouiCOM.BoMenuType.mt_STRING;
                    oCreationPackage.UniqueID = "Fijaciones";
                    oCreationPackage.String = "Fijaciones";
                    if (!oMenus.Exists("Fijaciones"))
                        oMenus.AddEx(oCreationPackage);
                   
                    ////Crear SubMenu
                    //oCreationPackage.Type = SAPbouiCOM.BoMenuType.mt_STRING;
                    //oCreationPackage.UniqueID = "Autorizaciones";
                    //oCreationPackage.String = "Autorizaciones";
                    //oMenus.AddEx(oCreationPackage);
                }
                catch (Exception ex)
                {

                    Comunes.ConexiónSAPB1.oSAPB1appl.SetStatusBarMessage("El menu ya existe", BoMessageTime.bmt_Short, true);
                }

                
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
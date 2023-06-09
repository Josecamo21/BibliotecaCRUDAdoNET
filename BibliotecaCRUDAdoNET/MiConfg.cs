using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibliotecaCRUDAdoNET
{
    public static class MiConfg
    {
        public static string GetCadConexion
        {
            get {
                return Properties.Settings.Default.cadenaConexion;
            }//Propiedad de Solo lectura
        }
    }
}


//Cadena Genérica = "Server = localhost; Database = biblioteca; Integrated Security = True;"
//"Server = localhost; Database = biblioteca; User ID=generico; Password=123;"
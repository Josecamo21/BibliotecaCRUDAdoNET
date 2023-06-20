using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Usuario
    {

        public Usuario()
        {
            ClaveUsuario = "";
            CURP = "";
            Nombre = "";
            ApPaterno = "";
            ApMaterno = "";
            Email = "";
            Direccion = "";
            Moroso = "";
        }
        
        public Usuario(string claveU, string curp, string nom, string apPat, string apMat, DateTime fecha, string mail,string direc, string moro)
        {
            ClaveUsuario = claveU;
            CURP = curp;
            Nombre = nom;
            ApPaterno = apPat;
            ApMaterno = apMat;
            FechaNacimiento = fecha;
            Email = mail;
            Direccion = direc;
            Moroso = moro;
        }

        //Propiedades
        public string ClaveUsuario { get; set; }
        public string CURP { get; set; }
        public string Nombre { get; set; }
        public string ApPaterno { get; set; }
        public string ApMaterno { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public string Email { get; set; }
        public string Direccion { get; set; }

        public string Moroso { get; set; }
    }
}

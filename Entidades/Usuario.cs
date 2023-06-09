using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidadess
{
    class Usuario
    {
        public string ClaveUsuario { get; set; }
        public string Curp { get; set; }
        public string Nombre { get; set; }
        public string ApPaterno { get; set; }
        public string ApMaterno { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public string Email { get; set; }
        public string Direccion { get; set; }

        public Usuario(string claveUsuario, string curp, string nombre, string apPaterno, string apMaterno, DateTime fechaNacimiento, string email, string direccion)
        {
            ClaveUsuario = claveUsuario;
            Curp = curp;
            Nombre = nombre;
            ApPaterno = apPaterno;
            ApMaterno = apMaterno;
            FechaNacimiento = fechaNacimiento;
            Email = email;
            Direccion = direccion;
        }
        public Usuario()
        {
            ClaveUsuario = "";
            Curp = "";
            Nombre = "";
            ApPaterno = "";
            ApMaterno = "";
            Email = "";
            Direccion = "";
        }
    }
}

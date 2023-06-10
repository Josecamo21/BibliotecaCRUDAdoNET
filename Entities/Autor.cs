using System;
using System.Collections.Generic;
using System.Text;

namespace Entities
{
    public class Autor
    {
        private string claveAutor;
        private string nombre;
        private string apMaterno;
        private string apPaterno;

        public Autor(string claveAutor, string nombre, string apMaterno, string apPaterno)
        {
            this.claveAutor = claveAutor;
            this.nombre = nombre;
            this.apMaterno = apMaterno;
            this.apPaterno = apPaterno;
        }

        public Autor()
        {
            this.claveAutor = "";
            this.nombre = "";
            this.apMaterno = "";
            this.apPaterno = "";
        }

        public string ClaveAutor { get => claveAutor; set => claveAutor = value; }
        public string Nombre { get => nombre; set => nombre = value; }
        public string ApMaterno { get => apMaterno; set => apMaterno = value; }
        public string ApPaterno { get => apPaterno; set => apPaterno = value; }
    }
}

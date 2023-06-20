using System;
using System.Collections.Generic;
using System.Text;

namespace Entities
{
    public class Ejemplar
    {
        public Ejemplar()
        {
            ClaveEjemplar = "";
            Libro = new Libro();
            ClaveCondicion = "";
            ClaveEstado = "";
            Edicion = "";
            ClaveEditorial = "";
            Paginas = 0;
        }

        public Ejemplar(string ejemplar, Libro libro, string condicion, string estado, string edicion, string editorial, int paginas)
        {
            ClaveEjemplar = ejemplar;
            Libro = libro;
            ClaveCondicion = condicion;
            ClaveEstado = estado;
            Edicion = edicion;
            ClaveEditorial = editorial;
            Paginas = paginas;
        }

        //Propiedades
        public string ClaveEjemplar { get; set; }
        public Libro Libro { get; set; }
        public string ClaveCondicion { get; set; }
        public string ClaveEstado { get; set; }
        public string Edicion { get; set; }
        public string ClaveEditorial { get; set; }
        public int Paginas { get; set; }
    }
}

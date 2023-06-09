using System;

namespace Entities
{
    public class Libro
    {
        //Atributos Automáticos
        //C# los genera por la definición de Propiedades

        //Constructor(es)
        public Libro()
        {
            ClaveLibro = "";
            Titulo = "";
            ClaveAutor = "";
            ClaveCategoria = "";
            Existente = false;
        }
        public Libro(string claveL, string titL, string claveA, string claveC)
        {
            ClaveLibro = claveL;
            Titulo = titL;
            ClaveAutor = claveA;
            ClaveCategoria = claveC;
            Existente = false;
        }

        //Propiedades
        public string ClaveLibro { get; set; }
        public string Titulo { get; set; }
        public string ClaveAutor { get; set; }
        public string ClaveCategoria { get; set; }
        public bool Existente { get; set; }

        //Métodos Opcional - Especialmente para clases que tengan funcionalidades particulares
    }
}

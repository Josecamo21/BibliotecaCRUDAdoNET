using System;

namespace Entidadess
{
    public class Libro
    {
        //Atributos Automáticos
            //C# los genera por la definición de Propiedades

        //Constructor(es)
        public Libro() {
            ClaveLibro = "";
            Titulo = "";
            ClaveAutor = "";
            ClaveCategoria = "";
        }
        public Libro(string claveL, string titL, string claveA, string claveC)
        {
            ClaveLibro = claveL;
            Titulo = titL;
            ClaveAutor = claveA;
            ClaveCategoria = claveC;
        }
    
        //Propiedades
        public string ClaveLibro{get; set;}
        public string Titulo { get; set; }
        public string ClaveAutor { get; set; }
        public string ClaveCategoria { get; set; }

        //Métodos Opcional - Especialmente para clases que tengan funcionalidades particulares
    }
}

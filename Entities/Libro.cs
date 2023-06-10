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
            Autor = new Autor();
            Categoria = new Categoria();
            Existente = false;
        }
        public Libro(string claveL, string titL, Autor aut, Categoria cat)
        {
            ClaveLibro = claveL;
            Titulo = titL;
            Autor = aut;
            Categoria = cat;
            Existente = false;
        }

        //Propiedades
        public string ClaveLibro { get; set; }
        public string Titulo { get; set; }
        public Autor Autor { get; set; }
        public Categoria Categoria { get; set; }
        public bool Existente { get; set; }

        //Métodos Opcional - Especialmente para clases que tengan funcionalidades particulares
    }
}

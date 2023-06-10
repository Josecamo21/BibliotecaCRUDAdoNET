using System;
using System.Collections.Generic;
using System.Text;

namespace Entities
{
    public class Categoria
    {
        private string claveCategoria;
        private string descripcion;

        public Categoria(string claveCategoria, string descripcion)
        {
            this.ClaveCategoria = claveCategoria;
            this.Descripcion = descripcion;
        }
        
        public Categoria()
        {
            this.ClaveCategoria = "";
            this.Descripcion = "";
        }

        public string ClaveCategoria { get => claveCategoria; set => claveCategoria = value; }
        public string Descripcion { get => descripcion; set => descripcion = value; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace Entities
{
    public class Prestamo
    {
        public Prestamo()
        {
            ClavePrestamo = "";
            Ejemplar = new Ejemplar();
            Usuario = new Usuario();
            //FechaPrestamo = "";
            //FechaDevolucion = "";
        }

        public Prestamo(string prestamo, Ejemplar ejemplar, Usuario usuario, DateTime fecPrestamo, DateTime fecDevolucion)
        {
            ClavePrestamo = prestamo;
            Ejemplar = ejemplar;
            Usuario = usuario;
            FechaPrestamo = fecPrestamo;
            FechaDevolucion = fecDevolucion;
        }

        //Propiedades
        public string ClavePrestamo { get; set; }
        public Ejemplar Ejemplar { get; set; }
        public Usuario Usuario { get; set; }
        public DateTime FechaPrestamo { get; set; }
        public DateTime FechaDevolucion { get; set; }
    }
}

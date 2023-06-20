using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using AccesoDatos;
using Entities;

namespace LogicaNegocio
{
    public class BLPrestamo
    {
        string cadConexion;

        /// <summary>
        /// Constructor con la cadena de conexion
        /// </summary>
        /// <param name="cadenaConexion">string con la cadena de conexion</param>
        public BLPrestamo(string cadenaConexion)
        {
            cadConexion = cadenaConexion;
        }

        public DataSet ListarRegistros(string filtro = "")
        {
            //Comunicarse a la capa de Acceso a Datos

            DataSet ds = new DataSet();
            DAPrestamo dAPrestamo = new DAPrestamo(cadConexion);//Instancia capa de ACCESO A DATOS

            try
            {
                ds = dAPrestamo.ListarRegistros(filtro);//Llamado a la capa de ACCESO A DATOS
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return ds;//Iría lleno, a la capa de PRESENTACION
        }

        public Prestamo RegistroCompleto(string condicion)
        {
            Prestamo prestamo;

            DAPrestamo dAPrestamo = new DAPrestamo(cadConexion);

            try
            {
                prestamo = dAPrestamo.RegistroCompleto(condicion);


                //deberan hacer lo necesario para llenar el autor y la categoria de forma completa
                //prestamo = dAAutor.RegistroCompleto($"claveAutor = '{prestamo.Autor.ClaveAutor}'");

                //prestamo = dACategoria.RegistroCompleto($"claveCategoria = '{prestamo.Categoria.ClaveCategoria}'");
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return prestamo;
        }

        public int Eliminar(string clavePrestamo)
        {
            int result = -1;
            bool act;
            DAPrestamo dAPrestamo = new DAPrestamo(cadConexion);

            try
            {
                result = dAPrestamo.Eliminar(clavePrestamo);

                act = dAPrestamo.ActualizarEstado(clavePrestamo, "ES003");
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }



        ////Prestamos
        public DataSet ListarRegistrosClientes()
        {
            DataSet ds = new DataSet();
            //Comunicarse a la capa de Accesso a Datos
            DAPrestamo daPrestamo = new DAPrestamo(cadConexion);//Instancia a la capa ACCSSO A DATOS

            try
            {
                ds = daPrestamo.ListarRegistrosClientes(); //Llamado a la capa ACCSSO A DATOS
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return ds; //Iria lleno a la capa de PRESENTACION
        }

        public Usuario RegistroCompletoUsuario(string condicion)
        {
            Usuario usuario;
            DAPrestamo daPrestamo = new DAPrestamo(cadConexion);

            try
            {
                usuario = daPrestamo.RegistroCompletoUsuario(condicion);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return usuario;
        }

        public DataSet ListarRegistrosEjemplares()
        {
            DataSet ds = new DataSet();
            //Comunicarse a la capa de Accesso a Datos
            DAPrestamo daPrestamo = new DAPrestamo(cadConexion);//Instancia a la capa ACCESSO A DATOS

            try
            {
                ds = daPrestamo.ListarRegistrosEjemplares(); //Llamado a la capa ACCESSO A DATOS
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return ds; //Iria lleno a la capa de PRESENTACION
        }

        public Ejemplar RegistroCompletoEjemplar(string condicion)
        {
            Ejemplar ejemplar;
            DAPrestamo daPrestamo = new DAPrestamo(cadConexion);
            DALibro daLibro = new DALibro(cadConexion);

            try
            {
                ejemplar = daPrestamo.RegistroCompletoEjemplar(condicion);

                ejemplar.Libro = daLibro.RegistroCompleto($"claveLibro = '{ejemplar.Libro.ClaveLibro}'");
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return ejemplar;
        }

        public int InsertarPrestamo(Prestamo prestamo)
        {
            DAPrestamo dAPrestamo = new DAPrestamo(cadConexion);
            int result = -1;
            string a = "",cero = "0";
            int ultimoP = 0;

            try
            {
                prestamo.ClavePrestamo = dAPrestamo.UltimoPrestamo();

                a = prestamo.ClavePrestamo.Substring(1, 4);
                ultimoP = int.TryParse(a, out ultimoP) ? int.Parse(a) : 0;
                ultimoP++;

                prestamo.ClavePrestamo = $"P{ultimoP}";

                while (prestamo.ClavePrestamo.Length != 5)
                {
                    prestamo.ClavePrestamo = $"P{cero}{ultimoP}";
                    cero += "0";
                }

                if (dAPrestamo.ActualizarEstado(prestamo.Ejemplar.ClaveEjemplar, "ES002"))
                {
                    result = dAPrestamo.InsertarPrestamo(prestamo);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }
    }
}

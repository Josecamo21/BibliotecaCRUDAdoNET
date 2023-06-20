using System;
using System.Data;
using AccesoDatos;
using Entities;

namespace LogicaNegocio
{
    public class BLLibro
    {
        string cadConexion;

        /// <summary>
        /// Constructor con Cadena de Conexion
        /// </summary>
        /// <param name="cadenaConexion">string con la cadena de Conexión</param>
        public BLLibro(string cadenaConexion) {
            cadConexion = cadenaConexion;
        }

        //Metodos - Este es el objetivo de la capa de Negocio! 
        public DataSet ListarRegistros(string filtro = "") {
            //Comunicarse a la capa de Acceso a Datos

            DataSet ds = new DataSet();
            DALibro daLibro = new DALibro(cadConexion);//Instancia capa de ACCESO A DATOS
            
            try
            {
                ds = daLibro.ListarRegistros(filtro);//Llamado a la capa de ACCESO A DATOS
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return ds;//Iría lleno, a la capa de PRESENTACION
        }

        public int Insertar(Libro libro) {
            int result;
            DALibro daLibro = new DALibro(cadConexion);

            try
            {
                result = daLibro.Insertar(libro);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }

        public bool Verificar(string tit, string claveA)
        {
            bool result;
            DALibro daLibro = new DALibro(cadConexion);

            try
            {
                result = daLibro.Verificar(tit, claveA);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }

        public bool Verificar(string claveLibro)
        {
            bool result;
            DALibro daLibro = new DALibro(cadConexion);

            try
            {
                result = daLibro.Verificar(claveLibro);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }

        public Libro RegistroCompleto(string condicion)
        {
            Libro libro;

            DAAutor dAAutor = new DAAutor(cadConexion);
            DALibro daLibro = new DALibro(cadConexion);
            DACategoria dACategoria = new DACategoria(cadConexion);

            try
            {
                libro = daLibro.RegistroCompleto(condicion);


                //deberan hacer lo necesario para llenar el autor y la categoria de forma completa
                libro.Autor = dAAutor.RegistroCompleto($"claveAutor = '{libro.Autor.ClaveAutor}'");

                libro.Categoria = dACategoria.RegistroCompleto($"claveCategoria = '{libro.Categoria.ClaveCategoria}'");
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return libro;
        }

        public bool Actualizar(Libro libro, string clave = "")
        {
            DALibro daLibro = new DALibro(cadConexion);
            bool result = false;

            try
            {
                result = daLibro.Actualizar(libro, clave);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }

        /// <summary>
        /// Funcion que Elimina un Libro permanentemente de la BD
        /// </summary>
        /// <param name="claveLibro">String con la clave del libro en la base de datos</param>
        /// <returns>Cualquier entero mayor a 0 indica que todo salio bien</returns>
        public int Eliminar(string claveLibro)
        {
            int result = -1;
            DALibro daLibro = new DALibro(cadConexion);

            try
            {
                result = daLibro.Eliminar(claveLibro);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }

        public bool EliminarConStoredProcedure(string claveLibro, out string msj)
        {
            bool result = false;
            DALibro daLibro = new DALibro(cadConexion);

            try
            {
                result = daLibro.EliminarConStoredProcedure(claveLibro, out msj);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }
    }
}

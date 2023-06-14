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
        public DataSet ListarRegistros() {
            //Comunicarse a la capa de Acceso a Datos

            DataSet ds = new DataSet();
            DALibro daLibro = new DALibro(cadConexion);//Instancia capa de ACCESO A DATOS
            
            try
            {
                ds = daLibro.ListarRegistros();//Llamado a la capa de ACCESO A DATOS
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
            //Autor autor;
            //Categoria categoria;

            DAAutor dAAutor = new DAAutor(cadConexion);
            DALibro daLibro = new DALibro(cadConexion);
            DACategoria dACategoria = new DACategoria(cadConexion);

            try
            {
                libro = daLibro.RegistroCompleto(condicion);

                libro.Autor = dAAutor.RegistroCompleto($"claveAutor = '{libro.Autor.ClaveAutor}'");


                //libro.Autor.Nombre = autor.Nombre;
                //libro.Autor.ApPaterno = autor.ApPaterno;
                //libro.Autor.ApMaterno = autor.ApMaterno;

                libro.Categoria = dACategoria.RegistroCompleto($"claveCategoria = '{libro.Categoria.ClaveCategoria}'");


                //libro.Categoria.Descripcion = categoria.Descripcion;
                //deberan hacer lo necesario para llenar el autor y la categoria de forma completa
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

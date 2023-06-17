using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using AccesoDatos;
using Entities;

namespace LogicaNegocio
{
    public class BLAutor
    {
        string cadConexion;

        /// <summary>
        /// Constructor con Cadena de Conexion
        /// </summary>
        /// <param name="cadenaConexion">string con la cadena de Conexión</param>
        public BLAutor(string cadenaConexion)
        {
            cadConexion = cadenaConexion;
        }

        public DataSet ListarRegistros(string condicion = "")
        {
            //Comunicarse a la capa de Acceso a Datos

            DataSet ds = new DataSet();
            DAAutor dAAutor = new DAAutor(cadConexion);//Instancia capa de ACCESO A DATOS

            try
            {
                ds = dAAutor.ListarRegistros(condicion);//Llamado a la capa de ACCESO A DATOS
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return ds;//Iría lleno, a la capa de PRESENTACION
        }

        public Autor RegistroCompleto(string condicion)
        {
            Autor autor;

            DAAutor dAAutor = new DAAutor(cadConexion);

            try
            {
                autor = dAAutor.RegistroCompleto(condicion);

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return autor;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using AccesoDatos;
using Entities;

namespace LogicaNegocio
{
    public class BLCategoria
    {
        string cadConexion;

        /// <summary>
        /// Constructor con Cadena de Conexion
        /// </summary>
        /// <param name="cadenaConexion">string con la cadena de Conexión</param>
        public BLCategoria(string cadenaConexion)
        {
            cadConexion = cadenaConexion;
        }

        public DataSet ListarRegistros()
        {
            //Comunicarse a la capa de Acceso a Datos

            DataSet ds = new DataSet();
            DACategoria dACategoria = new DACategoria(cadConexion);//Instancia capa de ACCESO A DATOS

            try
            {
                ds = dACategoria.ListarRegistros();//Llamado a la capa de ACCESO A DATOS
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return ds;//Iría lleno, a la capa de PRESENTACION
        }
    }
}

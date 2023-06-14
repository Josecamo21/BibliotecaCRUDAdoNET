using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Entities;

namespace AccesoDatos
{
    public class DACategoria
    {
        //Atributo
        string cadConn;

        public DACategoria(string cn)
        {
            cadConn = cn; // Inicializar Cadena de Conexión
        }

        public Categoria RegistroCompleto(string condicion)
        {
            Categoria categoria = new Categoria();

            SqlConnection conexionSQL = new SqlConnection(cadConn);
            SqlCommand comandoSQL = new SqlCommand();
            SqlDataReader dato;

            comandoSQL.Connection = conexionSQL;
            comandoSQL.CommandText = $"Select claveCategoria, descripcion from Categorias where {condicion}";

            try
            {
                conexionSQL.Open();
                dato = comandoSQL.ExecuteReader();
                if (dato.HasRows)
                {
                    dato.Read();//Cursor está en la primera fila

                    categoria.ClaveCategoria = dato.GetString(0);
                    categoria.Descripcion = dato.GetString(1);

                }
                conexionSQL.Close();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error recuperando Registro. Detalle: {ex.Message}");
            }
            finally
            {
                conexionSQL.Dispose();
                comandoSQL.Dispose();
            }

            return categoria;
        }
    }
}

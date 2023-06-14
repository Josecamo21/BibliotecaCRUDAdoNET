using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Entities;

namespace AccesoDatos
{
    public class DAAutor
    {
        //Atributo
        string cadConn;

        public DAAutor(string cn)
        {
            cadConn = cn; // Inicializar Cadena de Conexión
        }

        public Autor RegistroCompleto(string condicion)
        {
            Autor autor = new Autor();

            SqlConnection conexionSQL = new SqlConnection(cadConn);
            SqlCommand comandoSQL = new SqlCommand();
            SqlDataReader dato;

            comandoSQL.Connection = conexionSQL;
            comandoSQL.CommandText = $"Select claveAutor, nombre, apPaterno, apMaterno from Autores where {condicion}";

            try
            {
                conexionSQL.Open();
                dato = comandoSQL.ExecuteReader();
                if (dato.HasRows)
                {
                    dato.Read();//Cursor está en la primera fila

                    autor.ClaveAutor = dato.GetString(0);
                    autor.Nombre = dato.GetString(1);
                    autor.ApPaterno = dato.GetString(2);
                    if (!dato.IsDBNull(3))
                        autor.ApMaterno = dato.GetString(3);

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

            return autor;
        }
    }
}

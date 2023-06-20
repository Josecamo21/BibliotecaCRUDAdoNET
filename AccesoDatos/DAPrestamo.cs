using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Entities;

namespace AccesoDatos
{
    public class DAPrestamo
    {
        string cadConn;
        public DAPrestamo(string cn)
        {
            cadConn = cn; // Inicializar Cadena de Conexion
        }

        ////Prestamos
        public DataSet ListarRegistrosClientes()
        {
            //Comunicarse a la capa de Accesso a Datos
            DataSet miDS = new DataSet();
            string sentenciaSQL = "select * from vClientes";

            SqlConnection conexionSQL = new SqlConnection(cadConn);
            SqlDataAdapter adaptadorSQL;

            try
            {
                //En automatico el adaptador es capaz de abrir la conexion 
                adaptadorSQL = new SqlDataAdapter(sentenciaSQL, conexionSQL);
                adaptadorSQL.Fill(miDS); // Llenar dataSet con datos
            }
            catch (Exception ex)
            {
                throw new Exception($"Se ha presentado un error y no se logro cargar los Usuarios." +
                    $"Detalle: {ex.Message}");
            }
            finally
            {
                conexionSQL.Dispose();
            }

            return miDS;
        }

        public Usuario RegistroCompletoUsuario(string condicion)
        {
            Usuario usuario = new Usuario();

            SqlConnection conexionSQL = new SqlConnection(cadConn);
            SqlCommand comandoSQL = new SqlCommand();
            SqlDataReader dato;

            comandoSQL.Connection = conexionSQL;
            comandoSQL.CommandText = $"select u.claveUsuario,nombre,apPaterno,apMaterno,fechaNacimiento,email,direccion,claveEstado from USUARIOS u " +
                $"full join PRESTAMOS p on p.claveUsuario = u.claveUsuario " +
                $"full join EJEMPLARES e on e.claveEjemplar = p.claveEjemplar " +
                $"where {condicion}";

            try
            {
                conexionSQL.Open();
                dato = comandoSQL.ExecuteReader();
                if (dato.HasRows)
                {
                    dato.Read();
                    usuario.ClaveUsuario = dato.GetString(0);
                    usuario.Nombre = dato.GetString(1);
                    usuario.ApPaterno = dato.GetString(2);

                    if (!dato.IsDBNull(3))
                        usuario.ApMaterno = dato.GetString(3);

                    usuario.FechaNacimiento = dato.GetDateTime(4);

                    if (!dato.IsDBNull(5))
                        usuario.Email = dato.GetString(5);

                    if (!dato.IsDBNull(6))
                        usuario.Direccion = dato.GetString(6);

                    if (dato.IsDBNull(7))
                        usuario.Moroso = "No";
                    else if (dato.GetString(7) == "ES002")
                        usuario.Moroso = "Si";

                    //usuario.Moroso = dato.GetString(7) != "ES002" ? "No" : "Si";

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

            return usuario;
        }

        public DataSet ListarRegistrosEjemplares()
        {
            //Comunicarse a la capa de Accesso a Datos
            DataSet miDS = new DataSet();
            string sentenciaSQL = "select * from EJEMPLARES e " +
                "inner join LIBROS l on l.claveLibro = e.claveLibro  " +
                "where claveCondicion not like '%M%' and claveEstado = 'ES003'";

            SqlConnection conexionSQL = new SqlConnection(cadConn);
            SqlDataAdapter adaptadorSQL;

            try
            {
                //En automatico el adaptador es capaz de abrir la conexion 
                adaptadorSQL = new SqlDataAdapter(sentenciaSQL, conexionSQL);
                adaptadorSQL.Fill(miDS); // Llenar dataSet con datos
            }
            catch (Exception ex)
            {
                throw new Exception($"Se ha presentado un error y no se logro cargar los Ejemplares." +
                    $"Detalle: {ex.Message}");
            }
            finally
            {
                conexionSQL.Dispose();
            }

            return miDS;
        }

        public Ejemplar RegistroCompletoEjemplar(string condicion)
        {
            Ejemplar ejemplar = new Ejemplar();

            SqlConnection conexionSQL = new SqlConnection(cadConn);
            SqlCommand comandoSQL = new SqlCommand();
            SqlDataReader dato;

            comandoSQL.Connection = conexionSQL;
            comandoSQL.CommandText = $"select * from Ejemplares where {condicion}";

            try
            {
                conexionSQL.Open();
                dato = comandoSQL.ExecuteReader();
                if (dato.HasRows)
                {
                    dato.Read();
                    ejemplar.ClaveEjemplar = dato.GetString(0);
                    ejemplar.Libro.ClaveLibro = dato.GetString(1);
                    ejemplar.ClaveCondicion = dato.GetString(2) == "B" ? "Bueno" : "Regular";
                    ejemplar.ClaveEstado = dato.GetString(3);
                    ejemplar.Edicion = dato.GetString(4);
                    ejemplar.ClaveEditorial = dato.GetString(5);
                    ejemplar.Paginas = dato.GetInt32(6);
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

            return ejemplar;
        }

        public int InsertarPrestamo(Prestamo prestamo)
        {
            int result = -1;
            SqlConnection conexionSQL = new SqlConnection(cadConn);
            SqlCommand comandoSQL=new SqlCommand();

            comandoSQL.Connection = conexionSQL;
            comandoSQL.CommandText = $"insert into Prestamos values ('{prestamo.ClavePrestamo}','{prestamo.Ejemplar.ClaveEjemplar}','{prestamo.Usuario.ClaveUsuario}','{DateTime.Today}','{DateTime.Today.AddDays(30)}')";

            try
            {
                conexionSQL.Open();

                result = comandoSQL.ExecuteNonQuery();

                conexionSQL.Close();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error insertanto Registro. Detalle: {ex.Message}");
            }
            finally
            {
                conexionSQL.Dispose();
                comandoSQL.Dispose();
            }

            return result;
        }

        public string UltimoPrestamo()
        {
            string prestamo = "";

            SqlConnection conexionSQL = new SqlConnection(cadConn);
            SqlCommand comandoSQL = new SqlCommand();
            SqlDataReader dato;

            comandoSQL.Connection = conexionSQL;
            comandoSQL.CommandText = $"select clavePrestamo from PRESTAMOS order by clavePrestamo desc";

            try
            {
                conexionSQL.Open();
                dato = comandoSQL.ExecuteReader();
                if (dato.HasRows)
                {
                    dato.Read();
                    prestamo = dato.GetString(0);
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

            return prestamo;
        }

        public bool ActualizarEstado(string clave){
            bool result;

            string sentencia;

            SqlConnection conexionSQL = new SqlConnection(cadConn);
            SqlCommand comandoSQL = new SqlCommand();

            sentencia = $"Update Ejemplares set claveEstado='ES002' where claveEjemplar='{clave}'";
           
            comandoSQL.CommandText = sentencia;
            comandoSQL.Connection = conexionSQL;

            try
            {
                conexionSQL.Open();
                result = comandoSQL.ExecuteNonQuery() > 0 ? true : false;
                conexionSQL.Close();
            }
            catch (Exception ex)
            {
                throw new Exception($"Se ha presentado un problema! Detalle: {ex.Message}");
            }
            finally
            {
                conexionSQL.Dispose();
                comandoSQL.Dispose();
            }



            return result;
        }
    }
}

using System;
using System.Data;
using System.Data.SqlClient;
using Entities;

namespace AccesoDatos
{
    public class DALibro
    {
        //Atributo
        string cadConn;

        public DALibro(string cn) {
            cadConn = cn; // Inicializar Cadena de Conexión
        }

        //Métodos - Para conectarse a SQL Server
        public DataSet ListarRegistros()
        {
            //Comunicarse a la capa de Acceso a Datos
            DataSet miDS = new DataSet();
            string sentenciaSQL = "Select * From vLibros";//

            SqlConnection conexionSQL = new SqlConnection(cadConn);
            SqlDataAdapter adaptadorSQL;

            try
            {
                //En automático el Adaptador es capaz de abrir la conexión con SQL
                adaptadorSQL = new SqlDataAdapter(sentenciaSQL, conexionSQL);
                adaptadorSQL.Fill(miDS);//Llenar dataSet con datos
            }
            catch (Exception ex)
            {
                throw new Exception($"Se ha presentado un error y no se logró cargar los Libros.  Detalle:{ex.Message}");
            }
            finally {
                conexionSQL.Dispose();
            }

            return miDS;
        }

        public int Insertar(Libro libro) {
            SqlConnection conexionSQL = new SqlConnection(cadConn);
            SqlCommand comandoSQL = new SqlCommand();
            int result = -1;//Return de SQL nunca simpre regresan un valor entero positivo
            string sentencia;

            //sentencia = $"Insert into Libros(claveLibro, titulo, claveAutor,claveCategoria)values ('{libro.ClaveLibro}','{libro.Titulo}','{libro.ClaveAutor}','{libro.ClaveCategoria}')";


            //Sentencia con prámetros de SQL
            sentencia = "Insert into Libros(claveLibro, titulo, claveAutor,claveCategoria)values(@claveLibro, @titulo, @claveAutor, @claveCategoria)";

            //Rellenar los parámetros
            comandoSQL.Parameters.AddWithValue("@claveLibro", libro.ClaveLibro);
            comandoSQL.Parameters.AddWithValue("@titulo", libro.Titulo);
            comandoSQL.Parameters.AddWithValue("@claveAutor", libro.Autor.ClaveAutor);
            comandoSQL.Parameters.AddWithValue("@claveCategoria", libro.Categoria.ClaveCategoria);

            //Cargar la sentencia en el Objeto de Comando
            comandoSQL.CommandText = sentencia;

            //Relacionar comando con la conexión!
            comandoSQL.Connection = conexionSQL;

            try
            {
                //Abrir conexión con el servidor de bases de datos
                conexionSQL.Open();

                //Ejecutar Sentencias
                result = comandoSQL.ExecuteNonQuery();

                //Cerrar conexión
                conexionSQL.Close();
            }
            catch (Exception ex)
            {
                throw new Exception($"No se logró insertar el Libro. Detalle: {ex.Message}");
            }
            finally {
                conexionSQL.Dispose();
                comandoSQL.Dispose();
            }

            return result;
        }

        /// <summary>
        /// Este procedimiento recibe dos parámetros para revisar si el libro es repetido
        /// Se considera repetido al libro que tenga el mismo título y que sea 
        /// escrito por el mismo autor
        /// </summary>
        /// <param name="tit"></param>
        /// <param name="claveA"></param>
        /// <returns></returns>
        public bool Verificar(string tit, string claveA) {
            bool result;
            string sentencia;

            SqlConnection conexionSQL = new SqlConnection(cadConn);
            SqlCommand comandoSQL = new SqlCommand();
            SqlDataReader dato;

            sentencia = $"Select 1 From Libros Where titulo='{tit}' and claveAutor='{claveA}'";

            comandoSQL.Connection = conexionSQL;
            comandoSQL.CommandText = sentencia;

            try
            {
                conexionSQL.Open();
                dato = comandoSQL.ExecuteReader();
                result = dato.HasRows ? true : false;
                conexionSQL.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally {
                conexionSQL.Dispose();
                comandoSQL.Dispose();
            }

            return result;
        }

        /// <summary>
        /// Este procedimiento verificará si la Clave del Libro está repetida
        /// </summary>
        /// <param name="claveLibro"></param>
        /// <returns></returns>
        public bool Verificar(string claveLibro) {
            bool result;
            string sentencia;

            SqlConnection conexionSQL = new SqlConnection(cadConn);
            SqlCommand comandoSQL = new SqlCommand();
            SqlDataReader dato;

            sentencia = $"Select 1 From Libros Where claveLibro='{claveLibro}'";

            comandoSQL.Connection = conexionSQL;
            comandoSQL.CommandText = sentencia;

            try
            {
                conexionSQL.Open();
                dato = comandoSQL.ExecuteReader();
                result = dato.HasRows ? true : false;
                conexionSQL.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conexionSQL.Dispose();
                comandoSQL.Dispose();
            }

            return result;
        }

        public Libro RegistroCompleto(string condicion) {
            Libro libro = new Libro();

            SqlConnection conexionSQL = new SqlConnection(cadConn);
            SqlCommand comandoSQL = new SqlCommand();
            SqlDataReader dato;

            comandoSQL.Connection = conexionSQL;
            comandoSQL.CommandText = $"Select claveLibro, titulo, claveAutor, claveCategoria from vLibros where {condicion}";

            try
            {
                conexionSQL.Open();
                dato = comandoSQL.ExecuteReader();
                if (dato.HasRows)
                {
                    dato.Read();//Cursor está en la primera fila

                    //Se llena la instancia de Libro con los datos que están dentro del DATAREADER (dato)
                    libro.ClaveLibro = dato.GetString(0);
                    if (!dato.IsDBNull(1)) //Solo el título podría ser nulo en la BD, por eso validamos!
                        libro.Titulo = dato.GetString(1);
                    libro.Autor.ClaveAutor = dato.GetString(2);
                    libro.Categoria.ClaveCategoria = dato.GetString(3);
                    libro.Existente = true;//Porque es un libro cargado desde SQL
                }
                conexionSQL.Close();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error recuperando Registro. Detalle: {ex.Message}");
            }
            finally {
                conexionSQL.Dispose();
                comandoSQL.Dispose();
            }

            return libro;
        }

        public bool Actualizar(Libro libro, string clave="") { 
            //Clave traería una clave de libro vieja, que sería sustituída por una clave nueva!
            
            bool result = false;
            string sentencia;

            SqlConnection conexionSQL = new SqlConnection(cadConn);
            SqlCommand comandoSQL = new SqlCommand();

            if (string.IsNullOrEmpty(clave))
            {
                sentencia = "Update Libros set titulo=@titulo, claveAutor=@claveAutor, claveCategoria=@claveCategoria Where claveLibro=@claveLibro";
            }
            else
                sentencia = $"Update Libros set claveLibro=@claveLibro, titulo=@titulo, claveAutor=@claveAutor, claveCategoria=@claveCategoria Where claveLibro='{clave}'";

            comandoSQL.Parameters.AddWithValue("@claveLibro", libro.ClaveLibro);
            comandoSQL.Parameters.AddWithValue("@titulo", libro.Titulo);
            comandoSQL.Parameters.AddWithValue("@claveAutor", libro.Autor.ClaveAutor);
            comandoSQL.Parameters.AddWithValue("@claveCategoria", libro.Categoria.ClaveCategoria);

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
            finally {
                conexionSQL.Dispose();
                comandoSQL.Dispose();
            }

            return true;
        }

        public int Eliminar(string claveLibro) {
            int result = -1;
            string sentencia = "Delete From Libros Where claveLibro = @claveLibro";

            SqlConnection conexionSQL = new SqlConnection(cadConn);
            SqlCommand comandoSQL = new SqlCommand();

            comandoSQL.Connection = conexionSQL;
            comandoSQL.CommandText = sentencia;
            comandoSQL.Parameters.AddWithValue("@claveLibro", claveLibro);

            try
            {
                conexionSQL.Open();
                result = comandoSQL.ExecuteNonQuery();
                conexionSQL.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally {
                conexionSQL.Dispose();
                comandoSQL.Dispose();
            }

            return result;
        }

        public bool EliminarConStoredProcedure(string claveLibro, out string msj) {
            bool result = false;
            string sentencia = "SP_EliminarLibro";// "@claveLibro"

            SqlConnection conexionSQL = new SqlConnection(cadConn);
            SqlCommand comandoSQL = new SqlCommand();

            comandoSQL.Connection = conexionSQL;
            comandoSQL.CommandText = sentencia;

            //Esta especificación es OBLIGATORIA para ejecutar los Procedimientos Almacenados
            comandoSQL.CommandType = CommandType.StoredProcedure;

            //Pasar parámetros porque el StoredProcedure los necesita!
            //comandoSQL.Parameters.Add("@clave", SqlDbType.VarChar, 5).Value = claveLibro;
            comandoSQL.Parameters.AddWithValue("@clave", claveLibro);
            //Parámetro de entrada!
            comandoSQL.Parameters.Add("@msj", SqlDbType.VarChar, 100).Direction = ParameterDirection.Output;

            try
            {
                conexionSQL.Open();
                comandoSQL.ExecuteNonQuery();
                //Sacar lo que venga en el @MSJ del Stored Procedure!
                msj = comandoSQL.Parameters["@msj"].Value.ToString();
                result = true;
                conexionSQL.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally {
                conexionSQL.Dispose();
                comandoSQL.Dispose();
            }

            return result;
        }
    }
}

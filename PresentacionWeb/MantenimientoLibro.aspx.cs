using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LogicaNegocio;
using Entities;
using System.Data;

namespace PresentacionWeb
{
    public partial class MantenimientoLibro : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            BLLibro bLLibro = new BLLibro(MiConfig.GetCxString);
            Libro libro;

            LlenarAutores();
            LlenarCategorias();

            try
            {
                if (Session["_claveLibro"] != null)
                {
                    string claveLibro = Session["_claveLibro"].ToString();
                    libro = bLLibro.RegistroCompleto($"claveLibro='{claveLibro}'");

                    if (libro != null)
                    {
                        txtIdAutor.Text = libro.Autor.ClaveAutor;
                        txtAutor.Text = $"{libro.Autor.ApPaterno}, {libro.Autor.Nombre}";
                        txtIdCategoria.Text = libro.Categoria.ClaveCategoria;
                        txtCategoria.Text = libro.Categoria.Descripcion;
                        txtClaveLibro.Text = libro.ClaveLibro;
                        txtTitulo.Text = libro.Titulo;
                    }
                    else
                    {

                    }
                }
                else
                {

                }
            }
            catch (Exception ex)
            {
                Session["_Err"] = $"Error: {ex.Message}";
            }
            
        }

        /// <summary>
        /// Debe llenar el grid con Autores con Todos o con los Filtrados!
        /// </summary>
        private void LlenarAutores(string filtro = "")
        {
            DataSet ds;
            BLAutor bLAutor = new BLAutor(MiConfig.GetCxString);

            try
            {
                ds = bLAutor.ListarRegistros(); //Invoca a la capa de LOGICA DE NEGOCIO

                if (ds != null)
                {
                    dgvAutores.DataSource = ds.Tables[0];//Llenado desde el DATA SET

                    //Todos los GRID se ASP necesitan un Binding(vinculacion a una estructura de datos)
                    dgvAutores.DataBind();
                }

            }
            catch (Exception ex)
            {
                Session["_Err"] = $"Error en el Server: {ex.Message}";//Tratamiento del Error
            }
        }
        

        private void LlenarCategorias(string filtro = "")
        {
            DataSet ds;
            BLCategoria bLCategoria = new BLCategoria(MiConfig.GetCxString);

            try
            {
                ds = bLCategoria.ListarRegistros(); //Invoca a la capa de LOGICA DE NEGOCIO

                if (ds != null)
                {
                    dgvCategorias.DataSource = ds.Tables[0];//Llenado desde el DATA SET

                    //Todos los GRID se ASP necesitan un Binding(vinculacion a una estructura de datos)
                    dgvCategorias.DataBind();
                }

            }
            catch (Exception ex)
            {
                Session["_Err"] = $"Error en el Server: {ex.Message}";//Tratamiento del Error
            }
        }

        protected void dgvCategorias_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            dgvCategorias.PageIndex = e.NewPageIndex;
            LlenarCategorias();
        }

        protected void dgvAutores_PageIndexChanging1(object sender, GridViewPageEventArgs e)
        {
            dgvAutores.PageIndex = e.NewPageIndex;
            LlenarAutores();
        }
    }
}
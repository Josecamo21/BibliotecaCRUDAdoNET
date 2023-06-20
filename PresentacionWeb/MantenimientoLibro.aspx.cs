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
        Libro libro;
        protected void Page_Load(object sender, EventArgs e)
        {
            BLLibro bLLibro = new BLLibro(MiConfig.GetCxString);

            try
            {
                if (Session["_claveLibro"] != null)
                {
                    if (!IsPostBack)
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
                            libro.Existente = true;

                            LlenarAutores();
                            LlenarCategorias();
                        }
                        else
                        {
                            libro = new Libro();
                        }
                    }
                }
                else
                {
                    libro = new Libro();
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
                ds = bLAutor.ListarRegistros(filtro); //Invoca a la capa de LOGICA DE NEGOCIO

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
                ds = bLCategoria.ListarRegistros(filtro); //Invoca a la capa de LOGICA DE NEGOCIO

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
            
            string JSscript = "AbrirModalCategorias();";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "script", JSscript, true);
        }

        protected void dgvAutores_PageIndexChanging1(object sender, GridViewPageEventArgs e)
        {
            dgvAutores.PageIndex = e.NewPageIndex;
            LlenarAutores();

            string JSscript = "AbrirModalAutores();";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "script", JSscript, true);
        }

        protected void lnkSelecccionar_Command(object sender, CommandEventArgs e)
        {
            Autor autor;
            BLAutor bLAutor = new BLAutor(MiConfig.GetCxString);
            string claveAutor = e.CommandArgument.ToString();

            try
            {
                autor = bLAutor.RegistroCompleto($"claveAutor='{claveAutor}'");

                if (autor != null)
                {
                    txtIdAutor.Text = autor.ClaveAutor;
                    txtAutor.Text = $"{autor.ApPaterno}, {autor.Nombre}";
                }
            }
            catch (Exception ex)
            {
                Session["_Err"] = $"Error: {ex.Message}";
            }
        }

        protected void lnkSeleccionar_Command(object sender, CommandEventArgs e)
        {
            Categoria categoria;
            BLCategoria bLCategoria = new BLCategoria(MiConfig.GetCxString);
            string claveCategoria = e.CommandArgument.ToString();

            try
            {
                categoria = bLCategoria.RegistroCompleto($"claveCategoria='{claveCategoria}'");

                if (categoria != null)
                {
                    txtIdCategoria.Text = categoria.ClaveCategoria;
                    txtCategoria.Text = categoria.Descripcion;
                }
            }
            catch (Exception ex)
            {
                Session["_Err"] = $"Error: {ex.Message}";
            }
        }

        protected void btnFiltroAutor_Click(object sender, EventArgs e)
        {
            string filtro = $"nombre like '%{txtFiltroAutor.Text}%' or apPaterno like '%{txtFiltroAutor.Text}%' " +
                $"or apMaterno like '%{txtFiltroAutor.Text}%' or claveAutor like '%{txtFiltroAutor.Text}%'";
            LlenarAutores(filtro);

            string JSscript = "AbrirModalAutores();";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "script", JSscript, true);
        }

        protected void btnFiltroCategorias_Click(object sender, EventArgs e)
        {
            string filtro = $"descripcion like '%{txtFiltroCategorias.Text}%' or claveCategoria like '%{txtFiltroCategorias.Text}%'";
            LlenarCategorias(filtro);

            string JSscript = "AbrirModalCategorias();";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "script", JSscript, true);
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Session["_claveLibro"] = null;
            Session["_Err"] = null;
            Session["_Exito"] = null;
            Response.Redirect("Libros.aspx");
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            BLLibro bLLibro = new BLLibro(MiConfig.GetCxString);
            BLAutor bLAutor = new BLAutor(MiConfig.GetCxString);
            BLCategoria bLCategoria = new BLCategoria(MiConfig.GetCxString);
            libro = new Libro();

            try
            {
                libro.Titulo = txtTitulo.Text;
                libro.ClaveLibro = txtClaveLibro.Text;

                libro.Autor = bLAutor.RegistroCompleto($"claveAutor='{txtIdAutor.Text}'");
                libro.Categoria = bLCategoria.RegistroCompleto($"claveCategoria='{txtIdCategoria.Text}'");

                if(bLLibro.Verificar(txtClaveLibro.Text))
                {
                    if (bLLibro.Actualizar(libro))
                    {
                        Session["_Exito"] = "Se modificó el libro";
                    }
                    else
                    {
                        Session["_Err"] = "El libro no se pudo modificar en la base de datos";
                    }

                }
                else if (bLLibro.Verificar(txtTitulo.Text, txtIdAutor.Text))
                {
                    Session["_Err"] = "El libro ya está en la base de datos";
                }
                else
                {
                    if (bLLibro.Insertar(libro) > 0)
                    {
                        Session["_Exito"] = "Se guardó el libro";
                    }
                    else
                    {
                        Session["_Err"] = "El libro no se pudo guardar en la base de datos";
                    }
                }


            }
            catch (Exception ex)
            {
                Session["_Err"] = ex.Message;
            }

            Session["_claveLibro"] = null;
            Response.Redirect("Libros.aspx");

        }
    }
}
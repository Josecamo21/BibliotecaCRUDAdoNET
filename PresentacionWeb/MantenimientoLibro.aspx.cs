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
            Response.Redirect("Libros.aspx");
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            BLLibro bLLibro = new BLLibro(MiConfig.GetCxString);
            //Libro libro;
            string clave = "", titulo = "", autor = "";

            //Si el libro EXISTE(SQL) quiere decir que hemos cargado uno desde SQL
            if (libro.Existente)
            { //Entonces se debe realizar el proceso de UPDATE

                //Actualizar

                //Verificar Cambios
                if (HayCambios(ref clave, ref titulo, ref autor))
                {
                    libro.ClaveLibro = txtClaveLibro.Text;
                    libro.Titulo = txtTitulo.Text;
                    libro.Autor.ClaveAutor = txtIdAutor.Text;
                    libro.Categoria.ClaveCategoria = txtIdCategoria.Text;

                    if (titulo != "" || autor != "")
                    {
                        if (bLLibro.Verificar(txtTitulo.Text, txtIdAutor.Text))
                        {
                            Session["_Err"] = "Ya existe un libro con el mismo título y del mismo autor";
                        }
                        else
                        {
                            Actualizar(clave);
                        }

                    }
                    else
                    {
                        Actualizar(clave);
                    }
                }
                else
                {
                    Session["_Err"] = "No se han realizado cambios en los datos, por lo que no se ha actualizado nada!";
                    Limpiar();
                }
            }
            else
            {
                //Este caso solo se dará cuando se trata de Libros nuevos!

                //Insertar
                libro = new Libro();//Instancia de un Libro

                libro.ClaveLibro = txtClaveLibro.Text;
                libro.Titulo = txtTitulo.Text;
                libro.Autor.ClaveAutor = txtIdAutor.Text;
                libro.Categoria.ClaveCategoria = txtIdCategoria.Text;
                libro.Existente = false;

                try
                {
                    //Verificar si el Libro está repetido
                    if (bLLibro.Verificar(txtTitulo.Text, txtIdAutor.Text))
                    {
                        Session["_Err"] = "Ya existe un libro con el mismo título y del mismo autor";
                    }
                    else
                    {
                        //Validar claveLibro única!
                        if (bLLibro.Verificar(txtClaveLibro.Text))
                        {
                            Session["_Err"] = "La clave del Libro no se puede repetir. Modíquela";
                            txtClaveLibro.Focus();
                        }
                        else
                        {
                            if (bLLibro.Insertar(libro) > 0)
                            {
                                Session["_Err"] = "Se insertó con éxito el libro";
                                //LlenarGrid();
                            }
                            else
                            {
                                Session["_Err"] = "No se insertó el libro";
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Session["_Err"] = ex.Message;
                }
            }
            //Guarda algo NUEVO

            Session["_claveLibro"] = null;
            Response.Redirect("Libros.aspx");
        }



        private void Limpiar()
        {
            txtClaveLibro.Text = "";
            txtTitulo.Text = "";
            txtIdAutor.Text = "";
            txtAutor.Text = "";
            txtIdCategoria.Text = "";
            txtCategoria.Text = "";

            //LlenarGrid();
            libro = new Libro();

            //btnEliminar.Enabled = false;
        }

        private void Actualizar(string clave)
        {
            BLLibro bLLibro = new BLLibro(MiConfig.GetCxString);

            if (clave != "")
            {
                //Revisar la nueva Clave!!!
                if (bLLibro.Verificar(txtClaveLibro.Text))
                {
                    Session["_Err"] = "La clave del Libro no se puede repetir. Modíquela";
                    txtClaveLibro.Focus();
                }
                else
                {
                    //Asegurarse de mandar el Libro con los datos nuevos!)
                    if (bLLibro.Actualizar(libro, clave))
                    {
                        Session["_Err"] = "Se han actualizado los datos del Libro de manera exitosa";
                        Limpiar();
                    }
                    else
                        Session["_Err"] = "No se ha actualizado ningún registro!";
                }
            }
            else
            {
                if (bLLibro.Actualizar(libro))
                {
                    Session["_Err"] = "Se han actualizado los datos del Libro de manera exitosa";
                    Limpiar();
                }
                else
                    Session["_Err"] = "No se ha actualizado ningún registro!";
            }
        }

        private bool HayCambios(ref string clave, ref string titulo, ref string autor)
        {
            bool result = false;

            if (libro.ClaveLibro != txtClaveLibro.Text)
            {
                clave = libro.ClaveLibro;//Clave Vieja! La ocupo para el UPDATE
                result = true;
            }

            if (!(libro.Titulo == txtTitulo.Text))
            {
                result = true;
                titulo = txtTitulo.Text;//Sacar el nuevo!
            }

            if (libro.Autor.ClaveAutor != txtIdAutor.Text)
            {
                autor = txtIdAutor.Text;//
                result = true;
            }

            if (libro.Categoria.ClaveCategoria != txtIdCategoria.Text)
            {
                result = true;
            }

            return result;
        }
    }
}
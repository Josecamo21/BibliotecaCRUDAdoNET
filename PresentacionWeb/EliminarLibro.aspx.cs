using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LogicaNegocio;
using Entities;

namespace PresentacionWeb
{
    public partial class EliminarLibro : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            BLLibro bLLibro = new BLLibro(MiConfig.GetCxString);
            string claveLibro;
            Libro libro;
            try
            {
                if (Session["_claveLibro"] != null)
                {
                    claveLibro = Session["_claveLibro"].ToString();

                    libro = bLLibro.RegistroCompleto($"claveLibro='{claveLibro}'");

                    if (libro != null)
                    {
                        ViewState["_titulo"] = libro.Titulo;
                        ViewState["_autor"] = $"{libro.Autor.ApPaterno}, {libro.Autor.Nombre}";
                        ViewState["_categoria"] = libro.Categoria.Descripcion;
                    }
                    else
                    {
                        Session["_Err"] = "No se logró cargar los datos del libro. Intentelo nuevamente!!!";
                    }
                }
            }
            catch (Exception ex)
            {
                Session["_Err"] = $"Error Eliminando: {ex.Message}";
            }
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Session["_Exito"] = null;
            Session["_Err"] = null;
            Response.Redirect("Libros.aspx");
        }

        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            BLLibro bLLibro = new BLLibro(MiConfig.GetCxString);
            int result;

            try
            {
                if (Session["_claveLibro"] != null)
                {
                    result = bLLibro.Eliminar(Session["_claveLibro"].ToString());

                    if (result > 0)
                    {
                        Session["_Exito"] = "El libro fue eliminado con Exito!!!";
                        Session["_claveLibro"] = null;
                    }
                    else
                    {
                        Session["_Err"] = "No se logro eliminar el libro. Intentelo nuevamente";
                        Session["_Exito"] = null;
                    }
                    Response.Redirect("Libros.aspx", false);
                }
            }
            catch (Exception ex)
            {
                Session["_Err"] = $"Error Eliminando: {ex.Message}";
            }
           
        }
    }
}
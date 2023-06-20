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
    public partial class EliminarPrestamo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            BLPrestamo bLPrestamo = new BLPrestamo(MiConfig.GetCxString);
            string clavePrestamo;
            Prestamo prestamo;
            try
            {
                if (Session["_clavePrestamo"] != null)
                {
                    clavePrestamo = Session["_clavePrestamo"].ToString();

                    prestamo = bLPrestamo.RegistroCompleto($"clavePrestamo='{clavePrestamo}'");

                    if (prestamo != null)
                    {
                        ViewState["_titulo"] = prestamo.Ejemplar.Libro.Titulo;
                        ViewState["_usuario"] = prestamo.Usuario.Nombre;
                        ViewState["_fechaPrestamo"] = prestamo.FechaPrestamo;
                        ViewState["_fechaDevolucion"] = prestamo.FechaDevolucion;
                    }
                    else
                    {
                        Session["_Err"] = "No se logró cargar los datos del prestamo. Intentelo nuevamente!!!";
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
            Response.Redirect("Prestamos.aspx");
        }

        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            BLPrestamo bLPrestamo = new BLPrestamo(MiConfig.GetCxString);
            int result;

            try
            {
                if (Session["_clavePrestamo"] != null)
                {
                    result = bLPrestamo.Eliminar(Session["_clavePrestamo"].ToString());

                    if (result > 0)
                    {
                        Session["_Exito"] = "El prestamo fue eliminado con Exito!!!";
                        Session["_clavePrestamo"] = null;
                    }
                    else
                    {
                        Session["_Err"] = "No se logro eliminar el prestamo. Intentelo nuevamente";
                        Session["_Exito"] = null;
                    }
                    Response.Redirect("Prestamos.aspx", false);
                }
            }
            catch (Exception ex)
            {
                Session["_Err"] = $"Error Eliminando: {ex.Message}";
            }
        }
    }
}
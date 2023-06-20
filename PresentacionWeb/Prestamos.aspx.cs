using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using LogicaNegocio;
using Entities;

namespace PresentacionWeb
{
    public partial class Prestamos : System.Web.UI.Page
    {
        BLPrestamo blPrestamo = new BLPrestamo(MiConfig.GetCxString);
        protected void Page_Load(object sender, EventArgs e)
        {
            LlenarGrid();
        }

        protected void btnNuevoPrestamo_Click(object sender, EventArgs e)
        {
            Response.Redirect("PrestarLibro.aspx");
        }

        private void LlenarGrid(string filtro = "")
        {
            //Rellenar el DataGridView
            DataSet ds;

            try
            {
                ds = blPrestamo.ListarRegistros(filtro); //Invoca a la capa de LOGICA DE NEGOCIO

                if (ds != null)
                {
                    dgvPrestamos.DataSource = ds.Tables[0];//Llenado desde el DATA SET

                    //Todos los GRID se ASP necesitan un Binding(vinculacion a una estructura de datos)
                    dgvPrestamos.DataBind();
                }

            }
            catch (Exception ex)
            {
                Session["_Err"] = $"Error en el Server: {ex.Message}";//Tratamiento del Error
            }

        }

        protected void dgvPrestamos_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            dgvPrestamos.PageIndex = e.NewPageIndex;
            LlenarGrid();
        }

        protected void lnkEliminar_Command(object sender, CommandEventArgs e)
        {
            Session["_clavePrestamo"] = e.CommandArgument.ToString();
            Response.Redirect("EliminarPrestamo.aspx");
        }

        protected void lnkModificar_Command(object sender, CommandEventArgs e)
        {
            Session["_clavePrestamo"] = e.CommandArgument.ToString();
            Response.Redirect("PrestarLibro.aspx");
        }
    }
}
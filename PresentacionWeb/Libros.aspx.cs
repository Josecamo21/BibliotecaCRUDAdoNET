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
    public partial class Libros : System.Web.UI.Page
    {
        BLLibro blLibro = new BLLibro(MiConfig.GetCxString);
        protected void Page_Load(object sender, EventArgs e)
        {
            LlenarGrid();
        }

        private void LlenarGrid()
        {
            //Rellenar el DataGridView
            DataSet ds;

            try
            {
                ds = blLibro.ListarRegistros(); //Invoca a la capa de LOGICA DE NEGOCIO

                if (ds != null)
                {
                    dgvLibros.DataSource = ds.Tables[0];//Llenado desde el DATA SET

                    //Todos los GRID se ASP necesitan un Binding(vinculacion a una estructura de datos)
                    dgvLibros.DataBind();
                }

            }
            catch (Exception ex)
            {
                Session["_Err"] = $"Error en el Server: {ex.Message}";//Tratamiento del Error
            }

        }

        protected void dgvLibros_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            dgvLibros.PageIndex = e.NewPageIndex;
            LlenarGrid();
        }

        protected void lnkEliminar_Command(object sender, CommandEventArgs e)
        {
            Session["_claveLibro"] = e.CommandArgument.ToString();
            Response.Redirect("EliminarLibro.aspx");
        }

        protected void lnkModificar_Command(object sender, CommandEventArgs e)
        {
            Session["_claveLibro"] = e.CommandArgument.ToString();
            Response.Redirect("MantenimientoLibro.aspx");
        }

        protected void btnNuevoLibro_Click(object sender, EventArgs e)
        {
            Response.Redirect("MantenimientoLibro.aspx");
        }
    }
}
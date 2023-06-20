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
    public partial class PrestarLibro : System.Web.UI.Page
    {
        Prestamo prestamo = new Prestamo();
        protected void Page_Load(object sender, EventArgs e)
        {
            BLPrestamo bLPrestamo = new BLPrestamo(MiConfig.GetCxString);

            try
            {
                if (Session["_clavePrestamo"] != null)
                {
                    if (!IsPostBack)
                    {
                        string clavePrestamo = Session["_clavePrestamo"].ToString();
                        prestamo = bLPrestamo.RegistroCompleto($"clavePrestamo='{clavePrestamo}'");

                        if (prestamo != null)
                        {
                            txtIdUsuario.Text = prestamo.Usuario.ClaveUsuario;
                            txtUsuario.Text = $"{prestamo.Usuario.Nombre} {prestamo.Usuario.ApPaterno}";
                            txtIdEjemplar.Text = prestamo.Ejemplar.ClaveEjemplar;
                            txtEjemplar.Text = prestamo.Ejemplar.Libro.Titulo;
                            txtFechaPrestamo.Text = DateTime.Now.ToString();
                            txtFechaDevolucion.Text = DateTime.Now.AddDays(30).ToString();

                            LlenarGridUsuarios();
                            LlenarGridEjemplares();
                        }
                        else
                        {
                            Session["_Err"] = $"Error al cargar el prestamo";
                        }
                    }
                }
                else
                {

                    txtFechaPrestamo.Text = DateTime.Now.ToString();
                    txtFechaDevolucion.Text = DateTime.Now.AddDays(30).ToString();

                    LlenarGridUsuarios();
                    LlenarGridEjemplares();
                }
            }
            catch (Exception ex)
            {
                Session["_Err"] = $"Error: {ex.Message}";
            }

        }

        private void LlenarGridUsuarios()
        {
            BLPrestamo bLPrestamo = new BLPrestamo(MiConfig.GetCxString);
            //Rellenar el DataGridView
            DataSet ds = new DataSet(); // No se inicializan

            try
            {
                ds = bLPrestamo.ListarRegistrosClientes(); //Invoca a la capa de LOGICA DE NEGOCIO

                if (ds != null)
                {
                    dgvClientes.DataSource = ds.Tables[0];//Llenado desde el DATA SET

                    //Todos los GRID se ASP necesitan un Binding(vinculacion a una estructura de datos)
                    dgvClientes.DataBind();
                }
            }
            catch (Exception ex)
            {
                Session["_Err"] = ex.Message;
            }
        }

        protected void lnkSeleccionar_Command(object sender, CommandEventArgs e)
        {
            string clave = e.CommandArgument.ToString();
            string condicion = $"u.claveUsuario = '{clave}'";

            RegistroCompletoFunc(condicion);
        }

        private void RegistroCompletoFunc(string cond)
        {
            BLPrestamo bLPrestamo = new BLPrestamo(MiConfig.GetCxString);
            try
            {
                prestamo.Usuario = bLPrestamo.RegistroCompletoUsuario(cond);

                if (prestamo.Usuario != null)
                {
                    txtIdUsuario.Text = prestamo.Usuario.ClaveUsuario;
                    txtUsuario.Text = $"{prestamo.Usuario.Nombre} {prestamo.Usuario.ApPaterno}";
                }
            }
            catch (Exception ex)
            {
                Session["_Err"] = ex.Message;
            }
        }

        protected void dgvClientes_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            dgvClientes.PageIndex = e.NewPageIndex;
            LlenarGridUsuarios();
        }



        private void LlenarGridEjemplares()
        {
            BLPrestamo bLPrestamo = new BLPrestamo(MiConfig.GetCxString);
            //Rellenar el DataGridView
            DataSet ds = new DataSet(); // No se inicializan

            try
            {
                ds = bLPrestamo.ListarRegistrosEjemplares(); //Invoca a la capa de LOGICA DE NEGOCIO

                if (ds != null)
                {
                    dgvEjemplares.DataSource = ds.Tables[0];//Llenado desde el DATA SET

                    //Todos los GRID se ASP necesitan un Binding(vinculacion a una estructura de datos)
                    dgvEjemplares.DataBind();
                }
            }
            catch (Exception ex)
            {
                Session["_Err"] = ex.Message;
            }
        }

        protected void lnkSeleccionar_Command1(object sender, CommandEventArgs e)
        {
            string clave = e.CommandArgument.ToString();
            string condicion = $"claveEjemplar = '{clave}'";

            RegistroCompletoEjemplarFunc(condicion);
        }

        private void RegistroCompletoEjemplarFunc(string cond)
        {
            BLPrestamo bLPrestamo = new BLPrestamo(MiConfig.GetCxString);

            try
            {
                prestamo.Ejemplar = bLPrestamo.RegistroCompletoEjemplar(cond);

                if (prestamo.Ejemplar != null)
                {
                    txtIdEjemplar.Text = prestamo.Ejemplar.ClaveEjemplar;
                    txtEjemplar.Text = prestamo.Ejemplar.Libro.Titulo;
                }
            }
            catch (Exception ex)
            {
                Session["_Err"] = ex.Message;
            }
        }

        protected void dgvEjemplares_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            dgvEjemplares.PageIndex = e.NewPageIndex;
            LlenarGridEjemplares();
        }


        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Session["_clavePrestamo"] = null;
            Session["_Err"] = null;
            Session["_Exito"] = null;
            Response.Redirect("Prestamos.aspx");
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            BLPrestamo bLPrestamo = new BLPrestamo(MiConfig.GetCxString);
            int result = -1;
            prestamo = new Prestamo();

            try
            {
                prestamo.Usuario = bLPrestamo.RegistroCompletoUsuario($"u.claveUsuario = '{txtIdUsuario.Text}'");
                prestamo.Ejemplar = bLPrestamo.RegistroCompletoEjemplar($"claveEjemplar = '{txtIdEjemplar.Text}'");

                if (prestamo.Usuario.Moroso == "Si")
                {
                    Session["_Err"] = "El usuario cuenta con un prestamo pendiente";
                    Response.Redirect("Prestamos.aspx");
                }
                else
                {
                    result = bLPrestamo.InsertarPrestamo(prestamo);
                    if (result > 0)
                    {
                        Session["_Exito"] = "Prestamo realizado con Exito";
                    }
                }
            }
            catch (Exception ex)
            {
                Session["_Err"] = ex.Message;
            }



            Session["_clavePrestamo"] = null;
            Response.Redirect("Prestamos.aspx");
        }
    }
}
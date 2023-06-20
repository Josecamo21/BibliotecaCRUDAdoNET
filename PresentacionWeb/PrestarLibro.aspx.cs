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
            //llamar metodo
            LlenarGridUsuarios();
            LlenarGridEjemplares();

            if (IsPostBack)
            {
                RegistroCompletoFunc($"u.claveUsuario='{txtClaveUsuario.Text}'");
                RegistroCompletoEjemplarFunc($"claveEjemplar='{txtClaveEjemplar.Text}'");
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
                    txtClaveUsuario.Text = prestamo.Usuario.ClaveUsuario;
                    txtUsuario.Text = $"{prestamo.Usuario.Nombre} {prestamo.Usuario.ApPaterno}";
                    txtEmail.Text = prestamo.Usuario.Email;
                    txtDireccion.Text = prestamo.Usuario.Direccion;
                    txtPrestamo.Text = prestamo.Usuario.Moroso;

                    lblUsuario.Text = $"Al Cliente: {prestamo.Usuario.Nombre} {prestamo.Usuario.ApPaterno}";
                    lblCodigoU.Text = $"Codigo Usuario: {prestamo.Usuario.ClaveUsuario}";
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
                    txtClaveEjemplar.Text = prestamo.Ejemplar.ClaveEjemplar;
                    txtClaveLibro.Text = prestamo.Ejemplar.Libro.ClaveLibro;
                    txtTitulo.Text = prestamo.Ejemplar.Libro.Titulo;
                    txtCondicion.Text = prestamo.Ejemplar.ClaveCondicion;
                    txtEstado.Text = prestamo.Ejemplar.ClaveEstado;
                    txtEditorial.Text = prestamo.Ejemplar.ClaveEditorial;
                    txtPaginas.Text = prestamo.Ejemplar.Paginas.ToString();

                    lblTitulo.Text = $"Desea prestar el libro: {prestamo.Ejemplar.Libro.Titulo}";
                    lblLibro.Text = $"Codigo Libro: {prestamo.Ejemplar.ClaveEjemplar}";
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



        protected void btnAceptar_Click(object sender, EventArgs e)
        {
            BLPrestamo bLPrestamo = new BLPrestamo(MiConfig.GetCxString);
            int result = -1;

            if (prestamo.Usuario.Moroso == "Si")
            {
                Session["_Err"] = "El usuario cuenta con un prestamo pendiente";
            }
            else
            {
                try
                {
                    result = bLPrestamo.InsertarPrestamo(prestamo);
                    if (result > 0)
                    {
                        Session["_Exito"] = "Prestamo realizado con Exito";
                    }
                }
                catch (Exception ex)
                {
                    Session["_Err"] = ex.Message;
                }
            }

            //llamar metodo
            LlenarGridUsuarios();
            LlenarGridEjemplares();
        }

    }
}
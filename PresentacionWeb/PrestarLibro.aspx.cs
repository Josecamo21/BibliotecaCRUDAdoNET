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
        Ejemplar ejemplar;
        Usuario usuario;
        Prestamo prestamo;
        protected void Page_Load(object sender, EventArgs e)
        {
            prestamo = new Prestamo();
            usuario = new Usuario();
            ejemplar = new Ejemplar();
            //llamar metodo
            LlenarGridUsuarios();
            LlenarGridEjemplares();
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
                usuario = bLPrestamo.RegistroCompletoUsuario(cond);
                txtClaveUsuario.Text = usuario.ClaveUsuario;
                txtUsuario.Text = $"{usuario.Nombre} {usuario.ApPaterno}";
                txtEmail.Text = usuario.Email;
                txtDireccion.Text = usuario.Direccion;
                txtPrestamo.Text = usuario.Moroso;
                
                if (ejemplar != null)
                {
                    prestamo.Usuario = usuario;
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
                ejemplar = bLPrestamo.RegistroCompletoEjemplar(cond);
                txtClaveEjemplar.Text = ejemplar.ClaveEjemplar;
                txtClaveLibro.Text = ejemplar.Libro.ClaveLibro;
                txtTitulo.Text = ejemplar.Libro.Titulo;
                txtCondicion.Text = ejemplar.ClaveCondicion;
                txtEstado.Text = ejemplar.ClaveEstado;
                txtEditorial.Text = ejemplar.ClaveEditorial;
                txtPaginas.Text = ejemplar.Paginas.ToString();

                if (ejemplar != null) 
                {
                    prestamo.Ejemplar = ejemplar;
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

        protected void btnPrestar_Click(object sender, EventArgs e)
        {
            BLPrestamo bLPrestamo = new BLPrestamo(MiConfig.GetCxString);
            int result = -1;

            if (usuario.Moroso == "Si")
            {
                Session["_Err"] = "El usuario cuenta con un prestamo pendiente";
            }
            else
            {
                try
                {
                    result = bLPrestamo.InsertarPrestamo(prestamo);
                }
                catch (Exception ex)
                {
                    Session["_Err"] = ex.Message;
                }
            }
        }
    }
}
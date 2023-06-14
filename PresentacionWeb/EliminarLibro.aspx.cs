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

                    //cargar los datos en la pagina
                    txtCLibro.Text = libro.ClaveLibro;
                    txtTitulo.Text = libro.Titulo;
                    txtCAutor.Text = libro.Autor.ClaveAutor;
                    txtNombre.Text = libro.Autor.Nombre;
                    txtApPaterno.Text = libro.Autor.ApPaterno;
                    txtApMaterno.Text = libro.Autor.ApMaterno;
                    txtCCategoria.Text = libro.Categoria.ClaveCategoria;
                    txtDescripcion.Text = libro.Categoria.Descripcion;
                }
            }
            catch (Exception ex)
            {
                Session["_Err"] = $"Error Eliminando: {ex.Message}";
            }
        }
    }
}
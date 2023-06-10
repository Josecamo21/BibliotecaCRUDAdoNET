using Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LogicaNegocio;

namespace BibliotecaCRUDAdoNET
{
    public partial class frmLibros : Form
    {
        BLLibro blLibro;
        Libro libro;//Nuevo o Existente

        public frmLibros()
        {
            blLibro = new BLLibro(MiConfg.GetCadConexion);
            libro = new Libro();
            //Instancia que se comunica con la capa LOGICA DE NEGOCIO
            
            InitializeComponent();
        }

        #region Metodos Varios
        /// <summary>
        /// Verificar que todos los textos estén llenos
        /// </summary>
        /// <returns>bool - true: good - false: </returns>
        private bool TodosLlenos() {
            bool result = false;
            string msj = "";

            if (!string.IsNullOrEmpty(txtClaveLibro.Text))
            {
                if (!string.IsNullOrEmpty(txtTitulo.Text))
                {
                    if (!string.IsNullOrEmpty(txtClaveAutor.Text))
                    {
                        if (!string.IsNullOrEmpty(txtClaveCateroria.Text))
                        {
                            result = true;//Todo tenía texto!
                        }
                        else
                        {
                            msj = "Debe escribir un código de Categoría";
                            txtClaveCateroria.Focus();
                        }
                    }
                    else
                    {
                        msj = "Debe escribir la cláve del Autor";
                        txtClaveAutor.Focus();
                    }
                }
                else
                {
                    msj = "Debe anotar el Título del Libro";
                    txtTitulo.Focus();
                }
            }
            else {
                msj = "Debe digitar un Código para el Libro";
                txtClaveLibro.Focus();
            }

            if (! String.IsNullOrEmpty(msj))
            {
                MessageBox.Show(msj, "Atención!!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return result;
        }

        private void LlenarGrid() {
            //Rellenar el DataGridView
            DataSet ds = new DataSet();
            try
            {
                ds = blLibro.ListarRegistros(); //Invoca a la capa de LOGICA DE NEGOCIO
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);//Tratamiento del Error
            }

            dgvLibros.DataSource = ds.Tables[0];//Llenado desde el DATA SET
        }

        private bool HayCambios(ref string clave, ref string titulo, ref string autor) {
            bool result = false;

            if (libro.ClaveLibro != txtClaveLibro.Text )
            {
                clave = libro.ClaveLibro;//Clave Vieja! La ocupo para el UPDATE
                result = true;
            }

            if (libro.Titulo != txtTitulo.Text)
            {
                result = true;
                titulo = txtTitulo.Text;//Sacar el nuevo!
            }

            if (libro.Autor.ClaveAutor != txtClaveAutor.Text)
            {
                autor = txtClaveAutor.Text;//
                result = true;
            }

            if (libro.Categoria.ClaveCategoria != txtClaveCateroria.Text)
            {
                result = true;
            }

            return result;
        }

        private void Limpiar()
        {
            txtClaveAutor.Clear();
            txtClaveLibro.Clear();
            txtClaveCateroria.Clear();
            txtTitulo.Clear();

            LlenarGrid();
            libro = new Libro();

            btnEliminar.Enabled = false;
        }

        private void Actualizar(string clave) {
            if (clave != "")
            {
                //Revisar la nueva Clave!!!
                if (blLibro.Verificar(txtClaveLibro.Text))
                {
                    MessageBox.Show("La clave del Libro no se puede repetir. Modíquela");
                    txtClaveLibro.Focus();
                }
                else
                {
                    //Asegurarse de mandar el Libro con los datos nuevos!)
                    if (blLibro.Actualizar(libro, clave))
                    {
                        MessageBox.Show("Se han actualizado los datos del Libro de manera exitosa");
                        Limpiar();
                    }
                    else
                        MessageBox.Show("No se ha actualizado ningún registro!");
                }
            }
            else
            {
                if (blLibro.Actualizar(libro))
                {
                    MessageBox.Show("Se han actualizado los datos del Libro de manera exitosa");
                    Limpiar();
                }
                else
                    MessageBox.Show("No se ha actualizado ningún registro!");
            }
        }

        #endregion

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            Limpiar();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            string clave="", titulo="", autor="";

            
            if (TodosLlenos())
            {
                //Si el libro EXISTE(SQL) quiere decir que hemos cargado uno desde SQL
                if (libro.Existente)
                { //Entonces se debe realizar el proceso de UPDATE

                    //Actualizar

                    //Verificar Cambios
                    if (HayCambios(ref clave, ref titulo, ref autor))
                    {
                        libro.ClaveLibro = txtClaveLibro.Text;
                        libro.Titulo = txtTitulo.Text;
                        libro.Autor.ClaveAutor= txtClaveAutor.Text;
                        libro.Categoria.ClaveCategoria = txtClaveCateroria.Text;

                        if (titulo != "" || autor != "")
                        {
                            if (blLibro.Verificar(txtTitulo.Text, txtClaveAutor.Text))
                            {
                                MessageBox.Show("Ya existe un libro con el mismo título y del mismo autor");
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
                    else {
                        MessageBox.Show("No se han realizado cambios en los datos, por lo que no se ha actualizado nada!");
                        Limpiar();
                    }
                }
                else {
                    //Este caso solo se dará cuando se trata de Libros nuevos!
                    
                    //Insertar
                    libro = new Libro();//Instancia de un Libro

                    libro.Autor.ClaveAutor = txtClaveAutor.Text;
                    libro.Titulo = txtTitulo.Text;
                    libro.ClaveLibro = txtClaveLibro.Text;
                    libro.Categoria.ClaveCategoria = txtClaveCateroria.Text;
                    libro.Existente = false;

                    try
                    {
                        //Verificar si el Libro está repetido
                        if (blLibro.Verificar(txtTitulo.Text, txtClaveAutor.Text))
                        {
                            MessageBox.Show("Ya existe un libro con el mismo título y del mismo autor");
                        }
                        else
                        {
                            //Validar claveLibro única!
                            if (blLibro.Verificar(txtClaveLibro.Text))
                            {
                                MessageBox.Show("La clave del Libro no se puede repetir. Modíquela");
                                txtClaveLibro.Focus();
                            }
                            else
                            {
                                if (blLibro.Insertar(libro) > 0)
                                {
                                    MessageBox.Show("Se insertó con éxito el libro");
                                    LlenarGrid();
                                }
                                else
                                {
                                    MessageBox.Show("No se insertó el libro");
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }               
            } //Guarda algo NUEVO
        }

        private void frmLibros_Load(object sender, EventArgs e)
        {
            //Llamar método
            LlenarGrid();

            dgvLibros.Columns[0].HeaderText = "Clave de Libro";
            dgvLibros.Columns[1].HeaderText = "Título";
            dgvLibros.Columns[2].HeaderText = "Clave de Autor";
            dgvLibros.Columns[3].HeaderText = "Clave de Categoría";

            dgvLibros.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.DisplayedCells);
        }

        private void dgvLibros_DoubleClick(object sender, EventArgs e)
        {
            int fila = dgvLibros.CurrentRow.Index;
            string clave = txtClaveLibro.Text = dgvLibros[0, fila].Value.ToString();
            string condicion = $"claveLibro = '{clave}'";
            
            try
            {
                libro = blLibro.RegistroCompleto(condicion);
                txtClaveLibro.Text = libro.ClaveLibro;
                txtTitulo.Text = libro.Titulo;
                txtClaveAutor.Text = libro.Autor.ClaveAutor;
                txtClaveCateroria.Text = libro.Categoria.ClaveCategoria;

                btnEliminar.Enabled = true;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            //Desventaja - Información desactualizada!
            //txtClaveLibro.Text = dgvLibros[0, fila].Value.ToString();
            //txtTitulo.Text = dgvLibros[1, fila].Value.ToString();
            //txtClaveAutor.Text = dgvLibros[2, fila].Value.ToString();
            //txtClaveCateroria.Text = dgvLibros[3, fila].Value.ToString();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {

            string mensaje;//No es necesario inicializarla porque pasara por OUT
            DialogResult resp;//Un tipo especial que almacena respuestas por botón en los MessageBox

            if (libro.Existente)
            {
                resp = MessageBox.Show($"Confirma que desea ELIMINAR el Libro {libro.Titulo} código {libro.ClaveLibro}","Confirmar Eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                //Evaluar la respuesta
                if (resp == DialogResult.Yes)
                {
                    //if (blLibro.Eliminar(libro.ClaveLibro) > 0)

                    if (blLibro.EliminarConStoredProcedure(libro.ClaveLibro, out mensaje))
                    {
                        MessageBox.Show(mensaje);
                        Limpiar();
                    }
                    else {
                        MessageBox.Show("No se pudo eliminar el libro");
                    }
                }

            }
        }
    }
}

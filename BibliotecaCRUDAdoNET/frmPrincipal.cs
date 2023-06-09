using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BibliotecaCRUDAdoNET
{
    public partial class frmPrincipal : Form
    {
        frmLibros frmLibros = null;

        public frmPrincipal()
        {
            InitializeComponent();
        }

        private void gestionarLibrosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (frmLibros == null)
            {
                frmLibros = new frmLibros();
                frmLibros.MdiParent = this;
                frmLibros.FormClosed += (send, eve) => frmLibros=null;
                //frmLibros.FormClosed += new FormClosedEventHandler(CerraFRMLibros);
                frmLibros.Show();
            }
            else
                frmLibros.Activate();
        }

        //private void CerraFRMLibros(object sender, FormClosedEventArgs e) {
        //    frmLibros = null;
        //}

    }
}

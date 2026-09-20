using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TPWinForm_equipo_i
{
    public partial class FrmAltaArticulo : Form
    {
        public FrmAltaArticulo()
        {
            InitializeComponent();

            // Vinculación forzada de los eventos Click
            if (btnAgregarImagen != null)
                btnAgregarImagen.Click += new EventHandler(btnAgregarImagen_Click);

            if (btnAceptar != null)
                btnAceptar.Click += new EventHandler(btnAceptar_Click);
        }

        private void FrmAltaArticulo_Load(object sender, EventArgs e)
        {
            ArticuloNegocio negocio = new ArticuloNegocio();

            try
            {
                cboMarca.DataSource = negocio.listarMarcas();
                cboMarca.ValueMember = "Id";
                cboMarca.DisplayMember = "Descripcion";

                cboCategoria.DataSource = negocio.listarCategorias();
                cboCategoria.ValueMember = "Id";
                cboCategoria.DisplayMember = "Descripcion";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar desplegables: " + ex.Message);
            }
        }

        private void btnAgregarImagen_Click(object sender, EventArgs e)
        {
            cargarImagen(txtUrlImagen.Text);
        }

        private void cargarImagen(string url)
        {
            try
            {
                if (!string.IsNullOrEmpty(url))
                    pbxImagen.Load(url);
                else
                    pbxImagen.Load("https://blocks.astratic.com/img/general-img-landscape.png");
            }
            catch (Exception)
            {
                pbxImagen.Load("https://blocks.astratic.com/img/general-img-landscape.png");
            }
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            }
    

        private void label4_Click(object sender, EventArgs e)
        {

        }

        /**private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.Close();
        }**/

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtPrecio_TextChanged(object sender, EventArgs e)
        {

        }
    }
}

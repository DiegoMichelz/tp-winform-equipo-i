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
        //Atributo privado
        private Articulo articuloSeleccionado = null;

        // Constructor para Modificación
        public FrmAltaArticulo(Articulo articulo) : this()
        {
            this.articuloSeleccionado = articulo;
            Text = "Modificar Artículo";
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

                // PRECARGA DE DATOS: Solo si presionaron "Modificar"
                if (articuloSeleccionado != null)
                {
                    txtCodigo.Text = articuloSeleccionado.Codigo;
                    txtNombre.Text = articuloSeleccionado.Nombre;
                    txtDescripcion.Text = articuloSeleccionado.Descripcion;
                    txtPrecio.Text = articuloSeleccionado.Precio.ToString("0.00");

                    if (articuloSeleccionado.Marca != null)
                        cboMarca.SelectedValue = articuloSeleccionado.Marca.Id;

                    if (articuloSeleccionado.Categoria != null)
                        cboCategoria.SelectedValue = articuloSeleccionado.Categoria.Id;

                    if (articuloSeleccionado.Imagenes != null && articuloSeleccionado.Imagenes.Count > 0)
                    {
                        txtUrlImagen.Text = articuloSeleccionado.Imagenes[0].ImageUrl;
                        cargarImagen(txtUrlImagen.Text);
                    }
                }


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
            ArticuloNegocio negocio = new ArticuloNegocio();

            try
            {
                // 1. Validación de campos obligatorios
                if (string.IsNullOrEmpty(txtCodigo.Text) || string.IsNullOrEmpty(txtNombre.Text))
                {
                    MessageBox.Show("Ingresa al menos Código y Nombre.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // 2. Si estamos modificando reutilizamos articuloSeleccionado; si no, creamos uno nuevo
                Articulo target = articuloSeleccionado != null ? articuloSeleccionado : new Articulo();

                target.Codigo = txtCodigo.Text;
                target.Nombre = txtNombre.Text;
                target.Descripcion = txtDescripcion.Text;

                decimal precio = 0;
                decimal.TryParse(txtPrecio.Text, out precio);
                target.Precio = precio;

                target.Marca = (Marca)cboMarca.SelectedItem;
                target.Categoria = (Categoria)cboCategoria.SelectedItem;

                // 3. Asignación de la lista de imágenes desde el TextBox
                target.Imagenes = new List<Imagen>();

                if (!string.IsNullOrEmpty(txtUrlImagen.Text))
                {
                    Imagen img = new Imagen();

                    // Si estábamos modificando y ya existía una imagen, conservamos su Id
                    if (articuloSeleccionado != null && articuloSeleccionado.Imagenes != null && articuloSeleccionado.Imagenes.Count > 0)
                    {
                        img.Id = articuloSeleccionado.Imagenes[0].Id;
                    }

                    img.ImageUrl = txtUrlImagen.Text;
                    target.Imagenes.Add(img);
                }

                // 4. Guardar o Modificar en la Base de Datos
                if (articuloSeleccionado != null)
                {
                    negocio.modificar(target);
                    MessageBox.Show("¡Artículo modificado exitosamente!", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    negocio.agregar(target);
                    MessageBox.Show("¡Artículo guardado exitosamente!", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al guardar: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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

        private void txtPrecio_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Si la tecla NO es un número, NO es borrado, NO es punto y NO es coma -> la bloquea
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != (char)Keys.Back && e.KeyChar != '.' && e.KeyChar != ',')
            {
                e.Handled = true;
            }
        }
    }
}
